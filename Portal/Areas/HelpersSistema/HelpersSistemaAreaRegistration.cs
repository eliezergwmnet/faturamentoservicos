using System.Web.Mvc;

namespace Portal.Areas.HelpersSistema
{
    public class HelpersSistemaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "HelpersSistema";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "HelpersSistema",
                url: "HelpersSistema/{controller}/{action}/{id}",
                defaults: new { controller = "HelpersSistema", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HelpersSistema.Controllers" }
            );
        }

    }
}