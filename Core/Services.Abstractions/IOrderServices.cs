using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderServices
    {
        Task<OrderResultDto> GetOrderByIdAsync(Guid id);
        Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string email);
        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string email);
        Task<IEnumerable<DeliveryMethodsDto>> GetAllDeliveryMethods();
    }
}
