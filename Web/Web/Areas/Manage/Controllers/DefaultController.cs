using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Manage.Controllers
{
    public class DefaultController : BaseController
    {
        public IActionResult Index()
        {
            var sid = HttpContext.Request.QueryString;
           

            return View();
        }
    }
}