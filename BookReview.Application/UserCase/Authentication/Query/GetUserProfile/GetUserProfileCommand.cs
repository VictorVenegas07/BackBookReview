using AutoMapper;
using BookReview.Application.Helpers;
using BookReview.Application.UserCase.Authentication.Dtos;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookReview.Application.UserCase.Authentication.Query.GetUserProfile;

public record GetUserProfileCommand : IRequest<Response<UserResponse>>;

public class GetUserInfoHandler : IRequestHandler<GetUserProfileCommand, Response<UserResponse>>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetUserInfoHandler(IGenericRepository<User> userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<UserResponse>> Handle(GetUserProfileCommand request, CancellationToken cancellationToken)
    {
        var userId = UserContextHelper.GetUserId(_httpContextAccessor);
        _ = userId ?? throw new UnauthorizedAccessException("User not authenticated");
        var user = await _userRepository.GetByIdAsync(userId.Value);
        _ = user ?? throw new NotFoundException($"User with id {userId} not found");

        var userResponse = _mapper.Map<UserResponse>(user);
        return new Response<UserResponse>(200, "Successful query.", true, userResponse);
    }
}


