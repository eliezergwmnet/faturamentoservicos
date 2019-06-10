using System;
using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class _MetodosGlobaisController: Controller
    {
        #region LIsta de Endereço

        /// <summary>
        /// Busca CEP eu retorna objeto com os dados
        /// </summary>
        /// <param name="end_cep"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult BuscaCepEndereco(string end_cep)
        {
            try
            {
                CEP cep = new CEP(end_cep.Replace("-", "").Replace(" ", ""));
                return Json(cep, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Carrega os dados do endereço selecionado para cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CarregaEnderecoCliente(int id)
        {
            var result = new GlobaisClienteEnderecoBLL().SelectId(new GlobaisClienteEnderecoBE { end_id = id });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CarregaEndereco(int id)
        {
            var result = new GlobaisEnderecoBLL().SelectId(new GlobaisEnderecoBE { end_id = id });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Caso o campo end_id = 0 é efetuado a atualização do endereço, caso contrario é criar um novo endereço
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult CadastroEndereco(GlobaisEnderecoBE obj)
        {
            try
            {
                obj.end_cep = obj.end_cep.Replace("-", "");
                if (obj.end_id == 0)
                {
                    obj = new GlobaisEnderecoBLL().Insert(obj);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    new GlobaisEnderecoBLL().Update(obj);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Inclui o Novo Endereço
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult InsertClienteEndereco(GlobaisClienteEnderecoBE obj)
        {
            try
            {
                new GlobaisClienteEnderecoBLL().Insert(obj);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Deleta o Endereço
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult DeleteClienteEndereco(GlobaisClienteEnderecoBE obj)
        {
            try
            {
                new GlobaisClienteEnderecoBLL().Delete(obj);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region EnviarEmail
        public JsonResult EsqueciSenha(string email)
        {
            // new GlobaisEmailBLL().Select(new GlobaisEmailBE { ema_referencia = TipoEmail.EsqueciSenha.GetDescription() });
            return null;
        }
        #endregion

        #region LIsta de Contato Cliente
        /// <summary>
        /// Carrega os dados do contato selecionado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CarregaClienteContatos(int id)
        {
            var result = new GlobaisClienteContatoBLL().SelectId(new GlobaisClienteContatoBE { cont_id = id });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Cadastra um novo contato
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult CadastroContato(GlobaisClienteContatoBE obj)
        {
            try
            {
                if (obj.cont_id == 0)
                {
                    obj = new GlobaisClienteContatoBLL().Insert(obj);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    new GlobaisClienteContatoBLL().Update(obj);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Deleta o Endereço
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public JsonResult DeleteClienteContato(GlobaisClienteContatoBE obj)
        {
            try
            {
                new GlobaisClienteContatoBLL().Delete(obj);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Itens Sistema
        /// <summary>
        /// Desloga do sistema e finaliza sessões
        /// </summary>
        /// <returns></returns>
        public ActionResult SairSistema()
        {
            Session.RemoveAll();
            var cookie = Request.Cookies[Globais.Helper.Globais.NameCookie].Value;
            if (cookie != null)
            {
                Request.Cookies.Remove(Globais.Helper.Globais.NameCookie);
            }
            Response.Redirect("/Login");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retornar o logo em Base64 para o carregamento da NotaFiscal
        /// </summary>
        /// <returns></returns>
        public static string ImageToBase64()
        {
            string base64String = "";
            var path = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), "images\\logo-header.png");
            using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
            {
                using (System.IO.MemoryStream m = new System.IO.MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }
            return base64String;
        }
        #endregion
    }
}
