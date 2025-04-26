using AutoMapper;
using BookReview.Application.UserCase.Reviews.Dtos;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Reviews.Query.GetReviewByBookId;

public class GetReviewByBookIdQuery : IRequest<PaginatedResponse<ReviewResponse>>
{
    public int BookId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetReviewByBookIdQueryHandler : IRequestHandler<GetReviewByBookIdQuery, PaginatedResponse<ReviewResponse>>
{
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;
    public GetReviewByBookIdQueryHandler(IGenericRepository<Review> reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }
    public async Task<PaginatedResponse<ReviewResponse>> Handle(GetReviewByBookIdQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _reviewRepository.GetPaginatedAsync(
            x => x.BookId == request.BookId,
            x => x.OrderByDescending(r => r.CreatedAt),
            request.PageNumber,
            request.PageSize, false, "User,Book");

        var response = new PaginatedResponse<ReviewResponse>(
            data: _mapper.Map<List<ReviewResponse>>(reviews.Data),
            pageNumber: reviews.PageNumber,
            pageSize: reviews.PageSize,
            totalRecords: reviews.TotalRecords,
            totalCount: reviews.TotalCountRecords)
        {
            Message = "Successful query.",
            StatusCode = reviews.StatusCode,
            Success = reviews.Success,
            Errors = reviews.Errors
        };
        return response;
    }
}