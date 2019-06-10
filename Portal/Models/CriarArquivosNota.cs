//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using Portal.BE;
//using Portal.BLL;
//using Portal.Helper;

//namespace Portal.Models
//{
//    public class CriarArquivosNota
//    {
//        List<NotasPendentesBE> arrBENotaFiscal;
//        List<NotasPendentesChaveBE> arrBENotaFiscal2;
//        public void CarregarCampoChaveCodificacaoNF()
//        {
//            foreach (var objBENotaFiscal in arrBENotaFiscal)
//            {
//                string strEntrada = objBENotaFiscal.strCNPJ;
//            strEntrada = String.Concat(strEntrada, Double.Parse(objBENotaFiscal.strNumeroNF).ToString("000000000"))
//            strEntrada = String.Concat(strEntrada, Utilitario.Negocio.FormataCampoSaidaValor(objBENotaFiscal.dblValorTotalNF, 12))
//            strEntrada = String.Concat(strEntrada, Utilitario.Negocio.FormataCampoSaidaValor(objBENotaFiscal.dblValorTotalNF, 12))
//            strEntrada = String.Concat(strEntrada, Double.Parse("0").ToString("000000000000"))

//            '--- Versão 2017.03. Acrescentado os campos 09 (Data Emissão, 8 posições) e 27 (CNPJ do Emitente, 14 posições) na composição do MD5.
//            strEntrada = String.Concat(strEntrada, Negocio.FormataCampoSaidaData(objBENotaFiscal.dteEmissao))
//            strEntrada = String.Concat(strEntrada, "03031402000141")

//            If strEntrada.Length <> 81 Then Throw New Exception("String de Entrada para cálculo do MD5 inválida")

//            Dim strSaida As String = CriptografiaMD5.MD5.GerarMD5(strEntrada)

//            If strSaida.Length < 32 Then Throw New Exception("Chave de Codificação MD5 inválida!")

//            objBENotaFiscal.strChaveCodificacaoNotaFiscal = String.Concat(strSaida.Substring(0, 4), ".", strSaida.Substring(4, 4), ".", strSaida.Substring(8, 4), ".", strSaida.Substring(12, 4), ".", strSaida.Substring(16, 4), ".", strSaida.Substring(20, 4), ".", strSaida.Substring(24, 4), ".", strSaida.Substring(28, 4))

//            }
//        }
//        public StringBuilder GerarConteudoArquivoMestre() {

//            arrBENotaFiscal = new NotasPendentesBLL().Select();
//            Me.CarregarCampoChaveCodificacaoNF(arrBENotaFiscal)

//        Dim strSaida As New Text.StringBuilder()

//        For Each objBENotaFiscal As BENotaFiscal In arrBENotaFiscal

//            Dim strRegistro As New Text.StringBuilder("")

//            '---- 1: CNPJ
//            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Cliente.strCNPJ, 14))

//            '---- 2: IE
//            strRegistro.Append(Negocio.FormataCampoSaidaInscricaoEstadual(objBENotaFiscal.Cliente.strInscricaoEstadual, 14))

//            '---- 3: Razão Social
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.strNomeCliente, 35))

//            '---- 4: UF
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.Endereco.strUF, 2))

//            '---- 5: Tipo de Assinante
//            strRegistro.Append("0")    'Comercial/Industrial

//            '---- 6: Tipo de Utilização
//            strRegistro.Append("2")    'Comunicação de dados

//            '---- 7: Grupo de Tensão
//            strRegistro.Append("00")

//            '---- 8: Código de Identificação do consumidor ou assinante
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.strCodigo, 12))

//            '---- 9: Data de Emissão
//            strRegistro.Append(Negocio.FormataCampoSaidaData(objBENotaFiscal.dteEmissao))

//            '---- 10: Modelo
//            strRegistro.Append("21")

//            '---- 11: Série
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("UNI", 3))

//            '---- 12: Número
//            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.strNumeroNF, 9))

