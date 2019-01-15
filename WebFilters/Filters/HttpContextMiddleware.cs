using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters
{
    /// <summary>
    /// Http 请求中间件
    /// </summary>
    public class HttpContextMiddleware
    {
        /// <summary>
        /// 处理HTTP请求
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// 构造 Http 请求中间件
        /// </summary>
        /// <param name="next"></param>
        public HttpContextMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// 执行响应流指向新对象
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            context.Response.Body = new ReadableResponseBody(context.Response.Body);
            return this.next.Invoke(context);
        }

        /// <summary>
        /// 可读的Response Body
        /// </summary>
        private class ReadableResponseBody : MemoryStream, IReadableBody
        {
            /// <summary>
            /// 流内容
            /// </summary>
            private readonly Stream body;

            /// <summary>
            /// 获取或设置是否可读
            /// </summary>
            public bool IsRead { get; set; }

            /// <summary>
            /// 构造自定义流
            /// </summary>
            /// <param name="body"></param>
            public ReadableResponseBody(Stream body)
            {
                this.body = body;
            }

            /// <summary>
            /// 写入响应流
            /// </summary>
            /// <param name="buffer"></param>
            /// <param name="offset"></param>
            /// <param name="count"></param>
            public override void Write(byte[] buffer, int offset, int count)
            {
                this.body.Write(buffer, offset, count);
                if (this.IsRead)
                {
                    base.Write(buffer, offset, count);
                }
            }

            /// <summary>
            /// 写入响应流
            /// </summary>
            /// <param name="source"></param>
            public override void Write(ReadOnlySpan<byte> source)
            {
                this.body.Write(source);
                if (this.IsRead)
                {
                    base.Write(source);
                }
            }

            /// <summary>
            /// 刷新响应流
            /// </summary>
            public override void Flush()
            {
                this.body.Flush();

                if (this.IsRead)
                {
                    base.Flush();
                }
            }

            /// <summary>
            /// 读取响应内容
            /// </summary>
            /// <returns></returns>
            public Task<string> ReadAsStringAsync()
            {
                if (this.IsRead == false)
                {
                    throw new NotSupportedException();
                }

                this.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(this))
                {
                    return reader.ReadToEndAsync();
                }
            }

            protected override void Dispose(bool disposing)
            {
                this.body.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
