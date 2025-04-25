using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationInSec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheServises = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheServices;
            var cacheKey = GenerateCacheKey(context.HttpContext.Request);
            var result = await cacheServises.GetCacheValueAsynk(cacheKey);
            if (result is not null)
            {
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK,
                    Content = result
                };
                return;
            }

            var contextresult = await next.Invoke();
            if (contextresult.Result is OkObjectResult okObjectResult)
            {
                await cacheServises.SetCacheValueAsynk(cacheKey, okObjectResult, TimeSpan.FromSeconds(durationInSec));
            }

        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var key = new StringBuilder();

            key.Append(request.Path);

            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }

            return key.ToString();
        }
    }
}
