using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Security;

namespace CrediAdmin
{
    public class SessionExpireAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            // check  sessions here
            //if (Session["EmpresaId"] != null)
              //  Int32.TryParse(Session["EmpresaId"].ToString(), out empresaId);
            if (HttpContext.Current.Session["EmpresaId"] == null)
            {
                FormsAuthentication.SignOut();
                string redirectTo = "~/Account/Login";
                if (!string.IsNullOrEmpty(ctx.Request.RawUrl))
                {
                    redirectTo = string.Format("~/Account/Login?ReturnUrl={0}", HttpUtility.UrlEncode(ctx.Request.RawUrl));
                    filterContext.Result = new RedirectResult(redirectTo);
                    return;
                }
                
            }
            base.OnActionExecuting(filterContext);
        }
    }
}