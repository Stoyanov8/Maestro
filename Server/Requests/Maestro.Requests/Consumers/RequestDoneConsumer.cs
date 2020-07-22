using Core.Services.Messages;
using Maestro.Requests.Data;
using MassTransit;
using System.Linq;
using System.Threading.Tasks;

namespace Maestro.Requests.Consumers
{
    public class RequestDoneConsumer : IConsumer<RequestDoneMessage>
    {
        private readonly RequestDbContext _context;

        public RequestDoneConsumer(RequestDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<RequestDoneMessage> context)
        {
            var request = _context.Requests
                .FirstOrDefault(r => r.Id == context.Message.RequestId);

            if (request != null)
            {
                request.IsActive = false;

                await _context.SaveChangesAsync();
            }
        }
    }
}
