using Model.DAO;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using WebGreenhouse.Areas.Admin.Common;
using WebGreenhouse.Common;
using WebGreenhouse.Models;

namespace WebGreenhouse.Controllers
{
    public class HomeController : Controller
    {
        private GreenHouseDB db = new GreenHouseDB();
        public ActionResult Index()
        {
            ViewBag.SellNew = SellNew(6);
            ViewBag.user = db.Users.Where(x => x.Status == true && x.Role.Equals("Member")).ToList();
            
            return View();
        }
        [NonAction]
        private List<Post> SellNew(int count)
        {
            return db.Posts.OrderByDescending(x => x.CreateDate).Take(count).ToList();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var model = new ContactDAO().GetActiveContact();
            if (TempData["success"] != null)
            {
                ViewBag.success = TempData["success"];
            }
            return View(model);
        }
        public ActionResult Sells(int? page , string searchString)
        {
            int isize = 6;

            int iPageNum = (page ?? 1);

            
            IQueryable<Post> model = db.Posts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Alias.Contains(searchString));
            }
            var sell = model.Where(x => x.HomeFlag == true && x.Status == true).ToList();
            return View(sell.OrderByDescending(x=>x.CreateDate).ToPagedList(iPageNum, isize));
        }
        public ActionResult Rents(int? page, string searchString)
        {
           int isize = 6;

            int iPageNum = (page ?? 1);
            IQueryable<Post> model = db.Posts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Alias.Contains(searchString));
            }
            var rent = model.Where(x => x.HomeFlag == false && x.Status == true).ToList();

            return View(rent.OrderByDescending(x => x.CreateDate).ToPagedList(iPageNum, isize));
        }
        public ActionResult SendMail()
        {
            string name = Request.Form["name"].ToString();
            string email = Request.Form["email"].ToString();
            string numberphone = Request.Form["number"].ToString();
            string inputMessage = Request.Form["messeger"].ToString();
            string from = "ngokhanh858@gmail.com";
            //Replace this with your own correct Gmail Address
            //thanh.tran @viet-tradeplus.com.vn
            string to = "thaim73@gmail.com";
            //thanh.tran @viet-tradeplus.com.vn
            //Replace this with the Email Address
            //to whom you want to send the mail

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from, "ngo kim khanh", System.Text.Encoding.UTF8);
            mail.Subject = "Request from Clients";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = inputMessage;

            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            //587  port google mail
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);

            //Add the Creddentials- use your own email id and password
            System.Net.NetworkCredential nt =
            new System.Net.NetworkCredential(from, "206223432");

            client.Port = 587; // Gmail works on this port
            client.EnableSsl = true; //Gmail works on Server Secured Layer
            client.UseDefaultCredentials = false;
            client.Credentials = nt;
            client.Send(mail);
            TempData["Success"] = "Mail đã được gửi thành công.";
            return Redirect("/home/contact");
        }
        [HttpPost]
        public ActionResult Messenger(string content)
        {
            if(Session[CommonStants.ID_Customer] == null)
            {
                return RedirectToAction("Login","Login");
            }
            else
            {
                Messenger messenger = new Messenger();
                messenger.Content = content;
                messenger.CustomerID = (int)Session[CommonStants.ID_Customer];
                messenger.CreateDate = DateTime.Now;
                messenger.PostID = (int)TempData["PostID"];

                db.Messengers.Add(messenger);
                db.SaveChanges();
            }
            return RedirectToAction("Details", "home",new { id = TempData["PostID"]});
        }
        public ActionResult FAQ()
        {
            return View();
        }
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonStants.CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        public ActionResult Details(int id)
        {
                TempData["PostID"] = id;
                ViewBag.comment = (from a in db.Customers
                                   join b in db.Messengers on a.CustomerID equals b.CustomerID
                                   where b.PostID == id
                                   select new
                                   {
                                       a.CustomerName,
                                       a.UserName,
                                       a.CustomerEmail,
                                       b.Content,
                                       b.CreateDate
                                   }).ToList().OrderByDescending(x => x.CreateDate).Take(12);
                ViewBag.Post = (from a in db.Posts
                                join b in db.PostCategorys on a.CategoryID equals b.ID
                                where a.ID == id
                                select new
                                {
                                    a.Name,
                                    a.Images,
                                    a.Images2,
                                    a.HomeFlag,
                                    a.CreateDate,
                                    a.Content,
                                    a.Description,
                                    a.Address,
                                    a.Price,
                                    a.ID,
                                    b.NameCate
                                }).ToList();
                ViewBag.User = (from a in db.Posts
                                join b in db.Users on a.CreatedByID equals b.ID
                                where a.ID == id
                                select new
                                {
                                    b.Name,
                                    b.NumberPhone,
                                    b.Email,
                                    b.MyWork
                                }).ToList();
                ViewBag.ListG = (from a in db.Posts
                                 join b in db.PostCategorys on a.CategoryID equals b.ID
                                 select new
                                 {
                                     a.Name,
                                     a.Images,
                                     a.HomeFlag,
                                     a.CreateDate,
                                     a.Address,
                                     a.Price,
                                     a.ID
                                 }).OrderByDescending(x => x.CreateDate).Take(6).ToList();
                ViewBag.value = db.Messengers.Count() - 12;
            return View();
        }
        public ActionResult Logout()
        {
            //HttpContext.Session.Remove("username");
            Session[CommonStants.username_Customer] = null;
            Session[CommonStants.ID_Customer] = null;
            Session[CommonStants.username_Member] = null;
            Session[CommonStants.ID_Member] = null;
            FormsAuthentication.SignOut();
            Session[CommonStants.CartSession] = null;
            return RedirectToAction("index", "home");      
        }
    }
}