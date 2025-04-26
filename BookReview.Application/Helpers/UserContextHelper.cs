using Microsoft.AspNetCore.Http;

namespace BookReview.Application.Helpers;

public static class UserContextHelper 
{
    public static int? GetUserId(IHttpContextAccessor httpContextAccessor )
    {
        var userIdString = httpContextAccessor.HttpContext?.User?.FindFirst("UserId")?.Value;

        if (int.TryParse(userIdString, out int userId))
        {
            return userId;
        }

        return null;
    }
}
