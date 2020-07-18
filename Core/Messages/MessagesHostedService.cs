﻿namespace Core.Messages
{
    using Data.Models;
    using Hangfire;
    using MassTransit;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MessagesHostedService : IHostedService
    {
        private readonly IRecurringJobManager recurringJob;
        private readonly DbContext data;
        private readonly IBus publisher;

        public MessagesHostedService(
            IRecurringJobManager recurringJob,
            DbContext data,
            IBus publisher)
        {
            this.recurringJob = recurringJob;
            this.data = data;
            this.publisher = publisher;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            recurringJob.AddOrUpdate(
                nameof(MessagesHostedService),
                () => ProcessPendingMessages(),
                "*/5 * * * * *");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.CompletedTask;

        public void ProcessPendingMessages()
        {
            var messages = data
                .Set<Message>()
                .Where(m => !m.Published)
                .OrderBy(m => m.Id)
                .ToList();

            foreach (var message in messages)
            {
                publisher.Publish(message.Data, message.Type);

                message.MarkAsPublished();

                data.SaveChanges();
            }
        }
    }
}