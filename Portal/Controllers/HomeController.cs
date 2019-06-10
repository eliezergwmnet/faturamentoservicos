using System;
using System.Web.Mvc;
using System.Web.Security;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class HomeController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Clientes";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Clientes";

            try
            {
                //var teste = new GlobaisClienteBLL().SelectNovoTeste();

                /*MembershipCreateStatus status;
                Membership.CreateUser("leandro.martins@gwmnet.net", "!1$@b3lly!", "leandro.martins@gwmnet.net", "Questão Password", "Resposta Password", true, out status);

                MembershipUser user = Membership.GetUser("leandro.martins@gwmnet.net");
                user.UnlockUser();
                Membership.UpdateUser(user);

                Membership.ValidateUser("leandro.martins@gwmnet.net", "!1$@b3lly!");


                var email = Membership.GetAllUsers();//.GetUserNameByEmail("leandro.martins@gwmnet.net");
                var userlogado = Membership.GetNumberOfUsersOnline();*/


            }
            catch (MembershipCreateUserException ex)
            {
                var teste = ex;
            }
            return View();
        }


       /* [HttpGet]
        public JsonResult FaturamentoAnual()
        {
            var result = new RelatorioHomeBLL().SelectFaturamentoPorAno();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult FaturamentoMes()
        {
            var result = new RelatorioHomeBLL().SelectFaturamentoPorMesAtual();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FaturamentoPagamento()
        {
            var pagPen = new RelatorioHomeBLL().SelectPagamentoPendentes();
            var pagAta = new RelatorioHomeBLL().SelectPagamentoAtrasados();
            var pagBax = new RelatorioHomeBLL().SelectPagamentosPagoMes();
            return Json(new { Pendente = pagPen, Baixados = pagBax, Atrasados = pagAta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SelecionarEmpresa(int id)
        {
            Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada] = id;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    */



    }
}