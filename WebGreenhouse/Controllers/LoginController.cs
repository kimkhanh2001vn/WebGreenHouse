using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class LoginController : Controller
    {
        private GreenHouseDB db = new GreenHouseDB();
        // GET: Login
        public ActionResult Login()
        {
            if(ViewData["success"] != null)
            {
                ViewBag.success = ViewData["success"];
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                var result = CustomerDAO.getInstance().Login(model.UserName,Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var customer = CustomerDAO.getInstance().GetByUserName(model.UserName);
                    var CustomerSession = new CustomerLogin();
                    CustomerSession.UserName = customer.UserName;
                    CustomerSession.CustomerID = customer.CustomerID;
                    CustomerSession.CustomerName = customer.CustomerName;
                    Session.Add(CommonStants.User_Session, CustomerSession);
                    Session[CommonStants.username_Customer] = customer.CustomerName;
                    Session[CommonStants.ID_Customer] = customer.CustomerID;
                    Session["ID"] = customer.CustomerID;
                    HttpContext.Application["ID"] = customer.CustomerID;
                    return RedirectToAction("index", "home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài Khoản không Tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài Khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật Khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Tài Khoản không đúng");
                }
            }


            return View();
        }

        public JsonResult registration(string RegistrationModel)
        {
            var JsonRegistration = new JavaScriptSerializer().Deserialize<List<RegistrationModel>>(RegistrationModel);
            var model = JsonRegistration.SingleOrDefault();
            if (ModelState.IsValid)
            {
                if (!CustomerDAO.getInstance().Check(model.CustomerEmail, model.UserName))
                {
                    return Json(new { status = false });
                }
                else if (!CustomerDAO.getInstance().ConfirmPassword(model.Password,model.ConfirmPassWord))
                {
                    return Json(new { confim = false });
                }
                else if (model.NumberPhone == null)
                {
                    return Json(new { empty = false });
                }
                else
                {
                    var cus = new Customer();
                    cus.CustomerName = model.CustomerName;
                    cus.CustomerEmail = model.CustomerEmail;
                    cus.PassWord = Encryptor.MD5Hash(model.ConfirmPassWord);
                    cus.UserName = model.UserName;
                    cus.CustomerWork = model.CustomerWork;
                    cus.CustomerAddress = model.CustomerAddress;
                    cus.Description = "";
                    cus.NumberPhone = model.NumberPhone;
                    cus.Status = true;
                    cus.CreateDate = DateTime.Now;

                    var result = db.Customers.Add(cus);
                    if (result != null)
                    {
                        db.SaveChanges();
                        return Json(new { status = true });
                    }
                }
            }
            return Json(new { status = true });
        }
        public void SendVerificationEmail(string Email, string activationCode, string EmailFor = "VerifyAccount")
        {
            var verifyUrl = "/Login/" + EmailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("ngokhanh858@gmail.com", "Ngo Khanh");
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "206223432";
            string subject = "";
            string body = "";
            if (EmailFor != null)
            {
                subject = "Tài khoản của bạn được gửi mã xác thực thành công";
                body ="<br/><br/>Hãy chọn link bên dưới để xác thực " +
                    "Cập nhật thành công. Chọn link bên dưới để hoàn thành xác thực mật khẩu của bạn" +
                    "<br/><br/><a href='" + link +"'>" + link + "<a/>";
            }
            else if (EmailFor == "ResetPassWord")
            {
                subject = "Reset PassWord";
                body = "Hi,<br/>,<br/> Chúng tối đang gửi yêu cầu để cập nhật lại mật khẩu của bạn.Làm ơn hãy chọn link bên dưới để cập nhật lại mật khẩu." +
                    "<br/><br/><a href=" + link + ">Resest Password Link<a/>";
            }
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var messenge = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            smtp.Send(messenge);
        }
        public ActionResult ForgotPassword()
        {
            if (ViewData["success"] != null)
            {
                ViewBag.success = ViewData["success"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            //verify Email

            //gerenal reset password link

            //send mail
            string messenger = "";
            using (GreenHouseDB db = new GreenHouseDB())
            {
                var account = db.Customers.Where(x => x.CustomerEmail == email).FirstOrDefault();
                if(account != null)
                {
                    //send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationEmail(account.CustomerEmail, resetCode, "ResetPassword");
                    account.ResestPasswordCode = resetCode;
                    //this line i have added here to void confirm password not match issue
                    db.Configuration.ValidateOnSaveEnabled = false;
                    //save token guid in resetpassword
                    db.SaveChanges();
                    ViewData["success"] = "Mã xác thực đã được gửi đến email của bạn.";
                }
                else
                {
                    messenger = "Account Not found";
                }
            }
            return View();
        }
        public ActionResult ResetPassword(string id)
        {
            using(GreenHouseDB db = new GreenHouseDB())
            {
                var customer = db.Customers.Where(x => x.ResestPasswordCode == id).FirstOrDefault();
                if(customer != null)
                {
                    ResetPassWordModel model = new ResetPassWordModel();
                    model.ResetCode = id;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
        [HttpPost]
        public ActionResult ResestPassWord(ResetPassWordModel model)
        {
            if (ModelState.IsValid)
            {
                using (GreenHouseDB db = new GreenHouseDB())
                {
                    var customer = db.Customers.Where(x => x.ResestPasswordCode == model.ResetCode).FirstOrDefault();
                    if (customer != null)
                    {
                        customer.PassWord = Encryptor.MD5Hash(model.NewPassWord);
                        customer.ResestPasswordCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        ViewData["success"] = "Cập nhật mật khẩu thành công ! Hãy đăng nhập.";
                    }
                }
            }
            else
            {
                ViewData["success"] = "something invaild";
            }
            return RedirectToAction("Login","Login");
        }
        public ActionResult Logout()
        {
            Session[CommonStants.ID_Member] = null;
            Session[CommonStants.username_Member] = null;
            FormsAuthentication.SignOut();
            return Redirect("/admin/login/login");
        }
    }
}