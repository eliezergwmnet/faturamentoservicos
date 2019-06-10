using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;
using PortalSCM.BE;
using PortalSCM.BLL;

namespace PortalSCM.Controllers
{
    public class ServicosController : _SCMBaseController
    {
        // GET: Servicos
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Serviços";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Serviços";
            var lista = new SCMServicosBLL().Select();
            return View(lista);
        }

        public JsonResult SelectId(SCMServicosBE obj)
        {
            return Json(new SCMServicosBLL().SelectID(obj) , JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cadastrar(SCMServicosBE obj)
        {
            var serv = new SCMServicosBLL();

            if (obj.ser_id == 0)
            {
                obj.cat_id = TipoServicosCadastrados.Sistema_SCM.GetDescription();
                obj.conf_id = (int)Session[Globais.Helper.Globais.SessionSistemaConfId];

                serv.Insert(obj);
            }
            else
                serv.Update(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Deletar(SCMServicosBE obj)
        {
            var serv = new SCMServicosBLL();
            serv.Delete(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}