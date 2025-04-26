using System.Security.Cryptography;
using System.Text;

namespace BookReview.Domain.Common.Helpers;

public static class PasswordHelper
{
    public static byte[] HashPassword(string password, string salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var combinedPassword = password + salt;

            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));

            return hashBytes;
        }
    }

    public static string GenerateSalt()
    {
        byte[] saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes); 
        return Convert.ToBase64String(saltBytes);
    }

    public static bool VerifyPassword(byte[] hashedPassword, string password, string salt)
    {
        var hashedAttempt = HashPassword(password, salt);
        return hashedPassword.SequenceEqual(hashedAttempt);
    }
}

