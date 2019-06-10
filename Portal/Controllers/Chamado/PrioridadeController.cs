/*using System.Web.Mvc;
using Portal.BE.Chamado;
using Portal.BLL.Chamado;

namespace Portal.Controllers.Chamado
{
    public class PrioridadeController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Prioridade";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Prioridade";

            var listaClientes = new PrioridadeBLL().Select();
            return View("/Views/Chamado/Prioridade/Index.cshtml", listaClientes);
        }
        [HttpPost]
        public JsonResult Insert(PrioridadeBE _obj)
        {
            if (_obj.cPr_id == 0)
            {
                var lista = new PrioridadeBLL().Insert(_obj);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lista = new PrioridadeBLL().Update(_obj);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(PrioridadeBE _obj)
        {
            new PrioridadeBLL().Delete(_obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}*/