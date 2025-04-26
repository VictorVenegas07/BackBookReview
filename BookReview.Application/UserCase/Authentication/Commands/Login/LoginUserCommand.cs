using BookReview.Application.UserCase.Authentication.Dtos;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Helpers;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Authentication.Commands.Login;

public class LoginUserCommand : IRequest<Response<LoginResponse>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginUserHandler : IRequestHandler<LoginUserCommand, Response<LoginResponse>>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IAuthService _authService;

    public LoginUserHandler(IGenericRepository<User> userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }

    public async Task<Response<LoginResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetEntityAsync(x => x.Email == request.Email, isTracking: false);
        _ = user ?? throw new CustomException("User not found");

        var hashedPassword = PasswordHelper.VerifyPassword(user.PasswordHash, request.Password, user.Salt ?? string.Empty );

        if (!hashedPassword)
            throw new CustomException("The email or password is incorrect.");

        var token = _authService.GenerateAccessToken(user);

        return new Response<LoginResponse>(new LoginResponse { AccessToken = token });
    }
}
