/*using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Globais.Helper;
using Portal.BE;
using Portal.BLL;
using Portal.Models;

namespace Portal.Controllers
{
    public class FaturamentoController : _BaseController
    {
        #region Faturamento
        public ActionResult Index()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Faturamento";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Faturamento";
            return View();
        }

        public PartialViewResult IndexCarregaListaFaturamento(string fat_mes)
        {
            List<NotasClientesEmitidasMes> listaClientes = new NotasBLL().SelecionarNotasEmitidasMes(fat_mes);
            ViewBag.Empresa = new EmpresaBLL().SelectId(new EmpresaBE { });
            return PartialView(listaClientes);
        }

        public JsonResult CalcelarNota(int not_id)
        {
            new NotasBLL().Cancelar(not_id, GlobalVariables.User_EmpresaSelecionar);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CalcelarNotaMes()
        {
            new NotasBLL().CancelaNotasMes(GlobalVariables.User_EmpresaSelecionar);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public FileStreamResult RelatorioFaturamento(string mes)
        {
            string url = string.Concat(Common.UrlDados, "Login/GerarRelatorioFaturamentoPDF?fat_mes=", mes, "&conf_id=", Common.GetEmpresaSelecionada());

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = System.Net.WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream dataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            var pdf = new BoletoNet.BoletoBancario().MontaBytesPDF(responseFromServer);

            System.IO.MemoryStream workStream = new System.IO.MemoryStream();
            workStream.Write(pdf, 0, pdf.Length);
            workStream.Position = 0;
            //  var bw = new BinaryWriter(System.IO.File.Open(this.CriarEndereco(true, mes) + "\\" + nota + ".pdf", FileMode.OpenOrCreate));
            // bw.Write(pdf);
            // bw.Close();

            return new FileStreamResult(workStream, "application/pdf");
        }
        public FileStreamResult RelatorioFaturamentomes(string mes)
        {
            string url = string.Concat(Common.UrlDados, "Login/GerarRelatorioFaturamentoMesPDF?fat_mes=", mes, "&conf_id=", Common.GetEmpresaSelecionada());

            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            request.Method = System.Net.WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream dataStream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            var pdf = new BoletoNet.BoletoBancario().MontaBytesPDF(responseFromServer, NReco.PdfGenerator.PageOrientation.Landscape);

            System.IO.MemoryStream workStream = new System.IO.MemoryStream();
            workStream.Write(pdf, 0, pdf.Length);
            //workStream.
            workStream.Position = 0;
            //  var bw = new BinaryWriter(System.IO.File.Open(this.CriarEndereco(true, mes) + "\\" + nota + ".pdf", FileMode.OpenOrCreate));
            // bw.Write(pdf);
            // bw.Close();

            return new FileStreamResult(workStream, "application/pdf");
        }


        [HttpGet]
        public JsonResult RetornoBoletoBancoDados(int id)
        {
            var dados = new RetornoBancoBoletoBLL().SelectId(new RetornoBancoBoletoBE { retBol_seuBanco = id });
            if(dados != null)
                return Json(new { Tipo = "Pago", Dados = dados }, JsonRequestBehavior.AllowGet);
            else{
                var item = new NotasBLL().SelectId(new NotasBE { not_numero = id });
                if (item != null)
                {
                    var cliente = new ClienteBLL().SelectId(item.cli_id);
                    return Json(new { Tipo = "Pendente", Dados = item, Cliente = cliente }, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public JsonResult RetornoBoletoBancoDadosComentario(int id, string comentario)
        {
            var dados = new RetornoBancoBoletoBLL().Update(new RetornoBancoBoletoBE { retBol_id = id, retBol_comentarios = comentario });
            return Json(dados, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult BaixaManual(int id, string comentario, string formapagamento, string jurosparamento)
        {
            new RetornoBancoBoletoBLL().BaixaManual(id, comentario, formapagamento, jurosparamento, GlobalVariables.User.usu_id, GlobalVariables.User_EmpresaSelecionar);
            return Json(1, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult EnviarNotasEmail()
        {
            System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => new Models.EnviarNotasFiscais().EnviarNotas(cancellationToken));
            return Json(1, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Gerar Notas do Mes

        public ActionResult GerarNotasMes()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Faturamento";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Gerar Notas";
            return View();
        }

        [HttpPost]
        public JsonResult GerarNotasMesInsert(string notas)
        {
            try
            {
                var _modelNotas = new Models.CriarNotas();
                new Notas.GerarNotas(GlobalVariables.User_EmpresaSelecionar).GerarNotasMes(notas);
                System.Web.Hosting.HostingEnvironment.QueueBackgroundWorkItem(cancellationToken => _modelNotas.IniciarNotas(cancellationToken));
            }

            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GerarNotaCarregaListaFaturamento()
        {
            var notas = new Notas.GerarNotas(GlobalVariables.User_EmpresaSelecionar).GerarNotasMesContrato();
            ViewBag.Empresa = new EmpresaBLL().SelectId(new EmpresaBE { });
            return PartialView(notas);
        }

        public ActionResult GerarNotasPDF()
        {
            var clientes = new ClienteBLL().SelectBuscaNotasLista("", "", "", GlobalVariables.User_EmpresaSelecionar);

            var empresa = new EmpresaBLL().SelectId();
            ViewData["empresa"] = empresa;

            return View(clientes);
        }

        public ActionResult DescontoFaturamento()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Faturamento";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Desconto de Faturas";
            return View();
        }
        [HttpGet]
        public JsonResult DescontoFaturamentoCalcula(string idCliente, string horas)
        {
            var arrayCliente = idCliente.Split(',');
            ClienteBLL _cli = new ClienteBLL();
            ClienteServicosBLL _serv = new ClienteServicosBLL();

            List<ClienteServicosDescontoBE> retorno = new List<ClienteServicosDescontoBE>();
            foreach (var item in arrayCliente)
            {
                if (item != "")
                {
                    var cliente = _cli.SelectIdComServico(Convert.ToInt32(item));
                    var diasMes = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    var horasMes = diasMes * 24;
                    decimal valorProporcional = 0;
                    decimal valorTotalServicos = 0;
                    string servicos = "";
                    if (cliente.Servicos.Count > 0)
                    {
                        foreach (var itemServ in cliente.Servicos)
                        {
                            if (!itemServ.servCli_parcelado)
                            {
                                var dia = itemServ.servCli_valor / horasMes;
                                valorTotalServicos += itemServ.servCli_valor;
                                valorProporcional += dia * Convert.ToInt32(horas);
                                servicos = servicos == "" ? itemServ.servCli_descricao : servicos + " / " + itemServ.servCli_descricao;
                            }
                        }
                        retorno.Add(new ClienteServicosDescontoBE
                        {
                            cli_id = cliente.cli_id,
                            cli_nome = cliente.cli_nomeFantasia,
                            servCli_nome = "DESCONTO PROPORCIONAL A " + horas + " HORAS DE SERVIÇOS.",
                            horas = horas,
                            servicos = servicos,
                            servCli_valorServicos = valorTotalServicos,
                            servCli_valorDesconto = valorProporcional,
                            servCli_valorTotal = valorTotalServicos - valorProporcional
                        });
                    }
                    else
                    {
                        retorno.Add(new ClienteServicosDescontoBE
                        {
                            cli_id = cliente.cli_id,
                            cli_nome = cliente.cli_nomeFantasia,
                            servCli_nome = "DESCONTO PROPORCIONAL A " + horas + " HORAS DE SERVIÇOS.",
                            servicos = "--",
                            horas = horas,
                            servCli_valorServicos = 0,
                            servCli_valorDesconto = 0,
                            servCli_valorTotal = 0 - 0
                        });
                    }
                }
            }
            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DescontoFaturamentoEnviar(string idCliente, string horas)
        {
            try
            {
                ClienteBLL _cli = new ClienteBLL();
                ClienteServicosBLL _serv = new ClienteServicosBLL();
                ClienteNotaBLL _not = new ClienteNotaBLL();
                List<ClienteServicosDescontoBE> retorno = new List<ClienteServicosDescontoBE>();

                if (idCliente != "")
                {
                    var cliente = _cli.SelectIdComServico(Convert.ToInt32(idCliente));
                    var diasMes = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                    var horasMes = diasMes * 24;
                    decimal valorProporcional = 0;
                    decimal valorTotalServicos = 0;
                    string servicos = "";
                    if (cliente.Servicos.Count > 0)
                    {
                        foreach (var itemServ in cliente.Servicos)
                        {
                            if (!itemServ.servCli_parcelado)
                            {
                                var dia = itemServ.servCli_valor / horasMes;
                                valorTotalServicos += itemServ.servCli_valor;
                                valorProporcional += dia * Convert.ToInt32(horas);
                                servicos = servicos == "" ? itemServ.servCli_descricao : servicos + " / " + itemServ.servCli_descricao;
                            }
                        }
                        _serv.Insert(new ClienteServicosBE
                        {
                            cli_id = cliente.cli_id,
                            servCli_nome = "DESCONTO PROPORCIONAL A " + horas + " HORAS DE SERVIÇOS.",
                            servCli_descricao = "DESCONTO PROPORCIONAL A " + horas + " HORAS DE SERVIÇOS.",
                            servCli_valor =  valorProporcional * -1,
                            servCli_parcelado = true,
                            servCli_parceladoQtd = 1,
                            servCli_cobrarPorpor = 0,
                            servCli_qtdDias = 0,
                            serv_id = 0,
                            servCli_dataAtivacao = DateTime.Now
                        }, GlobalVariables.User_EmpresaSelecionar);
                        //_not.Insert(new ClienteNotaBE(cliente.cli_id, "DESCONTO PROPORCIONAL A " + horas + " HORAS DE SERVIÇOS.", valorProporcional * -1, 1, false, true, 0));
                    }
                }

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Gerar Arquivo XML e Remessa

        public ActionResult GerarXMLRemessa()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Faturamento";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Gerar XLM Remessa";
            return View();
        }

        public PartialViewResult GerarXMLRemessaCarregaListaFaturamento()
        {
            List<NotasClientesEmitidasMes> listaClientes = new NotasBLL().SelecionarNotasGerarXMl_Remessa();
            return PartialView(listaClientes);
        }

        [HttpGet]
        public void GerarXMLRemessaInsert(string notas, bool xml = false)
        {
            try
            {
                var url = new CriarNotas().IniciarXMLRemessa(notas, xml);
                StringBuilder sb = new StringBuilder();
                if(xml)
                    CriarArquivoDownloadZIP(url, DateTime.Now.ToString("dd-MM-yyyy") + ".zip");
                else
                    CriarArquivodownload(sb.Append(url), "cnab.txt", "text/plan");
                // return Json(url, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                //return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        void CriarArquivoDownloadZIP(string url, string namefile)
        {
            var file = new System.IO.FileInfo(url);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + namefile);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
            }
        }

        void CriarArquivodownload(System.Text.StringBuilder _build, string _nome, string type)
        {
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=" + _nome);
            Response.ContentType = type;
            System.IO.StringWriter sw = new System.IO.StringWriter(_build);
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
        #endregion

        #region Serviços Contratados

        public ActionResult ServicosContratado()
        {
            ViewData[TituloPageEnum.TituloPagina.ToString()] = "Serviços Contartados";
            ViewData[TituloPageEnum.TituloModuloPagina.ToString()] = "Lista Serviços";
            return View();
        }

        public PartialViewResult ServicosContratadoCarregaLista(string cli_nomeFantasia, string cli_razaoSocial, string cli_CGC)
        {
            List<ClienteBE> listaClientes = new ClienteBLL().SelectBuscaServicoLista(cli_nomeFantasia, cli_razaoSocial, cli_CGC, GlobalVariables.User_EmpresaSelecionar);
            return PartialView(listaClientes);
        }

        #endregion
    }
}*/