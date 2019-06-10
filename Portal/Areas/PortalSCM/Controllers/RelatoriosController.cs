using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;

namespace PortalSCM.Controllers
{
    public class RelatoriosController : _SCMBaseController
    {
        // GET: Relatorios
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Relatórios";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista de Relatórios";

            return View();
        }
    }
}