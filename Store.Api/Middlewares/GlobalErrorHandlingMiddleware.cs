using Domain.Exceptions;
using Shared.ModelErrors;

namespace Store.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                    throw new EndPointNotFoundException(context.Request.Path);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails()
                {
                    ErrorMessage = ex.Message,
                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                }; 

                context.Response.StatusCode = response.StatusCode;

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
