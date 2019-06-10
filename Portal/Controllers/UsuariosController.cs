using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class UsuariosController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Usuários";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Usuários";
            return View();
        }

        public PartialViewResult CarregaListaUsuario(string usu_nome, string usu_email, string usu_telefone)
       {
            var listaClientes = new GlobaisUsuarioBLL().SelectBuscaLista(usu_nome, usu_email, usu_telefone, GlobalVariables.User_EmpresaSelecionar);
            return PartialView(listaClientes);
        }

        public ActionResult Insert()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Usuários";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Cadastrar Usuários";

            return View();
        }
        [HttpPost]
        public JsonResult Insert(GlobaisUsuarioBE _usuario, GlobaisEnderecoBE _endereco)
        {
            _usuario.usu_senha = Common.CriptografiaSenha(_usuario.usu_senha);
            _usuario.Endereco = _endereco;
            _usuario.conf_id = GlobalVariables.User.conf_id;
            var user = new GlobaisUsuarioBLL().Insert(_usuario);
            
          
            return Json(user, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SelecId(int id)
        {
            var usuario = new GlobaisUsuarioBLL().SelectId(id);
            return Json(usuario, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(GlobaisUsuarioBE _usuario)
        {
            var listaClientes = new GlobaisUsuarioBLL().Update(_usuario);
            return Json(listaClientes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AlterarSenha(int id, string senha, string senhanova)
        {
            return Json(new GlobaisUsuarioBLL().SenhaAlterar(id, Common.CriptografiaSenha(senha), Common.CriptografiaSenha(senhanova)), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AlterarResetar(int id, string senha)
        {
            return Json(new GlobaisUsuarioBLL().SenhaResetar(id, Common.CriptografiaSenha(senha)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(GlobaisUsuarioBE _usuario)
        {
            new GlobaisUsuarioBLL().Delete(_usuario);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}