//            '---- 13: MD5 da NF
//            Dim strMD5NotaFiscalSemFormat As String = objBENotaFiscal.strChaveCodificacaoNotaFiscal.Replace(".", "")
//            strRegistro.Append(strMD5NotaFiscalSemFormat)

//            '---- 14: Valor Total
//            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.dblValorTotalNF, 12))

//            '---- 15: BC ICMS
//            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.dblValorTotalNF, 12))

//            '---- 16: ICMS destacado
//            strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 12))

//            '---- 17: Operações Isentas ou não tributadas
//            strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 12))

//            '---- 18: Outros valores
//            strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 12))

//            '---- 19: Situação do documento
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.strSituacaoDocumento, 1))

//            '---- 20: Ano Mes ref. apuração
//            strRegistro.Append(objBENotaFiscal.dteEmissao.ToString("yyMM")) '--- Telecomunicação refere-se a Data de Emissão

//            '---- 21: Referência ao item da NF. 
//            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.intPosicaoReferenciaItemNotaFiscal, 9)) '-- Posição do registro do 1º item de detalhe da Nota Fiscal

//            '---- 22: Nro. terminal telefônico ou nro. conta de consumo. Campo alterado de 10 para 12 a partir de 01.07.12
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 12))

//            '--- Versão 2017.03. Inclusão de novos campos

//            '---- 23: Indicação do tipo de informação
//            strRegistro.Append("1")

//            '---- 24: Tipo de cliente
//            Select Case objBENotaFiscal.strCFOP
//                Case "5.301", "6.301"
//                    strRegistro.Append("06")

//                Case "5.302", "6.302"
//                    strRegistro.Append("02")

//                Case "5.303", "6.303"
//                    strRegistro.Append("01")

//                Case Else
//                    strRegistro.Append("99")
//            End Select


//            '---- 25: Subclasse de consumo
//            strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 2))

//            '---- 26: Número do terminal telefônico principal
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 12))

//            '---- 27: CNPJ do emitente
//            strRegistro.Append("03031402000141")

//            '---- 28: Número ou código da fatura comercial 
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 20))

//            '---- 29: Valor total da fatura comercial
//            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.dblValorTotalNF, 12))

//            '---- 30: Data da leitura anterior
//            strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 8))

//            '---- 31: Data de leitura atual
//            strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 8))

//            '---- 32: Brancos - uso futuro
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 50))

//            '---- 33: Brancos - uso futuro
//            strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 8))

//            '---- 34: Informações adicionais
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 30))

//            '---- 35: Brancos - uso futuro
//            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 5))

//            '---- 36: Autenticação Digital do registro. Código MD5 do registro
//            objBENotaFiscal.strChaveCodificacaoRegistroMestre = CriptografiaMD5.MD5.GerarMD5(strRegistro.ToString)
//            strRegistro.Append(objBENotaFiscal.strChaveCodificacaoRegistroMestre)

//            '---- Adiciona o registro ao conteúdo de saída
//            strSaida.Append(strRegistro.ToString)
//            strSaida.Append(Environment.NewLine)
//        Next

//        Return strSaida


//    }
//}


////namespace Portal.Models
////{
////    public class CriarArquivosNota
////    {
////        public string btnExportarArquivoCompleto() {
////            try
////            {

////            Dim objBONotaFiscal As BONotaFiscal = New BONotaFiscal(_strCaminhoCompletoMDB)
////            Dim strDados As Text.StringBuilder

////            '------------------------------------------------------------
////            Dim strNomeDiretorioSaida As String = String.Concat(DateTime.Now.Hour.ToString, DateTime.Now.Minute.ToString, DateTime.Now.Second.ToString, DateTime.Now.Millisecond.ToString, HttpContext.Current.Session.SessionID.ToUpper)

