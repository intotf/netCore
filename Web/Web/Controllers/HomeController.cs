using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exceptionless;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Models;
using Web.Server;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        NpgsSqlContext _npgsSqlContext { get; set; }
        SqliteContext _sqliteContext;

        public HomeController(NpgsSqlContext npgsSqlContext, SqliteContext sqliteContext)
        {
            this._npgsSqlContext = npgsSqlContext;
            this._sqliteContext = sqliteContext;
        }

        public async Task<IActionResult> Index()
        {
            //给当前游客配置一个默认Id,有效期 30秒
            if (this.HttpContext.Request.Cookies["VisitorsId"] == null)
            {
                var options = new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddSeconds(30)
                };
                var newId = Guid.NewGuid().ToString().Replace("-", "");
                this.HttpContext.Response.Cookies.Append("VisitorsId", newId, options);
            }


            ///数据库读取
            var npgsData = await _npgsSqlContext.ManageUser.Where(item => true).ToListAsync();
            var sqLiteData = await _sqliteContext.ManageUser.Where(item => true).ToListAsync(); ;

            //var db = this.HttpContext.RequestServices.GetService<NpgsSqlContext>();
            //var lists = db.User.Where(item => true);

            try
            {
                throw new ApplicationException(Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                ex.ToExceptionless().Submit();
            }

            var list = new SqliteContext("bizBd.db").ManageUser.Where(item => true);

            return View(sqLiteData);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
