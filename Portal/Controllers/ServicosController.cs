/*using System.Collections.Generic;
using System.Web.Mvc;
using Portal.BE;
using Portal.BLL;

namespace Portal.Controllers
{
    public class ServicosController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Serviços";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Serviços";
            return View();
        }

        public PartialViewResult CarregaListaServicos(ServicosBE obj)
        {
            List<ServicosBE> listaServicos = new ServicosBLL().Select(obj, true);           
            return PartialView(listaServicos);
        }
        public ActionResult Insert()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Serviços";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Cadastrar Serviço";

            return View();
        }
        [HttpPost]
        public ActionResult Insert(ServicosBE obj)
        {
            obj.serv_nome = obj.serv_descricao;
            var servico = new ServicosBLL().Insert(obj);
            Response.Redirect("/Servicos");
            return View();
        }

        public ActionResult Update(int id)
        {
            var cliente = new ServicosBLL().SelectId(new ServicosBE { });
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Serviços";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Alterar " + cliente.serv_descricao;

            ViewData["servico"] = cliente;
            return View();
        }
        [HttpPost]
        public ActionResult Update(ServicosBE obj)
        {
            obj.serv_nome = obj.serv_descricao;
            var _cliente = new ServicosBLL().Update(obj);

            var cliente = new ServicosBLL().SelectId(new ServicosBE { serv_id = obj.serv_id });
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Serviços";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Alterar " + cliente.serv_descricao;

            ViewData["servico"] = cliente;
            Response.Redirect("/Servicos");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var cliente = new ServicosBLL().Delete(new ServicosBE { serv_id = id });
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AdicionarItem(ServicosBE obj)
        {
            if (obj.serv_id == 0)
                new ServicosBLL().Insert(obj);
            else
                new ServicosBLL().Update(obj);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletarItem(int id)
        {
            new ServicosBLL().Delete(new ServicosBE { serv_id = id });

            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}*/