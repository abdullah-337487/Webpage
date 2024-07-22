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
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Description = "Description 1", ImageUrl = "ImageUrl 1" },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Description = "Description 2", ImageUrl = "ImageUrl 2" }
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
            // Iterate through the books collection
            foreach (var book in books)
            {
                if (book.Id == id)
                {
                    return book; // Return the book if the ID matches
                }
            }

            // Return 404 Not Found if no book matches the ID
            return NotFound();
        }


        // POST /api/books
        [HttpPost]
        public ActionResult<Book> AddBook(Book book)
        {
            // Initialize maxId to zero
            int maxId = 0;

            // Find the maximum ID in the books collection
            foreach (var b in books)
            {
                if (b.Id > maxId)
                {
                    maxId = b.Id;
                }
            }

            // Set the new book's ID to be the maximum ID plus one
            book.Id = maxId + 1;

            // Add the new book to the collection
            books.Add(book);

            // Return a response with the location of the newly created book
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }


        // PUT /api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            // Find the book to update
            Book bookToUpdate = null;
            foreach (var b in books)
            {
                if (b.Id == id)
                {
                    bookToUpdate = b;
                    break;
                }
            }

            // If the book was not found, return a NotFound result
            if (bookToUpdate == null)
            {
                return NotFound();
            }

            // Update the book's details
            bookToUpdate.Title = updatedBook.Title;
            bookToUpdate.Author = updatedBook.Author;
            bookToUpdate.Description = updatedBook.Description;
            bookToUpdate.ImageUrl = updatedBook.ImageUrl;

            // Return a NoContent response indicating the update was successful
            return NoContent();
        }


        // DELETE /api/books/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Find the book to delete
            Book bookToDelete = null;
            foreach (var b in books)
            {
                if (b.Id == id)
                {
                    bookToDelete = b;
                    break;
                }
            }

            // If the book was not found, return a NotFound result
            if (bookToDelete == null)
            {
                return NotFound();
            }

            // Remove the book from the list
            books.Remove(bookToDelete);

            // Return a NoContent response indicating the deletion was successful
            return NoContent();
        }


        // GET /api/books/author/{author}
        [HttpGet("author/{author}")]
        public ActionResult<IEnumerable<Book>> GetBooksByAuthor(string author)
        {
            var authorBooks = new List<Book>();

            // Convert author to lowercase for case-insensitive comparison
            string lowerAuthor = author.ToLower();

            // Find all books by the given author
            foreach (var book in books)
            {
                if (book.Author.ToLower() == lowerAuthor)
                {
                    authorBooks.Add(book);
                }
            }

            // If no books were found, return NotFound
            if (authorBooks.Count == 0)
            {
                return NotFound();
            }

            // Return the list of books by the author
            return authorBooks;
        }

    }
}
