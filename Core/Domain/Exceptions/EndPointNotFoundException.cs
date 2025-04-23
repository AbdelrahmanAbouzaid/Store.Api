using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EndPointNotFoundException(string message) : NotFoundException($"End Piont {message} Not Found!")
    {
    }
}
