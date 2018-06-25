using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web.Common;

namespace Web.Model
{
    /// <summary>
    /// 管理员信息表
    /// </summary>
    [Serializable]
    [Table("T_Sys_ManageUser")]
    public class ManageUser
    {
        /// <summary>
        /// 唯一 Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 登录帐号
        /// </summary>
        [Required]
        [StringLength(20)]
        public string Account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required]
        [StringLength(20)]
        [ConfigName("Pass")]
        public string PassWord { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        
        [NotMapped]
        public Info Info { get; set; }
    }

    public class Info
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Info()
        {

        }
    }
}
