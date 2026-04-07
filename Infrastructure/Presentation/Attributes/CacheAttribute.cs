using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;

namespace Presentation.Attributes
{
    public class CacheAttribute(int durationinsec) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheservice= context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            var cachekey = GenerateKey(context.HttpContext.Request);
            var result = await cacheservice.GetCacheValueAsync(cachekey);
            if (!string.IsNullOrEmpty(result))
            {
                //return response
                context.Result = new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode=StatusCodes.Status200OK,
                    Content = result
                };
                return;
            }
            //Excute The Endpoint
            var contextresult=await next.Invoke();
            if(contextresult.Result is OkObjectResult okObject)
            {
                await cacheservice.SetCacheValueAsync(cachekey,okObject.Value,TimeSpan.FromSeconds(durationinsec));
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(q=>q.Key))
            {
                key.Append($"|{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
