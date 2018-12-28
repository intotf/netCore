using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace WebFilters.Filters
{
    /// <summary>
    /// Acion过滤器
    /// </summary>
    public class GlobalActonFilter : Attribute, IActionFilter
    {
        /// <summary>
        /// Acion 执行后拦截
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("IActionFilter OnActionExecuted Acion 执行后拦截");
        }

        /// <summary>
        /// Acion 执行前拦截
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("IActionFilter OnActionExecuting Acion 执行前拦截");
        }
    }
}
