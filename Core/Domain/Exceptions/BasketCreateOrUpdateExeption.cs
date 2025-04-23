using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class BasketCreateOrUpdateExeption() 
        : BadRequestException("Invalid operation when create or update Basket!")
    {
    }
}
