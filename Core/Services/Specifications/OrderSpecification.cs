using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderSpecification : BaseSpecification<Order, Guid>
    {
        public OrderSpecification(Guid id) 
            : base(o => o.Id == id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }
        public OrderSpecification(string email)
            : base(o => o.UserEmail == email)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            AddOrderBy(o => o.OrderDate);
        }

    }
}
