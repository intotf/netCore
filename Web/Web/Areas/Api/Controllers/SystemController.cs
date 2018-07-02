using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Api.Controllers
{
    [Area("Api")]
    public class SystemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}