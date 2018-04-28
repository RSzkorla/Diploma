using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diploma.Security
{
    public class AutorizationService : System.Web.Mvc.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["loginSuccess"] == null)
            {
                filterContext.Result = new System.Web.Mvc.RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller", "Home"},
                    {"Action", "Index"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}