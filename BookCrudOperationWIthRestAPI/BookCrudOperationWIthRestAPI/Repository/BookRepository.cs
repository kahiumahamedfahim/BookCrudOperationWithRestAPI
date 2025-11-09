using BookCrudOperationWIthRestAPI.DataAcess;
using BookCrudOperationWIthRestAPI.Entity;
using BookCrudOperationWIthRestAPI.Repository.Interfaces;
namespace BookCrudOperationWIthRestAPI.Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {

        public BookRepository(AppDbContext context) : base(context)
        {

        }

    }
  
}
