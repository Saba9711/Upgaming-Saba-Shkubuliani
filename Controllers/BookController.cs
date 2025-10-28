using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using BookCatalog.Models;
using BookCatalog.Dto;
using BookCatalog.Data;

namespace BookCatalog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        //  Get All Books (with Author Name)
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetAllBooks()
        {
            var books = InMemoryStore.Books
                .Select(b =>
                {
                    var author = InMemoryStore.Authors.FirstOrDefault(a => a.ID == b.AuthorID);
                    return new BookDto
                    {
                        ID = b.ID,
                        Title = b.Title,
                        AuthorName = author?.Name ?? "Unknown",
                        PublicationYear = b.PublicationYear
                    };
                })
                .ToList();

            return Ok(books);
        }

        //  Get Books by a Specific Author
        [HttpGet("/api/authors/{id}/books")]
        public ActionResult<IEnumerable<BookDto>> GetBooksByAuthor(int id)
        {
            var author = InMemoryStore.Authors.FirstOrDefault(a => a.ID == id);
            if (author == null) return NotFound();

            var books = InMemoryStore.Books
                .Where(b => b.AuthorID == id)
                .Select(b => new BookDto
                {
                    ID = b.ID,
                    Title = b.Title,
                    AuthorName = author.Name,
                    PublicationYear = b.PublicationYear
                })
                .ToList();

            return Ok(books);
        }

        // Add a New Book
        // POST /api/books
        [HttpPost]
        public ActionResult<BookDto> CreateBook([FromBody] CreateBookDto dto)
        {
            if (dto == null) return BadRequest("Request body is required.");
            if (string.IsNullOrWhiteSpace(dto.Title)) return BadRequest("Title cannot be null or empty.");
            if (dto.PublicationYear > System.DateTime.UtcNow.Year) return BadRequest("PublicationYear cannot be in the future.");

            var author = InMemoryStore.Authors.FirstOrDefault(a => a.ID == dto.AuthorID);
            if (author == null) return BadRequest("AuthorID must correspond to an existing author.");

            var newId = InMemoryStore.Books.Any() ? InMemoryStore.Books.Max(b => b.ID) + 1 : 1;
            var book = new Book
            {
                ID = newId,
                Title = dto.Title,
                AuthorID = dto.AuthorID,
                PublicationYear = dto.PublicationYear
            };

            InMemoryStore.Books.Add(book);

            var result = new BookDto
            {
                ID = book.ID,
                Title = book.Title,
                AuthorName = author.Name,
                PublicationYear = book.PublicationYear
            };

            return Created($"/api/books/{book.ID}", result);
        }

        // PUT /api/books/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updated)
        {
            if (updated == null) return BadRequest("Request body is required.");

            var existing = InMemoryStore.Books.FirstOrDefault(b => b.ID == id);
            if (existing == null) return NotFound();

            if (string.IsNullOrWhiteSpace(updated.Title)) return BadRequest("Title cannot be null or empty.");
            if (updated.PublicationYear > System.DateTime.UtcNow.Year) return BadRequest("PublicationYear cannot be in the future.");
            var author = InMemoryStore.Authors.FirstOrDefault(a => a.ID == updated.AuthorID);
            if (author == null) return BadRequest("AuthorID must correspond to an existing author.");

            // apply updates
            existing.Title = updated.Title;
            existing.AuthorID = updated.AuthorID;
            existing.PublicationYear = updated.PublicationYear;

            return NoContent(); // 204
        }
    }
}

