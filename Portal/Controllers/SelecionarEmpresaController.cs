/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Portal.BE;
using Portal.BLL;

namespace Portal.Controllers
{
    public class SelecionarEmpresaController : Controller
    {
        // GET: SelecionarEmpresa
        public ActionResult Index()
        {
            //'================================================================='
            //REMOVER ESSA PAGINA
            if (Session[Globais.Helper.Globais.SessionSistema] != null)
            {
                UsuarioBE user = (UsuarioBE)Session[Globais.Helper.Globais.SessionSistema];
                if(user.Empresas == null)
                    return new RedirectResult("/Login");
                else if(user.Empresas.Count == 1)
                {
                    Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada] = user.Empresas[0].useemp_empresa;
                    return new RedirectResult("/");
                }
                else
                {
                    List<EmpresaBE> emp = new List<EmpresaBE>();
                    var empresas = new EmpresaBLL();
                    foreach (var item in user.Empresas)
                    {
                        emp.Add(empresas.SelectId(new EmpresaBE {  }));
                    }
                    return View(emp);
                }
            }
            else
            {
                return new RedirectResult("/Login");
            }
        }

        public RedirectResult AtivaEmpresa(string id)
        {
            Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada] = Convert.ToInt32(id);
            return Redirect("/");
        }
        public RedirectResult DesativarEmpresa()
        {
            Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada] = null;
            return Redirect("/SelecionarEmpresa");
        }
    }
}*/