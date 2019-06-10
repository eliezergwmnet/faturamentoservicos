using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;

namespace Portal.Controllers
{
    public class ErrorController : _BaseController
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PagaNaoEncotrada()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Error";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Pagina Não Encontrada";
            return View();
        }
        public ActionResult PagaError()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Error";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Erro ao Carregar Pagina";
            return View();
        }
    }
}