////            Dim strCaminhoCompletoDiretorioSaida As String = String.Concat(HttpContext.Current.Server.MapPath("../DownloadTemp/"), strNomeDiretorioSaida)
////            Dim strArquivoZip As String = String.Concat(HttpContext.Current.Server.MapPath("../DownloadTemp/"), strNomeDiretorioSaida, ".zip")
////            Dim strArquivoZipURL As String = String.Concat("../DownloadTemp/", strNomeDiretorioSaida, ".zip")
////            Dim strArquivoSaida As String = ""

////            '--- Cria diretório
////            If IO.Directory.Exists(strCaminhoCompletoDiretorioSaida) Then IO.Directory.Delete(strCaminhoCompletoDiretorioSaida)

////            Dim objDiretorio As DirectoryInfo = System.IO.Directory.CreateDirectory(strCaminhoCompletoDiretorioSaida)

////            'Abre o arquivo zip, se ele existe, será sobrescito
////            Using objZIP As Package = ZipPackage.Open(strArquivoZip, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)

////                '--- Mestre
////                strDados = objBONotaFiscal.GerarConteudoArquivoMestre()
////                strArquivoSaida = System.IO.Path.Combine(objDiretorio.FullName, objBONotaFiscal.ConsultarNomeArquivoSaida(hdnMes.Value, hdnAno.Value, "M"))
////                Funcao.ExportarConteudoToTXT(Me.Page, strDados, strArquivoSaida, "ISO-8859-1")
////                Funcao.AdicionarArquivoNoZip(objZIP, strArquivoSaida)

////                '--- Dados Cadastrais
////                strDados = objBONotaFiscal.GerarConteudoArquivoCadastro()
////                strArquivoSaida = System.IO.Path.Combine(objDiretorio.FullName, objBONotaFiscal.ConsultarNomeArquivoSaida(hdnMes.Value, hdnAno.Value, "D"))
////                Funcao.ExportarConteudoToTXT(Me.Page, strDados, strArquivoSaida, "ISO-8859-1")
////                Funcao.AdicionarArquivoNoZip(objZIP, strArquivoSaida)

////                '--- Item
////                strDados = objBONotaFiscal.GerarConteudoArquivoItem()
////                strArquivoSaida = System.IO.Path.Combine(objDiretorio.FullName, objBONotaFiscal.ConsultarNomeArquivoSaida(hdnMes.Value, hdnAno.Value, "I"))
////                Funcao.ExportarConteudoToTXT(Me.Page, strDados, strArquivoSaida, "ISO-8859-1")
////                Funcao.AdicionarArquivoNoZip(objZIP, strArquivoSaida)

////                '--- Fecha o arquivo zip
////                objZIP.Close()

////            End Using
////            '---------------------------------

////            Funcao.GetScriptManager(Me.Page).AddScript(String.Format("location.href = '{0}';", strArquivoZipURL))
////}
////            catch(InvalidOperationException ex)
////            {
////                Common.TratarLogErro(ex);
////            }
////            catch(Exception ex)
////            {
////                Common.TratarLogErro(ex);
////            }
////    }
////        public string GerarConteudoArquivoMestre(List<ClienteBE> arrBENotaFiscal)
////        {
////            var Negocio = new NegocioHelper();
////            var empresa = new EmpresaBLL().SelectId(new EmpresaBE());
////            StringBuilder strSaida = new StringBuilder();

////            foreach (var objBENotaFiscal in arrBENotaFiscal)
////            {
////                StringBuilder strRegistro = new StringBuilder();

////                //----1: CNPJ
////                strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.cli_CGC, 14));

////                //---- 2: IE
////                strRegistro.Append(Negocio.FormataCampoSaidaInscricaoEstadual(objBENotaFiscal.cli_tipoInscricao, 14));

////                //---- 3: Razão Social
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.cli_razaoSocial, 35));

////                //---- 4: UF
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Endereco[0].end_estado, 2));

////                //---- 5: Tipo de Assinante
////                strRegistro.Append("0");   //Comercial/Industrial

