using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFilters
{
    /// <summary>
    /// Http 上下日志
    /// </summary>
    public class HttpContextMessage
    {
        /// <summary>
        /// 请求Qurey消息
        /// </summary>
        public string RequestQurey { get; set; }

        /// <summary>
        /// 请求者本地ip
        /// </summary>
        public string RequestLocalIp { get; set; }

        /// <summary>
        /// 请求者远程ip
        /// </summary>
        public string RequestRemoteIp { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string RequestMethod { get; set; }

        /// <summary>
        /// 请求协议标识
        /// </summary>
        public string RequestScheme { get; set; }

        /// <summary>
        /// 请求内容类型
        /// </summary>
        public string RequestContextType { get; set; }

        /// <summary>
        /// 请求Body消息
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// 请求域名消息
        /// </summary>
        public string RequestHost { get; set; }

        /// <summary>
        /// 请求路径消息
        /// </summary>
        public string RequestPath { get; set; }

        /// <summary>
        /// 响应内容
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// 响应状态码
        /// </summary>
        public int ResponseStatusCode { get; set; }

        /// <summary>
        /// 重写ToString 方便阅读
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"请求者IP：{this.RequestLocalIp};{this.RequestRemoteIp}");
            sb.AppendLine($"请求方式：{this.RequestMethod}");
            sb.AppendLine($"请求地址：{this.RequestScheme}://{this.RequestHost}");
            sb.AppendLine($"请求路径：{this.RequestPath}{this.RequestQurey}");
            sb.AppendLine($"请求Body：{this.RequestBody}");
            sb.AppendLine($"响应内容({this.ResponseStatusCode})：");
            sb.AppendLine(this.ResponseBody);
            sb.AppendLine("----------------------------------------");
            return sb.ToString();
        }
    }
}
