namespace BookReview.Infrastructure.Extensions;

public static class ApiConstants
{
    public const string LocalEnviroment = "Local";
    public const string ApplicationProject = "BookReview.Application";
    public const string DomainProject = "BookReview.Domain";
    public const string Infrastructure = "BookReview.Infrastructure";
    public const string MalformedToken = "El token de autenticación tiene un formato no válido.";
    public const string InvalidSignature = "La firma del token de autenticación no es válida.";
    public const string ExpiredToken = "El token de autenticación ha expirado.";
    public const string UnauthorizedToken = "El token de autenticación no es válido.";
    public const string NoPermissions = "El usuario no tiene permisos para acceder a este recurso.";
}