////                //---- 6: Tipo de Utilização
////                strRegistro.Append("2");   //Comunicação de dados

////                //---- 7: Grupo de Tensão
////                strRegistro.Append("00");

////                //=================================
////                //=================================
////                //=================================
////                //---- 8: Código de Identificação do consumidor ou assinante
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.cli_CodCidIBGE, 12));
////                //=================================
////                //=================================
////                //=================================

////                //---- 9: Data de Emissão
////                strRegistro.Append(Negocio.FormataCampoSaidaData(objBENotaFiscal.Nota.not_dtEmissao));

////                //---- 10: Modelo
////                strRegistro.Append("21");

////                //---- 11: Série
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("UNI", 3));

////                //---- 12: Número
////                strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_numero.ToString(), 9));

////                //---- 13: MD5 da NF
////                strRegistro.Append(objBENotaFiscal.Nota.not_chave);

////                //---- 14: Valor Total
////                strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_totalliquido.ToString(), 12));

////                //---- 15: BC ICMS
////                strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_totalliquido.ToString(), 12));

////                //---- 16: ICMS destacado
////                strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 12));

////                //---- 17: Operações Isentas ou não tributadas
////                strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 12));

////                //---- 18: Outros valores
////                strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 12));

////                //---- 19: Situação do documento
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Nota.not_situacao, 1));

////                //---- 20: Ano Mes ref. apuração
////                strRegistro.Append(objBENotaFiscal.Nota.not_dtEmissao.ToString("yyMM")); //--- Telecomunicação refere-se a Data de Emissão

////                //=================================
////                //=================================
////                //=================================
////                //---- 21: Referência ao item da NF. 
////                strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_numero.ToString(), 9)); //-- Posição do registro do 1º item de detalhe da Nota Fiscal
////                //=================================
////                //=================================
////                //=================================

////                //---- 22: Nro. terminal telefônico ou nro. conta de consumo. Campo alterado de 10 para 12 a partir de 01.07.12
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 12));

////                //--- Versão 2017.03. Inclusão de novos campos

////                //---- 23: Indicação do tipo de informação
////                strRegistro.Append("1");

////                //---- 24: Tipo de cliente
////                switch (objBENotaFiscal.cli_CFOP.ToString()) {
////                    case "5.301":
////                        "6.301":
////                        strRegistro.Append("06");
////                        break;

////                    case "5.302":
////                        "6.302":
////                    strRegistro.Append("02");
////                        break;

////                    case "5.303":
////                        "6.303":
////                        strRegistro.Append("01");
////                        break;

////                    default:
////                        strRegistro.Append("99");
////                }


////                //---- 25: Subclasse de consumo
////                strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 2));

////                //---- 26: Número do terminal telefônico principal
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 12));

////                //---- 27: CNPJ do emitente
////                strRegistro.Append("03031402000141");

////                //---- 28: Número ou código da fatura comercial 
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 20));

////                //---- 29: Valor total da fatura comercial
////                strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Nota.not_totalliquido.ToString(), 12));

////                //---- 30: Data da leitura anterior
////                strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 8));

////                //---- 31: Data de leitura atual
////                strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 8));

////                //---- 32: Brancos - uso futuro
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 50));

////                //---- 33: Brancos - uso futuro
////                strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 8));

////                //---- 34: Informações adicionais
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 30));

////                //---- 35: Brancos - uso futuro
////                strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 5));

////                //=================================
////                //=================================
////                //=================================

////                //---- 36: Autenticação Digital do registro. Código MD5 do registro
////                strRegistro.Append(objBENotaFiscal.Nota.not_chave);
////                //=================================
////                //=================================
////                //=================================

////                //---- Adiciona o registro ao conteúdo de saída
////                strSaida.Append(strRegistro.ToString());

////                strSaida.Append(Environment.NewLine);

