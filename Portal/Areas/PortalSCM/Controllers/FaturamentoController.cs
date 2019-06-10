using System;
using System.Web.Mvc;
using Globais.BLL;
using Globais.Helper;
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

            BackConsultaLote consultaRPS = new BackConsultaLote(29);
            System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => consultaRPS.IniciarConsultar(cancellationToken));


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
                    BackConsultaLote consultaRPS = new BackConsultaLote(faturamento);
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