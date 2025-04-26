using BookReview.Application.UserCase.Books.Commands.CreateBook;
using BookReview.Application.UserCase.Books.Query.GetBookById;
using BookReview.Application.UserCase.Books.Query.GetPaginedBook;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers;

/// <summary>
/// Controller for managing books.
/// </summary>
public class BookController : BaseController
{

    /// <summary>
    /// Gets a paginated list of books.
    /// </summary>
    /// <param name="query">The query parameters for pagination.</param>
    /// <returns>A paginated response containing the list of books.</returns>
    [HttpPost]
    [Route("Pagined")]
    public async Task<IActionResult> GetPaginedBook([FromQuery] GetPaginedBookQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    /// <summary>
    /// Creates a new book.
    /// </summary>
    /// <param name="command">The command containing book details.</param>
    /// <returns>The created book response.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetPaginedBook), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// Gets a book by its ID.
    /// </summary>
    /// <param name="id">The ID of the book.</param>
    /// <return> A response containing the book details.</return>

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var result = await Mediator.Send(new GetBookByIdQuery(id));
        return Ok(result);
    }
}
