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
    public class ConfiguracaoItensController : Controller
    {
        // GET: ConfiguracaoItens
        public ActionResult Index(TipoConfiguracao tipo, string descricao)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Configuração";
            ViewBag.tipo = tipo;

            List<GlobaisConfiguracaoBE> listaConfiguracao = new List<GlobaisConfiguracaoBE>();

            switch (tipo)
            {
                case TipoConfiguracao.TPVenc:
                    listaConfiguracao = new GlobaisConfiguracaoBLL().SelectTipoVencimento(descricao);
                    ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = TipoConfiguracao.TPVenc.GetDescription();
                    break;

                case TipoConfiguracao.IBGE:
                    listaConfiguracao = new GlobaisConfiguracaoBLL().SelectCodigoIBGE(descricao);
                    ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = TipoConfiguracao.IBGE.GetDescription();
                    break;

                case TipoConfiguracao.CFOP:
                    listaConfiguracao = new GlobaisConfiguracaoBLL().SelectCFOP(descricao);
                    ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = TipoConfiguracao.CFOP.GetDescription();
                    break;

                case TipoConfiguracao.PAGE:
                    listaConfiguracao = new GlobaisConfiguracaoBLL().SelectPage(descricao);
                    ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = TipoConfiguracao.PAGE.GetDescription();
                    break;

                case TipoConfiguracao.TIPEND:
                    listaConfiguracao = new GlobaisConfiguracaoBLL().SelectTipoEndereco(descricao);
                    ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = TipoConfiguracao.TIPEND.GetDescription();
                    break;

                case TipoConfiguracao.INSTBOL:
                    listaConfiguracao = new GlobaisConfiguracaoBLL().SelectInstrucaoBoleto(descricao);
                    ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = TipoConfiguracao.INSTBOL.GetDescription();
                    break;
            }

            return View(listaConfiguracao);
        }

        public JsonResult SelectId(int id)
        {
            var obj = new GlobaisConfiguracaoBE();
            obj.Con_id = id;
            return Json(new GlobaisConfiguracaoBLL().SelectId(obj), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Insert(GlobaisConfiguracaoBE obj)
        {
            if (obj.Con_id == 0)
                new GlobaisConfiguracaoBLL().Insert(obj);
            else
                new GlobaisConfiguracaoBLL().Update(obj);

            return Json(1, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(GlobaisConfiguracaoBE obj)
        {
            var result = new GlobaisConfiguracaoBLL().Delete(obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}