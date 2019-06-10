using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class PerfilController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Perfil";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Perfil";
            var listaPerfil = new GlobaisPermissaoBLL().Select(new GlobaisPermissaoBE { });
            var pagesSistema = new GlobaisConfiguracaoBLL().SelectPage("");

            ViewData["perfil"] = listaPerfil;
            ViewData["page"] = pagesSistema;

            return View();
        }

        public JsonResult InsertPerfil(int id, string nomeperfil)
        {
            if(id == 0)
                new GlobaisPermissaoBLL().Insert(new GlobaisPermissaoBE { perm_nome = nomeperfil });
            else
                new GlobaisPermissaoBLL().Update(new GlobaisPermissaoBE { perm_id = id, perm_nome = nomeperfil });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePeril(GlobaisPermissaoBE obj)
        {
            new GlobaisPermissaoBLL().Delete(obj);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CadastrarItem(TipoPagePermissao tipo, string page, int perfil, int idpage)
        {
            new GlobaisPermissaoPageBLL().Insert(tipo, page, perfil, idpage);
            return View();
        }
        public ActionResult Update()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }
    }
}