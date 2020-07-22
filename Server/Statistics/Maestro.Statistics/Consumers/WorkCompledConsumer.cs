using Maestro.Statistics.Services;
using MassTransit;
using Server.Core.Messages;
using System.Threading.Tasks;

namespace Maestro.Statistics.Consumers
{
    public class WorkCompledConsumer : IConsumer<WorkCompletedMessage>
    {
        private readonly IStatisticsService _statisticsService;

        public WorkCompledConsumer(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        public async Task Consume(ConsumeContext<WorkCompletedMessage> context)
        {
            await _statisticsService.AddOrUpdate(context.Message.EmployeeId, context.Message.PastWorkPeriod);
        }
    }
}
