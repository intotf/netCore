using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFilters
{
    /// <summary>
    /// 定义可读Body的接口
    /// </summary>
    public interface IReadableBody
    {
        /// <summary>
        /// 获取或设置是否可读
        /// </summary>
        bool IsRead { get; set; }

        /// <summary>
        /// 读取文本内容
        /// </summary>
        /// <returns></returns>
        Task<string> ReadAsStringAsync();
    }
}
