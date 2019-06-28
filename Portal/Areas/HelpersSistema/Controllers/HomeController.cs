using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;
using HelpersSistema.BE;
using HelpersSistema.Models;
using HelpersSistema.Models.Base;

namespace HelpersSistema.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Clientes";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Clientes";

            var conections = ConfigurationManager.GetSection("connectionStrings");
            List<ConectionsStringSitemaBE> dados = new List<ConectionsStringSitemaBE>();

            foreach (var item in ((System.Configuration.ConnectionStringsSection)conections).ConnectionStrings)
                dados.Add(new ConectionsStringSitemaBE { Nome = ((System.Configuration.ConnectionStringSettings)item).Name, Conexao = item.ToString() });

            return View(dados);
        }

        public JsonResult CarregarTabelas(string conection)
        {
            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            var lista = new CriarProcedures("", null).ListaTodasTabelas();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarProcedure(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar Procedure";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();

            var variaveis = new CriarProcedures(Nome, colunas).layoutProcedure();
            var variaveis2 = new CriarProcedures(Nome, colunas).layoutVariaveis();
            var variaveis3 = new CriarProcedures(Nome, colunas).ProcSelect();
            var variaveis4 = new CriarProcedures(Nome, colunas).ProcInsert();
            var variaveis5 = new CriarProcedures(Nome, colunas).ProcUpdate();
            var variaveis6 = new CriarProcedures(Nome, colunas).ProcDelete();
            variaveis = variaveis.Replace("@queryvariaveis", variaveis2).Replace("@nomeprocedure", Nome.Replace("tbl", "proc_")).Replace("@queryselect", variaveis3).Replace("@queryinsert", variaveis4).Replace("@queryupdate", variaveis5).Replace("@querydelete", variaveis6);
            ViewBag.Dados = variaveis;

            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(variaveis, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarTrigger(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar Trigger";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            string retorno = new CriarTrigger().CriarTriggerTable(Nome, colunas[0].Coluna);

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarBE(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar Classe";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            var retorno = new CriarClasses(Nome, colunas).CriarClasse(Nome);

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarDao(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar Dao";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            var retorno = new CriarDao().CriarDaoTable(Nome, colunas);

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarBLL(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar BLL";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            var retorno = new CriarBLL().CriarClasseBLL(Nome, colunas);

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult CriarLista(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar Tela Lista Dados";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            var retorno = new CriarHTML(Nome, Nome, colunas).CriarIndex();

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarModal(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar Modal Cadastro";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            var retorno = new CriarHTML(Nome, Nome, colunas).CriarInsert();

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarJS(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar JQuery";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            var retorno = new CriarHTML(Nome, Nome, colunas).CriarUpdate();

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CriarController(string conection, string Nome)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Criar Controller";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Tabela: " + Nome;

            Base.ConnectionString = ConfigurationManager.ConnectionStrings[conection].ConnectionString;
            List<Table> colunas = new CriarProcedures(Nome).CarregaColunas();
            var retorno = new CriarControler().CriarClasseController(Nome, colunas);

            ViewBag.Dados = retorno;
            return View("~/Areas/HelpersSistema/Views/Home/MostrarDados.cshtml");
            //return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}