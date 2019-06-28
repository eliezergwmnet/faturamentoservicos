using System.Collections.Generic;
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

            
            foreach (var item in GlobalVariables.EmpresaSelecionada.Modulos)
            {
                foreach (var itemPage in item.Paginas)
                {
                    pagesSistema.Add(new GlobaisConfiguracaoBE { Con_IdItem = itemPage.modPage_url, Con_Descricao = itemPage.modPage_nome, Con_Tipo = "PAG", conf_id = GlobalVariables.User_EmpresaSelecionar });
                }
            }

            ViewData["perfil"] = listaPerfil;
            ViewData["page"] = pagesSistema;

            return View();
        }

        public JsonResult InsertPerfil(GlobaisPermissaoBE obj)
        {
            if(obj.perm_id == 0)
                new GlobaisPermissaoBLL().Insert(obj);
            else
                new GlobaisPermissaoBLL().Update(obj);
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