using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpersSistema.Models
{
    public class CriarClasses
    {
        string table;
        List<Table> colunas;
        public CriarClasses(string _table, List<Table> coln)
        {
            table = _table;
            if (_table != "")
                colunas = coln;
        }

        public string CriarClasse(string namespa)
        {
            bool log = false;
            string retorno = Help.CriarTituloPage();
            string tipo = "";

            foreach (var item in colunas)
            {
                if (item.Coluna == "log_id")
                    log = true;
            }

            if (log)
            {
                retorno += "using System;\n";
                retorno += "using Globais.BE;\n";
                retorno += "namespace " + table.Replace("tbl", "") + ".BE \n{";
                retorno += "\n\tpublic class " + table.Replace("tbl", "") + "BE: GlobaisLogBE {\n";
            }
            else
            {
                retorno += "using System;\n\n";
                retorno += "namespace " + table.Replace("tbl", "") + ".BE \n{";
                retorno += "\n\tpublic class " + table.Replace("tbl", "") + "BE {\n";
            }
            
            foreach (var item in colunas)
            {
                tipo = TipoEntidade(item.Tipo);
                retorno += "\t\tpublic " + tipo + " " + item.Coluna + " { get; set; }\n ";

            }
            retorno += "\t}";
            retorno += "\n}";

            return retorno;
        }
        public string TipoEntidade(string nome)
        {
            var retorno = "";
            switch (nome)
            {
                case "varchar":
                    retorno = "string";
                    break;
                case "int":
                    retorno = "int";
                    break;
                case "date":
                    retorno = "DateTime";
                    break;
                case "datetime":
                    retorno = "DateTime";
                    break;
                case "longtext":
                    retorno = "string";
                    break;
                case "tinyint":
                    retorno = "int";
                    break;
                case "bool":
                    retorno = "bool";
                    break;
                case "bit":
                    retorno = "bool";
                    break;
                case "decimal":
                    retorno = "decimal";
                    break;
                case "float":
                    retorno = "decimal";
                    break;
                default:
                    retorno = "string";
                    break;
            }
            return retorno;
        }
    }
    public class teste
    {
        public string sdfds { get; set; }
    }
}
