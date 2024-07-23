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
            // Loop through the list of books
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Id == id)
                {
                    // If the book is found, return it
                    return books[i];
                }
            }

            // If no book is found with the given ID, return a 404 Not Found response
            return NotFound();
        }


        // POST /api/books
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            // Determine the maximum ID in the existing books list
            int maxId = 0;
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Id > maxId)
                {
                    maxId = books[i].Id;
                }
            }

            // Set the new book's ID to be one greater than the current maximum ID
            book.Id = maxId + 1;

            // Add the new book to the books list
            books.Add(book);

            // Return a response indicating that the book was successfully created
         
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // PUT /api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            // Find the book to update using a for loop
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Id == id)
                {
                    // Update the book  with the values from updatedBook
                    books[i].Title = updatedBook.Title;
                    books[i].Author = updatedBook.Author;
                    books[i].Description = updatedBook.Description;
                    books[i].ImageUrl = updatedBook.ImageUrl;
                    books[i].Price = updatedBook.Price;

                    // Return a No Content response indicating successful update
                    return NoContent();
                }
            }

            // If no book is found with the given ID, return a 404 Not Found response
            return NotFound();
        }


        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Find the book to delete using a for loop
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Id == id)
                {
                    // Remove the book from the list
                    books.RemoveAt(i);

                    // Return a NoContent response indicating successful deletion
                    return NoContent();
                }
            }

            // If book is not found with the given ID, return a 404 Not Found response
            return NotFound();
        }


        // GET /api/books/author/{author}
        [HttpGet("author/{author}")]
        public ActionResult<IEnumerable<Book>> GetBooksByAuthor(string author)
        {
            // Initialize a list to store books by the given author
            List<Book> authorBooks = new List<Book>();

            // Loop through the books to find book by author name
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].Author.ToLower() == author.ToLower())
                {
                    authorBooks.Add(books[i]);
                }
            }

            // If no books are found by the given author, return a 404 Not Found response
            if (authorBooks.Count == 0)
            {
                return NotFound();
            }

            // Return the list of books by the given author
            return authorBooks;
        }

    }
}
