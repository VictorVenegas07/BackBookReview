namespace BookReview.Application.UserCase.Books.Dtos;

public class CreateBookResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int YearPublished { get; set; }
}
