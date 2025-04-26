using BookReview.Domain.Entities;

namespace BookReview.Domain.Ports;

public interface IAuthService
{
    string GenerateAccessToken(User token);
}
