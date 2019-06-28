using System;
using System.Text;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Security.Cryptography;

namespace Globais.Helper
{

    public class Common
    {
        public bool Producao = false;
        public static int GetLoginID()
        {
            return 2;
        }

        public static bool AmbienteProducao
        {
            get
            {
               //if (HttpContext.Current.Request.Url.Host == "localhost" || HttpContext.Current.Request.Url.Host == "demofaturamento.gwmnet.net")
                   return true;
              // else
              //      return true;
            }
        }
        
        public static string UrlDados
        {
            get
            {
                return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + "/";//.Replace(HttpContext.Current.Request.Url.AbsolutePath, " /");
            }
        }

        public static string GetCaminhoAplicacao(string strDiretorioComplemento) {
            return String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), strDiretorioComplemento);
        }

        public static string CriarArquivos(string pasta, string nome, string conteudo)
        {
            var end = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), pasta);
            if (!System.IO.Directory.Exists(end))
                System.IO.Directory.CreateDirectory(end);

            string caminho = string.Format("{0}\\{1}", end, nome);
            if (System.IO.File.Exists(caminho))
                System.IO.File.Delete(caminho);

            /*System.IO.StreamWriter objLogFile = System.IO.File.AppendText(caminho);

            objLogFile.WriteLine(conteudo);
            objLogFile.Flush();
            objLogFile.Close();*/

            return caminho;
        }

        public static string RemoveCaracter(string input)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
                input = input.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());


            string caracteres = "'\"!@#$%¨&*()_+=§[{]}ªº;:|/*-+.";
            for (int i = 0; i < caracteres.Length; i++)
                input = input.Replace(caracteres[i].ToString(), "");

            return input;

        }
            
        public static void TratarLogErro(Exception exc)
        {
            //TO DO. Implementar
            StringBuilder strMensagem = new StringBuilder();
            strMensagem.Append(String.Format("#{0}| {1}| {2}| {3}", DateTime.Now.ToString(), exc.GetType(), exc.StackTrace, exc.Source));
            strMensagem.Append("| ");
            strMensagem.Append(exc.Message);
            strMensagem.Append("| ");
            strMensagem.Append(System.Web.Hosting.HostingEnvironment.SiteName);

            String strNomeArquivo = String.Format("{0}\\{1}.txt", Common.GetCaminhoAplicacao("LogErro"), DateTime.Today.ToString("yyyy-MM-dd"));
            Log.GravarLog(strMensagem.ToString(), strNomeArquivo);
        }


        /// <summary>
        /// Senha uma senha padrão para o cadastro de usuário
        /// </summary>
        /// <returns></returns>
        public static string GeradorDeSenha()
        {
            string caracteresPermitidos = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[Globais.TamanhoSenhaPadrao];
            Random rd = new Random();
            for (int i = 0; i < Globais.TamanhoSenhaPadrao; i++)
            {
                chars[i] = caracteresPermitidos[rd.Next(0, caracteresPermitidos.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// Gera a criptografia da senha
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string CriptografiaSenha(string value)
        {
            try
            {
                byte[] Results;
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Globais.ChaveAcessoSenha));
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

        public static string GetConnectionStringSqlServer(string conection)
        {
          //  if (Common.AmbienteProducao)
                return ConfigurationManager.ConnectionStrings[conection].ConnectionString;
          //  else
               // return ConfigurationManager.ConnectionStrings["SERVERHOSTHOMOLOG"].ConnectionString;
            //return "Data Source=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=faturamento2;Data Source=DESKTOP-K9TLL9M\\SQLEXPRESS;Pooling=True;Max Pool Size=10;";
        }


        public static int GetEmpresaSelecionada()
        {
            int conf_id = 9;
            try
            {
                conf_id = (int)HttpContext.Current.Session[Globais.SessionSistemaConfId];
            }
            catch(Exception ex)
            {

            }
            //return Convert.ToInt32(ConfigurationManager.AppSettings["idEmpresaSelecionada"]);
            return Convert.ToInt32(conf_id);
        }

        public static string GetNameMonth(int month)
        {
            string mesExtenso;

            //Mês em português por extenso
            //mesExtenso = new DateTime(1900, month, 1).ToString("MMMM", new System.Globalization.CultureInfo("pt-BR"));
            //Mês abreviado em português também.
            //mesExtenso = new System.Globalization.CultureInfo("pt-BR").DateTimeFormat.GetAbbreviatedMonthName(month);
            //Mês (int) por extenso com primeira letra maiúscula.
            string result = new System.Globalization.CultureInfo("pt-BR").DateTimeFormat.GetMonthName(month);
            mesExtenso = char.ToUpper(result[0]) + result.Substring(1);
            return mesExtenso;
        }

        public static string NomeStatusPagamentoBoleto(string nome){
            string result = "";
            switch (nome)
            {
                case "NOV":
                    result = "NOVO";
                    break;
                case "LIQ":
                    result = "LIQUITADO";
                    break;
                case "BAX":
                    result = "BAIXADO";
                    break;
                case "GET":
                    result = "GERADO";
                    break;
                case "ATR":
                    result = "ATRASADO";
                    break;
            }
            return result;
        }


        public static string NumChamado()
        {
            Random random = new Random();
            var num01 = DateTime.Now.Day.ToString().PadLeft(2, '0') + random.Next(001, 999).ToString().PadLeft(6, '0') + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0');

            return num01;
        }

        public static string ConvertXMLItem(XmlElement item, string nome)
        {
            try
            {
                string valor = "";
                if (item.GetElementsByTagName(nome).Count > 0)
                    valor = item.GetElementsByTagName(nome).Item(0).InnerText.ToString();

                return valor;
            }
            catch(Exception ex)
            {
                return "";
            }
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

        public static string CriptografarEmail(string valor)
        {
            string chaveCripto;
            Byte[] cript = System.Text.ASCIIEncoding.ASCII.GetBytes(valor);
            chaveCripto = Convert.ToBase64String(cript);
            return chaveCripto;


        }

        public static string DescriptografarEmail(string valor)
        {
            string chaveCripto;
            Byte[] cript = Convert.FromBase64String(valor);
            chaveCripto = System.Text.ASCIIEncoding.ASCII.GetString(cript);
            return chaveCripto;

        }
    }
}
