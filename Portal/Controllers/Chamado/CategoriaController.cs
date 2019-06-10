/*using System.Web.Mvc;
using Portal.BE.Chamado;
using Portal.BLL.Chamado;

namespace Portal.Controllers.Chamado
{
    public class CategoriaController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Categoria";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Categoria";

            var listaClientes = new CategoriaBLL().Select();
            return View("/Views/Chamado/Categoria/Index.cshtml", listaClientes);
        }
        [HttpPost]
        public JsonResult Insert(CategoriaBE _obj)
        {
            if (_obj.cCa_id == 0)
            {
                var lista = new CategoriaBLL().Insert(_obj);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lista = new CategoriaBLL().Update(_obj);
                return Json(lista, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Delete(CategoriaBE _obj)
        {
            new CategoriaBLL().Delete(_obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}*/