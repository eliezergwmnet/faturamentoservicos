using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.BE;

namespace NFENotasFiscais.Controllers
{
    [_NFEAutentica]
    public class _NFEBaseController : Controller
    {
    }
    
}
/*
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
        return (int)HttpContext.Current.Session[Globais.Helper.Globais.SessionSistemaConfId];
    }
}

public static GlobaisEmpresaBE EmpresaSelecionada
{
    get
    {
        return (GlobaisEmpresaBE)HttpContext.Current.Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada];
    }
}*/