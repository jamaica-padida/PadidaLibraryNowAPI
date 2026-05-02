using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PadidaLibraryNowAPI.Models;
using System.Reflection;

namespace PadidaLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "The Vampire Diaries",
                Author = "L.J. Smith",
                Genre = "Supernatural Teen Drama",
                Available = true,
                PublisherYear = 1991
            },
             new Book
            {
                Id = 2,
                Title = "El Filibusterismo",
                Author = "Dr. Jose Rizal",
                Genre = "Historical fiction",
                Available = true,
                PublisherYear = 1891
            },
        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retrieved."
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Book Retrieved."
            });
        }
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id },
                new
                {
                    status = "sucess",
                    data = newBook,
                    message = "Book created."
                });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublisherYear = updateBook.PublisherYear;

            return Ok(new
            {
                status = "sucess",
                data = book,
                message = "Book updated."
            });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete (int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            books.Remove(book);
            return Ok(new
            {
                status = "error",
                data = (object?)null,
                message = "Book deleted."
            });
        }
    }
}


