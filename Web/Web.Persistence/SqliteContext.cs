using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Web.Model;
using System.IO;
using Microsoft.Extensions.Configuration;
using JetBrains.Annotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Web.Persistence
{
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
                Regex reg = new Regex("(?<=Source=)\\S+(?<!;)");
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
