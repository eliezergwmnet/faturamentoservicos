using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;
using RestSharp;

namespace Portal.Controllers
{
    [_Autentica]
    public class _BaseController : _MetodosGlobaisController
    {
        public JsonResult FileUpload(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/images/profile"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                using (MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }

            }
            // after successfully uploading redirect the user
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string Base64ToImage(string base64String, TipoImagem tipo)
        {
            var retorno = new ConverterImagem().Base64ToImage(base64String, tipo);
            return retorno;
        }
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
            return (int)HttpContext.Current.Session[Globais.Helper.Globais.SessionSistemaConfId];
        }
    }

    public static GlobaisEmpresaBE EmpresaSelecionada
    {
        get
        {
            return (GlobaisEmpresaBE)HttpContext.Current.Session[Globais.Helper.Globais.SessionSistemaEmpresaSelecionada];
        }
    }
}