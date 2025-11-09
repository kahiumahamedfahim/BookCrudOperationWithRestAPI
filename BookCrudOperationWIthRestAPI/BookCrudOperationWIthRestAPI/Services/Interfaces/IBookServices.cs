using BookCrudOperationWIthRestAPI.Entity;
using BookCrudOperationWIthRestAPI.Models;
namespace BookCrudOperationWIthRestAPI.Services.Interfaces
{
    public interface IBookServices
    {
        IEnumerable<BookViewModel> GetAll();
        BookViewModel? GetById(int id);
        void Create(BookViewModel book);
        void Update(int id,BookViewModel book);
        void Delete(int id);
         Book BookViewModelToBook(BookViewModel bookVm);
        BookViewModel BookToBookViewModel(Book book);
        List<Book> BookViewModelToBookList(List<BookViewModel> bookVmList);
        List<BookViewModel> BookToBookViewModelList(List<Book> bookList);

    }
}
