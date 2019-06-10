using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Globais.BLL;
using Globais.BE;
using System.Web.Security;

namespace Portal.Controllers
{
    public class _Autentica : ActionFilterAttribute
    {
        ActionExecutingContext context;
        public override void OnActionExecuting(ActionExecutingContext _context)
        {
            context = _context;
            if (context.HttpContext.Session[Globais.Helper.Globais.SessionSistema] == null)
            {
                

                try
                {
                    var cookie = HttpContext.Current.Request.Cookies[Globais.Helper.Globais.NameCookie].Value;
                    if (cookie == null)
                    {
                        HttpContext.Current.Response.Redirect("~/Login");
                    }
                    else
                    {
                        var _user = new GlobaisUsuarioBLL().SelectId(Convert.ToInt32(cookie));
                        
                        context.HttpContext.Session[Globais.Helper.Globais.SessionSistema] = _user;
                        context.HttpContext.Session[Globais.Helper.Globais.SessionSistemaConfId] = _user.conf_id;
                        context.HttpContext.Session[Globais.Helper.Globais.SessionSistemaPermissao] = _user.Permissao;
                        context.HttpContext.Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada] = new GlobaisEmpresaBLL().SelectId(new GlobaisEmpresaBE { conf_id = _user.conf_id });
                        this.ValidaUsuario();
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Response.Redirect("~/Login");
                   /* var _user = new GlobaisUsuarioBLL().SelectId(4);

                    context.HttpContext.Session[Globais.Helper.Globais.SessionSistema] = _user;
                    context.HttpContext.Session[Globais.Helper.Globais.SessionSistemaConfId] = _user.conf_id;
                    context.HttpContext.Session[Globais.Helper.Globais.SessionSistemaPermissao] = _user.Permissao;
                    this.ValidaUsuario();*/
                }
            }
            else
                ValidaUsuario();
        }

        void ValidaUsuario()
        {
            if (context.HttpContext.Request.Url.LocalPath != "/")
            {
                var user = (GlobaisUsuarioBE)HttpContext.Current.Session[Globais.Helper.Globais.SessionSistema];
                //var page = (from x in user.Permissao.Paginas where context.HttpContext.Request.Url.LocalPath.Contains(x.permPag_url) select x).Count();
               /* if (page == 0)
                    HttpContext.Current.Response.Redirect("~/");*/
            }

        }
    }
}