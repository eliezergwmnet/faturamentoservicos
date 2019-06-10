using System;
using System.Threading;
using Globais.BE;
using Globais.BLL;
using Globais.Helper;
using NFENotasFiscais.BE;
using NFENotasFiscais.BE.NFEServicos;
using NFSE.Net;

namespace NFENotasFiscais.BLL.NFEServicos
{
    public class NFEEnviarNota
    {
        NTLoteBE lote;
        GlobaisEmpresaBE empresa;

        string senhaCetificado = "Yih577^h";
        public object NFeEnvioRetornoBLL { get; private set; }

        public NFEEnviarNota(NTLoteBE _lote, GlobaisEmpresaBE _empresa)
        {
            this.lote = _lote;
            this.empresa = _empresa;
        }

        public int EnviarNotaXML()
        {
            try
            {
                NTLoteBLL _lote = new NTLoteBLL();

                //Colocar o update no lote
                var not = new NFEGerarXMLNotasServicos(this.lote.lote_id.ToString(), this.empresa.conf_cnpj, this.empresa.conf_inscricaoestadual);
                lote.lote_urlXmlEnvio = not.GerarArquivoXML(this.lote);
                string fileXmlRetorno = not.GerarArquivoXMLRetorno();

                lote.lote_status = StatusNotaNFe.LoteGerado.GetDescription();
                _lote.UpdateStatus(lote);//Atualiza Status
                _lote.UpdateXmlEnvio(lote);//Atualiza Campos XML Lote
                
                this.EnviarRPSNotas(lote.lote_urlXmlEnvio, fileXmlRetorno);

                var retorno = not.LerXMLLote(fileXmlRetorno);
                if (retorno.Erro)
                {
                    this.CriarLoteRetornoErro(retorno);
                    lote.lote_status = StatusNotaNFe.LoteErro.GetDescription();
                    return 0;
                }
                else
                {
                    lote.lote_status = StatusNotaNFe.LoteEnviado.GetDescription();
                    _lote.UpdateStatus(lote);
                    this.ConsultaLoteRPS(retorno.Protocolo);
                    return lote.lote_id;
                }
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return 0;
            }
        }

        void CriarLoteRetornoErro(NFeEnvioRetornoBE obj)
        {
            NTLoteErroBLL erro = new NTLoteErroBLL();
            erro.Insert(new NTLoteErroBE
            {
                lote_id = this.lote.lote_id,
                LoteErr_Codigo = obj.ErroCodigo,
                LoteErr_Mensagem = obj.ErroMensagem,
                LoteErr_Correcao = obj.ErroCorrecao
            });
        }

        void ConsultaLoteRPS(string protocolo)
        {
            var not = new NFEGerarXMLNotasServicos(this.lote.lote_id.ToString(), this.empresa.conf_cnpj, this.empresa.conf_inscricaoestadual);
            lote.lote_urlXmlConsulta = not.GerarArquivoXMLConsulta(protocolo);
            NTLoteBLL _lote = new NTLoteBLL();

            _lote.UpdateXmlConsulta(lote);
            var ulXmlConsultaRetorno = not.GerarArquivoXMLConsultaRetorno();
        }

        public NFSE.Net.Core.Empresa RetornaEmpresa(bool criptografado)
        {
            var empresa = new NFSE.Net.Core.Empresa();
            empresa.Nome = Common.RemoveCaracter(this.empresa.conf_razaosocial);
            empresa.CNPJ = Common.RemoveCaracter(this.empresa.conf_cnpj);
            empresa.InscricaoMunicipal = this.empresa.conf_inscricaoestadual;
            empresa.CertificadoArquivo = @"D:\NotasEletronicas\Certificados\INFOGER.pfx";
            if (criptografado)
                empresa.CertificadoSenha = NFSE.Net.Certificado.Criptografia.criptografaSenha(senhaCetificado);
            else
                empresa.CertificadoSenha = senhaCetificado;

            empresa.tpAmb = Propriedade.TipoAmbiente.taHomologacao;// 2;
            empresa.tpEmis = 1;


            empresa.CodigoMunicipio = 3529401;
            return empresa;
        }

