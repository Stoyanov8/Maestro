using AutoMapper;
using Core.Models;
using Core.Services;
using Core.Services.Identity;
using Maestro.Employees.Data.Models;
using Maestro.Employees.Enums;
using Maestro.Employees.Models;
using Maestro.Requests.Data;
using Microsoft.EntityFrameworkCore;
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


        public EmployeeService(EmployeesDbContext context,
            IMapper mapper,
            ICurrentUserService currentUserService)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<EmployeeWorkOutputModel>> GetMyWork()
        {
            var employee = await GetCurrentEmployee();

            return new EmployeeWorkOutputModel()
            {
                Id = employee.Id,
                Work = _mapper.Map<IEnumerable<Work>, IEnumerable<WorkOutputModel>>(employee.Work ?? Enumerable.Empty<Work>())
            };
        }

        public async Task<Result> TakeWork(TakeWorkInputModel input)
        {
            var employee = await GetCurrentEmployee();

            var work = await _context.Work.FirstOrDefaultAsync(w => w.Id == input.Id);

            if (work.StartDate != null || string.IsNullOrEmpty(work.EmployeeId))
            {
                return Result.Failure("Work already taken");
            }

            work.StartDate = DateTime.Now;

            work.EmployeeId = employee.Id;
            work.Status = WorkStatus.InProgress;

            return Result.Success;
        }

        public async Task<Result> CloseWork(WorkInputModel input)
        {
            var work = await _context.Work.FirstOrDefaultAsync(w => w.Id == input.Id);

            work.EndDate = DateTime.Now;

            work.Status = WorkStatus.Completed;

            await this._context.SaveChangesAsync();

            return await this._context.SaveChangesAsync() > 0
                ? Result.Success
                : Result.Failure(InternalServerError);
        }

        //TODO This is Intended to be executed when new requests comes in the system by a client
        public async Task CreateWork(CreateWorkInputModel input)
        {
            var work = new Work()
            {
                RequestId = input.RequestId,
                Status = Enums.WorkStatus.Pending
            };

            await this._context.Work.AddAsync(work);

            await this._context.SaveChangesAsync();
        }

        private async Task<Employee> GetCurrentEmployee()    
            => await All().Include(x => x.Work)     
            .FirstOrDefaultAsync(x => x.UserId == _currentUserService.UserId);
    }
}
