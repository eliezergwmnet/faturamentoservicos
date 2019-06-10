using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            Session.RemoveAll();
            Session.Clear();
            Session.Abandon();
            HttpCookie cookieuser = new HttpCookie(Globais.Helper.Globais.NameCookie);
            cookieuser.Expires = DateTime.Now.AddDays(-1d);
            Response.Cookies.Add(cookieuser);
            return View();
        }

        [HttpGet]
        public ActionResult EsqueciSenha()
        {
            return View();
        }

        [HttpPost]
        public JsonResult EsqueciSenhaEmail(string emailsenha)
        {
            var retorno = new GlobaisEmailBLL().EsqueciSenha(emailsenha);
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RecuperarSenha(string userautenticationvalue)
        {
            var user = new GlobaisEmailBLL().ValidaTrocaSenha(userautenticationvalue);
            if (user == null)
                ViewBag.Tipo = 1;
            else
                ViewBag.Tipo = 2;
            ViewBag.User = user;
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarSenha(string userautenticationvalue, string novasenha, string confirmarsenha)
        {
            var user = new GlobaisEmailBLL().ValidaTrocaSenha(userautenticationvalue);
            if (user != null)
            {
                ViewBag.User = user;
                if (novasenha.Length < 5)
                    ViewBag.Tipo = 3;
                else
                {
                    if (novasenha != confirmarsenha)
                        ViewBag.Tipo = 4;
                    else
                    {

                        //new GlobaisUsuarioBLL().SenhaAlterar(user.usu_id, GerarChave.HashValue(novasenha), GerarChave.HashValue(novasenha));
                        ViewBag.Tipo = 5;
                    }
                }
            }
            else
                Response.Redirect("~/Login");
           
            return View();
        }


        [HttpPost]
        public ActionResult Index(string txtLogin, string txtSenha, string cbolembrarsenha)
        {
            try
            {
                GlobaisUsuarioBLL _login = new GlobaisUsuarioBLL();
                var _user = _login.Login(new GlobaisUsuarioBE { usu_email = txtLogin, usu_senha = Common.CriptografiaSenha(txtSenha) , log_ativo = true });

                if (_user != null)
                {
                    if (cbolembrarsenha == "on")
                    {
                        if (Globais.Helper.Globais.CoolieAtivo)
                        {
                            HttpCookie cookieuser = new HttpCookie(Globais.Helper.Globais.NameCookie);
                            cookieuser.Value = _user.usu_id.ToString();
                            cookieuser.Expires = DateTime.Now.AddDays(Globais.Helper.Globais.CookieDias);
                            Response.Cookies.Add(cookieuser);
                        }
                    }

                    Session[Globais.Helper.Globais.SessionSistema] = _user;
                    Session[Globais.Helper.Globais.SessionSistemaConfId] = _user.conf_id;
                    Session[Globais.Helper.Globais.SessionSistemaPermissao] = _user.Permissao;
                    Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada] = new GlobaisEmpresaBLL().SelectId(new GlobaisEmpresaBE { conf_id = _user.conf_id });
                    return new RedirectResult("/");
                    /*if(_user.Empresas == null)
                        return new RedirectResult("/Login/Index?company=true");
                    else if(_user.Empresas.Count > 1)
                        return new RedirectResult("/SelecionarEmpresa");
                    else
                    {
                        Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada] = _user.Empresas[0].useemp_empresa;
                        return new RedirectResult("/");
                    }*/
                }
                else
                    return new RedirectResult("/Login/Index?error=true");
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return View();
            }
        }

        /*public ActionResult GerarNotasPDF(int id)
        {
            var clientes = new ClienteBLL().SelectBuscaNotasLista("", "", "", GlobalVariables.User_EmpresaSelecionar, id, true);

            var empresa = new EmpresaBLL().SelectId();
            ViewData["empresa"] = empresa;

            return View("/Views/Faturamento/GerarNotasPDF.cshtml", clientes);
        }*/

     /*   public ActionResult GerarBoletoPDF(int id)
        {
            var clientes = new ClienteBLL().SelectBuscaNotasLista("", "", "", GlobalVariables.User_EmpresaSelecionar, id, true);
            var boleto = new Models.GerarBoleto(33).Santander(clientes[0]);

            ViewBag.Boleto = boleto.MontaHtmlEmbedded();

            return View("/Views/Faturamento/GerarBoletoPDF.cshtml");
        }*/


       /* public ActionResult GerarRelatorioFaturamentoPDF(string fat_mes)
        {
            List<NotasClientesEmitidasMes> listaClientes = new NotasBLL().SelecionarNotasEmitidasMes(fat_mes);
            return PartialView("/Views/Faturamento/GerarRelatorioFaturamentoPDF.cshtml", listaClientes);
        }
        public ActionResult GerarRelatorioFaturamentoMesPDF(string fat_mes)
        {
            List<NotasClientesEmitidasMes> listaClientes = new NotasBLL().SelecionarNotasEmitidasMes(fat_mes);
            return PartialView("/Views/Faturamento/GerarRelatorioFaturamentoMesPDF.cshtml", listaClientes);
        }*/

        public ActionResult HTML()
        {
            return View("~/Views/Home/HTML.cshtml");
        }
        [HttpGet]
        public JsonResult TesteAPI()
        {
            var client = new RestSharp.RestClient("https://api-movida.loyaltysci.com/loyalty-api-external/webresources/v0/member/by-email/iceman2@example.com");
            var request = new RestSharp.RestRequest(RestSharp.Method.GET);
            request.AddHeader("Postman-Token", "1032995c-606a-42ed-a687-1f3e50c87e60");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddHeader("authorization", "bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJIdktRbzJRaGg5cWQ2TWVmaHp4UWw1d1FTdEtaVlFWZGlhS0RnRC1GekswIn0.eyJqdGkiOiIxZWYzYTU3YS00MzE2LTQxYWYtYWQ3ZS0yY2RkMDZjNzc5MjMiLCJleHAiOjE1Mzc0MTM0NDgsIm5iZiI6MCwiaWF0IjoxNTM3Mzc3NDQ4LCJpc3MiOiJodHRwczovL2lkcC1tb3ZpZGEubG95YWx0eXNjaS5jb20vYXV0aC9yZWFsbXMvTG95YWx0eS1leHRlcm5vIiwiYXVkIjoibG95YWx0eS1hcGktZXh0ZXJuYWwiLCJzdWIiOiJhYWRkNDc4My0xM2FiLTRkOTgtYWJmOS0xOTg2Yzc0MzQzYzIiLCJ0eXAiOiJCZWFyZXIiLCJhenAiOiJsb3lhbHR5LWFwaS1leHRlcm5hbCIsImF1dGhfdGltZSI6MCwic2Vzc2lvbl9zdGF0ZSI6ImUzNTY1ZWUwLWU4YjYtNDdiMy05MTZmLTA0MGE3NzM4OTE0MiIsImFjciI6IjEiLCJhbGxvd2VkLW9yaWdpbnMiOlsiKiJdLCJyZWFsbV9hY2Nlc3MiOnsicm9sZXMiOlsiY2xpZW50IiwidW1hX2F1dGhvcml6YXRpb24iXX0sInJlc291cmNlX2FjY2VzcyI6eyJyZWFsbS1tYW5hZ2VtZW50Ijp7InJvbGVzIjpbInZpZXctaWRlbnRpdHktcHJvdmlkZXJzIiwidmlldy1yZWFsbSIsIm1hbmFnZS1pZGVudGl0eS1wcm92aWRlcnMiLCJpbXBlcnNvbmF0aW9uIiwicmVhbG0tYWRtaW4iLCJjcmVhdGUtY2xpZW50IiwibWFuYWdlLXVzZXJzIiwicXVlcnktcmVhbG1zIiwidmlldy1hdXRob3JpemF0aW9uIiwicXVlcnktY2xpZW50cyIsInF1ZXJ5LXVzZXJzIiwibWFuYWdlLWV2ZW50cyIsIm1hbmFnZS1yZWFsbSIsInZpZXctZXZlbnRzIiwidmlldy11c2VycyIsInZpZXctY2xpZW50cyIsIm1hbmFnZS1hdXRob3JpemF0aW9uIiwibWFuYWdlLWNsaWVudHMiLCJxdWVyeS1ncm91cHMiXX0sImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJ2aWV3LXByb2ZpbGUiXX19LCJjbGllbnRIb3N0IjoiMTg5LjIwMS4xOTYuMTQ1IiwiY2xpZW50SWQiOiJsb3lhbHR5LWFwaS1leHRlcm5hbCIsInByZWZlcnJlZF91c2VybmFtZSI6InNlcnZpY2UtYWNjb3VudC1sb3lhbHR5LWFwaS1leHRlcm5hbCIsImNsaWVudEFkZHJlc3MiOiIxODkuMjAxLjE5Ni4xNDUiLCJlbWFpbCI6InNlcnZpY2UtYWNjb3VudC1sb3lhbHR5LWFwaS1leHRlcm5hbEBwbGFjZWhvbGRlci5vcmcifQ.BVEf7Ve2zODBgEUyjWZg4PSefhNNJfowUM8-WkEPAnT_YRY7tKQkqRkVqpjW5eKHSzERBeWJeLhwNw6wmP34Cj23u_kQkjEX7rzKVhvCXUjuMsSY_Lny61npmmd0suqAXT5Jin03_h4ab7XvOtjb-2_LY4bRG1ZuPCuAei1wcWJ0hoCk3rD3-1sCbc1Wl_4b0DorygxbkWjbX-Nmrb5VXMtayHrdZazfk9pjocGzb7-0hZY8mCYrr3kErwd6EgNGAJm6l3PJLovAB1J4wYxgnK0btzYREXroAiryt59hvCHoK2AXKsFftgk5uvxPKG-HfnC-KG4VoT9tlcNTCROk8w");
            RestSharp.IRestResponse response = client.Execute(request);

            return Json(response.Content, JsonRequestBehavior.AllowGet);


        }

        [HttpGet]
        public ActionResult EmailConfirmacaoNotas(int total, int erro, string valortotal, string tempo)
        {
            return View();
        }

        public ActionResult Teste()
        {
            return View();
        }

    }
}