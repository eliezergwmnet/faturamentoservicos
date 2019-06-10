/*using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.Helper;
using Portal.BE;
using Portal.BE.Chamado;
using Portal.BLL;
using Portal.BLL.Chamado;
using Portal.Models;

namespace Portal.Controllers.Chamado
{
    public class TicketController : _BaseController
    {
        // GET: Ticket
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Ticket";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Ticket";

            return View("~/Views/Chamado/Chamado/Index.cshtml");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Ticket";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Cadastro Ticket";

            var chamado = new ChamadoBLL().SelectId(new ChamadoBE { cOs_id = id, cOs_ativo = true });
            return View("~/Views/Chamado/Chamado/Update.cshtml", chamado);
        }
        [HttpPost]
        public JsonResult InserirMensagem(string objArquivo, int cOs_id, string cOsMes_mensagem)
        {
            new MensagemBLL().Insert(new MensagemBE { cOs_id = cOs_id, cOsMes_mensagem = cOsMes_mensagem });
            return Json("", JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult CarregaListaTicket(ChamadoBE obj)
        {
            obj.cOs_ativo = true;
            var ticket = new ChamadoBLL().Select(obj, true);
            return PartialView("~/Views/Chamado/Chamado/CarregaList.cshtml", ticket);
        }

        public ActionResult Insert()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Ticket";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Cadastro Ticket";

            ViewBag.ID = Common.NumChamado();
            return View("~/Views/Chamado/Chamado/Insert.cshtml");
        }

        [HttpPost]
        //public ActionResult Insert(ChamadoBE obj,MensagemBE objMensagem, string objArquivo)
        public JsonResult SalvarDados(string objArquivo, string cOs_numero, string cli_id, string cPr_id, string cCa_id, string cOs_titulo, string cOsMes_mensagem)
        {
            ChamadoBE _cham = new ChamadoBE { cOs_numero = cOs_numero, cli_id = Convert.ToInt32(cli_id), cPr_id = Convert.ToInt32(cPr_id), cCa_id = Convert.ToInt32(cCa_id), cOs_titulo = cOs_titulo, usu_id = GlobalVariables.User.usu_id };
            List<ArquivosBE> arq = new List<ArquivosBE>();
            if (!string.IsNullOrEmpty(objArquivo))
            {
                foreach (var item in objArquivo.Split(','))
                    arq.Add(new ArquivosBE { cOsAr_nome = item, cOsAr_arquivo = item });
            }
            var cha = new ChamadoBLL();
            _cham = cha.Insert(_cham, cOsMes_mensagem, arq);
            _cham = cha.SelectId(new ChamadoBE { cOs_id = _cham.cOs_id, cOs_ativo=true });
            new ChamadoEnviarEmail(_cham.cOs_id, _cham).EnviarTicketEmail();
            return Json("", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            string filePath = "";
            foreach (var file in files)
            {
                if (!Directory.Exists(Server.MapPath("~/TicketUploads")))
                    Directory.CreateDirectory(Server.MapPath("~/TicketUploads"));

                filePath = Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath("~/TicketUploads"), filePath));
                //Here you can write code for save this information in your database if you want
            }
            return Json(filePath, JsonRequestBehavior.AllowGet);
        }
    }
}*/