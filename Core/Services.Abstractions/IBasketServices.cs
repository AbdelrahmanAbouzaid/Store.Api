using Shared;


namespace Services.Abstractions
{
    public interface IBasketServices
    {
        Task<BasketDto?> GetBaskesAsync(string id);
        Task<BasketDto?> UpdateBaskesAsync(BasketDto basketDto);
        Task<bool> DeleteBaskesAsync(string id);
    }
}
