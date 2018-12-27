using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EasenApi.Models
{
    /// <summary>
    /// 住户基本资料
    /// </summary>
    [Serializable]
    public class House
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public string name { get; set; } = "张三";
        /// <summary>
        /// 手机号/帐号
        /// </summary>
        [Required]
        public string mobile { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? birthday { get; set; }
        /// <summary>
        /// 小区名
        /// </summary>
        public string communityName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }
    }
}
