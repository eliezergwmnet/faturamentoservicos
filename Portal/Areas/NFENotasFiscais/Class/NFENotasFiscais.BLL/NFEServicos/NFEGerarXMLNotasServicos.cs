using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Globais.Helper;
using NFENotasFiscais.BE;
using NFENotasFiscais.BE.NFEServicos;
using PortalSCM.Helper;

namespace NFENotasFiscais.BLL.NFEServicos
{
    public class NFEGerarXMLNotasServicos
    {
        string numLote;
        string cnpj;
        string incricaoMunicipal;
        string xml = "";
        string fileXml;

        public NFEGerarXMLNotasServicos()
        {

        }
        public NFEGerarXMLNotasServicos(string _numLote, string _cnpj, string _incricaoMunicipal)
        {
            this.numLote = _numLote;//.PadLeft(4, '0');
            this.cnpj = _cnpj.Replace("/", "").Replace("-", "").Replace(".", "").Replace(" ", "");
            this.incricaoMunicipal = _incricaoMunicipal;
        }

        public string GerarArquivoXML(NTLoteBE obj)
        {
            this.CarregaXMCabecalho(obj.Notas.Count);
            this.CarregaXMLRPSServico(obj);
            //this.CarregaXML();
            this.SalvarXML();
            return fileXml; ;
        }

        public string GerarArquivoXMLRetorno()
        {
            return Common.CriarArquivos("NotaFiscalNFe", string.Format("{0}-Retorno.xml", this.numLote), xml);
        }

        public NFeEnvioRetornoBE LerXMLLote(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            NFeEnvioRetornoBE retorno = new NFeEnvioRetornoBE();

            if (doc.LastChild.LastChild.Name == "ListaMensagemRetorno")
            {
                retorno.Erro = true;
                retorno.ErroCodigo = doc.LastChild.LastChild.LastChild.ChildNodes[0].InnerText;
                retorno.ErroMensagem = doc.LastChild.LastChild.LastChild.ChildNodes[1].InnerText;
                retorno.ErroCorrecao = doc.LastChild.LastChild.LastChild.ChildNodes[2].InnerText;
            }
            else
            {
                retorno.Erro = false;
                retorno.NumeroLote = doc.LastChild.ChildNodes[0].InnerText;
                retorno.DataRecebimento = doc.LastChild.ChildNodes[1].InnerText;
                retorno.Protocolo = doc.LastChild.ChildNodes[2].InnerText;
            }

            return retorno;
        }

        public NFeConsultaRetorno LerXMLConsulta(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            NFeConsultaRetorno retorno = new NFeConsultaRetorno();

            retorno.NumeroLote = doc.LastChild.ChildNodes[0].InnerText;
            retorno.Situacao = doc.LastChild.ChildNodes[1].InnerText;

            return retorno;
        }

        void CarregaXMCabecalho(int quant)
        {
            string retorno = "";
            retorno += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            retorno += "<EnviarLoteRpsEnvio xmlns=\"http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd\" xmlns:tipos=\"http://www.ginfes.com.br/tipos_v03.xsd\">";
            retorno += "   <LoteRps>";
            retorno += "      <tipos:NumeroLote>" + this.numLote + "</tipos:NumeroLote>";
            retorno += "      <tipos:Cnpj>" + this.cnpj + "</tipos:Cnpj>";
            retorno += "      <tipos:InscricaoMunicipal>" + this.incricaoMunicipal + "</tipos:InscricaoMunicipal>";
            retorno += "      <tipos:QuantidadeRps>" + quant.ToString() + "</tipos:QuantidadeRps>";
            retorno += "      <tipos:ListaRps>";
            retorno += "@DadosClientes";
            retorno += "      </tipos:ListaRps>";
            retorno += "   </LoteRps>";
            retorno += "</EnviarLoteRpsEnvio>";

            xml = retorno;
        }

