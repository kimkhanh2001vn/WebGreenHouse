using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebGreenhouse.Areas.Admin.Code;
using WebGreenhouse.Areas.Admin.Common;
using WebGreenhouse.Common;
using WebGreenhouse.Models;

namespace WebGreenhouse.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private GreenHouseDB db = new GreenHouseDB();
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = UserDAO.getInstance().Login(model.UserName,Encryptor.MD5Hash(model.PassWord));
                if (result == 1)
                {
                    var user = UserDAO.getInstance().GetByUserName(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    userSession.Name = user.Name;
                    Session.Add(CommonStants.User_Session, userSession);
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if(user.Role == "Member")
                    {
                        Session[CommonStants.username_Member] = user.UserName;
                        Session[CommonStants.ID_Member] = user.ID;
                        return RedirectToAction("ProfileUser", "profile",new { id = user.ID});
                    }
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
            return View(model);
        }
        public ActionResult Logout()
        {
            Session[CommonStants.username_Member] = null;
            Session[CommonStants.ID_Member] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Login");
        }
    }
}