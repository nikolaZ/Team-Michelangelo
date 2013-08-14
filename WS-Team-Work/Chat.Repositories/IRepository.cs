using System.Linq;

namespace Chat.Repositories
{
    public interface IRepository<T>
    {
        T Add(T entity);

        T Update(int id, T entity);

        void Delete(int id);

        T Get(int id);

        IQueryable<T> All();
    }
}