        void CarregaXMLRPSServico(NTLoteBE obj)
        {
            string retorno = "";
            string Tipo = "1";
            string NaturezaOperacao = "1";
            string OptanteSimplesNacional = "2";
            string IncentivadorCultural = "2";
            string Status = "1";

            string ItemListaServico = "108";
            string CodigoTributacaoMunicipio = "2706";
            string Discriminacao = "CONTRATO PRESTACAO DE SERVICOS DE CLOUD COMPUTING DE SERVIDOR VIRTUAL PARA O SISTEMA DE RECADASTRAMENTO VALOR REFERENTE USO NO MES R$ 1.976,00 - VALOR APROXIMADO DOS TRIBUTOS: R$ 358,84(18, 16 %)  FONTE: IBPT";
            string CodigoMunicipio = "3548708";

            foreach (var item in obj.Notas)
            {
                var iss = Math.Round(((item.not_totalbruto * GlobaisSCM.TaxaISS) / 100), 2);

                retorno += "         <tipos:Rps>";
                retorno += "            <tipos:InfRps>";
                retorno += "               <tipos:IdentificacaoRps>";
                retorno += "                  <tipos:Numero>" + item.not_id + "</tipos:Numero>";
                retorno += "                  <tipos:Serie>" + obj.lote_serie + "</tipos:Serie>";
                retorno += "                  <tipos:Tipo>" + Tipo + "</tipos:Tipo>";
                retorno += "               </tipos:IdentificacaoRps>";
                retorno += "               <tipos:DataEmissao>" + obj.lote_emissao.ToString("yyyy-MM-dd") + "T08:00:00</tipos:DataEmissao>";//Removida a hora de envio para teste, se der erro alterar
                retorno += "               <tipos:NaturezaOperacao>" + NaturezaOperacao + "</tipos:NaturezaOperacao>";
                retorno += "               <tipos:OptanteSimplesNacional>" + OptanteSimplesNacional + "</tipos:OptanteSimplesNacional>";
                retorno += "               <tipos:IncentivadorCultural>" + IncentivadorCultural + "</tipos:IncentivadorCultural>";
                retorno += "               <tipos:Status>" + Status + "</tipos:Status>";
                retorno += "               <tipos:Servico>";
                retorno += "                  <tipos:Valores>";
                retorno += "                     <tipos:ValorServicos>" + item.not_totalbruto.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:ValorServicos>";
                retorno += "                     <tipos:ValorDeducoes>0.00</tipos:ValorDeducoes>";
                retorno += "                     <tipos:ValorPis>" + item.not_pis.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:ValorPis>";
                retorno += "                     <tipos:ValorCofins>" + item.not_confins.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:ValorCofins>";
                retorno += "                     <tipos:ValorIr>" + item.not_irrf.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:ValorIr>";
                retorno += "                     <tipos:ValorCsll>" + item.not_cssl.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:ValorCsll>";
                retorno += "                     <tipos:IssRetido>2</tipos:IssRetido>";
                retorno += "                     <tipos:ValorIss>" + iss.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:ValorIss>";
                retorno += "                     <tipos:ValorIssRetido>0.00</tipos:ValorIssRetido>";
                retorno += "                     <tipos:BaseCalculo>" + item.not_totalbruto.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:BaseCalculo>";
                retorno += "                     <tipos:Aliquota>3</tipos:Aliquota>";
                retorno += "                     <tipos:ValorLiquidoNfse>" + item.not_totalliquido.ToString("n2").Replace(".", "").Replace(",", ".") + "</tipos:ValorLiquidoNfse>";
                retorno += "                  </tipos:Valores>";
                retorno += "                  <tipos:ItemListaServico>" + ItemListaServico + "</tipos:ItemListaServico>";
                retorno += "                  <tipos:CodigoTributacaoMunicipio>" + CodigoTributacaoMunicipio + "</tipos:CodigoTributacaoMunicipio>";//Deixar Fixo
                retorno += "                  <tipos:Discriminacao>" + Common.RemoveCaracter(Discriminacao.ToUpper()) + "</tipos:Discriminacao>";
                retorno += "                  <tipos:CodigoMunicipio>" + CodigoMunicipio + "</tipos:CodigoMunicipio>";//Deixar Fixo
                retorno += "               </tipos:Servico>";
                retorno += "               <tipos:Prestador>";
                retorno += "                  <tipos:Cnpj>" + this.cnpj + "</tipos:Cnpj>";
                retorno += "                  <tipos:InscricaoMunicipal>" + this.incricaoMunicipal + "</tipos:InscricaoMunicipal>";
                retorno += "               </tipos:Prestador>";
                retorno += "               <tipos:Tomador>";
                retorno += "                  <tipos:IdentificacaoTomador>";
                retorno += "                     <tipos:CpfCnpj>";
                retorno += "                        <tipos:Cnpj>" + Common.RemoveCaracter(item.Contrato.Cliente.cli_CPF) + "</tipos:Cnpj>";
                retorno += "                     </tipos:CpfCnpj>";
                retorno += "                  </tipos:IdentificacaoTomador>";
                retorno += "                  <tipos:RazaoSocial>" + Common.RemoveCaracter(item.Contrato.Cliente.cli_razaoSocial.ToUpper()) + "</tipos:RazaoSocial>";
                retorno += "                  <tipos:Endereco>";
                retorno += "                     <tipos:Endereco>" + Common.RemoveCaracter(item.Contrato.Cliente.Endereco[0].end_logradouro.ToUpper()) + "</tipos:Endereco>";
                retorno += "                     <tipos:Numero>" + Common.RemoveCaracter(item.Contrato.Cliente.Endereco[0].end_numero) + "</tipos:Numero>";
                retorno += "                     <tipos:Bairro>" + Common.RemoveCaracter(item.Contrato.Cliente.Endereco[0].end_bairro.ToUpper()) + "</tipos:Bairro>";
                retorno += "                     <tipos:CodigoMunicipio>3550308</tipos:CodigoMunicipio>";
                retorno += "                     <tipos:Uf>" + Common.RemoveCaracter(item.Contrato.Cliente.Endereco[0].end_estado.ToUpper()) + "</tipos:Uf>";
                retorno += "                     <tipos:Cep>" + Common.RemoveCaracter(item.Contrato.Cliente.Endereco[0].end_cep) + "</tipos:Cep>";
                retorno += "                  </tipos:Endereco>";
                retorno += "               </tipos:Tomador>";
                retorno += "            </tipos:InfRps>";
                retorno += "         </tipos:Rps>";

            }
            xml = xml.Replace("@DadosClientes", retorno);
        }

