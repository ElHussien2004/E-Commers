using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    //Attribute, IAsyncActionFilter =ActionFilterAttribute
    public class CacheAttribute(int DurationInSec=90) :ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           //Create Cache Key
           string CacheKey=CreateCacheKey(context.HttpContext.Request);

          //search for Value With Cache key 

            ICacheService CacheService =context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var CacheValue= await CacheService.GetAsync(CacheKey);

            //check if Cachevalue has value 
            if(CacheValue is not null)
            {
                context.Result = new ContentResult()  // using Microsoft.AspNetCore.Mvc;
                {
                    Content= CacheValue,
                    StatusCode = StatusCodes.Status200OK,
                    ContentType="application/json"

                };
                return;
            }
            //if value is Null
            var ExecutedContext = await next.Invoke();
            if(ExecutedContext.Result is OkObjectResult result)
            {
               await  CacheService.SetAsync(CacheKey,result.Value,TimeSpan.FromSeconds(DurationInSec));
            }

        }

        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder();
            Key.Append(request.Path + '?');
            foreach (var item in request.Query.OrderBy(O => O.Key))
            {
                Key.Append($"{item.Key}={item.Value}&");
            }
            return Key.ToString();

        }
    }
}
