using Maestro.Core.Messages;
using Maestro.Employees.Data.Models;
using Maestro.Employees.Services;
using Maestro.Requests.Data;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Maestro.Employees.Consumers
{
    public class UserPromotedConsumer : IConsumer<UserPromotedMessage>
    {
        private readonly EmployeesDbContext _context;

        public UserPromotedConsumer(EmployeesDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<UserPromotedMessage> context)
        {
            var employee = new Employee
            {
                UserId = context.Message.UserId,
                EmployeeSince = DateTime.Now
            };

            await _context.Employees.AddAsync(employee);

            await _context.SaveChangesAsync();
        }
    }
}
