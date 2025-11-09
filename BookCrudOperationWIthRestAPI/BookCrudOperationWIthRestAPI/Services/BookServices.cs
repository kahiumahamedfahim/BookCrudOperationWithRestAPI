using BookCrudOperationWIthRestAPI.Entity;
using BookCrudOperationWIthRestAPI.Models;
using BookCrudOperationWIthRestAPI.Repository.Interfaces;
using BookCrudOperationWIthRestAPI.Services.Interfaces;
using System.Security.Cryptography.Xml;

namespace BookCrudOperationWIthRestAPI.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository _repo;
        public BookServices(IBookRepository repo)
        {
            _repo = repo;
        }
        
        public Book BookViewModelToBook(BookViewModel bookVm)
        {
            Book book = new Book();
            book.Id = bookVm.Id;
            book.Title = bookVm.Title;
            book.Author = bookVm.Author;
            book.PublishedDate = bookVm.PublishedDate;
            return book;
        }
        public BookViewModel BookToBookViewModel(Book book)
        {
            BookViewModel bookVm = new BookViewModel();
            bookVm.Id = book.Id;
            bookVm.Title = book.Title;
            bookVm.Author = book.Author;
            bookVm.PublishedDate = book.PublishedDate;
            return bookVm;
        }

        public List<Book> BookViewModelToBookList(List<BookViewModel> bookVmList)
        {
            List<Book> books= new List<Book>();
            foreach(BookViewModel bookVm in bookVmList)
            {
                var book=BookViewModelToBook(bookVm);
                books.Add(book);
            }
            return books;
        }
        public List<BookViewModel> BookToBookViewModelList(List<Book> bookList)
        {
            List<BookViewModel> books = new List<BookViewModel>();
            foreach (Book book in bookList)
            {
                var bookVm = BookToBookViewModel(book);
                books.Add(bookVm);
            }
            return books;
        }

        public IEnumerable<BookViewModel> GetAll()
        {
            var listofBooks = _repo.GetAll().ToList();
            var listOfBookVM=BookToBookViewModelList(listofBooks);
            return listOfBookVM;

        }
      
        public BookViewModel GetById(int id)
        {
            var book = _repo.GetById(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with Id {id} not found.");

            return BookToBookViewModel(book);
        }
        public void Create(BookViewModel bookVm)
        {
            var book= BookViewModelToBook(bookVm);
            _repo.Add(book);
            _repo.Save();
        }
        public void Update(int id, BookViewModel bookVm)
        {
            var updatedBook = BookViewModelToBook(bookVm);
            _repo.Update(id, updatedBook);
            _repo.Save();
        }

        public void Delete(int id)
        {
            _repo.Delete(id);
            _repo.Save();

        }


    }
}
