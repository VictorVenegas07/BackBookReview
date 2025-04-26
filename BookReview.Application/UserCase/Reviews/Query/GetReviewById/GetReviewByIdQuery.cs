using AutoMapper;
using BookReview.Application.UserCase.Reviews.Dtos;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Reviews.Query.GetReviewById;

public record GetReviewByIdQuery(int Id) : IRequest<Response<ReviewResponse>>;


public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Response<ReviewResponse>>
{
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IMapper _mapper;
    public GetReviewByIdQueryHandler(IGenericRepository<Review> reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
    }
    public async Task<Response<ReviewResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(request.Id, "User,Book");
        _ = review ?? throw new NotFoundException($"Review with id {request.Id} not found");
        var response = _mapper.Map<ReviewResponse>(review);
        return new Response<ReviewResponse>(response, "Successful query.");
    }
}