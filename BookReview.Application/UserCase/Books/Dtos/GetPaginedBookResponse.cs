namespace BookReview.Application.UserCase.Books.Dtos;

public class GetPaginedBookResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public IEnumerable<string> Genres { get; set; } = null!;
    public int YearPublished { get; set; }
}
