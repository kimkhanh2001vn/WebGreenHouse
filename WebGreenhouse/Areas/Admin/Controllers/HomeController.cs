using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGreenhouse.Areas.Admin.Controllers
{
    //[Authorize]
    //[AllowAnonymous] : vao trang voi dac danh
    public class HomeController : BaseController
    {
        private GreenHouseDB db = new GreenHouseDB();
        // GET: Admin/Home
        public ActionResult Index()
        {
            ViewBag.User = db.Users.Where(x => x.Role == "Member").ToList();

            return View();
        }
    }
}