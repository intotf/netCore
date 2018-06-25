using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Common
{
    /// <summary>
    /// 配置信息另外特性
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class ConfigNameAttribute : Attribute
    {
        /// <summary>
        /// 节点名别称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        public ConfigNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