////            }
////            return strSaida.ToString();
////        }
////        Public Function GerarConteudoArquivoCadastro() As Text.StringBuilder


////    Dim arrBENotaFiscal As List(Of BENotaFiscal) = Me.ListarNotaFiscalSCMComItem()

////        Dim strSaida As New Text.StringBuilder()

////        For Each objBENotaFiscal As BENotaFiscal In arrBENotaFiscal

////            If objBENotaFiscal.Cliente Is Nothing Then Throw New Exception("Cliente não encontrado!")
////            If objBENotaFiscal.Cliente.Endereco Is Nothing Then Throw New Exception(String.Format("Endereço do Cliente {0} não encontrado!", objBENotaFiscal.Cliente.strNomeCliente))

////            Dim strRegistro As New Text.StringBuilder("")

////            '---- 1: CNPJ
////            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Cliente.strCNPJ, 14))

////            '---- 2: IE
////            strRegistro.Append(Negocio.FormataCampoSaidaInscricaoEstadual(objBENotaFiscal.Cliente.strInscricaoEstadual, 14))

////            '---- 3: Razão Social
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.strNomeCliente, 35))

////            '---- 4: Logradouro
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.Endereco.strLogradouro, 45))

////            '---- 5: Número
////            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Cliente.Endereco.strNumero, 5))

////            '---- 6: Complemento
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.Endereco.strComplemento, 15))

////            '---- 7: CEP
////            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Cliente.Endereco.strCEP, 8))

////            '---- 8: Bairro
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.Endereco.strBairro, 15))

////            '---- 9: Município
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.Endereco.strCidade, 30))

////            '---- 10: UF
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.Endereco.strUF, 2))

////            '---- 11: Telefone de Contato (com Código de Área). Campo alterado de 10 para 12 a partir de 01.07.12
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(String.Concat(objBENotaFiscal.Cliente.strCodigoAreaTelefone, objBENotaFiscal.Cliente.strTelefone), 12))

////            '---- 12: Código de Identificação do Cliente / Consumidor
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.strCodigo, 12))

////            '---- 13: Número terminal telefônico ou nro. da conta de consumo. Campo alterado de 10 para 12 a partir de 01.07.12
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 12))

////            '---- 14: UF habilitação telefone
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 2))

////            '--- Versão 2017.03. Inserido novos campos.

////            '---- 15: Data de Emissão
////            strRegistro.Append(Negocio.FormataCampoSaidaData(objBENotaFiscal.dteEmissao))

////            '---- 16: Modelo
////            strRegistro.Append("21")

////            '---- 17: Série
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("UNI", 3))

////            '---- 18: Número
////            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.strNumeroNF, 9))

////            '---- 19: Código do Munícipio
////            strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Cliente.Endereco.strCodigoCidadeIBGE, 7))

////            '---- 20: Brancos - reservado para uso futuro
////            strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 5))

////            '---- 21: MD5 do Registro
////            objBENotaFiscal.strChaveCodificacaoRegistroCadastro = CriptografiaMD5.MD5.GerarMD5(strRegistro.ToString)
////            strRegistro.Append(objBENotaFiscal.strChaveCodificacaoRegistroCadastro)

////            '---- Adiciona o registro ao conteúdo de saída
////            strSaida.Append(strRegistro.ToString)
////            strSaida.Append(Environment.NewLine)
////        Next

////        Return strSaida
////    End Function

////        Public Function GerarConteudoArquivoItem() As Text.StringBuilder

////        Dim arrBENotaFiscal As List(Of BENotaFiscal) = Me.ListarNotaFiscalSCMComItem()
////        Me.CarregarCampoChaveCodificacaoNF(arrBENotaFiscal)

////        Dim strSaida As New Text.StringBuilder()

////        Dim strRegistro As Text.StringBuilder

////        For Each objBENotaFiscal As BENotaFiscal In arrBENotaFiscal

////            For Each objBENotaFiscalItem As BENotaFiscalItem In objBENotaFiscal.NotaFiscalItem

