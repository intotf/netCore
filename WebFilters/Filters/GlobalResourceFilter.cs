using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace WebFilters.Filters
{
    /// <summary>
    /// 资源过滤器
    /// </summary>
    public class GlobalResourceFilter : Attribute, IResourceFilter
    {
        /// <summary>
        /// 获取资源后拦截
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("IResourceFilter OnResourceExecuted 获取资源后拦截");
        }

        /// <summary>
        /// 获取资源前拦截
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("IResourceFilter OnResourceExecuting 获取资源前拦截");
        }
    }
}
