using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGreenhouse.Areas.Admin.Code
{
    public class SessionHelper
    {
        public static void SetSession(UserSession userSession)
        {
            HttpContext.Current.Session["LoginSession"] = userSession;
        }
        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["LoginSession"];
            if(session == null)
            {
                return null;
            }
            else
            {
                return session as UserSession;
            }
        }
    }
}