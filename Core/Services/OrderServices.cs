using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared.OrderModels;

namespace Services
{
    public class OrderServices(IMapper mapper,
        IBasketRepository basketRepository,
        IUnitOfWork unitOfWork) : IOrderServices
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string email)
        {
            //1.Address
            var address = mapper.Map<Address>(orderRequest.ShipToAddress);

            //2.Order Items => basket
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
            if (basket is null) throw new BasketNotFoundException(orderRequest.BasketId);
            
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product =await unitOfWork.GetRepository<Product, int>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundException(item.Id);

                var orderItem = new OrderItem(
                    new ProductInOrderItem(product.Id,product.Name, product.PictureUrl),
                    item.Quantity,
                    product.Price
                    );

                items.Add(orderItem);
            }

            //3.Delivery Method
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod,  int>().GetAsync(orderRequest.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(orderRequest.DeliveryMethodId);

            var subTotal = items.Sum(i => i.Quentity * i.Price);

            //4.TODO:: Payment
            var orderSpec = new OrderWithPaymentIntentSpecification(basket.PaymentIntentId);
            var existOrder = await unitOfWork.GetRepository<Order, Guid>().GetAsync(orderSpec);
            if(existOrder is not null) unitOfWork.GetRepository<Order, Guid>().Delete(existOrder);

            var order = new Order(email,address,items, deliveryMethod, subTotal,basket.PaymentIntentId);
            await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<OrderResultDto>(order);
        }

        public async Task<IEnumerable<DeliveryMethodsDto>> GetAllDeliveryMethods()
        {
            var deliveryMethods = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<DeliveryMethodsDto>>(deliveryMethods);
            return result;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(id.ToString());
            var result = mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string email)
        {
            var spec = new OrderSpecification(email);
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
            var result = mapper.Map<IEnumerable<OrderResultDto>>(orders); 
            return result;
        }
    }
}
