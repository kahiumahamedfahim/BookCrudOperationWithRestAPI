using System.Security.Cryptography;
using System.Xml.Serialization;

namespace BookCrudOperationWIthRestAPI.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        void Add(T entity);
        void Update(int id,T entity);
        void Delete(int id);
        void Save();
    }
}
