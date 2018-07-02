using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Web.Model;

namespace Web.Server
{
    /// <summary>
    /// SqLite 数据库上下文
    /// </summary>
    public class SqliteContext : DbContext
    {
        IConfiguration Configuration;

        private string DbFile;

        /// <summary>
        /// 根据配置文件连接字符串连接
        /// </summary>
        /// <param name="configuration"></param>
        public SqliteContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// 根据数据库名称连接
        /// </summary>
        /// <param name="dbName"></param>
        public SqliteContext(string dbName)
        {
            this.DbFile = dbName;
        }

        /// <summary>
        /// 管理员信息
        /// </summary>
        public DbSet<ManageUser> ManageUser { get; set; }

        /// <summary>
        /// 绑定连接数据库
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var cntString = $"Data Source={DbFile};";
            if (string.IsNullOrEmpty(this.DbFile))
            {
                var reg = new Regex("(?<=Source=)\\S+(?<!;)");
                cntString = Configuration.GetConnectionString("SqliteContext");
                this.DbFile = reg.Match(cntString).Value;
            }
            if (File.Exists(DbFile) == false)
            {
                throw new FileNotFoundException($"找不到db文件{DbFile}");
            }
            optionsBuilder.UseSqlite(cntString);
        }
    }
}
