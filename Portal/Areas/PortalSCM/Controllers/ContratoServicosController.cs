using System.Web.Mvc;
using Globais.Helper;
using PortalSCM.BE;
using PortalSCM.BLL;

namespace PortalSCM.Controllers
{
    public class ContratoServicosController : _SCMBaseController
    {
        public ActionResult Index(int contrato)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Cliente Serviços";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Serviços Cliente";

            var cont = new SCMContratoBLL().SelectID(new SCMContratoBE { cont_id = contrato });
            return View(cont);
        }

        public JsonResult SelectId(SCMClienteServicosBE obj)
        {
            return Json(new SCMClienteServicosBLL().SelectID(obj), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cadastrar(SCMClienteServicosBE obj)
        {
            var serv = new SCMClienteServicosBLL();

            if (obj.servCli_id == 0)
            {
                serv.Insert(obj);
            }
            else
                serv.Update(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Deletar(SCMClienteServicosBE obj)
        {
            var serv = new SCMClienteServicosBLL();
            serv.Delete(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}