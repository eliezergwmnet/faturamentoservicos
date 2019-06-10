using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;

namespace Portal.Controllers
{
    public class ClientesController : _BaseController
    {
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Clientes";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Clientes";
            return View();
        }

        public PartialViewResult CarregaListaCliente(string cli_nomeFantasia, string cli_razaoSocial, string cli_CGC)
        {
            List<GlobaisClienteBE> listaClientes = new GlobaisClienteBLL().Select(new GlobaisClienteBE { cli_nomeFantasia = cli_nomeFantasia, cli_razaoSocial = cli_razaoSocial, cli_CGC = cli_CGC });
            return PartialView(listaClientes);
        }


        public ActionResult Insert()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Clientes";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Cadastrar Cliente";

            return View();
        }
        [HttpPost]
        public ActionResult Insert(GlobaisClienteBE _cliente, string cli_CFOP, GlobaisClienteEnderecoBE _endereco, GlobaisClienteContatoBE _contato)
        {
            _cliente.cli_CFOP = Convert.ToDecimal(cli_CFOP.Replace(".", ","));
            _cliente.cli_id = new GlobaisClienteBLL().Insert(_cliente);
            _endereco.cli_id = _cliente.cli_id;
            _endereco.cliEnd_Tipo = "EP";
            new GlobaisClienteEnderecoBLL().Insert(_endereco);

            if (_contato.cont_nome != null)
            {
                _contato.cli_id = _cliente.cli_id;
                new GlobaisClienteContatoBLL().Insert(_contato);
            }

            //Response.Redirect("/ClienteServicos?cli_id=" + listaClientes.cli_id);
            return View();
        }

        public JsonResult CarregarDados(int id)
        {
            var cliente = new GlobaisClienteBLL().SelectID(new GlobaisClienteBE { cli_id = id });
            return Json(cliente, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(GlobaisClienteBE obj, string cli_CFOP)
        {
            obj.cli_CFOP = Convert.ToDecimal(cli_CFOP.Replace(".", ","));
            var _cliente = new GlobaisClienteBLL().Update(obj);
            return Json(_cliente, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Deletar(GlobaisClienteBE obj)
        {
            var _cliente = new GlobaisClienteBLL().Delete(obj);
            return Json(_cliente, JsonRequestBehavior.AllowGet);
        }
    }
}



        /*
          public ActionResult Notas(int id)
         {
             ViewBag.IdCliente = id;
             ViewData[TituloPageEnum.TituloPagina.ToString()] = "Notas Cliente";
             ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista de Notas";
             return View();
         }

         public PartialViewResult NotasCarregaLista(int cliente)
         {
             List<NotasClientesEmitidasMes> listaClientes = new NotasBLL().SelecionarNotasEmitidasCliente(cliente);
             return PartialView(listaClientes);
         }

          [HttpPost]
         public ActionResult UpdateEndereco(GlobaisClienteEnderecoBE obj)
         {
             if(obj.end_id ==0)
                 new ClienteEnderecoBLL().Insert(obj);
             else
                 new ClienteEnderecoBLL().Update(obj);
             Response.Redirect("/Clientes/Update/" + obj.cli_id);
             return View();
         }

         [HttpPost]
         public JsonResult CarregaContatoCliente(int id)
         {
             var result = new ClienteContatoBLL().SelectId(new GlobaisClienteContatoBE { cont_id = id });
             return Json(result, JsonRequestBehavior.AllowGet);
         }

         [HttpPost]
         public ActionResult UpdateContato(GlobaisClienteContatoBE obj)
         {
             if(obj.cont_id == 0)
                 new ClienteContatoBLL().Insert(obj);
             else
                 new ClienteContatoBLL().Update(obj);
             Response.Redirect("/Clientes/Update/" + obj.cli_id);
             return View();
         }

         [HttpPost]
         public JsonResult DeleteServico(int id, int cli_id)
         {
             new ClienteServicosBLL().Delete(new ClienteServicosBE { servCli_id = id, cli_id=cli_id });
             return Json(true, JsonRequestBehavior.AllowGet);
         }

         [HttpPost]
         public JsonResult DeleteEndereco(int id)
         {
             new ClienteEnderecoBLL().Delete(new GlobaisClienteEnderecoBE { cliEnd_id = id });
             return Json(true, JsonRequestBehavior.AllowGet);
         }

         [HttpPost]
         public JsonResult DeleteContato(int id)
         {
             new ClienteContatoBLL().Delete(new GlobaisClienteContatoBE { cont_id = id });
             return Json(true, JsonRequestBehavior.AllowGet);
         }

         public ActionResult Delete()
         {
             return View();
         }
    }
}*/