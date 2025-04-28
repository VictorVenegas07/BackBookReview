using AutoMapper;
using BookReview.Application.Helpers;
using BookReview.Application.UserCase.Books.Dtos;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookReview.Application.UserCase.Books.Query.GetBookById;

public record GetBookByIdQuery(int Id) : IRequest<Response<CreateBookResponse>>;


public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Response<CreateBookResponse>>
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<Review> _reviewepository;

    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetBookByIdQueryHandler(IGenericRepository<Book> bookRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IGenericRepository<Review> reviewepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _reviewepository = reviewepository;
    }

    public async Task<Response<CreateBookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = UserContextHelper.GetUserId(_httpContextAccessor);
        var book = await _bookRepository.GetByIdAsync(request.Id);
       _ = book ?? throw new NotFoundException($"Book with id {request.Id} not found");

        var reviews = await _reviewepository.Exist(x => x.BookId == request.Id && x.UserId == userId);


        var response = _mapper.Map<CreateBookResponse>(book);
        response.Reviewed = reviews;
        return new Response<CreateBookResponse>(response, "Successful query.");
    }
}