using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;

namespace PortalChamados.Controllers
{
    public class HomeController : _ChamadosBaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Clientes";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Clientes";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}