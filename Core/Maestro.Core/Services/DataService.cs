namespace Core.Services
{
    using Core.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class DataService<TEntity> : IDataService<TEntity>
        where TEntity : class
    {
        protected DataService(DbContext db) => Data = db;

        protected DbContext Data { get; }

        protected IQueryable<TEntity> All() => Data.Set<TEntity>();

        public async Task MarkMessageAsPublished(int id)
        {
            var message = await Data.FindAsync<Message>(id);

            message.MarkAsPublished();

            await Data.SaveChangesAsync();
        }

        public async Task Save(TEntity entity, params Message[] messages)
        {
            foreach (var message in messages)
            {
                Data.Add(message);
            }

            Data.Update(entity);

            await Data.SaveChangesAsync();
        }
    }
}