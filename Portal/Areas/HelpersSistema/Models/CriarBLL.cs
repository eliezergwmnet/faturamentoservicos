using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpersSistema.Models
{
    public class CriarBLL
    {
        public string CriarClasseBLL(string table, List<Table> colunas)
        {
            string retorno = Help.CriarTituloPage();

            retorno += "using System.Collections.Generic;\n";
            retorno += "using System.Linq;\n";
            retorno += "using " + table.Replace("tbl", "") + ".BE;\n";
            retorno += "using " + table.Replace("tbl", "") + ".Dao;\n";
            retorno += "\n";
            retorno += "namespace " + table.Replace("tbl", "") + ".BLL\n";
            retorno += "{\n";
            retorno += "    public class " + table.Replace("tbl", "") + "BLL\n";
            retorno += "    {\n";
            retorno += "        public List<" + table.Replace("tbl", "") + "BE> Select()\n";
            retorno += "        {\n";
            retorno += "            return new " + table.Replace("tbl", "") + "Dao().Select<" + table.Replace("tbl", "") + "BE>().ToList();\n";
            retorno += "        }\n";
            retorno += "        public List<" + table.Replace("tbl", "") + "BE> Select(" + table.Replace("tbl", "") + "BE obj)\n";
            retorno += "        {\n";
            retorno += "            return new " + table.Replace("tbl", "") + "Dao().Select<" + table.Replace("tbl", "") + "BE>(obj).ToList();\n";
            retorno += "        }\n";
            retorno += "        public " + table.Replace("tbl", "") + "BE SelectId(" + table.Replace("tbl", "") + "BE obj)\n";
            retorno += "        {\n";
            retorno += "            return new " + table.Replace("tbl", "") + "Dao().SelectId<" + table.Replace("tbl", "") + "BE>(obj);\n";
            retorno += "        }\n";
            retorno += "        public " + table.Replace("tbl", "") + "BE Insert(" + table.Replace("tbl", "") + "BE obj)\n";
            retorno += "        {\n";
            retorno += "            obj." + colunas[0].Coluna + " = new " + table.Replace("tbl", "") + "Dao().Insert(obj);\n";
            retorno += "            return obj;\n";
            retorno += "        }\n";
            retorno += "        public bool Update(" + table.Replace("tbl", "") + "BE obj)\n";
            retorno += "        {\n";
            retorno += "            return new " + table.Replace("tbl", "") + "Dao().Update(obj).Value;\n";
            retorno += "        }\n";
            retorno += "        public bool Delete(" + table.Replace("tbl", "") + "BE obj)\n";
            retorno += "        {\n";
            retorno += "            return new " + table.Replace("tbl", "") + "Dao().Delete(obj).Value;\n";
            retorno += "        }\n";
            retorno += "    }\n";
            retorno += "}\n";

            return retorno;
        }
    }
}