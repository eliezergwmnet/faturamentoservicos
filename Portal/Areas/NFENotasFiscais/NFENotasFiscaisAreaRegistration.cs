using System.Web.Mvc;

namespace Portal.Areas.NFENotasFiscais
{
    public class NFENotasFiscaisAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "NFENotasFiscais";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "NFENotasFiscais",
                url: "NFENotasFiscais/{controller}/{action}/{id}",
                defaults: new { controller = "NFENotasFiscais", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NFENotasFiscais.Controllers" }
            );
        }
    }
}