        public void EnviarRPSNotas(string caminhoXml, string caminhoXmlRetorno)
        {
            //string caminhoXml = @"D:\NotasEletronicas\LoteEnviar.xml";
           // string caminhoXmlRetorno = @"D:\NotasEletronicas\LoteEnviarRetorno.xml";
            System.Net.ServicePointManager.Expect100Continue = false;

            var envio = new NFSE.Net.Envio.Processar();
            var empresa = RetornaEmpresa(false);
            envio.ProcessaArquivo(empresa, caminhoXml, caminhoXmlRetorno, Servicos.RecepcionarLoteRps);
        }

        
    }

    public class BackConsultaLote
    {
        int lote_id;
        public BackConsultaLote(int lote_id)
        {
            this.lote_id = lote_id;
        }


        /// <summary>
        /// 
        /// Código de situação de lote de RPS
        /// 1 – Não Recebido
        /// 2 – Não Processado
        /// 3 – Processado com Erro
        /// 4 – Processado com Sucesso
        /// </summary>
        /// <param name="cancellationToken"></param>
        public void IniciarConsultar(CancellationToken cancellationToken = default(CancellationToken))
        {
            for (int i = 0; i < 20000; i++)
            {
                NTLoteBLL ntLote = new NTLoteBLL();
                var LoteConsulta = ntLote.SelectId(new NTLoteBE { lote_id = this.lote_id });
                var empresaDados = new GlobaisEmpresaBLL().SelectId(new Globais.BE.GlobaisEmpresaBE { conf_id = LoteConsulta.conf_id });
                System.Net.ServicePointManager.Expect100Continue = false;

                var empresa = new NFEEnviarNota(LoteConsulta, empresaDados).RetornaEmpresa(false);
                var envio = new NFSE.Net.Envio.Processar();
                envio.ProcessaArquivo(empresa, LoteConsulta.lote_urlXmlConsulta, LoteConsulta.lote_urlXmlConsulta.Replace(".xml", "-Retorno.xml"), Servicos.ConsultarSituacaoLoteRps);

                var retorno = new NFEGerarXMLNotasServicos().LerXMLConsulta(LoteConsulta.lote_urlXmlConsulta.Replace(".xml", "-Retorno.xml"));
                if (retorno.Situacao == "2")
                    Thread.Sleep(1500);
                else
                    return;
            }
        }

        /*int numTentativas;
        string urlConsulta;
        string urlConsultaRetorno;
        int conf_id;
        public BackConsultaLote(int conf_id, int numTentativas, string urlConsulta, string urlConsultaRetorno)
        {
            this.numTentativas = numTentativas;
            this.urlConsulta = urlConsulta;
            this.urlConsultaRetorno = urlConsultaRetorno;
            this.conf_id = conf_id;
        }

        public void RunLoop()
        {
            for (int i = 0; i < numTentativas; i++)
            {
                var situacao = this.ConsultarRPSNotas();
                if (situacao == "2")
                {
                    Thread.Sleep(1500);
                }
            }
        }

        public string ConsultarRPSNotas()
        {
            //string caminhoXml = @"D:\NotasEletronicas\ConsultarLoteEnviar.xml";
            //string retornocaminhoXml = @"D:\NotasEletronicas\ConsultarLoteEnviarRetorno.xml";
            new GlobaisEmpresaBLL().SelectId(new Globais.BE.GlobaisEmpresaBE { conf_id = this.conf_id });
            System.Net.ServicePointManager.Expect100Continue = false;

            var empresa = new NFEEnviarNota().RetornaEmpresa(false);
            var envio = new NFSE.Net.Envio.Processar();
            envio.ProcessaArquivo(empresa, urlConsulta, urlConsultaRetorno, Servicos.ConsultarSituacaoLoteRps);

            var retorno = new NFEGerarXMLNotasServicos().LerXMLConsulta(this.urlConsultaRetorno);
            return retorno.Situacao;
        }*/
    }
}
