using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasenApi.Filters
{
    /// <summary>
    /// 响应模型验证不通过时
    /// </summary>
    public class HttpGlobalResultFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errMsg = context.ModelState.Values.Where(item => item.ValidationState != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid).FirstOrDefault().Errors.FirstOrDefault().ErrorMessage;
                context.Result = new JsonResult(new { state = false, data = errMsg });
            }
        }
    }
}
