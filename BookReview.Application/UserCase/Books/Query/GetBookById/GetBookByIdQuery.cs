using AutoMapper;
using BookReview.Application.UserCase.Books.Dtos;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Books.Query.GetBookById;

public record GetBookByIdQuery(int Id) : IRequest<Response<CreateBookResponse>>;


public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Response<CreateBookResponse>>
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IGenericRepository<Book> bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<Response<CreateBookResponse>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);
       _ = book ?? throw new NotFoundException($"Book with id {request.Id} not found");
        var response = _mapper.Map<CreateBookResponse>(book);
        return new Response<CreateBookResponse>(response, "Successful query.");
    }
}