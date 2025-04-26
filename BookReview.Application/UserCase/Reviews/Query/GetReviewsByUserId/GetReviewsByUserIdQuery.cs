using AutoMapper;
using BookReview.Application.Helpers;
using BookReview.Application.UserCase.Reviews.Dtos;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookReview.Application.UserCase.Reviews.Query.GetReviewsByUserId;

public class GetReviewsByUserIdQuery : IRequest<PaginatedResponse<ReviewResponse>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

}

public class GetReviewsByUserIdQueryHandler : IRequestHandler<GetReviewsByUserIdQuery, PaginatedResponse<ReviewResponse>>
{
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public GetReviewsByUserIdQueryHandler(IGenericRepository<Review> reviewRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<PaginatedResponse<ReviewResponse>> Handle(GetReviewsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userId = UserContextHelper.GetUserId(_httpContextAccessor);
        _ = userId ?? throw new UnauthorizedAccessException("User not authenticated");

        var reviews = await _reviewRepository.GetPaginatedAsync(
            x => x.UserId == userId,
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

