using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Globais.BLL;
using Globais.Helper;
using NFENotasFiscais.BE;
using NFENotasFiscais.BE.NFEServicos;
using NFENotasFiscais.BLL.NFEServicos;
using PortalSCM.BLL;

namespace PortalSCM.Controllers
{
    public class FaturamentoController : _SCMBaseController
    {
        // GET: Faturamento
        public ActionResult Index(bool avulso = true)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Faturamento";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Faturar Serviços Mês";

            var faturamento  = new SCMFaturamentoMensalBLL(Globais.Helper.Common.GetEmpresaSelecionada()).CalcularFaturamento();
            return View(faturamento);
        }

        public JsonResult SarvarFaturamento(string contratos)
        {
            try
            {
                var faturamento = new SCMFaturamentoMensalBLL(Globais.Helper.Common.GetEmpresaSelecionada()).CalcularFaturamento(contratos);
                if (faturamento == 0)
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    NFEConsultarNotaStatus consultaRPS = new NFEConsultarNotaStatus(faturamento);
                    System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => consultaRPS.IniciarConsultar(cancellationToken));
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}