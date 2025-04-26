using BookReview.Domain.Entities.Base;

namespace BookReview.Domain.Entities;

public class Review : BaseEntity<int>
{
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; } 
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
}
