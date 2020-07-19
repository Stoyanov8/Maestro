namespace Core.Services
{
    using Core.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDataService<TEntity>
        where TEntity : class
    {
        Task MarkMessageAsPublished(int id);

        Task Save(TEntity entity, params Message[] messages);
    }
}