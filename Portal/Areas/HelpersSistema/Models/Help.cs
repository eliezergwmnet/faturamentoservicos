using System;

namespace HelpersSistema.Models
{
    public class Help
    {
        public bool CriarPasta()
        {
            return true;
        }

        public string CriarArquivos(string pasta, string nome, string conteudo)
        {
            try
            {
                //File.CreateText(pasta)

                return nome;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static string CriarTituloPage()
        {
            string retorno = "";
            retorno += "/*\n";
            retorno += "********************************************\n";
            retorno += "********************************************\n";
            retorno += "********************************************\n";
            retorno += "*** DESENVOLVIDO POR LEANDRO MARTINS *******\n";
            retorno += "*** PLATAFORMA DE CRIAÇÃO DE TELAS**********\n";
            retorno += "********************************************\n";
            retorno += "********************************************\n";
            retorno += "********************************************\n";
            retorno += "*/\n\n\n";
            return retorno;
        }
    }
}