/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globais.Helper;
using Portal.BE;
using Portal.BLL;

namespace Portal.Notas
{
    public class GerarXMLReceita
    {
        int conf_id;
        List<ClienteBE> arrBENotaFiscal;
        List<NotasBE> arrBENotas;
        public GerarXMLReceita()
        {
            conf_id = Common.GetEmpresaSelecionada();
        }
        public GerarXMLReceita(string mes, List<NotasBE> notas)
        {
            conf_id = Common.GetEmpresaSelecionada();

            var _cliente = new ClienteBLL();
            arrBENotaFiscal = _cliente.SelectClienteNotasArquivos(mes, conf_id);
            arrBENotas = notas;
        }


        public StringBuilder GerarConteudoArquivoMestre()
        {
            StringBuilder strSaida = new StringBuilder();
            int count = 1;
            int i = 1;

            foreach (var item in arrBENotas)
            {
                var objBENotaFiscal = (from x in arrBENotaFiscal where x.cli_id.Equals(item.cli_id) select x).FirstOrDefault<ClienteBE>();
                objBENotaFiscal.Nota = item;
                if (objBENotaFiscal.Nota.not_chave == "")
                {
                    objBENotaFiscal.Nota.not_chave = new GerarChave().CarregarCampoChaveCodificacaoNF(objBENotaFiscal.cli_CGC, item.not_numero, item.not_totalliquido, item.not_dtEmissao);
                }
                StringBuilder strRegistro = new StringBuilder();

                //---- 1: CNPJ
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.cli_CGC, 14));

                //---- 2: IE
                strRegistro.Append(NegocioHelper.FormataCampoSaidaInscricaoEstadual(objBENotaFiscal.cli_incricaoEstadual, 14));

