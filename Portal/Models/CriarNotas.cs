/*using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.Packaging;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml;
using BoletoNet;
using Globais.Helper;
using Portal.BE;
using Portal.BLL;
using Portal.Notas;

namespace Portal.Models
{
    public class CriarNotas
    {
        EmpresaBE empresa;
        public CriarNotas()
        {
            empresa = new EmpresaBLL().SelectId(new EmpresaBE { });
        }


        public string IniciarXMLRemessa(string notasItens, bool fileXml)
        {
            if (fileXml)
                return this.GerarxmlNotas(notasItens);
            else
            {
                NotasBLL _notas = new NotasBLL();
                var notas = _notas.SelecionarNotasGerarXMl_Remessa_Selecionadas(notasItens);
                return this.GerarRemessaArquivo(notas);
            }

        }
        public string GerarxmlNotas(string notasItens) { 
            NotasXMLLoteBLL _notasXml = new NotasXMLLoteBLL();
            NotasXMLLoteItensItensBLL _notasXmlItens = new NotasXMLLoteItensItensBLL();
            StringBuilder _xmlStringM = new StringBuilder();
            StringBuilder _xmlStringD = new StringBuilder();
            StringBuilder _xmlStringI = new StringBuilder();
            var endereco = "";
            var _nomeXml = "";

            List<NotasBE> notas = new ClienteBLL().CarregaNotaClienteinMes(notasItens);
            GerarXMLReceita xml = new GerarXMLReceita(notasItens, notas);
            if (notas.Count > 0)
            {
                //Criar a parta com os arquivos
                endereco = CriarEnderecoXMLRemessa(notas[0].not_dtEmissao) + "\\" + String.Concat(DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString(), DateTime.Now.Second.ToString(), DateTime.Now.Millisecond.ToString(), HttpContext.Current.Session.SessionID.ToUpper());
                
                string enderecotxt = endereco;
                Directory.CreateDirectory(endereco);

                string hdnMes = notas[0].not_dtEmissao.ToString("MM");
                string hdnAno = notas[0].not_dtEmissao.ToString("yy");

                _xmlStringM = xml.GerarConteudoArquivoMestre();
                _xmlStringD = xml.GerarConteudoArquivoCadastro();
                _xmlStringI = xml.GerarConteudoArquivoItem();

                using (Package objZIP = ZipPackage.Open(endereco + ".zip", FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                    _nomeXml = String.Concat("SP03031402000141", "21", "UNI", hdnAno, hdnMes, "N01", "M", ".001");
                    ExportarConteudoToTXT(enderecotxt + "\\" + _nomeXml, _xmlStringM, "ISO-8859-1");
                    AdicionarArquivoNoZip(objZIP, enderecotxt + "\\" + _nomeXml);
                    DownloadFile(enderecotxt + "\\" + _nomeXml);

                    _nomeXml = String.Concat("SP03031402000141", "21", "UNI", hdnAno, hdnMes, "N01", "D", ".001");
                    ExportarConteudoToTXT(enderecotxt + "\\" + _nomeXml, _xmlStringD, "ISO-8859-1");
                    AdicionarArquivoNoZip(objZIP, enderecotxt + "\\" + _nomeXml);


                    _nomeXml = String.Concat("SP03031402000141", "21", "UNI", hdnAno, hdnMes, "N01", "I", ".001");
                    ExportarConteudoToTXT(enderecotxt + "\\" + _nomeXml, _xmlStringI, "ISO-8859-1");
                    AdicionarArquivoNoZip(objZIP, enderecotxt + "\\" + _nomeXml);
                }

               /* var _notaXml = new BE.NotasXMLLoteBE
                {
                    notXml_nome = DateTime.Now.ToString("dd_MM_yyyy_HH_MM"),
                    notXml_data = DateTime.Now,
                    notXml_xml = _xmlString.ToString()
                };
                _notasXml.Insert(_notaXml);

                foreach (var item in notas)
                {
                    _notasXmlItens.Insert(new BE.NotasXMLLoteItensBE
                    {
                        notXml_id = _notaXml.notXml_id,
                        not_id = item.not_id
                    });
                }*/
          /*  }

            string arquivo = endereco + DateTime.Now.ToString("ddMMyyyy") + ".zip";
            ZipFile.CreateFromDirectory(endereco, arquivo);


            return arquivo;
        }


        private bool AdicionarArquivoNoZip(System.IO.Packaging.Package zip, string fileToAdd) {

            try
            {
                string uriFileName = fileToAdd.Replace(" ", "_");

                //A Uri sempre começa com uma barra "/"  
                string zipUri = string.Concat("/", uriFileName);

                Uri partUri = new Uri(zipUri, UriKind.Relative);
                string contentType = "application/zip";

                //O PackagePart contém as informações:
                // Sempre extrair o arquivo quando ele é extraído (partUri) 
                // O tipo do conteúdo do fluxo (MIME type) - (contentType) 
                // O tipo de compressão usado (CompressionOption.Normal)   
                System.IO.Packaging.PackagePart pkgPart = zip.CreatePart(partUri, contentType, System.IO.Packaging.CompressionOption.Normal);

                //Leia todos os bytes do arquivo para adiciona-lo ao arquivo zip 
                byte[] bites = System.IO.File.ReadAllBytes(fileToAdd);

                //Comprimir e escrever os byte no arquivo zip 
                pkgPart.GetStream().Write(bites, 0, bites.Length);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        private bool ExportarConteudoToTXT(string url, StringBuilder strContent, string strEnconding) {
            try
            {
                using (StreamWriter objWriter = new StreamWriter(url, false, System.Text.Encoding.GetEncoding(strEnconding)))
                {
                    objWriter.Write(strContent.ToString());
                    objWriter.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GerarRemessaArquivo(List<NotasClientesEmitidasMes> _notas)
        {
            Boletos boletos = new Boletos();

            var empresa = new EmpresaBLL().SelectId(new EmpresaBE());
            ClienteBLL _cliente = new ClienteBLL();
            Cedente c = new Cedente(empresa.conf_cnpj, empresa.conf_razaosocial, empresa.conf_agencia, empresa.conf_agenciadiito, empresa.conf_conta, empresa.conf_digito);

            Boletos _boletos = new Boletos();
            foreach (var item in _notas)
            {
                var _cli = _cliente.SelectId(item.cli_id);
                c.Codigo = "3247120";// item.not_id.ToString();

                Boleto b = new Boleto(item.not_dtVencimento, item.not_totalliquido, "101", item.not_numero.ToString(), c);
                b.NumeroDocumento = item.not_numero.ToString();
                b.DataDocumento = item.not_dtEmissao;

                b.Sacado = new Sacado(item.cli_CGC, item.cli_razaoSocial);
                if (_cli != null && _cli.Endereco != null && _cli.Endereco.Count > 0)
                {
                    b.Sacado.Endereco.End = _cli.Endereco[0].end_logradouro + " " + _cli.Endereco[0].end_numero + " " + _cli.Endereco[0].end_complemento;
                    b.Sacado.Endereco.Bairro = _cli.Endereco[0].end_bairro;
                    b.Sacado.Endereco.Cidade = _cli.Endereco[0].end_cidade;
                    b.Sacado.Endereco.CEP = _cli.Endereco[0].end_cep;
                    b.Sacado.Endereco.UF = _cli.Endereco[0].end_estado;
                }
                else
                {
                    b.Sacado.Endereco.End = "";
                    b.Sacado.Endereco.Bairro = "";
                    b.Sacado.Endereco.Cidade = "";
                    b.Sacado.Endereco.CEP = "";
                    b.Sacado.Endereco.UF = "";
                }
                b.EspecieDocumento = new EspecieDocumento_Santander("4");

                boletos.Add(b);
            }

            return GeraArquivoCNAB240(new Banco(33), c, boletos);
        }

        public string GeraArquivoCNAB240(IBanco banco, Cedente cedente, Boletos boletos)
        {
            //StreamWriter sr = new StreamWriter(@"d:\teste_cnab.txt");

            var end = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), "Notas_Fiscais");
            var _nomeXml = end + "\\CNAB_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".txt";
            var item = HttpContext.Current.Request.Url.AbsoluteUri.Replace("/Faturamento/GerarXMLRemessaInsert", "") + "/Notas_Fiscais/CNAB_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm") + ".txt";
            ArquivoRemessa arquivo = new ArquivoRemessa(TipoArquivo.CNAB400);
            cedente.CodigoTransmissao = cedente.CodigoTransmissao == null ? "" : cedente.CodigoTransmissao;
            arquivo.GerarArquivoRemessa("3247120", banco, cedente, boletos, _nomeXml, 1);
            //arquivo.GerarArquivoRemessa("1200303001417053", banco, cedente, boletos, saveFileDialog.OpenFile(), 1);
            string text = System.IO.File.ReadAllText(_nomeXml);

            return text;

        }

        public string DownloadFile(string url)
        {
            string text = System.IO.File.ReadAllText(url);

            return text;

        }

        public void IniciarNotas(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                NotasBLL _notas = new NotasBLL();
                var pendentes = _notas.SelecionarNotasNaoEmitidas();

                if(pendentes.Count == 0)
                {
                    ProcessCancellation();
                }
                else
                {
                    foreach (var item in pendentes)
                    {
                        if (empresa.confCom_nota)
                        {
                            if (!item.not_emitida)
                            {
                                var retornoNota = this.CarregaNota(item.cli_id, item.not_id, item.not_dtEmissao);
                                if (retornoNota != "")
                                {
                                    item.not_emitida = true;
                                    item.not_linkNota = retornoNota;
                                }
                            }
                        }

                        if (empresa.confCom_boleto)
                        {
                            if (!item.not_boleto)
                            {
                                var retornoBoleto = this.CarregaBoletos(item.cli_id, item.not_id, item.not_dtEmissao);
                                if (retornoBoleto != "")
                                {
                                    item.not_boleto = true;
                                    item.not_linkBoleto = retornoBoleto;
                                }
                            }
                        }
                        item.not_statusPagamento = "GET";
                        _notas.Update(item);
                        Thread.Sleep(1500);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                ProcessCancellation();
            }
        }

        private void ProcessCancellation()
        {
            Thread.Sleep(10);
        }

        public string CarregaNota(int id, int nota, DateTime mes)
        {
            try
            {
                //var u = System.Web.HttpContext.Current.Request.Url;

                string url = string.Concat("http://faturamento.sbc.com.br/", "Login/GerarNotasPDF?id=", id);
                //string url = string.Concat("http://localhost:49496/", "Login/GerarNotasPDF?id=", id);
                
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = System.Net.WebRequestMethods.Http.Get;
                request.Accept = "application/json";

                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                var pdf = new BoletoNet.BoletoBancario().MontaBytesPDF(responseFromServer);


                var bw = new BinaryWriter(System.IO.File.Open(this.CriarEndereco(true, mes) + "\\" + nota + ".pdf", FileMode.OpenOrCreate));
                bw.Write(pdf);
                bw.Close();

                return id + ".pdf";
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return "";
            }
        }

        public string CarregaBoletos(int id, int nota, DateTime mes)
        {
            try
            {
                string url = string.Concat("http://faturamento.sbc.com.br/", "Login/GerarBoletoPDF?id=", id);
                //string url = string.Concat("http://localhost:49496/", "Login/GerarBoletoPDF?id=", id);

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                request.Method = System.Net.WebRequestMethods.Http.Get;
                request.Accept = "application/json";

                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream dataStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                var pdf = new BoletoNet.BoletoBancario().MontaBytesPDF(responseFromServer);


                var bw = new BinaryWriter(System.IO.File.Open(this.CriarEndereco(false, mes) + "\\" + nota + ".pdf", FileMode.OpenOrCreate));
                bw.Write(pdf);
                bw.Close();

                return id + ".pdf";
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return "";
            }
        }

        public string CriarEndereco(bool notas, DateTime mes)
        {
            try
            {
                var end = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), "Notas_Fiscais");
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                end = end + "\\" + mes.Year.ToString();
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                end = end + "\\" + mes.Month;
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                var endBoleto = end + "\\Boletos";
                if (!Directory.Exists(endBoleto))
                    Directory.CreateDirectory(endBoleto);

                var endNota = end + "\\Notas";
                if (!Directory.Exists(endNota))
                    Directory.CreateDirectory(endNota);

                if (notas)
                    return endNota;
                else
                    return endBoleto;
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return "";
            }
        }

        public string CriarEnderecoXMLRemessa(DateTime mes)
        {
            try
            {
                var end = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), "Notas_Fiscais");
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                end = end + "\\" + mes.Year.ToString();
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                end = end + "\\" + mes.Month;
                if (!Directory.Exists(end))
                    Directory.CreateDirectory(end);

                var endArquivos = end + "\\Arquivos";
                if (!Directory.Exists(endArquivos))
                    Directory.CreateDirectory(endArquivos);

                return endArquivos;
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return "";
            }
        }
    }
}*/