using System.Threading.Tasks;
using Maestro.Core.Enums;
using Maestro.Core.Messages;
using Maestro.Employees.Data.Models;
using Maestro.Requests.Data;
using MassTransit;

namespace Maestro.Employees.Consumers
{
    public class RequestCreatedConsumer : IConsumer<RequestCreatedMessage>
    {
        private readonly EmployeesDbContext _context;

        public RequestCreatedConsumer(EmployeesDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<RequestCreatedMessage> context)
        {
            var work = new Work()
            {
                RequestId = context.Message.RequestId,
                Status = WorkStatus.Pending
            };

            await _context.Work.AddAsync(work);

            await _context.SaveChangesAsync();
        }
    }
}
