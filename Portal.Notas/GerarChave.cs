/*using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Portal.BE;

namespace Portal.Notas
{
    public class GerarChave
    {
        public string CarregarCampoChaveCodificacaoNF(ClienteNotasContratoBE obj)
        {
            string strEntrada = obj.cli_CGC.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "").Trim();
            strEntrada = string.Concat(strEntrada, Double.Parse(obj.Nota.not_numero.ToString()).ToString("000000000"));
            strEntrada = string.Concat(strEntrada, this.FormataCampoSaidaValor(obj.Nota.not_totalliquido, 12));
            strEntrada = string.Concat(strEntrada, this.FormataCampoSaidaValor(obj.Nota.not_totalliquido, 12));
            strEntrada = string.Concat(strEntrada, Double.Parse("0").ToString("000000000000"));

            //--- Versão 2017.03. Acrescentado os campos 09 (Data Emissão, 8 posições) e 27 (CNPJ do Emitente, 14 posições) na composição do MD5.
            strEntrada = string.Concat(strEntrada, this.FormataCampoSaidaData(obj.Nota.not_dtEmissao));
            strEntrada = string.Concat(strEntrada, "03031402000141");

            if (strEntrada.Length != 81) return ""; //throw new Exception("String de Entrada para cálculo do MD5 inválida");

            string strSaida = this.GerarMD5(strEntrada);

            if (strSaida.Length < 32) return "";// throw new Exception("Chave de Codificação MD5 inválida!");

            string retorno = String.Concat(strSaida.Substring(0, 4), ".", strSaida.Substring(4, 4), ".", strSaida.Substring(8, 4), ".", strSaida.Substring(12, 4), ".", strSaida.Substring(16, 4), ".", strSaida.Substring(20, 4), ".", strSaida.Substring(24, 4), ".", strSaida.Substring(28, 4));
            return retorno;
        }

        public string CarregarCampoChaveCodificacaoNF(string cli_CGC, int not_numero, decimal not_totalliquido, DateTime not_dtEmissao)
        {
            string strEntrada = cli_CGC.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "").Trim();
            strEntrada = string.Concat(strEntrada, Double.Parse(not_numero.ToString()).ToString("000000000"));
            strEntrada = string.Concat(strEntrada, this.FormataCampoSaidaValor(not_totalliquido, 12));
            strEntrada = string.Concat(strEntrada, this.FormataCampoSaidaValor(not_totalliquido, 12));
            strEntrada = string.Concat(strEntrada, Double.Parse("0").ToString("000000000000"));

            //--- Versão 2017.03. Acrescentado os campos 09 (Data Emissão, 8 posições) e 27 (CNPJ do Emitente, 14 posições) na composição do MD5.
            strEntrada = string.Concat(strEntrada, this.FormataCampoSaidaData(not_dtEmissao));
            strEntrada = string.Concat(strEntrada, "03031402000141");

            if (strEntrada.Length != 81) return ""; //throw new Exception("String de Entrada para cálculo do MD5 inválida");

            string strSaida = this.GerarMD5(strEntrada);

            if (strSaida.Length < 32) return "";// throw new Exception("Chave de Codificação MD5 inválida!");

            string retorno = String.Concat(strSaida.Substring(0, 4), ".", strSaida.Substring(4, 4), ".", strSaida.Substring(8, 4), ".", strSaida.Substring(12, 4), ".", strSaida.Substring(16, 4), ".", strSaida.Substring(20, 4), ".", strSaida.Substring(24, 4), ".", strSaida.Substring(28, 4));
            return retorno;
        }

        string FormataCampoSaidaValor(decimal dblValor, int intQtdeCasas) {
            string strSaida = dblValor.ToString("F");
            strSaida = strSaida.Replace(",", "");
            strSaida = strSaida.Replace(".", "");
            strSaida = strSaida.PadLeft(intQtdeCasas, '0');
            //strSaida = Right(strSaida, intQtdeCasas)
            return strSaida;
        }
        string FormataCampoSaidaData(DateTime dteEntrada) {
            return dteEntrada.ToString("yyyyMMdd");
        }

        public string GerarMD5(string valor)
        {
            System.Security.Cryptography.MD5 md5Hasher = System.Security.Cryptography.MD5.Create();
            byte[] valorCriptografado = md5Hasher.ComputeHash(Encoding.Default.GetBytes(valor));
            StringBuilder strBuilder = new StringBuilder();

            for (int i = 0; i < valorCriptografado.Length; i++)
                strBuilder.Append(valorCriptografado[i].ToString("x2"));

            return strBuilder.ToString();
        }
        public static string HashValue(string value)
        {
            try
            {
                byte[] Results;
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes("leandro"));
                TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;
                byte[] DataToEncrypt = UTF8.GetBytes(value);
                try
                {
                    ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                }
                finally
                {
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }
                return Convert.ToBase64String(Results);
                //return value;
            }
            catch (Exception ex)
            {
                // Se algum erro ocorrer, dispara a exceção            
                throw new ApplicationException("Erro ao criptografar", ex);
            }
        }

    }

}
*/