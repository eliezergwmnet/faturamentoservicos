/*using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Portal.BE;
using Portal.BLL;
using Portal.Notas;

namespace Portal.Controllers
{
    public class MeusDadosController : _BaseController
    {
        // GET: meusDados
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Meus Dados";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Alterar Usuários";

            return View();
        }
        [HttpPost]
        public ActionResult Index(GlobaisUsuarioBE _usuario, GlobaisEnderecoBE _endereco)
        {
            if (_usuario.usu_senha != null && _usuario.usu_senha != "")
                _usuario.usu_senha = GerarChave.HashValue(_usuario.usu_senha);
            else
                _usuario.usu_senha = new GlobaisUsuarioBLL().SelectId(_usuario.usu_id).usu_senha;
            _usuario.Endereco = _endereco;
            new GlobaisUsuarioBLL().Update(_usuario);
            Session[Globais.Helper.Globais.SessionSistema] = _usuario;
            Response.Redirect("/MeusDados");
            return View();
        }
    }
}*/