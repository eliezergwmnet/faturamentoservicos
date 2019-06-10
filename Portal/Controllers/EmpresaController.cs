using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class EmpresaController : _BaseController
    {
        // GET: Empresa
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Empresas";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista de Empresas";
            var list = new GlobaisEmpresaBLL().Select();
            return View(list);
        }

        public ActionResult Detalhes(int id)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Empresas";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Detalhes Empresas";
            return View();
        }

        public ActionResult Insert()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Empresas";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Cadastro de Empresas";
            return View();
        }
        [HttpPost]
        public ActionResult Insert(GlobaisEmpresaBE _cliente, GlobaisEnderecoBE _endereco)
        {
            var listaClientes = new GlobaisEmpresaBLL().Insert(_cliente, _endereco);
            return Redirect("/Empresa");
        }
        public ActionResult Update(int id)
        {
            var empresa = new GlobaisEmpresaBLL().SelectId(new GlobaisEmpresaBE());
            ViewData["empresa"] = empresa;
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Empresa";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Dados da Empresa";
            return View();
        }
        public ActionResult UpdateEmpresa(GlobaisEmpresaBE obj)
        {
           /* if (obj.confCom_logo != null && obj.confCom_logo != "")
                obj.confCom_logo = new Globais.Helper.ConverterImagem().Base64ToImage(obj.confCom_logo, Globais.Helper.TipoImagem.Empresa);*/
            var emp = new GlobaisEmpresaBLL();
            emp.Update(obj);
            var empresa = emp.SelectId(new GlobaisEmpresaBE());
            ViewData["empresa"] = empresa;
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Empresa";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Dados da Empresa";

            return Redirect("/Empresa");
        }

        public JsonResult CadastroModuloEmpresa(int mod_id, int conf_id)
        {
            new GlobaisModulosEmpresaBLL().Insert(mod_id, conf_id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}