        void CarregaXML()
        {
            string retorno = "";
            retorno += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            retorno += "<EnviarLoteRpsEnvio xmlns=\"http://www.ginfes.com.br/servico_enviar_lote_rps_envio_v03.xsd\" xmlns:tipos=\"http://www.ginfes.com.br/tipos_v03.xsd\">";
            retorno += "   <LoteRps>";
            retorno += "      <tipos:NumeroLote>10</tipos:NumeroLote>";
            retorno += "      <tipos:Cnpj>14829991000124</tipos:Cnpj>";
            retorno += "      <tipos:InscricaoMunicipal>41187</tipos:InscricaoMunicipal>";
            retorno += "      <tipos:QuantidadeRps>1</tipos:QuantidadeRps>";
            retorno += "      <tipos:ListaRps>";
            retorno += "         <tipos:Rps>";
            retorno += "            <tipos:InfRps>";
            retorno += "               <tipos:IdentificacaoRps>";
            retorno += "                  <tipos:Numero>105207</tipos:Numero>";
            retorno += "                  <tipos:Serie>NFSe</tipos:Serie>";
            retorno += "                  <tipos:Tipo>1</tipos:Tipo>";
            retorno += "               </tipos:IdentificacaoRps>";
            retorno += "               <tipos:DataEmissao>2019-06-06T09:29:54</tipos:DataEmissao>";
            retorno += "               <tipos:NaturezaOperacao>1</tipos:NaturezaOperacao>";
            retorno += "               <tipos:OptanteSimplesNacional>2</tipos:OptanteSimplesNacional>";
            retorno += "               <tipos:IncentivadorCultural>2</tipos:IncentivadorCultural>";
            retorno += "               <tipos:Status>1</tipos:Status>";
            retorno += "               <tipos:Servico>";
            retorno += "                  <tipos:Valores>";
            retorno += "                     <tipos:ValorServicos>40.00</tipos:ValorServicos>";
            retorno += "                     <tipos:ValorDeducoes>0.00</tipos:ValorDeducoes>";
            retorno += "                     <tipos:ValorPis>0.00</tipos:ValorPis>";
            retorno += "                     <tipos:ValorCofins>0.00</tipos:ValorCofins>";
            retorno += "                     <tipos:ValorInss>0.00</tipos:ValorInss>";
            retorno += "                     <tipos:ValorIr>0.00</tipos:ValorIr>";
            retorno += "                     <tipos:ValorCsll>0.00</tipos:ValorCsll>";
            retorno += "                     <tipos:IssRetido>2</tipos:IssRetido>";
            retorno += "                     <tipos:ValorIss>0.00</tipos:ValorIss>";
            retorno += "                     <tipos:ValorIssRetido>0.00</tipos:ValorIssRetido>";
            retorno += "                     <tipos:OutrasRetencoes>0.00</tipos:OutrasRetencoes>";
            retorno += "                     <tipos:BaseCalculo>0.00</tipos:BaseCalculo>";
            retorno += "                     <tipos:Aliquota>0.00</tipos:Aliquota>";
            retorno += "                     <tipos:ValorLiquidoNfse>40.00</tipos:ValorLiquidoNfse>";
            //retorno += "                     <tipos:DescontoIncondicionado>0.00</tipos:DescontoIncondicionado>";
            //retorno += "                     <tipos:DescontoCondicionado>0.00</tipos:DescontoCondicionado>";
            retorno += "                  </tipos:Valores>";
            retorno += "                  <tipos:ItemListaServico>13045</tipos:ItemListaServico>";
            retorno += "                  <tipos:CodigoTributacaoMunicipio>13.04/107602/1641</tipos:CodigoTributacaoMunicipio>";
            retorno += "                  <tipos:Discriminacao>SERVICOS DE IMPRESSAO - ARQUIVAMENTO - 5366070/49119900 /UN/100,00000/2,00000/200,00000/NAO SUJEITO AO ICMS CONF. PORT. CAT. N 54 DE 16/10/8, RESPOSTA A CONSULTA N 847/1981 E RESPOSTA A CONSULTA N 109/1982. MATERIAL DESTINADO EXCLUSIVAMENTE A USO DO ENCOMENDANTE. \"ISS RECOLHIDO PELO PRESTADOR DE SERVICOS NO MUNICIPIO DE SBC CONF.LEI FEDERAL 116 / 03 E MUNICIPAL 5360 / 04\"</tipos:Discriminacao>";
            retorno += "                  <tipos:CodigoMunicipio>3548708</tipos:CodigoMunicipio>";
            retorno += "               </tipos:Servico>";
            retorno += "               <tipos:Prestador>";
            retorno += "                  <tipos:Cnpj>14829991000124</tipos:Cnpj>";
            retorno += "                  <tipos:InscricaoMunicipal>41187</tipos:InscricaoMunicipal>";
            retorno += "               </tipos:Prestador>";
            retorno += "               <tipos:Tomador>";
            retorno += "                  <tipos:IdentificacaoTomador>";
            retorno += "                     <tipos:CpfCnpj>";
            retorno += "                        <tipos:Cnpj>00063960001091</tipos:Cnpj>";
            retorno += "                     </tipos:CpfCnpj>";
            retorno += "                  </tipos:IdentificacaoTomador>";
            retorno += "                  <tipos:RazaoSocial>WAL MART BRASIL LTDA</tipos:RazaoSocial>";
            retorno += "                  <tipos:Endereco>";
            retorno += "                     <tipos:Endereco>MARECHAL DEODORO</tipos:Endereco>";
            retorno += "                     <tipos:Numero>2785</tipos:Numero>";
            retorno += "                     <tipos:Bairro>CENTRO</tipos:Bairro>";
            retorno += "                     <tipos:CodigoMunicipio>3548708</tipos:CodigoMunicipio>";
            retorno += "                     <tipos:Uf>SP</tipos:Uf>";
            retorno += "                     <tipos:Cep>12345678</tipos:Cep>";
            retorno += "                  </tipos:Endereco>";
            retorno += "               </tipos:Tomador>";
            retorno += "            </tipos:InfRps>";
            retorno += "         </tipos:Rps>";
            retorno += "      </tipos:ListaRps>";
            retorno += "   </LoteRps>";
            retorno += "</EnviarLoteRpsEnvio>";

            xml = retorno;
            //return retorno;
        }

