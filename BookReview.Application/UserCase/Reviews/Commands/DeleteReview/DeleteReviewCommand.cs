using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;

namespace BookReview.Application.UserCase.Reviews.Commands.DeleteReview;

public record DeleteReviewCommand(int Id) : IRequest<Response<int>>;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Response<int>>
{
    private readonly IGenericRepository<Review> _reviewRepository;
    public DeleteReviewCommandHandler(IGenericRepository<Review> reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    public async Task<Response<int>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _reviewRepository.GetByIdAsync(request.Id);
        _ = review ?? throw new NotFoundException($"Review with id {request.Id} not found");
        await _reviewRepository.DeleteAsync(review);
        return new Response<int>(request.Id, "Review deleted successfully");
    }
}