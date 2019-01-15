using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 日志提供者
        /// </summary>
        private static ILogger logger;

        /// <summary>
        /// 静太方法构造函数
        /// </summary>
        static LogHelper()
        {
            logger = new LoggerFactory().AddConsole().AddDebug().AddLog4Net().CreateLogger("Logs");
        }

        /// <summary>
        /// 打印提示
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(object message)
        {
            logger.LogInformation(message?.ToString());
        }

        /// <summary>
        /// 打印错误
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Error(object message)
        {
            logger.LogError(message?.ToString());
        }

        /// <summary>
        /// 打印错误
        /// </summary>
        /// <param name="ex">异常信息</param>
        /// <param name="message">日志内容</param>
        public static void Error(Exception ex, string message)
        {
            logger.LogError(ex, message);
        }

        /// <summary>
        /// 调试信息打印
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(object message)
        {
            logger.LogDebug(message?.ToString());
        }
    }
}
