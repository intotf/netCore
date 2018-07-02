using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Web.Common;
using Web.Model;

namespace Web.Server
{
    /// <summary>
    /// PostgreSql 数据库上下文
    /// </summary>
    public class NpgsSqlContext : DbContext
    {
        /// <summary>
        /// 管理员表
        /// </summary>
        public DbSet<ManageUser> ManageUser { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        public NpgsSqlContext(DbContextOptions<NpgsSqlContext> options) : base(options)
        {

        }

        /// <summary>
        /// 模型创建
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            ////UserRole关联配置
            //builder.Entity<UserRole>()
            //  .HasKey(ur => new { ur.UserId, ur.RoleId });

            ////RoleMenu关联配置
            //builder.Entity<RoleMenu>()
            //  .HasKey(rm => new { rm.RoleId, rm.MenuId });
            //builder.Entity<RoleMenu>()
            //  .HasOne(rm => rm.Role)
            //  .WithMany(r => r.RoleMenus)
            //  .HasForeignKey(rm => rm.RoleId).HasForeignKey(rm => rm.MenuId);

            ////启用Guid主键类型扩展
            //builder.HasPostgresExtension("uuid-ossp");

            base.OnModelCreating(builder);
        }
    }
}
