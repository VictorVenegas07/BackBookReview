using AutoMapper;
using BookReview.Application.Helpers;
using BookReview.Application.UserCase.Reviews.Dtos;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Entities;
using BookReview.Domain.Ports;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BookReview.Application.UserCase.Reviews.Commands.CrateReviews;

public class CrateReviewsCommand : IRequest<Response<ReviewResponse>>
{
    public int BookId { get; set; }
    public string Review { get; set; } = string.Empty;
    public int Rating { get; set; }
}

public class CrateReviewsCommandHandler : IRequestHandler<CrateReviewsCommand, Response<ReviewResponse>>
{
    private readonly IGenericRepository<Review> _reviewRepository;
    private readonly IGenericRepository<Book> _bookRepository;
    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CrateReviewsCommandHandler(IGenericRepository<Review> reviewRepository, IGenericRepository<Book> bookRepository, IGenericRepository<User> userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<ReviewResponse>> Handle(CrateReviewsCommand request, CancellationToken cancellationToken)
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

        await _reviewRepository.AddAsync(review, x=> x.User, b=> b.Book);

        var response = _mapper.Map<ReviewResponse>(review);

        return new Response<ReviewResponse>(response, "Review created successfully");
    }
}

