using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BookStoreApi.Models;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // In-memory list to store books data
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "To Kill a Mockingbird", Author = "Harper Lee", Description = "A novel about the serious issues of rape and racial inequality, but it is also full of warmth and humor.", ImageUrl = "https://pro2-bar-s3-cdn-cf5.myportfolio.com/f01e52a529972294633eb1d545abb880/6523f2b85a0afee32e30926e_rw_600.gif?h=2cfba3ea482680d909ce2cb7393cc2c7", Price = 10.99m },
            new Book { Id = 2, Title = "The Alchemist", Author = "Paulo Coelho", Description = "A journey of self-discovery and spiritual awakening follows Santiago, an Andalusian shepherd boy, as he dreams of finding worldly treasure.", ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1654371463i/18144590.jpg", Price = 12.99m }
        };

        // GET /api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return books;
        }

        // GET /api/books/{id}
        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // POST /api/books
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            int maxId = books.Count > 0 ? books.Max(b => b.Id) : 0;
            book.Id = maxId + 1;
            books.Add(book);
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT /api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            var bookToUpdate = books.FirstOrDefault(b => b.Id == id);

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.Author = updatedBook.Author;
            bookToUpdate.Description = updatedBook.Description;
            bookToUpdate.ImageUrl = updatedBook.ImageUrl;
            bookToUpdate.Price = updatedBook.Price;

            return NoContent();
        }

        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var bookToDelete = books.FirstOrDefault(b => b.Id == id);

            if (bookToDelete == null)
            {
                return NotFound();
            }

            books.Remove(bookToDelete);

            return NoContent();
        }

        // GET /api/books/author/{author}
        [HttpGet("author/{author}")]
        public ActionResult<IEnumerable<Book>> GetBooksByAuthor(string author)
        {
            var authorBooks = books.Where(b => b.Author.ToLower() == author.ToLower()).ToList();

            if (authorBooks.Count == 0)
            {
                return NotFound();
            }

            return authorBooks;
        }
    }
}