////                If objBENotaFiscalItem.intNumeriItemNF > 0 Then

////                    strRegistro = New Text.StringBuilder("")

////                    '---- 1: CNPJ
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.Cliente.strCNPJ, 14))

////                    '---- 2: UF
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.Cliente.Endereco.strUF, 2))

////                    '---- 3: Classe de Consumo 
////                    strRegistro.Append("0")

////                    '---- 4: Tipo de Utilização
////                    strRegistro.Append("2")    'Comunicação de dados

////                    '---- 5: Grupo de Tensão
////                    strRegistro.Append("00")

////                    '---- 6: Data de Emissão
////                    strRegistro.Append(Negocio.FormataCampoSaidaData(objBENotaFiscal.dteEmissao))

////                    '---- 7: Modelo
////                    strRegistro.Append("21")

////                    '---- 8: Série
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("UNI", 3))

////                    '---- 9: Número
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.strNumeroNF, 9))

////                    '---- 10: CFOP
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscal.strCFOP, 4))

////                    '---- 11: Item
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscalItem.intNumeriItemNF, 3))

////                    '---- 12: Código do serviço ou fornecimento
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 10))  '--- Vamos deixar em branco

////                    '---- 13: Descrição do serviço ou fornecimento
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscalItem.strDescricao, 40))

////                    '---- 14: Código de Classificação do item
////                    strRegistro.Append("0102")

////                    '---- 15: Unidade
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 6))

////                    '---- 16: Quantidade contratada (com 3 decimais)
////                    '--- Versão 2017.03. Alterado de 11 posições para 12.
////                    strRegistro.Append("000000001000")

////                    '---- 17: Quantidade prestada ou fornecida (com 3 decimais)
////                    '--- Versão 2017.03. Alterado de 11 posições para 12.
////                    strRegistro.Append("000000001000")

////                    '---- 18: Total (com 2 decimais)
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscalItem.dblValorItem, 11))

////                    '---- 19: Desconto (com 2 decimais)
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 20: Acréscimo e Despesa Acessórias
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 21: BC ICMS (com 2 decmais)
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor(objBENotaFiscalItem.dblValorItem, 11))

////                    '---- 22: ICMS
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 23: Operações Isentas ou não tributadas
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 24: Outros valores
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 25: Alíquota do ICMS
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 4))

////                    '---- 26: Situação
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico(objBENotaFiscal.strSituacaoDocumento, 1))

////                    '---- 27: Ano e Mês de referência da apuração
////                    strRegistro.Append(objBENotaFiscal.dteEmissao.ToString("yyMM"))

////                    '--- Versão 2017.03. Novos campos (do 28 em diante) inseridos no layout.

////                    '---- 28: Número do Contrato
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 15))  '--- Vamos deixar em branco

////                    '---- 29: Quantidade faturada (com 3 decimais) - Será igual a qtde contratada e medida (= 1.000).
////                    strRegistro.Append("000000001000")

////                    '---- 30: Tarifa Aplicada / Preço Médio Efetivo
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 31: Alíquota PIS/PASEP (com 4 decimais)
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 6))

////                    '---- 32: PIS/PASEP (com 2 decimais)
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 33: Alíquota COFINS (com 4 decimais)
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 6))

////                    '---- 34: COFINS (com 2 decimais)
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////                    '---- 35: Indicador de Desconto Judicial
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 1))  '--- Vamos deixar em branco

////                    '---- 36: Tipo de Isenção/Redução de Base de Cálculo
////                    strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 2))

////                    '---- 37: Brancos - Reservado para uso futuro
////                    strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 5))

////                    '---- 38: Código de Autenticação Digital do Registro
////                    objBENotaFiscal.strChaveCodificacaoRegistroItem = CriptografiaMD5.MD5.GerarMD5(strRegistro.ToString)
////                    strRegistro.Append(objBENotaFiscal.strChaveCodificacaoRegistroItem)

