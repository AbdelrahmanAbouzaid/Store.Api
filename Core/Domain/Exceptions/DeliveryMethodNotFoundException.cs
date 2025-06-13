using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DeliveryMethodNotFoundException(int id) : NotFoundException($"Delivery Method With Id {id} Not Found!")
    {
    }
}
