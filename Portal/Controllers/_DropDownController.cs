using System.Linq;
using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;

namespace Portal.Controllers
{
    public class _DropDownController : _BaseController
    {
        [HttpPost]
        public JsonResult CarregaModulos()
        {
            var lista = new GlobaisModulosBLL().Select();
            var result = (from x in lista select new GlobaisDropDown { Id = x.mod_id.ToString(), Nome = x.mod_nome });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CarregaClientes()
        {
            var lista = new GlobaisClienteBLL().Select();
            var result = (from x in lista select new GlobaisDropDown { Id = x.cli_id.ToString(), Nome = x.cli_nomeFantasia });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaPerfil()
        {
            var lista = new GlobaisPermissaoBLL().Select();
            var result = (from x in lista select new GlobaisDropDown { Id = x.perm_id.ToString(), Nome = x.perm_nome });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaTipoVencimento()
        {
            var lista = new GlobaisConfiguracaoBLL().SelectTipoVencimento();
            var result = (from x in lista select new GlobaisDropDown { Id = x.Con_IdItem.ToString(), Nome = x.Con_IdItem.ToString() + " | " + x.Con_Descricao });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaCodigoIBGE()
        {
            var lista = new GlobaisConfiguracaoBLL().SelectCodigoIBGE();
            var result = (from x in lista select new GlobaisDropDown { Id = x.Con_IdItem.ToString(), Nome = x.Con_IdItem.ToString() + " | " + x.Con_Descricao });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaFOP()
        {
            var lista = new GlobaisConfiguracaoBLL().SelectCFOP();
            var result = (from x in lista select new GlobaisDropDown { Id = x.Con_IdItem.ToString(), Nome = x.Con_IdItem.ToString() + " | " + x.Con_Descricao });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaTIPOEND()
        {
            var lista = new GlobaisConfiguracaoBLL().SelectTipoEndereco();
            var result = (from x in lista select new GlobaisDropDown { Id = x.Con_IdItem.ToString(), Nome = x.Con_IdItem.ToString() + " | " + x.Con_Descricao });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

       /* [HttpPost]
        public JsonResult CarregaServicos()
        {
            var lista = new ServicosBLL().Select(GlobalVariables.User_EmpresaSelecionar);
            var result = (from x in lista select new ModelDropDown { Id = x.serv_id.ToString(), Nome = x.serv_nome.ToString() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }*/

        #region Lista de Chamados
        /*[HttpPost]
        public JsonResult CarregaChamadoCategoria()
        {
            var lista = new CategoriaBLL().Select();
            var result = (from x in lista select new ModelDropDown { Id = x.cCa_id.ToString(), Nome = x.cCa_nome.ToString() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaChamadoSubCategoria(int id)
        {
            var lista = new CategoriaBLL().Select(new BE.Chamado.CategoriaBE { cCa_categoria = id });
            var result = (from x in lista select new ModelDropDown { Id = x.cCa_id.ToString(), Nome = x.cCa_nome.ToString() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaChamadoStatus()
        {
            var lista = new StatusBLL().Select();
            var result = (from x in lista select new ModelDropDown { Id = x.cSt_id.ToString(), Nome = x.cSt_nome.ToString() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregaChamadoPrioridade()
        {
            var lista = new PrioridadeBLL().Select();
            var result = (from x in lista select new ModelDropDown { Id = x.cPr_id.ToString(), Nome = x.cPr_nome.ToString() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }*/

        #endregion
    }
}
