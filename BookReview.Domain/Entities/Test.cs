using BookReview.Domain.Entities.Base;

namespace BookReview.Domain.Entities;

public class Test : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;
}
