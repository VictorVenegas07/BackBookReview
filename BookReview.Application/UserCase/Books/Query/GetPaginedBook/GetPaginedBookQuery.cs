using System.Diagnostics.Metrics;
using System.Linq.Expressions;
using AutoMapper;
using BookReview.Application.UserCase.Books.Dtos;
using BookReview.Domain.Common.Helpers;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Books.Query.GetPaginedBook;

public class GetPaginedBookQuery : IRequest<PaginatedResponse<GetPaginedBookResponse>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; }
    public int? Category { get; set; } = null;
}

public class GetPaginedBookQueryHandler : IRequestHandler<GetPaginedBookQuery, PaginatedResponse<GetPaginedBookResponse>>
{
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IMapper _mapper;

    public GetPaginedBookQueryHandler(IGenericRepository<Book> bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedResponse<GetPaginedBookResponse>> Handle(GetPaginedBookQuery request, CancellationToken cancellationToken)
    {
       var filter = BuildFilter(request.SearchTerm, request.Category);


        var orderBy = (Func<IQueryable<Book>, IOrderedQueryable<Book>>)(q => q.OrderBy(b => b.CreatedAt));

        var books = await _bookRepository.GetPaginatedAsync(filter, orderBy, request.PageNumber, request.PageSize, false );

        var mappedData = _mapper.Map<IEnumerable<GetPaginedBookResponse>>(books.Data);

        var response = new PaginatedResponse<GetPaginedBookResponse>(
            data: mappedData,
            pageNumber: books.PageNumber,
            pageSize: books.PageSize,
            totalRecords: books.TotalRecords,
            totalCount: books.TotalCountRecords
        )
        {
            Message = "Successful query.",
            StatusCode = books.StatusCode,
            Success = books.Success,
            Errors = books.Errors
        };


        return response;
    }

    private static Expression<Func<Book, bool>> BuildFilter(string? searchTerm = null, int? category = null)
    {
        Expression<Func<Book, bool>>? filter = null;
        if (string.IsNullOrEmpty(searchTerm))
        {
            filter = searchTerm != null
            ? (Expression<Func<Book, bool>>)(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm)
            || b.Description.Contains(searchTerm))
            : null;
        }

        if (category is not null && category is not 0)
        {
            filter = filter == null
               ? b => b.BookCategories.Any(c => c.CategoryId == category)
               : filter.And(b => b.BookCategories.Any(c => c.CategoryId == category));
        }

        filter = b => b.BookCategories.Any(c => c.CategoryId == category);

     
        return filter;
    }

}