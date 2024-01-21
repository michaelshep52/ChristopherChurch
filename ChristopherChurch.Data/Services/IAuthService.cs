namespace ChristopherChurch.Data.Services
{
    public interface IAuthService
    {
        Task LoginAsync();
        Task LogoutAsync();
    }
}