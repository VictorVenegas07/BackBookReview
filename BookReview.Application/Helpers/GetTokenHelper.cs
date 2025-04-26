using BookReview.Domain.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BookReview.Domain.Common.Helpers;

public static class GetTokenHelper
{
    public static string GetJwtToken(IHttpContextAccessor httpContextAccessor)
    {
        var authorizationHeader = httpContextAccessor.HttpContext!.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader))
        {
            throw new UnauthorizedException("Authorization header is missing.");
        }

        var token = authorizationHeader.Replace("Bearer ", "");

        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedException("JWT token is missing in Authorization header.");
        }

        return token;
    }
}

