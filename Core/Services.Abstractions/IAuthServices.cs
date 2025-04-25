using Shared;


namespace Services.Abstractions
{
    public interface IAuthServices
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
    }
}
