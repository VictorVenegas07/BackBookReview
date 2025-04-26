using BookReview.Application.UserCase.Category.Dtos;
using BookReview.Application.UserCase.Category.Query.GetAllCategory;
using BookReview.Domain.Common.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers;


/// <summary>
/// Controller for managing categories.
/// /// </summary>
public class CategoryController : BaseController
{
    /// <summary>
    /// Gets all categories.
    /// </summary>
    /// <returns>A response containing the list of categories.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(Response<IEnumerable<CategoryResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        var result = await Mediator.Send(new GetAllCategoryQuery());
        return Ok(result);
    }
}
