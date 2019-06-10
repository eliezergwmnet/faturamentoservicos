using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalChamados
{
    public class PortalChamadosAreaRegistration : AreaRegistration
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
                name: "Chamados",
                url: "Chamados/{controller}/{action}/{id}",
                defaults: new { controller = "Chamados", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PortalChamados.Controllers" }
            );
        }
    }
}