using Domain.Models.OrderModels;
using Stripe;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentSpecification : BaseSpecification<Order, Guid>
    {
        public OrderWithPaymentIntentSpecification(string paymentIntentId)
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
          
        }
    }
}
