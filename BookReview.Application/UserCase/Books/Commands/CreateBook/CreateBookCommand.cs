using BookReview.Application.UserCase.Books.Dtos;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Ports;
using MediatR;
using BookReview.Domain.Entities;
using AutoMapper;

namespace BookReview.Application.UserCase.Books.Commands.CreateBook;

public class CreateBookCommand : IRequest<Response<CreateBookResponse>>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int YearPublished { get; set; }
}

public class CreateBookHandler : IRequestHandler<CreateBookCommand, Response<CreateBookResponse>>
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public CreateBookHandler(IGenericRepository<Book> bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<Response<CreateBookResponse>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var bookEntity =  _mapper.Map<Book>(request);
        var createdBook = await _bookRepository.AddAsync(bookEntity);
        var response = _mapper.Map<CreateBookResponse>(createdBook);

        return new Response<CreateBookResponse>(response);
    }
}
