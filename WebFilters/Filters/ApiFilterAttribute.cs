using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFilters.Filters
{
    /// <summary>
    /// Api 过滤器,记录请求上下文及响应上下文
    /// </summary>
    public class ApiFilterAttribute : Attribute, IActionFilter, IAsyncResourceFilter
    {


        /// <summary>
        /// 请求Api 资源时
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            // 执行前
            try
            {
                await next.Invoke();
            }
            catch
            {
            }
            // 执行后
            await OnResourceExecutedAsync(context);
        }

        /// <summary>
        /// 记录Http请求上下文
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnResourceExecutedAsync(ResourceExecutingContext context)
        {
            var log = new HttpContextMessage
            {
                RequestMethod = context.HttpContext.Request.Method,
                ResponseStatusCode = context.HttpContext.Response.StatusCode,
                RequestQurey = context.HttpContext.Request.QueryString.ToString(),
                RequestContextType = context.HttpContext.Request.ContentType,
                RequestHost = context.HttpContext.Request.Host.ToString(),
                RequestPath = context.HttpContext.Request.Path,
                RequestScheme = context.HttpContext.Request.Scheme,
                RequestLocalIp = (context.HttpContext.Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" + context.HttpContext.Request.HttpContext.Connection.LocalPort),
                RequestRemoteIp = (context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString() + ":" + context.HttpContext.Request.HttpContext.Connection.RemotePort)
            };

            //获取请求的Body
            //数据流倒带 context.HttpContext.Request.EnableRewind();
            if (context.HttpContext.Request.Body.CanSeek)
            {
                using (var requestSm = context.HttpContext.Request.Body)
                {
                    requestSm.Position = 0;
                    var reader = new StreamReader(requestSm, Encoding.UTF8);
                    log.RequestBody = reader.ReadToEnd();
                }
            }

            //将当前 http 响应Body 转换为 IReadableBody
            if (context.HttpContext.Response.Body is IReadableBody body)
            {
                if (body.IsRead)
                {
                    log.ResponseBody = await body.ReadAsStringAsync();
                }
            }
            if (string.IsNullOrEmpty(log.ResponseBody) == false && log.ResponseBody.Length > 200)
            {
                log.ResponseBody = log.ResponseBody.Substring(0, 200) + "......";
            }
            LogHelper.Debug(log);
        }

        /// <summary>
        /// Action 执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //设置 Http请求响应内容设为可读
            if (context.HttpContext.Response.Body is IReadableBody responseBody)
            {
                responseBody.IsRead = true;
            }
        }

        /// <summary>
        /// Action 执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