                //---- 3: Razão Social
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.cli_razaoSocial, 35));

                //---- 4: UF
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_estado, 2));

                // ---- 5: Tipo de Assinante
                strRegistro.Append("0");   // //Comercial/Industrial

                //---- 6: Tipo de Utilização
                strRegistro.Append("2");//    //Comunicação de dados

                //---- 7: Grupo de Tensão
                strRegistro.Append("00");

                //---- 8: Código de Identificação do consumidor ou assinante
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.cli_id.ToString(), 12));

                //---- 9: Data de Emissão
                strRegistro.Append(NegocioHelper.FormataCampoSaidaData(objBENotaFiscal.Nota.not_dtEmissao));

                //---- 10: Modelo
                strRegistro.Append("21");

                //---- 11: Série
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("UNI", 3));

                //---- 12: Número
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_numero.ToString(), 9));

                //---- 13: MD5 da NF
                string strMD5NotaFiscalSemFormat = objBENotaFiscal.Nota.not_chave.Replace(".", "");
                strRegistro.Append(strMD5NotaFiscalSemFormat);

                //---- 14: Valor Total
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_totalliquido.ToString(), 12));

                //---- 15: BC ICMS
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_totalliquido.ToString(), 12));

                //---- 16: ICMS destacado
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 12));

                //---- 17: Operações Isentas ou não tributadas
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 12));

                //---- 18: Outros valores
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 12));

                //---- 19: Situação do documento
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Nota.not_situacao == "" ? "N" : objBENotaFiscal.Nota.not_situacao, 1));

                //---- 20: Ano Mes ref. apuração
                strRegistro.Append(objBENotaFiscal.Nota.not_dtEmissao.ToString("yyMM")); //--- Telecomunicação refere-se a Data de Emissão

                //---- 21: Referência ao item da NF. 
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(count.ToString(), 9)); //-- Posição do registro do 1º item de detalhe da Nota Fiscal

                if (objBENotaFiscal.Nota.ItensNota.Count > 1)
                {
                    var maiorqzero = false;
                    for (var a = 0; a < objBENotaFiscal.Nota.ItensNota.Count; a++)
                    {
                        if (objBENotaFiscal.Nota.ItensNota[a].notI_total > 0)
                        {
                            if (!maiorqzero)
                                maiorqzero = true;
                            else
                                count++;
                        }
                    }
                }


                //---- 22: Nro. terminal telefônico ou nro. conta de consumo. Campo alterado de 10 para 12 a partir de 01.07.12
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 12));

                //--- Versão 2017.03. Inclusão de novos campos

                //---- 23: Indicação do tipo de informação
                strRegistro.Append("1");

                switch (objBENotaFiscal.cli_CFOP.ToString().Replace(",", "."))
                {
                    case "5.301":
                    case "6.301":
                        strRegistro.Append("06");
                        break;

                    case "5.302":
                    case "6.302":
                        strRegistro.Append("02");
                        break;

                    case "5.303":
                    case "6.303":
                        strRegistro.Append("01");
                        break;

                    default:
                        strRegistro.Append("99");
                        break;
                }


                //---- 25: Subclasse de consumo
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 2));

                //---- 26: Número do terminal telefônico principal
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 12));

                //---- 27: CNPJ do emitente
                strRegistro.Append("03031402000141");

                //---- 28: Número ou código da fatura comercial 
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 20));

                //---- 29: Valor total da fatura comercial
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_totalliquido.ToString(), 12));

                //---- 30: Data da leitura anterior
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 8));

                //---- 31: Data de leitura atual
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 8));

                //---- 32: Brancos - uso futuro
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 50));

                //---- 33: Brancos - uso futuro
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 8));

                //---- 34: Informações adicionais
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 30));

                //---- 35: Brancos - uso futuro
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 5));

                //---- 36: Autenticação Digital do registro. Código MD5 do registro
                strMD5NotaFiscalSemFormat = this.GerarMD5(strRegistro.ToString());
                strRegistro.Append(strMD5NotaFiscalSemFormat);

                //---- Adiciona o registro ao conteúdo de saída
                strSaida.Append(strRegistro.ToString());
                strSaida.Append(Environment.NewLine);

                count++;

                i++;
            }
            return strSaida;
        }

        public string GerarMD5(string valor)
        {
            // Cria uma nova intância do objeto que implementa o algoritmo para
            // criptografia MD5
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();

            // Criptografa o valor passado
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(valor));

            // Cria um StringBuilder para passarmos os bytes gerados para ele
            StringBuilder strBuilder = new StringBuilder();

            // Converte cada byte em um valor hexadecimal e adiciona ao
            // string builder
            // and format each one as a hexadecimal string.
            for (int i = 0; i < valorCriptografado.Length; i++)
            {
                strBuilder.Append(valorCriptografado[i].ToString("x2"));
            }

            // retorna o valor criptografado como string
            return strBuilder.ToString();
        }


        public StringBuilder GerarConteudoArquivoCadastro()
        {
            StringBuilder strSaida = new StringBuilder();

            foreach (var item in arrBENotas)
            {
                var objBENotaFiscal = (from x in arrBENotaFiscal where x.cli_id.Equals(item.cli_id) select x).FirstOrDefault<ClienteBE>();
                objBENotaFiscal.Nota = item;
                if (objBENotaFiscal.Nota.not_chave == "")
                {
                    objBENotaFiscal.Nota.not_chave = new GerarChave().CarregarCampoChaveCodificacaoNF(objBENotaFiscal.cli_CGC, item.not_numero, item.not_totalliquido, item.not_dtEmissao);
                }
                StringBuilder strRegistro = new StringBuilder();

                //---- 1: CNPJ
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.cli_CGC, 14));

                //---- 2: IE
                strRegistro.Append(NegocioHelper.FormataCampoSaidaInscricaoEstadual(objBENotaFiscal.cli_incricaoEstadual, 14));

                //---- 3: Razão Social
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.cli_razaoSocial, 35));

                //---- 4: Logradouro
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_logradouro, 45));

                //---- 5: Número
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Endereco[0].end_numero, 5));

                //---- 6: Complemento
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_complemento, 15));

                //---- 7: CEP
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Endereco[0].end_cep, 8));

                //---- 8: Bairro
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_bairro, 15));

                //---- 9: Município
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_cidade, 30));

                //---- 10: UF
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_estado, 2));

                //---- 11: Telefone de Contato (com Código de Área). Campo alterado de 10 para 12 a partir de 01.07.12
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico((objBENotaFiscal.Contato[0].cont_ddd + objBENotaFiscal.Contato[0].cont_fone).Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Trim(), 12));

                //---- 12: Código de Identificação do Cliente / Consumidor
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.cli_id.ToString(), 12));

                //---- 13: Número terminal telefônico ou nro. da conta de consumo. Campo alterado de 10 para 12 a partir de 01.07.12
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 12));

                //---- 14: UF habilitação telefone
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 2));

                //--- Versão 2017.03. Inserido novos campos.

                //---- 15: Data de Emissão
                strRegistro.Append(NegocioHelper.FormataCampoSaidaData(objBENotaFiscal.Nota.not_dtEmissao));

                //---- 16: Modelo
                strRegistro.Append("21");

                //---- 17: Série
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("UNI", 3));

                //---- 18: Número
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_numero.ToString(), 9));

                //---- 19: Código do Munícipio
                strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.cli_CodCidIBGE, 7));

                //---- 20: Brancos - reservado para uso futuro
                strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 5));

                //---- 21: MD5 do Registro
                string strMD5NotaFiscalSemFormat = this.GerarMD5(strRegistro.ToString());
                strRegistro.Append(strMD5NotaFiscalSemFormat);

                //---- Adiciona o registro ao conteúdo de saída
                strSaida.Append(strRegistro.ToString());
                strSaida.Append(Environment.NewLine);

            }

            return strSaida;
        }

        public StringBuilder GerarConteudoArquivoItem()
        {
            StringBuilder strSaida = new StringBuilder();
            StringBuilder strRegistro;

            foreach (var item in arrBENotas)
            {
                var objBENotaFiscal = (from x in arrBENotaFiscal where x.cli_id.Equals(item.cli_id) select x).FirstOrDefault<ClienteBE>();
                objBENotaFiscal.Nota = item;
                if (objBENotaFiscal.Nota.not_chave == "")
                {
                    objBENotaFiscal.Nota.not_chave = new GerarChave().CarregarCampoChaveCodificacaoNF(objBENotaFiscal.cli_CGC, item.not_numero, item.not_totalliquido, item.not_dtEmissao);
                }
                int count = 1;
                foreach (var objBENotaFiscalItem in objBENotaFiscal.Nota.ItensNota)
                {
                    if (objBENotaFiscalItem.notI_total > 0)
                    {
                        strRegistro = new StringBuilder();

                        //---- 1: CNPJ
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.cli_CGC, 14));

                        //---- 2: UF
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_estado, 2));

                        //---- 3: Classe de Consumo 
                        strRegistro.Append("0");

                        //---- 4: Tipo de Utilização
                        strRegistro.Append("2");   //Comunicação de dados

                        //---- 5: Grupo de Tensão
                        strRegistro.Append("00");

                        //---- 6: Data de Emissão
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaData(objBENotaFiscal.Nota.not_dtEmissao));

                        //---- 7: Modelo
                        strRegistro.Append("21");

                        //---- 8: Série
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("UNI", 3));

                        //---- 9: Número
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_numero.ToString(), 9));

                        //---- 10: CFOP
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscal.cli_CFOP.ToString(), 4));

                        //---- 11: Item
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(count.ToString(), 3));

                        //---- 12: Código do serviço ou fornecimento
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 10));  //--- Vamos deixar em branco

                        //---- 13: Descrição do serviço ou fornecimento
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscalItem.notI_descricao, 40));

                        //---- 14: Código de Classificação do item
                        strRegistro.Append("0102");

                        //---- 15: Unidade
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 6));

                        //---- 16: Quantidade contratada (com 3 decimais)
                        //--- Versão 2017.03. Alterado de 11 posições para 12.
                        strRegistro.Append("000000001000");

                        //---- 17: Quantidade prestada ou fornecida (com 3 decimais)
                        //--- Versão 2017.03. Alterado de 11 posições para 12.
                        strRegistro.Append("000000001000");

                        //---- 18: Total (com 2 decimais)
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscalItem.notI_total.ToString(), 11));

                        //---- 19: Desconto (com 2 decimais)
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 20: Acréscimo e Despesa Acessórias
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 21: BC ICMS (com 2 decmais)
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor(objBENotaFiscalItem.notI_total.ToString(), 11));

                        //---- 22: ICMS
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 23: Operações Isentas ou não tributadas
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 24: Outros valores
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 25: Alíquota do ICMS
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 4));

                        //---- 26: Situação
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Nota.not_situacao == "" ? "N" : objBENotaFiscal.Nota.not_situacao, 1));

                        //---- 27: Ano e Mês de referência da apuração
                        strRegistro.Append(objBENotaFiscal.Nota.not_dtEmissao.ToString("yyMM"));

                        //--- Versão 2017.03. Novos campos (do 28 em diante) inseridos no layout.

                        //---- 28: Número do Contrato
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 15)); //--- Vamos deixar em branco

                        //---- 29: Quantidade faturada (com 3 decimais) - Será igual a qtde contratada e medida (= 1.000).
                        strRegistro.Append("000000001000");

                        //---- 30: Tarifa Aplicada / Preço Médio Efetivo
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 31: Alíquota PIS/PASEP (com 4 decimais)
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 6));

                        //---- 32: PIS/PASEP (com 2 decimais)
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 33: Alíquota COFINS (com 4 decimais)
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 6));

                        //---- 34: COFINS (com 2 decimais)
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 11));

                        //---- 35: Indicador de Desconto Judicial
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 1));  //--- Vamos deixar em branco

                        //---- 36: Tipo de Isenção/Redução de Base de Cálculo
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaValor("0", 2));

                        //---- 37: Brancos - Reservado para uso futuro
                        strRegistro.Append(NegocioHelper.FormataCampoSaidaAlfanumerico("", 5));

                        //---- 38: Código de Autenticação Digital do Registro
                        string strMD5NotaFiscalSemFormat = this.GerarMD5(strRegistro.ToString());
                        strRegistro.Append(strMD5NotaFiscalSemFormat);

                        //---- Adiciona o registro ao conteúdo de saída
                        strSaida.Append(strRegistro.ToString());
                        strSaida.Append(Environment.NewLine);
                        count++;
                    }

                }
            }

            return strSaida;
        }

        public StringBuilder GerarConteudoArquivoXMLGWMNetServicos(string notas) {

            List<ClienteBE> arrBENotaFiscal = new ClienteBLL().SelectBuscaNotasInNotIdLista(notas, conf_id, true);
            StringBuilder strSaida = new StringBuilder();
            var quote = (char)34;

            strSaida.Append(string.Concat("<?xml version=", quote, "1.0", quote, " encoding=", quote, "UTF-8", quote, "?>"));
            strSaida.Append(Environment.NewLine);
            strSaida.Append(string.Concat("<EnviarLoteRpsEnvio xmlns=", quote, "http://www.ginfes.com.br/servico_enviar_lote_rps_envio", quote, " xmlns:tip=", quote, "http://www.ginfes.com.br/tipos", quote, ">"));
            strSaida.Append(Environment.NewLine);
            strSaida.Append(string.Concat("  <NumeroLote>", arrBENotaFiscal[0].Nota.not_dtEmissao.ToString("yy"), arrBENotaFiscal[0].Nota.not_dtEmissao.ToString("MM"), "</NumeroLote>"));
            strSaida.Append(Environment.NewLine);
            strSaida.Append("  <Cnpj>14829991000124</Cnpj>");
            strSaida.Append(Environment.NewLine);
            strSaida.Append("  <InscricaoMunicipal>41187</InscricaoMunicipal>");
            strSaida.Append(Environment.NewLine);
            strSaida.Append(string.Concat("  <QuantidadeRps>", arrBENotaFiscal.Count(), "</QuantidadeRps>"));//===============================================
            strSaida.Append(Environment.NewLine);
            strSaida.Append("  <ListaRps>");
            strSaida.Append(Environment.NewLine);

            foreach (var objBENotaFiscal in arrBENotaFiscal)
            {
                if (objBENotaFiscal.Nota != null && objBENotaFiscal.Endereco.Count > 0)
                {
                    StringBuilder strRegistro = new StringBuilder();//

                    strRegistro.Append("    <Rps>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:IdentificacaoRps>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("        <tip:Numero>{0}</tip:Numero>", objBENotaFiscal.Nota.not_numero.ToString()));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:Serie>NFSe</tip:Serie>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:Tipo>1</tip:Tipo>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      </tip:IdentificacaoRps>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("      <tip:DataEmissao>{0}</tip:DataEmissao>", objBENotaFiscal.Nota.not_dtEmissao.ToString("s")));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:NaturezaOperacao>1</tip:NaturezaOperacao>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:RegimeEspecialTributacao>6</tip:RegimeEspecialTributacao>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:OptanteSimplesNacional>2</tip:OptanteSimplesNacional>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:IncentivadorCultural>2</tip:IncentivadorCultural>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:Status>1</tip:Status>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:Servico>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:Valores>");
                    strRegistro.Append(Environment.NewLine);

                    strRegistro.Append(string.Format("          <tip:ValorServicos>{0}</tip:ValorServicos>", objBENotaFiscal.Nota.not_totalliquido.ToString("F2", new System.Globalization.CultureInfo("en-US"))));
                    strRegistro.Append(Environment.NewLine);

                    strRegistro.Append(string.Format("          <tip:ValorPis>{0}</tip:ValorPis>", objBENotaFiscal.Nota.not_pis.ToString("F2", new System.Globalization.CultureInfo("en-US"))));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:ValorCofins>{0}</tip:ValorCofins>", objBENotaFiscal.Nota.not_confins.ToString("F2", new System.Globalization.CultureInfo("en-US"))));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:ValorIr>{0}</tip:ValorIr>", objBENotaFiscal.Nota.not_irrf.ToString("F2", new System.Globalization.CultureInfo("en-US"))));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:ValorCsll>{0}</tip:ValorCsll>", objBENotaFiscal.Nota.not_cssl.ToString("F2", new System.Globalization.CultureInfo("en-US"))));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("          <tip:IssRetido>2</tip:IssRetido>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:ValorIss>{0}</tip:ValorIss>", objBENotaFiscal.Nota.not_irrf.ToString("F2", new System.Globalization.CultureInfo("en-US"))));//==============================================
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:BaseCalculo>{0}</tip:BaseCalculo>", objBENotaFiscal.Nota.not_totalliquido.ToString("F2", new System.Globalization.CultureInfo("en-US"))));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("          <tip:Aliquota>0.0300</tip:Aliquota>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:ValorLiquidoNfse>{0}</tip:ValorLiquidoNfse>", objBENotaFiscal.Nota.not_totalliquido.ToString("F2", new System.Globalization.CultureInfo("en-US"))));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("          <tip:ValorIssRetido>0.00</tip:ValorIssRetido>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        </tip:Valores>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:ItemListaServico>108</tip:ItemListaServico>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:CodigoTributacaoMunicipio>2706</tip:CodigoTributacaoMunicipio>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:Discriminacao>");

                    foreach (var item in objBENotaFiscal.Nota.ItensNota)
                    {
                        if (!String.IsNullOrEmpty(item.notI_descricao) && item.notI_descricao != "")
                        {
                            strRegistro.Append(item.notI_descricao.ToUpper());
                            if (item.notI_total > 0)
                            {
                                strRegistro.Append("    ");
                                strRegistro.Append(item.notI_total.ToString("C2", new System.Globalization.CultureInfo("pt-BR")));
                            }
                            strRegistro.Append(Environment.NewLine);
                        }
                    }

                    //For Each objBENotaFiscalItem As BENotaFiscalItem In objBENotaFiscal.NotaFiscalItem

                    //    If objBENotaFiscalItem.intNumeriItemNF > 0 Then
                    //    End If

                    //    If Not String.IsNullOrEmpty(objBENotaFiscalItem.strDescricao) AndAlso objBENotaFiscalItem.strDescricao <> " " Then
                    //        strRegistro.Append(objBENotaFiscalItem.strDescricao.ToUpper)
                    //        If objBENotaFiscalItem.dblValorItem > 0 Then
                    //            strRegistro.Append("    ")
                    //            strRegistro.Append(objBENotaFiscalItem.dblValorItem.ToString("C2", new System.Globalization.CultureInfo("pt-BR")))
                    //        End If
                    //        strRegistro.Append(Environment.NewLine)
                    //    End If

                    //Next

                    strRegistro.Append("        </tip:Discriminacao>");
                    strRegistro.Append(Environment.NewLine);

                    strRegistro.Append("        <tip:MunicipioPrestacaoServico>3529401</tip:MunicipioPrestacaoServico>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      </tip:Servico>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:Prestador>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:Cnpj>14829991000124</tip:Cnpj>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:InscricaoMunicipal>41187</tip:InscricaoMunicipal>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      </tip:Prestador>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      <tip:Tomador>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:IdentificacaoTomador>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("          <tip:CpfCnpj>");
                    strRegistro.Append(Environment.NewLine);

                    strRegistro.Append(string.Format("            <tip:Cpf>{0}</tip:Cpf>", objBENotaFiscal.cli_CGC));

                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("          </tip:CpfCnpj>");
                    strRegistro.Append(Environment.NewLine);

                    if (objBENotaFiscal.cli_incricaoEstadual != "")
                    {
                        strRegistro.Append(string.Format("          <tip:InscricaoMunicipal>{0}</tip:InscricaoMunicipal>", objBENotaFiscal.cli_incricaoEstadual));
                        strRegistro.Append(Environment.NewLine);
                    }

                    strRegistro.Append("        </tip:IdentificacaoTomador>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("        <tip:RazaoSocial>{0}</tip:RazaoSocial>", objBENotaFiscal.cli_nomeFantasia.ToUpper()));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:Endereco>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:Endereco>{0}</tip:Endereco>", objBENotaFiscal.Endereco[0].end_logradouro.ToUpper()));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:Numero>{0}</tip:Numero>", objBENotaFiscal.Endereco[0].end_numero.ToUpper()));

                    if (objBENotaFiscal.Endereco[0].end_complemento != null && objBENotaFiscal.Endereco[0].end_complemento != "")
                    {
                        strRegistro.Append(Environment.NewLine);
                        strRegistro.Append(string.Format("          <tip:Complemento>{0}</tip:Complemento>", objBENotaFiscal.Endereco[0].end_complemento.ToUpper()));
                    }

                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:Bairro>{0}</tip:Bairro>", objBENotaFiscal.Endereco[0].end_bairro.ToUpper()));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:Cidade>{0}</tip:Cidade>", objBENotaFiscal.cli_CodCidIBGE));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:Estado>{0}</tip:Estado>", objBENotaFiscal.Endereco[0].end_estado.ToUpper()));
                    strRegistro.Append(Environment.NewLine);

                    strRegistro.Append(string.Format("          <tip:Cep>{0}</tip:Cep>", objBENotaFiscal.Endereco[0].end_cep.Replace("-", "")));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        </tip:Endereco>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        <tip:Contato>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append(string.Format("          <tip:Email>{0}</tip:Email>", objBENotaFiscal.Contato[0].cont_email));
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("        </tip:Contato>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("      </tip:Tomador>");
                    strRegistro.Append(Environment.NewLine);
                    strRegistro.Append("    </Rps>");
                    strRegistro.Append(Environment.NewLine);

                    //---- Adiciona o registro ao conteúdo de saída
                    strSaida.Append(strRegistro.ToString());
                }
            }
            strSaida.Append("  </ListaRps>");
            strSaida.Append(Environment.NewLine);
            strSaida.Append("</EnviarLoteRpsEnvio>");

            return strSaida;
        }
    }
}
*/