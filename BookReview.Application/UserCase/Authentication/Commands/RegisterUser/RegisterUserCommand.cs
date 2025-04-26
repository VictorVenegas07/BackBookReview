using BookReview.Application.Helpers;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Helpers;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Authentication.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Response<int>>
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Photo { get; set; }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Response<int>>
{
    private readonly IGenericRepository<User> _userRepository;
    public RegisterUserHandler(IGenericRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Response<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var existingUser = await _userRepository.GetEntityAsync(
           u => u.Username == request.Username || u.Email == request.Email);

        if (existingUser != null)
        {
            throw new CustomException("Username or email already exists");
        }

        var salt = PasswordHelper.GenerateSalt();
        var user = new User
        {
            Username = request.Username,
            PasswordHash = PasswordHelper.HashPassword(request.PasswordHash, salt),
            FullName = request.FullName,
            Email = request.Email,
            Photo = ConvertBase64Helper.ConvertBase64ToByteArray(request.Photo),
            Salt = salt
        };
        await _userRepository.AddAsync(user);
        return new Response<int>(200,"User registered successfully", true, user.Id);
    }


}
