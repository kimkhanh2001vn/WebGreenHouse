using Model.DAO;
using Model.EF;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebGreenhouse.Areas.Admin.Common;

namespace WebGreenhouse.Controllers
{
    public class ProfileController : Controller
    {
        private GreenHouseDB db = new GreenHouseDB();

        // GET: Profile
        public ActionResult Profile(int? id)
        {
            if (id == null)
            {
                return Redirect("/login/login");
            }
            return View(CustomerDAO.getInstance().GetById(id));
        }

        // GET: Admin/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin/Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,UserName,PassWord,CustomerName,CustomerAddress,CustomerWork,Description,Status,CustomerEmail,NumberPhone,CreateDate,Avata,LinkZalo,LinkFacebook")] Customer customer)
        {
            customer.Status = true;
            customer.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/profile/profile/" + customer.CustomerID);
            }
            return View(customer);
        }

        public ActionResult ChangePassWord(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            if (ViewData["err"] != null)
            {
                ViewBag.err = ViewData["err"];
            }
            else if (ViewData["success"] != null)
            {
                ViewBag.success = ViewData["success"];
            }
            return View(CustomerDAO.getInstance().GetById(id));
        }

        [HttpPost]
        public ActionResult ChangePassWord(int? id, string PassOld, string PassNew, string PassConfirm)
        {
            var idCustomer = db.Customers.SingleOrDefault(x => x.CustomerID == id);
            if (PassNew != PassConfirm)
            {
                ViewData["err"] = "Mật khẩu không trùng khớp";
            }
            else if (idCustomer.PassWord != Encryptor.MD5Hash(PassOld))
            {
                ViewData["err"] = "Mật Khẩu cũ không đúng";
            }
            else if(PassOld == PassConfirm)
            {
                ViewData["err"] = "Mật khẩu nay hiện tại đang sử dụng";
            }
            else
            {
                SqlParameter[] sql = new SqlParameter[]
                {
                new SqlParameter("@password" ,Encryptor.MD5Hash(PassConfirm)),
                new SqlParameter("@id",id)
                };
                try
                {
                    ViewData["success"] = "Thay đổi mật khẩu thành công";
                    var data = db.Database.SqlQuery<bool>("ChangePassWord @password,@id", sql).ToList();
                }
                catch(Exception ex)
                {

                }
            }
            return View(CustomerDAO.getInstance().GetById(id));
        }

        public PartialViewResult InfoCustomerPartial(int id)
        {
            var item = CustomerDAO.getInstance().GetById(id);
            return PartialView(item);
        }

        public ActionResult Delete(int orderid)
        {
            try
            {
                Order order = db.Orders.Find(orderid);

                order.Status = 2;
            }
            catch
            {
            }

            db.SaveChanges();

            return Redirect("/profile/historyorder/" + Session["ID"]);
        }

        public ActionResult HistoryOrder(int id, int page = 1, int isize = 8)
        {
            InfoCustomerPartial(id);

            ViewBag.post = (from a in db.Posts
                            join b in db.OrderDetails on a.ID equals b.PostID
                            join c in db.Orders on b.OrderID equals c.OrderID
                            where c.CustomerId == id 
                            select new
                            {
                                b.Price,
                                b.Quantity,
                                a.Name,
                                a.Images,
                                c.Status,
                                c.CreatedDate,
                                a.Address,
                                c.OrderID,
                                a.ID
                            }).ToList();
            //ViewBag.post = model.Skip((page - 1) * isize).Take(isize);
            return View();
        }
    }
}