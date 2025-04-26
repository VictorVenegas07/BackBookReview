using BookReview.Domain.Entities.Base;

namespace BookReview.Domain.Entities;

public class User: BaseEntity<int>
{
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = [];
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[]? Photo { get; set; }
    public string? Salt { get; set; } = string.Empty;
    public List<Review> Reviews { get; set; } = null!;
}
