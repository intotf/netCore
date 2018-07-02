using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BaseController : Controller
    {

        public string Sid { get; set; }
    }
}