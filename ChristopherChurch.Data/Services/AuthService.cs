using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ChristopherChurch.Data.Services
{
    // AuthService.cs
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public AuthService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;

        }

        public async Task LoginAsync()
        {
            var redirectUri = _configuration["Auth0:PostLogoutRedirectUri"];
            //var redirectUri = _httpContextAccessor.HttpContext?.Request.Host.ToUriComponent() ?? "default-redirect-uri";
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(redirectUri!)
                .Build();

            await PerformAuthenticationAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        public async Task LogoutAsync()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(_configuration["Auth0:PostLogoutRedirectUri"]!)
                .Build();

            await _httpContextAccessor.HttpContext!.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task PerformAuthenticationAsync(string scheme, AuthenticationProperties properties)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;

                if (httpContext == null || httpContext.Response.HasStarted)
                {
                    Console.WriteLine("Response has already started.");
                    return;
                }
                await Task.Delay(1);

                await httpContext.ChallengeAsync(scheme, properties);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
            }
        }
    }
}


