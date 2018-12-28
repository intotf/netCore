using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters.Filters
{

    /// <summary>
    /// 异步异常全局过滤器
    /// </summary>
    public class GlobalAsyncExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// 异常异步处理
        /// </summary>
        /// <param name="context">异常上下文</param>
        /// <returns></returns>
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            Console.WriteLine(context.Exception.Message);
            //如果这里设为false，就表示告诉MVC框架，我没有处理这个错误。然后让它跳转到自己定义的错误页（设为true的话，就表示告诉MVC框架，异常我已经处理了。不需要在跳转到错误页了，也部会抛出黄页了）
            context.ExceptionHandled = false;
            await Task.CompletedTask;
        }
    }
}
