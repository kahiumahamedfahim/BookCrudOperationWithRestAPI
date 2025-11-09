using BookCrudOperationWIthRestAPI.DataAcess;
using BookCrudOperationWIthRestAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookCrudOperationWIthRestAPI.Repository
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet=_context.Set<T>();

        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity=_dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
           return _dbSet.ToList();
        }

        public T? GetById(int id)
        {
           return _dbSet.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(int id, T entity)
        {
            var excisting_entity=_dbSet.Find(id);
            if(excisting_entity!=null)
            {
                _dbSet.Entry(excisting_entity).CurrentValues.SetValues(entity);
            }
        }

    }
}
