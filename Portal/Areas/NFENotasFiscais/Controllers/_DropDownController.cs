using System.Linq;
using System.Web.Mvc;
using Globais.BE;

namespace NFENotasFiscais.Controllers
{
    public class _DropDownController : _NFEBaseController
    {
        [HttpPost]
        public JsonResult CarregaServicos()
        {
            /*var lista = new SCMServicosBLL().Select();
            var result = (from x in lista select new GlobaisDropDown { Id = x.ser_id.ToString(), Nome = string.Format("{0} - {1}", x.ser_codigoServico, x.ser_descricaoServico) });*/
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}