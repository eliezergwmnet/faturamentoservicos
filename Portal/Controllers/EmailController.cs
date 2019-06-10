using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;

namespace Portal.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            var teste = TipoEmail.NotasFiscal.GetDescription();
            return View();
        }
    }
}

public class Html
{
    [AllowHtml]
    public string html { get; set; }
}