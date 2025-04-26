using BookReview.Domain.Entities.Base;

namespace BookReview.Domain.Entities;

public class Book : BaseEntity<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int YearPublished { get; set; }
    public List<BookCategory> BookCategories { get; set; } = null!;
    public List<Review> Reviews { get; set; } = null!;
}
