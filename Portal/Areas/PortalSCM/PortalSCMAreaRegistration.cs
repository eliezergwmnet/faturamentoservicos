using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalSCM
{
    public class PortalSCMAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PortalSCM";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "SCM",
                url: "SCM/{controller}/{action}/{id}",
                defaults: new { controller = "SCM", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PortalSCM.Controllers" }
            );
        }
    }
}