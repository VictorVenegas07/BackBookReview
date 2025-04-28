using AutoMapper;
using BookReview.Application.Helpers;
using BookReview.Application.UserCase.Reviews.Dtos;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookReview.Application.UserCase.Reviews.Commands.UpdateReview;

public class UpdateReviewCommand : IRequest<Response<ReviewResponse>>
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string Review { get; set; } = string.Empty;
    public int Rating { get; set; }
}

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Response<ReviewResponse>>
{
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateReviewCommandHandler(IGenericRepository<Review> reviewRepository, IGenericRepository<Book> bookRepository, IGenericRepository<User> userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<Response<ReviewResponse>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = UserContextHelper.GetUserId(_httpContextAccessor);
        _ = userId ?? throw new UnauthorizedAccessException("User not authenticated");

        var review = new Review
        {
            BookId = request.BookId,
            Content = request.Review,
            Rating = request.Rating,
            UserId = userId.Value,

        };
        var book = await _bookRepository.GetByIdAsync(request.BookId);
        _ = book ?? throw new NotFoundException($"Book with id {request.BookId} not found");

        var user = await _userRepository.GetByIdAsync(userId.Value);
        _ = user ?? throw new NotFoundException($"User with id {userId} not found");

        var existingReview = await _reviewRepository.GetByIdAsync(request.Id);
        _ = existingReview ?? throw new NotFoundException($"Review with id {request.Id} not found");


        if (!IsUserOwnerOfReview(userId, existingReview.UserId))
            throw new UnauthorizedAccessException("You do not have permission to update this review");
        var updatedReview = HasReviewChanged(existingReview, review);

        if (updatedReview is null)
        {
            return new Response<ReviewResponse>(_mapper.Map<ReviewResponse>(existingReview), "Review updated successfully");
        }


        await _reviewRepository.UpdateAsync(existingReview);

        var response = _mapper.Map<ReviewResponse>(review);

        return new Response<ReviewResponse>(response, "Review updated successfully");
    }

    private Review? HasReviewChanged(Review existingReview, Review updatedReview)
    {
        bool isRatingChanged = existingReview.Rating != updatedReview.Rating;
        bool isContentChanged = existingReview.Content != updatedReview.Content;
        bool isBookChanged = existingReview.BookId != updatedReview.BookId;

        if (isRatingChanged) existingReview.Rating = updatedReview.Rating;
        if (isContentChanged) existingReview.Content = updatedReview.Content;
        if (isBookChanged) existingReview.BookId = updatedReview.BookId;

        return isRatingChanged || isContentChanged || isBookChanged ? existingReview : null;
    }

    private bool IsUserOwnerOfReview(int? userId, int reviewUserId)
    {
        return userId.HasValue && userId.Value == reviewUserId;
    }
}
