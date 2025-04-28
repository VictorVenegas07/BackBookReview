using BookReview.Application.UserCase.Authentication.Commands.Login;
using BookReview.Application.UserCase.Authentication.Commands.RegisterUser;
using BookReview.Application.UserCase.Authentication.Commands.UpdatePicture;
using BookReview.Application.UserCase.Authentication.Query.GetProfilePicture;
using BookReview.Application.UserCase.Authentication.Query.GetUserProfile;
using BookReview.Domain.Common.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace BookReview.Api.Controllers;

/// <summary>
/// Auth Controller
/// </summary>
public class AuthController : BaseController
{
    /// <summary>
    /// Register User
    /// </summary>
    /// <param name="command">Register Request</param>
    /// <returns>Result</returns>

    [HttpPost("register")]

    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Login User
    /// </summary>
    /// <param name="command">Login Request</param>
    /// <returns>Result</returns>

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Success ? Ok(result) : BadRequest(result);

    }

    /// <summary>
    /// Get user profile
    /// </summary>
    /// <returns>Result</returns>

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var result = await Mediator.Send(new GetUserProfileCommand());
        return result.Success ? Ok(result) : BadRequest(result);
    }


    /// <summary>
    /// Get user profile picture
    /// </summary>
    /// <returns>Result</returns>
    

    [HttpGet("picture")]
    public async Task<IActionResult> GetProfilePicture()
    {
        var query = new GetProfilePictureQuery();
        var response = await Mediator.Send(query);

        return (response.Success)? File(response.Data!, "image/jpeg"): NotFound(response.Message);
        
    }

    /// <summary>
    /// update profile picture
    /// </summary>
    /// <param name="command">File</param>
    /// <returns>Result</returns>

    [HttpPut("picture")]
    public async Task<IActionResult> UpdateProfilePicture([FromBody] UpdatePictureCommand command)
    {
        var result = await Mediator.Send(command);
        return (result.Success) ? File(result.Data!, "image/jpeg") : NotFound(result.Message);
    }

}
