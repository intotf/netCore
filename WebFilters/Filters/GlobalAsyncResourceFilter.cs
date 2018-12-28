using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters.Filters
{
    /// <summary>
    /// 异步资源加载过滤器
    /// </summary>
    public class GlobalAsyncResourceFilter : IAsyncResourceFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
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
        public async Task OnExecutedAsync(ResourceExecutingContext context)
        {
            Console.WriteLine("IAsyncResourceFilter 执行后");
            await Task.CompletedTask;
        }
    }
}
