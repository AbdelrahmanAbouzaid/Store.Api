using Shared;
using Shared.OrderModels;


namespace Services.Abstractions
{
    public interface IAuthServices
    {
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> CheckEmailExistAsync(string email);
        Task<UserResultDto> GetCurrentUserAsync(string email);
        Task<AddressDto> GetCurrentUserAddressAsync(string email);
        Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto address, string email);



    }
}
