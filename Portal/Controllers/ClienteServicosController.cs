/*using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Portal.BE;
using Portal.BLL;

namespace Portal.Controllers
{
    public class ClienteServicosController : _BaseController
    {
        public ActionResult Index()
        {
            if (Request["cli_id"] != null && Request["cli_id"].ToString() != "0")
            {
                int idCliente = Convert.ToInt32(Request["cli_id"]);
                ViewData["Cliente"] = new ClienteBLL().SelectId(idCliente);

                if (Request["contrato"] != null && Request["contrato"].ToString() != "0" && Request["contrato"].ToString() != "")
                    ViewData["servicos"] = new ClienteServicosBLL().Select(new ClienteServicosBE { cli_id = idCliente, servCli_contrato = Convert.ToInt32(Request["contrato"]), log_ativo = true });
                else
                    ViewData["servicos"] = new List<ClienteServicosBE>();
            }
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Adicionar Serviços";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Serviços Cliente";

            return View();
        }

        [HttpPost]
        public JsonResult Insert(ClienteServicosBE obj)
        {
            var servico = new ClienteServicosBLL().Insert(obj, GlobalVariables.User_EmpresaSelecionar);
            return Json(servico, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CarregarServico(int id)
        {
            var servico = new ServicosBLL().SelectId(new ServicosBE { serv_id = id});
            return Json(servico, JsonRequestBehavior.AllowGet);
        }
    }
}*/