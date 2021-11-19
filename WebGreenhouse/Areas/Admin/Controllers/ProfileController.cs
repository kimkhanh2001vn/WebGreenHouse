using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebGreenhouse.Common;

namespace WebGreenhouse.Areas.Admin.Controllers
{
    public class ProfileController : BaseController
    {
        private GreenHouseDB db = new GreenHouseDB();
        // GET: Admin/Profile
        public ActionResult Profile(int id)
        {
            var user = from a in db.Users where a.ID == id select a;
            return View(user);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include ="ID,UserName,PassWord,Role,Name,Email,NumberPhone,Address,Nation,MyWork,Status,Description")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "profile",new { id = user.ID});
            }
            return View(user);
        }
        
        public ActionResult OrderCustomer(int id,int ?value)
        {
            ViewBag.post = (from a in db.Posts
                        join b in db.OrderDetails on a.ID equals b.PostID
                        join c in db.Orders on b.OrderID equals c.OrderID
                        where a.CreatedByID == id
                        select new
                        {
                            a.Name,
                            c.CustomerEmail,
                            c.CustomerName,
                            c.CustomerMobile,
                            c.Status,
                            c.OrderID
                        }).ToList();
            ViewBag.user = UserDAO.getInstance().GetById(id);

            OrderDAO.getInstance().CheckSuccess(id,value);
            OrderDAO.getInstance().CheckPhone(id,value);

            return View();
        }
        //TODO:Profile_Menber
        public ActionResult ProfileUser(int? id)
        {
            ViewBag.user = UserDAO.getInstance().GetById(id);
            return View();
        }
    }
}