using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WebFilters.Filters
{
    /// <summary>
    /// 权限过滤器
    /// </summary>
    public class GlobalAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// 有请求时
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //context.Result = new ObjectResult("IAuthorizationFilter 拦截输出");
            Console.WriteLine("IAuthorizationFilter 拦截输出");
        }
    }
}
