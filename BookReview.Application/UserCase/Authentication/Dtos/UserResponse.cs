namespace BookReview.Application.UserCase.Authentication.Dtos;

public class UserResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public byte[]? Photo { get; set; }
}
