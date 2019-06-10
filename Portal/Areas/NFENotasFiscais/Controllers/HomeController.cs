using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;
using NFENotasFiscais.BE;
using NFENotasFiscais.BLL;
using NFENotasFiscais.BLL.NFEServicos;

namespace NFENotasFiscais.Controllers
{
    public class HomeController : _NFEBaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Notas";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Notas Fiscais";

            var lista = new NTNotasBLL().Select();

            /*var nota = new NFEEnviarNota();
            nota.EnviarRPSNotas();
            nota.ConsultarRPSNotas();*/
            return View(lista);
        }

        public JsonResult SelectId(NTNotasBE obj)
        {
            return Json(new NTNotasBLL().SelectId(obj), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cadastrar(NTNotasBE obj)
        {
            var nt = new NTNotasBLL();

            if (obj.cont_id == 0)
            {
                obj.conf_id = (int)Session[Globais.Helper.Globais.SessionSistemaConfId];
                obj = nt.Insert(obj);
            }
            else
                nt.Update(obj);
            return Json(obj.cont_id, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Deletar(NTNotasBE obj)
        {
            var serv = new NTNotasBLL();
            serv.Delete(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}