        public string GerarArquivoXMLConsulta(string protocolo)
        {
            string retorno = "";
            retorno += "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            retorno += "<ConsultarSituacaoLoteRpsEnvio xmlns=\"http://www.ginfes.com.br/servico_consultar_situacao_lote_rps_envio_v03.xsd\" xmlns:tipos=\"http://www.ginfes.com.br/tipos_v03.xsd\" Id=\"" + this.numLote + "\">";
            retorno += "   <Prestador>";
            retorno += "      <tipos:Cnpj>" + this.cnpj + "</tipos:Cnpj>";
            retorno += "      <tipos:InscricaoMunicipal>" + this.incricaoMunicipal + "</tipos:InscricaoMunicipal>";
            retorno += "   </Prestador>";
            retorno += "   <Protocolo>" + protocolo + "</Protocolo>";
            retorno += "</ConsultarSituacaoLoteRpsEnvio>";

            xml = retorno;
            this.SalvarXML("Consulta");
            return fileXml;
        }

        public string GerarArquivoXMLConsultaRetorno()
        {
            return Common.CriarArquivos("NotaFiscalNFe", string.Format("{0}-Consulta-Retorno.xml", this.numLote), xml);
        }

        void SalvarXML(string nome = "")
        {
            StreamWriter SW = null;

            try
            {
                nome = nome == "" ? string.Format("{0}.xml", this.numLote) : string.Format("{0}-{1}.xml", this.numLote, nome);
                fileXml = Common.CriarArquivos("NotaFiscalNFe", nome, xml);
                SW = File.CreateText(fileXml);
                SW.Write(xml);
                SW.Close();
                SW = null;
            }
            finally
            {
                if (SW != null)
                {
                    SW.Close();
                    SW = null;
                }
            }
        }
    }
}
