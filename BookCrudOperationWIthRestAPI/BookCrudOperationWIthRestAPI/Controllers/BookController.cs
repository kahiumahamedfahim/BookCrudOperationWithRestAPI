using BookCrudOperationWIthRestAPI.Models;
using BookCrudOperationWIthRestAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BookCrudOperationWIthRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Fixed")]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookService;
        private readonly IHttpClientFactory _httpClientFactory;
        public BookController(IBookServices bookServices , IHttpClientFactory clientFactory)
        {
            _bookService = bookServices;
            _httpClientFactory = clientFactory;


        }

        // GET: api/Book/GetAll
        [HttpGet("GetAll")]
        [EnableRateLimiting("sliding")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var bookVms = _bookService.GetAll().ToList();
                var books=_bookService.BookViewModelToBookList(bookVms);

                return Ok(books); // 200 OK
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching books", error = ex.Message });
            }
        }

        // GET: api/Book/GetById/5
        [HttpGet("GetById/{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                var bookVm = _bookService.GetById(id);
               var book=_bookService.BookViewModelToBook(bookVm);
                return Ok(book);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error fetching book", error = ex.Message });
            }
        }

        // POST: api/Book/Create
        [HttpPost("Create")]
        [DisableRateLimiting]
        public IActionResult CreateBook([FromBody] BookViewModel bookVm)
        {
            try
            {
               
                _bookService.Create(bookVm);
                return Ok(new { message = "Book created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating book", error = ex.Message });
            }
        }

        // PUT: api/Book/Update/5
        [HttpPut("Update/{id}")]
        public IActionResult UpdateBook(int id, [FromBody] BookViewModel bookVm)
        {
            try
            {
                _bookService.Update(id, bookVm);
                return Ok(new { message = "Book updated successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating book", error = ex.Message });
            }
        }

        // DELETE: api/Book/Delete/5
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _bookService.Delete(id);
                return Ok(new { message = "Book deleted successfully" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting book", error = ex.Message });
            }
        }
        //add a third party api 
        [HttpGet("GetPosts")]
        [DisableRateLimiting]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var result = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode (500, $"Internal Server Error : {ex.Message}");
            }

            
        }
    }
}
