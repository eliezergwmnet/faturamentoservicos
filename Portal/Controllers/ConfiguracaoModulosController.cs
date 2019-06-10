using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class ConfiguracaoModulosController : _BaseController
    {
        // GET: ConfiguracaoModulos
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Modulos";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Modulos";
            return View(new GlobaisModulosBLL().Select());
        }

        public JsonResult SelectId(int id)
        {
            return Json(new GlobaisModulosBLL().SelectId(new GlobaisModulosBE { mod_id = id }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Cadastro(GlobaisModulosBE obj)
        {
            try
            {
                if (obj.mod_id == 0)
                    new GlobaisModulosBLL().Insert(obj);
                else
                    new GlobaisModulosBLL().Update(obj);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Deletar(GlobaisModulosBE obj)
        {
            new GlobaisModulosBLL().Delete(obj);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CadastroPage(GlobaisModulosPagesBE obj)
        {
            try
            {
                if (obj.modPage_id == 0)
                    new GlobaisModulosPagesBLL().Insert(obj);
                else
                    new GlobaisModulosPagesBLL().Update(obj);

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeletarPagina(GlobaisModulosPagesBE obj)
        {
            try
            {
                new GlobaisModulosPagesBLL().Delete(obj);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}