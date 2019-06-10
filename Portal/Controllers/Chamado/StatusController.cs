/*using System.Web.Mvc;


namespace Portal.Controllers.Chamado
{
    public class StatusController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Status";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Status";

            var listaClientes = new StatusBLL().Select();
            return View("/Views/Chamado/Status/Index.cshtml", listaClientes);
        }
        [HttpPost]
        public JsonResult Insert(StatusBE _obj)
        {
            if (_obj.cSt_id == 0)
            {
                var lista = new StatusBLL().Insert(_obj);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lista = new StatusBLL().Update(_obj);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(StatusBE _obj)
        {
            new StatusBLL().Delete(_obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}*/