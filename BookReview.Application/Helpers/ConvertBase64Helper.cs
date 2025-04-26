namespace BookReview.Application.Helpers;

public abstract class ConvertBase64Helper
{
    public static string ConvertToBase64(string base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return string.Empty;
        var bytes = Convert.FromBase64String(base64String);
        return Convert.ToBase64String(bytes);
    }
    public static byte[] ConvertBase64ToByteArray(string? base64String)
    {
        if (string.IsNullOrEmpty(base64String))
            return [];

        return Convert.FromBase64String(base64String);
    }

}
