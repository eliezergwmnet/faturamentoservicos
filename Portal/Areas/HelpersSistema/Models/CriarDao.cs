using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpersSistema.Models
{
    public class CriarDao
    {
        public string CriarDaoTable(string table, List<Table> colunas)
        {
            string retorno = Help.CriarTituloPage();
            retorno += "using System;\n";
            retorno += "using System.Collections.Generic;\n";
            retorno += "using System.Data.SqlClient;\n";
            retorno += "using Globais.Dao.Base;\n";
            retorno += "using " + table.Replace("tbl", "") + ".BE;\n\n";

            retorno += "\n";
            retorno += "namespace Globais.Dao\n";
            retorno += "{\n";
            retorno += "    public class " + table.Replace("tbl", "") + "Dao : Functions\n";
            retorno += "    {\n";
            retorno += "        public " + table.Replace("tbl", "") + "Dao()\n";
            retorno += "        {\n";
            retorno += "            this.procedure = \"" + table.Replace("tbl", "proc_") + "\";\n";
            retorno += "        }\n";
            retorno += "        \n";
            retorno += "         public override IList<T> Select<T>()\n";
            retorno += "         {\n";
            retorno += "             return (IList<T>)this.CarregaDados(null);\n";
            retorno += "        }\n";
            retorno += "\n";
            retorno += "         public override IList<T> Select<T>(object obj)\n";
            retorno += "         {\n";
            retorno += "            return (IList<T>)this.CarregaDados((" + table.Replace("tbl", "") + "BE)obj);\n";
            retorno += "        }\n";
            retorno += "\n";
            retorno += "         public override int Insert(object obj)\n";
            retorno += "         {\n";
            retorno += "             return base.Insert(obj);\n";
            retorno += "         }\n";
            retorno += "         public override bool? Update(object obj)\n";
            retorno += "         {\n";
            retorno += "             return base.Update(obj).Value;\n";
            retorno += "         }\n";
            retorno += "         public override bool? Delete(object obj)\n";
            retorno += "         {\n";
            retorno += "             return base.Delete(obj).Value;\n";
            retorno += "         }\n";
            retorno += "\n";
            retorno += "\n";
            retorno += "        List<" + table.Replace("tbl", "") + "BE> CarregaDados(" + table.Replace("tbl", "") + "BE obj)\n";
            retorno += "        {\n";
            retorno += "            List<" + table.Replace("tbl", "") + "BE> result = new List<" + table.Replace("tbl", "") + "BE>();\n";
            retorno += "            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), obj);\n";
            retorno += "\n";
            retorno += "            if (dr.HasRows)\n";
            retorno += "            {\n";
            retorno += "                while (dr.Read())\n";
            retorno += "                {\n";
            retorno += "                    " + table.Replace("tbl", "") + "BE item = new " + table.Replace("tbl", "") + "BE();\n";
            retorno += "\n";
            foreach (var item in colunas)
            {
                if (item.Coluna == "log_id")
                {
                    retorno += "\n";
                    retorno += "                    item.log_id = DBNull.Value.Equals(dr[\"log_id\"]) ? 0 : Convert.ToInt32(dr[\"log_id\"]);\n";
                    retorno += "                    //item.user_id = DBNull.Value.Equals(dr[\"user_id\"]) ? 0 : Convert.ToInt32(dr[\"user_id\"]);\n";
                    retorno += "                    item.log_cadastro = Convert.ToDateTime(dr[\"log_cadastro\"]);\n";
                    retorno += "                    item.log_alteracao = DBNull.Value.Equals(dr[\"log_alteracao\"]) ? default(DateTime) : Convert.ToDateTime(dr[\"log_alteracao\"]);\n";
                    retorno += "                    item.log_exclusao = DBNull.Value.Equals(dr[\"log_exclusao\"]) ? default(DateTime) : Convert.ToDateTime(dr[\"log_exclusao\"]);\n";
                    retorno += "                    item.log_ativo = DBNull.Value.Equals(dr[\"log_ativo\"]) ? false : Convert.ToBoolean(dr[\"log_ativo\"]);\n";
                    retorno += "\n";

                }
                else
                {
                    if (item.Tipo == "varchar")
                        retorno += "                    item." + item.Coluna + " = dr[\"" + item.Coluna + "\"].ToString();\n";
                    if (item.Tipo == "int")
                        retorno += "                    item." + item.Coluna + "  = DBNull.Value.Equals(dr[\"" + item.Coluna + "\"]) ? 0 : Convert.ToInt32(dr[\"" + item.Coluna + "\"]);\n";
                    if (item.Tipo == "bit")
                        retorno += "                    item." + item.Coluna + " = DBNull.Value.Equals(dr[\"" + item.Coluna + "\"]) ? false : Convert.ToBoolean(dr[\"" + item.Coluna + "\"]);\n";
                    if (item.Tipo == "decimal")
                        retorno += "                    item." + item.Coluna + " = DBNull.Value.Equals(dr[\"" + item.Coluna + "\"]) ? 0 : Convert.ToDecimal(dr[\"" + item.Coluna + "\"]);\n";
                    if (item.Tipo == "datetime")
                        retorno += "                    item." + item.Coluna + " = DBNull.Value.Equals(dr[\"" + item.Coluna + "\"]) ? default(DateTime) : Convert.ToDateTime(dr[\"" + item.Coluna + "\"]);\n";
                }
            }


            retorno += "\n";
            retorno += "                    result.Add(item);\n";
            retorno += "                }\n";
            retorno += "                dr.Close();\n";
            retorno += "            }\n";
            retorno += "            return result;\n";
            retorno += "        }\n";
            retorno += "    }\n";
            retorno += "}\n";

            return retorno;
        }
    }
}