using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class ConfiguracaoController : _BaseController
    {
        public ActionResult Index(string tipo)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Configuração";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Configuração";// tipo;
            ViewBag.tipo = tipo;
            return View();
        }
    }
}
        /*
        public PartialViewResult CarregaLista(string tipo, string descricao)
        {
            List<GlobaisConfiguracaoBE> listaConfiguracao = new List<GlobaisConfiguracaoBE>();
            if (tipo == "TPVenc")
                listaConfiguracao = new GlobaisConfiguracaoBLL().SelectTipoVencimento(descricao);
            else if (tipo == "IBGE")
                listaConfiguracao = new GlobaisConfiguracaoBLL().SelectCodigoIBGE(descricao);
            else if (tipo == "CFOP")
                listaConfiguracao = new GlobaisConfiguracaoBLL().SelectCFOP(descricao);
            else if (tipo == "PAGE")
                listaConfiguracao = new GlobaisConfiguracaoBLL().SelectPage(descricao);
            else if (tipo == "TIPEND")
                listaConfiguracao = new GlobaisConfiguracaoBLL().SelectTipoEndereco(descricao);
            else if (tipo == "INSTBOL")
                listaConfiguracao = new GlobaisConfiguracaoBLL().SelectInstrucaoBoleto(descricao);

            return PartialView(listaConfiguracao);
        }

        public ActionResult Insert()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Clientes";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Cadastrar Cliente";

            return View();
        }
        [HttpPost]
        public JsonResult Insert(GlobaisConfiguracaoBE obj)
        {
            var result = new GlobaisConfiguracaoBLL().Insert(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Update(GlobaisConfiguracaoBE obj)
        {
            var result = new GlobaisConfiguracaoBLL().Update(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(GlobaisConfiguracaoBE obj)
        {
            var result = new GlobaisConfiguracaoBLL().Delete(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}*/