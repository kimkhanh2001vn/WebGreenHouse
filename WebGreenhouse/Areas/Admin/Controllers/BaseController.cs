using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebGreenhouse.Common;

namespace WebGreenhouse.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var session = (UserLogin)Session[CommonStants.User_Session]; 
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                                new
                                {
                                    controller = "Login",
                                    action = "Login"
                                }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}