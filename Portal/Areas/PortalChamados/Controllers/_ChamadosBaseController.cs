using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Globais.BE;
using Globais.Helper;

namespace PortalChamados.Controllers
{
    [_ChamadosAutentica]
    public class _ChamadosBaseController : Controller
    {
    }
}
public static class GlobalVariables
{
    public static GlobaisUsuarioBE User
    {
        get
        {
            return (GlobaisUsuarioBE)HttpContext.Current.Session[Globais.Helper.Globais.SessionSistema];
        }
    }
    public static int User_EmpresaSelecionar
    {
        get
        {
            return Convert.ToInt32(1);// HttpContext.Current.Session[Globais.SessionSistemaEmpresaSelecionada]);
        }
    }
}