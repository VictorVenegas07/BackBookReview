using AutoMapper;
using BookReview.Application.Helpers;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookReview.Application.UserCase.Authentication.Query.GetProfilePicture;

public record GetProfilePictureQuery : IRequest<Response<byte[]>>;


public class GetProfilePictureQueryHandler : IRequestHandler<GetProfilePictureQuery, Response<byte[]>>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProfilePictureQueryHandler(IGenericRepository<User> userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<byte[]>> Handle(GetProfilePictureQuery request, CancellationToken cancellationToken)
    {
        var userId = UserContextHelper.GetUserId(_httpContextAccessor);
        _ = userId ?? throw new UnauthorizedAccessException("User not authenticated");
        var user = await _userRepository.GetByIdAsync(userId.Value);
        _ = user ?? throw new NotFoundException($"User with id {userId} not found");

     
        return new Response<byte[]>(200, "Successful query.", true, user.Photo);
    }
}