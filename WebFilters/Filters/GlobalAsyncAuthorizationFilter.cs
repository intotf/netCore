using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters.Filters
{
    /// <summary>
    /// 异步权限过滤器
    /// </summary>
    public class GlobalAsyncAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 执行异步Action 过滤器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            Console.WriteLine("IAsyncAuthorizationFilter 拦截输出");
            await Task.CompletedTask;
        }
    }
}
