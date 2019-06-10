using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Servicos
{
    public class ServicosAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PortalChamados";
            }
        }
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "Servicos",
                url: "Servicos/{controller}/{action}/{id}",
                defaults: new { controller = "Servicos", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Servicos.Controllers" }
            );
        }
    }
}