﻿using AutoMapper;
using Core.Models;
using Core.Services;
using Core.Services.Identity;
using Core.Services.Messages;
using Maestro.Core.Enums;
using Maestro.Employees.Data.Models;
using Maestro.Employees.Models;
using Maestro.Requests.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Server.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Maestro.Core.Constants.ErrorMessages;
namespace Maestro.Employees.Services
{
    public class EmployeeService : DataService<Employee>, IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly EmployeesDbContext _context;
        private readonly IBus _bus;

        public EmployeeService(EmployeesDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService,
            IBus bus)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _bus = bus;
        }

        public async Task<Result<EmployeeWorkOutputModel>> GetMyWork()
        {
            var employee = await GetCurrentEmployee();         

            return new EmployeeWorkOutputModel()
            {
                Id = employee?.Id,
                Work = _mapper.Map<IEnumerable<Work>, IEnumerable<WorkOutputModel>>(employee?.Work ?? Enumerable.Empty<Work>())
            };
        }

        public async Task<Result> TakeWork(TakeWorkInputModel input)
        {
            var employee = await GetCurrentEmployee();

            if(employee == null)
            {
                return Result.Failure("User is not employee");
            }

            var work = await _context.Work.FirstOrDefaultAsync(w => w.Id == input.Id);

            if (work.StartDate != null || !string.IsNullOrEmpty(work.EmployeeId))
            {
                return Result.Failure("Work already taken");
            }

            work.StartDate = DateTime.Now;
            work.EmployeeId = employee.Id;
            work.Status = WorkStatus.InProgress;

            await _context.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> CloseWork(WorkInputModel input)
        {
            var work = await _context.Work.FirstOrDefaultAsync(w => w.Id == input.Id);

            work.EndDate = DateTime.Now;

            work.Status = WorkStatus.Completed;

            await _bus.Publish(new RequestDoneMessage() { RequestId = work.RequestId });

            //TODO another message to statistics microservice with parameters EmployeeId 

            var employee = await _context.Employees.Include(e => e.Work).FirstOrDefaultAsync(e => e.Id == work.EmployeeId);

            var pastWork = employee.Work
                .Where(x => x.Status == WorkStatus.Completed)
                .Select(w => new Tuple<DateTime, DateTime>(w.StartDate.Value, w.EndDate.Value));

            await _bus.Publish(new WorkCompletedMessage()
            {
                EmployeeId = employee.Id,
                PastWorkPeriod = pastWork
            });

            return await this._context.SaveChangesAsync() > 0
                ? Result.Success
                : Result.Failure(InternalServerError);
        }

        private async Task<Employee> GetCurrentEmployee()
            => await All().Include(x => x.Work)
            .FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId);

        public async Task<Result<WorkListOutputModel>> AvailableWork()
        {
            var model = await _context.Work.Where(x => x.Status == WorkStatus.Pending).ToListAsync();

            return new WorkListOutputModel { Work = _mapper.Map<IEnumerable<Work>, IEnumerable<WorkOutputModel>>(model ?? Enumerable.Empty<Work>()) };
        }

        public async Task<Result<EmployeesOutputModel>> GetEmployees()
        {
            var model = new List<EmployeeOutputModel>();

            var employees = await _context.Employees.Include(e => e.Work).ToListAsync();


            return new EmployeesOutputModel()
            {
                Employees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeOutputModel>>(employees)
            };
        }
    }
}
