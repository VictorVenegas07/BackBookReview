using System.Net;
using System.Text;
using BookReview.Domain.Common.Exceptions;
using BookReview.Domain.Common.Wrappers;
using BookReview.Domain.Ports;
using BookReview.Infrastructure.Adapters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BookReview.Infrastructure.Extensions.Jwt
{
    public static class JwtExtension
    {
        public static IServiceCollection AddJsonWebToken(this IServiceCollection services)
        {  
            services.AddTransient<IAuthService, AuthService>();

            services.AddHttpContextAccessor()
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var issuer = System.Environment.GetEnvironmentVariable("ISSUER");
                    var audience = Environment.GetEnvironmentVariable("AUDIENCE");
                    var secret = Environment.GetEnvironmentVariable("SECRET_KEY");
                    _ = secret ?? throw new CustomException("Secreto no encontrado.");
                    var secretKey = Encoding.UTF8.GetBytes(secret);
                    //var encryption = Environment.GetEnvironmentVariable("ENCRYPTKEY");
                    //var encryptionKey = Encoding.UTF8.GetBytes(encryption);

                    var validationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = true,

                        ValidateIssuer = true,
                        ValidIssuer = issuer,

                        ValidateAudience = true,
                        ValidAudience = audience,

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                        //TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = validationParameters;

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception is SecurityTokenMalformedException)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";

                                var result = JsonConvert.SerializeObject(new Response<string>((int)HttpStatusCode.Unauthorized,ApiConstants.MalformedToken));
                                return context.Response.WriteAsync(result);
                            }
                            if (context.Exception is SecurityTokenSignatureKeyNotFoundException)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";

                                var result = JsonConvert.SerializeObject(new Response<string>((int)HttpStatusCode.Unauthorized, ApiConstants.InvalidSignature));
                                return context.Response.WriteAsync(result);
                            }
                            else if (context.Exception is SecurityTokenExpiredException)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";

                                var result = JsonConvert.SerializeObject(new Response<string>((int)HttpStatusCode.Unauthorized,ApiConstants.ExpiredToken));
                                return context.Response.WriteAsync(result);
                            }

                            context.NoResult();
                            context.Response.StatusCode = 500;
                            context.Response.ContentType = "text/plain";
                            return context.Response.WriteAsync(context.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (!context.Response.HasStarted)
                            {
                                context.Response.StatusCode = 401;
                                context.Response.ContentType = "application/json";

                                var result = JsonConvert.SerializeObject(new Response<string>((int)HttpStatusCode.Unauthorized,
                                    ApiConstants.UnauthorizedToken));
                                return context.Response.WriteAsync(result);
                            }

                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(new Response<string>((int)HttpStatusCode.Unauthorized,
                                 ApiConstants.NoPermissions));
                            return context.Response.WriteAsync(result);
                        }
                    };
                });
            return services;
        }
    }
}
