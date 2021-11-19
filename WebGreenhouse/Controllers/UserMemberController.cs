using Model.DAO;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebGreenhouse.Controllers
{
    public class UserMemberController : Controller
    {
        private GreenHouseDB db = new GreenHouseDB();
        // GET: UserMember
        public ActionResult Index(string searchString,int page = 1)
        {
            int isize = 12;
            var user = db.Users.Where(x => x.Status == true && x.Role.Equals("Member")).ToList();

            ViewBag.list = UserDAO.getInstance().ListAllPaging(searchString, page, isize);

            ViewBag.CategoryID = new SelectList(db.PostCategorys.Where(x => x.Status == true).Select(x => new { CategoryID = x.ID, NameCate = x.NameCate }), "CategoryID", "NameCate");
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
    }
}