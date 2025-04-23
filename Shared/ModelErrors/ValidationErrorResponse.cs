using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ModelErrors
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; } = StatusCodes.Status404NotFound;
        public string ErrorMessage { get; set; } = "Validation Error!";

        public IEnumerable<ValidationError> Errors { get; set; }
    }
}
