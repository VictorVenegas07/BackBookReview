using BookReview.Domain.Entities.Base;

namespace BookReview.Domain.Entities;

public class Category : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public List<BookCategory> BookCategories { get; set; } = new();
}
