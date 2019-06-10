using System;

namespace Globais.Helper
{
    public class Log
    {

        public static void GravarLog(string strMensagem, string strCaminhoArquivo)
        {
            try
            {

                var end = String.Format(System.Web.Hosting.HostingEnvironment.MapPath("~\\{0}"), "LogErro");
                if (!System.IO.Directory.Exists(end))
                    System.IO.Directory.CreateDirectory(end);

                if(!System.IO.File.Exists(strCaminhoArquivo)){
                    System.IO.File.Create(strCaminhoArquivo);
                }

                System.IO.StreamWriter objLogFile = System.IO.File.AppendText(strCaminhoArquivo);

                //throw new Exception("Não foi possível salvar o log de erro no local especificado!");

                objLogFile.WriteLine(strMensagem);
                objLogFile.Flush();
                objLogFile.Close();
            }
            catch(Exception ex)
            {
                //throw new Exception("Não foi possível salvar o log de erro no local especificado!");
            }
        }
    }
}
