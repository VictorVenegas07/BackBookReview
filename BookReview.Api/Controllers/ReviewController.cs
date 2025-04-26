using BookReview.Application.UserCase.Reviews.Commands.CrateReviews;
using BookReview.Application.UserCase.Reviews.Commands.DeleteReview;
using BookReview.Application.UserCase.Reviews.Commands.UpdateReview;
using BookReview.Application.UserCase.Reviews.Dtos;
using BookReview.Application.UserCase.Reviews.Query.GetReviewByBookId;
using BookReview.Application.UserCase.Reviews.Query.GetReviewById;
using BookReview.Application.UserCase.Reviews.Query.GetReviewsByUserId;
using BookReview.Domain.Common.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers;

/// <summary>
/// Controller for managing reviews.
/// </summary>
public class ReviewController : BaseController
{
    /// <summary>
    /// Creates a new review.
    /// </summary>
    /// <param name="command">The command containing review details.</param>
    /// <returns>A response containing the created review.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Response<ReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReview([FromBody] CrateReviewsCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Gets a review by its ID.
    /// </summary>
    /// <param name="id">The ID of the review.</param>
    /// <returns>A response containing the review.</returns>

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Response<ReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReviewById(int id)
    {
        var result = await Mediator.Send(new GetReviewByIdQuery(id));
        return Ok(result);
    }

    /// <summary>
    /// Updates an existing review.
    /// </summary>
    /// <param name="id">The ID of the review to update.</param>
    /// <param name="command">The command containing updated review details.</param>
    /// <returns>A response containing the updated review.</returns>

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Response<ReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdateReviewCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Review ID mismatch.");
        }
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a review by its ID.
    /// </summary>
    /// <param name="id">The ID of the review.</param>
    /// <returns>A response indicating the result of the deletion.</returns>

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Response<ReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var result = await Mediator.Send(new DeleteReviewCommand(id));
        return Ok(result);
    }


    /// <summary>
    /// Gets all reviews for a specific book.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of reviews per page.</param>
    /// <param name="bookId">The ID of the book.</param>
    /// <returns>A response containing a list of reviews.</returns>

    [HttpGet("book/{bookId}")]

    [ProducesResponseType(typeof(PaginatedResponse<ReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllReviews(int bookId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await Mediator.Send(new GetReviewByBookIdQuery { BookId = bookId, PageNumber = pageNumber, PageSize = pageSize });
        return Ok(result);
    }

    /// <summary>
    /// Gets all reviews for a specific user.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination.</param>
    /// <param name="pageSize">The number of reviews per page.</param>
    /// <returns>A response containing a list of reviews.</returns>

    [HttpGet("ByUser")]

    [ProducesResponseType(typeof(PaginatedResponse<ReviewResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReviewsByUser([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await Mediator.Send(new GetReviewsByUserIdQuery { PageNumber = pageNumber, PageSize = pageSize });
        return Ok(result);
    }

}
