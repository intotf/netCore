using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters.Filters
{
    /// <summary>
    /// 异步 Action 过滤器
    /// </summary>
    public class GlobalAsyncActonFilter : Attribute, IAsyncActionFilter
    {
        /// <summary>
        /// 执行异步Action 过滤器
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 执行前
            await next.Invoke();

            // 执行后
            await OnExecutedAsync(context);
        }

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnExecutedAsync(ActionExecutingContext context)
        {
            Console.WriteLine("IAsyncActionFilter 执行后");
            await Task.CompletedTask;
        }
    }
}
