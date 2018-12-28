using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters.Filters
{
    /// <summary>
    /// 结果过滤器
    /// </summary>
    public class GlobalResultFilter : IResultFilter
    {
        /// <summary>
        /// 动作结果执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("IResultFilter OnResultExecuted Acion 动作结果执行后");
        }

        /// <summary>
        /// 动作结果执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("IResultFilter OnResultExecuting Acion 动作结果执行前");
        }
    }
}
