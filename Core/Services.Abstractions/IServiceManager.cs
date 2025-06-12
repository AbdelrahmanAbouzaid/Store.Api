using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManager
    {
        IProductServices ProductServices { get; }
        IBasketServices BasketServices { get; }
        ICacheServices CacheServices { get; }
        IAuthServices AuthServices { get; }
        IOrderServices OrderServices { get; }

    }
}
