namespace Server.Core.Models
{
    public interface IModel<T> where T : class, new()
    {
        T Empty() => new T();
    }
}
