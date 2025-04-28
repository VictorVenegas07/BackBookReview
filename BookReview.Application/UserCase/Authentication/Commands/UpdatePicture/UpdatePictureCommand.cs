using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookReview.Application.Helpers;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookReview.Application.UserCase.Authentication.Commands.UpdatePicture;

public record UpdatePictureCommand(string Picture): IRequest<Response<byte[]>>;


public class UpdatePictureCommandHandler : IRequestHandler<UpdatePictureCommand, Response<byte[]>>
{
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePictureCommandHandler(IGenericRepository<User> userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<Response<byte[]>> Handle(UpdatePictureCommand request, CancellationToken cancellationToken)
    {
        var userId = UserContextHelper.GetUserId(_httpContextAccessor);
        _ = userId ?? throw new UnauthorizedAccessException("User not authenticated");
        var user = await _userRepository.GetByIdAsync(userId.Value);
        _ = user ?? throw new NotFoundException($"User with id {userId} not found");

        user.Photo = ConvertBase64Helper.ConvertBase64ToByteArray(request.Picture);

        await _userRepository.UpdateAsync(user);

        return new Response<byte[]>(200, "Successful query.", true, user.Photo);


    }
}