////                    '---- Adiciona o registro ao conteúdo de saída
////                    strSaida.Append(strRegistro.ToString)
////                    strSaida.Append(Environment.NewLine)

////                End If
////            Next
////        Next

////        ''---- Adicionando Registro Extra em decorrência da opção pelo SIMPLES Nacional.
////        'strRegistro = New Text.StringBuilder("")

////        ''---- 1: CNPJ
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 14))

////        ''---- 2: UF
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 2))

////        ''---- 3: Classe de Consumo ou Tipo de Assinante
////        'strRegistro.Append("0")

////        ''---- 4: Tipo de Utilização
////        'strRegistro.Append("0")

////        ''---- 5: Grupo de Tensão
////        'strRegistro.Append("00")

////        ''---- 6: Data de Emissão
////        'strRegistro.Append("00000000")

////        ''---- 7: Modelo
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 2))

////        ''---- 8: Série
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 3))

////        ''---- 9: Número
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor(0, 9))

////        ''---- 10: CFOP
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor(0, 4))

////        ''---- 11: Item
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor(0, 3))

////        ''---- 12: Código do serviço ou fornecimento
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 10))

////        ''---- 13: Descrição do serviço ou fornecimento
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("OPTANTE SN - ALIQUOTA 00,00", 40))

////        ''---- 14: Código de Classificação do item
////        'strRegistro.Append("0000")

////        ''---- 15: Unidade
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 6))

////        ''---- 16: Quantidade contratada (com 3 decimais)
////        ''--- Versão 2017.03. Alterado de 11 posições para 12.
////        'strRegistro.Append("000000000000")

////        ''---- 17: Quantidade prestada ou fornecida(com 3 decimais)
////        ''--- Versão 2017.03. Alterado de 11 posições para 12.
////        'strRegistro.Append("000000000000")

////        ''---- 18: Total(com 2 decimais)
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor(0, 11))

////        ''---- 19: Desconto(com 2 decimais)
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 20: Acréscimo e Despesa Acessórias
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 21: BC ICMS(com 2 decmais)
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 22: ICMS
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 23: Operações Isentas ou não tributadas
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 24: Outros valores
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 25: Alíquota do ICMS
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 4))

////        ''---- 26: Situação
////        'strRegistro.Append(" ")

////        ''---- 27: Ano e Mês de referência da apuração
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 4))

////        ''--- Versão 2017.03. Novos campos(do 28 em diante) inseridos no layout.

////        ''---- 28: Número do Contrato
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 15))  '--- Vamos deixar em branco

////        ''---- 29: Quantidade faturada(com 3 decimais) - Será igual a qtde contratada e medida(= 1.000).
////        'strRegistro.Append("000000000000")

////        ''---- 30: Tarifa Aplicada / Preço Médio Efetivo
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 31: Alíquota PIS/PASEP(com 4 decimais)
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 6))

////        ''---- 32: PIS/PASEP(com 2 decimais)
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 33: Alíquota COFINS(com 4 decimais)
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 6))

////        ''---- 34: COFINS(com 2 decimais)
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 11))

////        ''---- 35: Indicador de Desconto Judicial
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 1))  '--- Vamos deixar em branco

////        ''---- 36: Tipo de Isenção/Redução de Base de Cálculo
////        'strRegistro.Append(Negocio.FormataCampoSaidaValor("0", 2))

////        ''---- 37: Brancos - Reservado para uso futuro
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 5))

////        ''---- 38: Código de Autenticação Digital do Registro
////        ''---- Código MD5 do registro
////        'strRegistro.Append(Negocio.FormataCampoSaidaAlfanumerico("", 32))

////        ''---- Adiciona o registro ao conteúdo de saída
////        'strSaida.Append(strRegistro.ToString)
////        'strSaida.Append(Environment.NewLine)

////        Return strSaida
////    End Function
////    }
////}