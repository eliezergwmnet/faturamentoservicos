using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using HelpersSistema.Models.Base;

namespace HelpersSistema.Models
{
    public class CriarProcedures
    {
        string table;
        List<Table> colunas;
        public CriarProcedures(string _table, List<Table> coln)
        {
            table = _table;
            if (_table != "")
                colunas = coln;
        }

        public CriarProcedures(string _table)
        {
            table = _table;
        }
        public void MontaProcs(bool executa = false)
        {
            if (executa == false)
            {
                var select = ProcSelect();
                new Help().CriarArquivos(HelpersSistema.Models.Base.Base.EnderecoArquivos + "\\procedure", HelpersSistema.Models.Base.Base.Select + ".txt", select);

                var selectId = ProcSelectId();
                new Help().CriarArquivos(HelpersSistema.Models.Base.Base.EnderecoArquivos + "\\procedure", HelpersSistema.Models.Base.Base.SelectId + ".txt", selectId);

                var selectname = ProcSelectName();
                new Help().CriarArquivos(HelpersSistema.Models.Base.Base.EnderecoArquivos + "\\procedure", HelpersSistema.Models.Base.Base.SelectName + ".txt", selectname);

                var insert = ProcInsert();
                new Help().CriarArquivos(HelpersSistema.Models.Base.Base.EnderecoArquivos + "\\procedure", HelpersSistema.Models.Base.Base.Insert + ".txt", insert);

                var update = ProcUpdate();
                new Help().CriarArquivos(HelpersSistema.Models.Base.Base.EnderecoArquivos + "\\procedure", HelpersSistema.Models.Base.Base.Update + ".txt", update);

                var delete = ProcDelete();
                new Help().CriarArquivos(HelpersSistema.Models.Base.Base.EnderecoArquivos + "\\procedure", HelpersSistema.Models.Base.Base.Delete + ".txt", delete);

                var retonro = select + "\n\n" + selectId + "\n\n" + insert + "\n\n" + update + "\n\n" + delete;
            }
            else
            {
                var select = ProcSelect();
                ExecutaProc(select);

                var selectId = ProcSelectId();
                ExecutaProc(selectId);

                var insert = ProcInsert();
                ExecutaProc(insert);

                var update = ProcUpdate();
                ExecutaProc(update);

                var delete = ProcDelete();
                ExecutaProc(delete);

            }
        }

        public string layoutProcedure()
        {
            string retorno = Help.CriarTituloPage();

            retorno += "CREATE PROCEDURE[dbo].[@nomeprocedure] ";
            retorno += "    \n\t@selTipo varchar(10), ";
            retorno += "    @queryvariaveis";
            retorno += "\nAS ";
            retorno += "\nBEGIN ";
            retorno += "    \n\tdeclare @Result bit; ";
            retorno += "    \n\tIF(@selTipo = sistemaviaweb_helper.dbo.TipoSelect('S')) ";
            retorno += "    \n\tBEGIN ";
            retorno += "        \n\t\t@queryselect";
            retorno += "    \n\tEND ";
            retorno += " ";
            retorno += "    \n\tIF(@selTipo = sistemaviaweb_helper.dbo.TipoSelect('I')) ";
            retorno += "    \n\tBEGIN ";
            retorno += "        \n\t\t@queryinsert";
            retorno += "    \n\tEND ";
            retorno += " ";
            retorno += "    \n\tIF(@selTipo = sistemaviaweb_helper.dbo.TipoSelect('U')) ";
            retorno += "    \n\tBEGIN ";
            retorno += "        \n\t\tset @Result = 1;";
            retorno += "        \n\t\tBEGIN TRAN";
            retorno += "            \n\t\t\t@queryupdate";
            retorno += "            \n";
            retorno += "            \n\t\t\tIF @@ROWCOUNT = 0";
            retorno += "                \n\t\t\t\tSET @Result = 0";
            retorno += "        \n\t\tCOMMIT TRAN";
            retorno += "        \n\t\treturn @result;";
            retorno += "    \n\tEND ";
            retorno += " ";
            retorno += "    \n\tIF(@selTipo = sistemaviaweb_helper.dbo.TipoSelect('D')) ";
            retorno += "    \n\tBEGIN ";
            retorno += "        \n\t\tset @Result = 1;";
            retorno += "        \n\t\tBEGIN TRAN";
            retorno += "            \n\t\t\t@querydelete";
            retorno += "            \n";
            retorno += "            \n\t\t\tIF @@ROWCOUNT = 0";
            retorno += "                \n\t\t\t\tSET @Result = 0";
            retorno += "        \n\t\tCOMMIT TRAN";
            retorno += "        \n\t\treturn @result;";
            retorno += "    \n\tEND ";
            retorno += "\nEND ";
            return retorno;
        }

        public string layoutVariaveis()
        {
            var retorno = "";
            foreach (var item in colunas)
            {
                if (item.Tipo == "varchar" || item.Tipo == "char")
                {
                    retorno += string.Format("\n\t@{0} {1}({2}) = null,", item.Coluna, item.Tipo, item.Tamanho);
                }
                else if (item.Tipo == "decimal")
                {
                    retorno += string.Format("\n\t@{0} {1}(18, 2) = null,", item.Coluna, item.Tipo);
                }
                else
                {
                    retorno += string.Format("\n\t@{0} {1} = null,", item.Coluna, item.Tipo);
                }

            }
            retorno = retorno.Remove(retorno.Length - 1);
            return retorno;
        }

        public string ProcSelect()
        {
            bool log = false;
            var retorno = "";
            retorno += "\n\t\tDECLARE @vSql NVARCHAR(max);";            retorno += "\n\t\tDECLARE @vParm NVARCHAR(max);";
            retorno += "\n\t\tDECLARE @Condicao NVARCHAR(max);";
            retorno += "\n\t\tBEGIN";

            retorno += "    \n\t\t\tSET @Condicao = ''";
            retorno += "    \n\t\t\tset @vsql = '	SELECT ";
            foreach (var item in colunas)
            {
                if(item.Coluna == "log_id")
                {
                    retorno += "L.log_id, L.log_cadastro, L.log_alteracao, L.log_exclusao, L.log_ativo, ";
                    log = true;
                }
                else
                    retorno += string.Format("{0}, ", item.Coluna);

            }

            retorno = retorno.Remove(retorno.Length - 2);
            retorno = retorno + "  FROM " + table + " ";

            if (log)
                retorno += " INNER JOIN sistemaviaweb_helper.dbo.tblLog as L on " + table + ".log_id = L.log_id '";
            else
                retorno += " '";

            retorno += "    \n\t\t\tif not(";
            foreach (var item in colunas)
                retorno += string.Format("\n\t\t\t\t(@{0} is null) and ", item.Coluna);
            retorno = retorno.Remove(retorno.Length - 4);
            retorno += "    \n\t\t\t)";

            retorno += "    \n\t\t\tBEGIN";

            retorno += "    \n\t\t\t\tset @vsql = @vsql + '	WHERE '";
            foreach (var item in colunas)
            {
                retorno += "    \n\t\t\t\tif (@" + item.Coluna + " is not null)";                retorno += "    \n\t\t\t\tBEGIN";
                retorno += "        \n\t\t\t\t\tset @Condicao = @Condicao + (SELECT CASE WHEN @Condicao = '' THEN '' ELSE ' AND ' END)+' " + item.Coluna + "  = @p" + item.Coluna + "'";
                retorno += "    \n\t\t\t\tEND";
            }

            retorno += "        \n\t\t\t\tset @vsql = @vsql + @Condicao";
            retorno += "    \n\t\t\tEND";

            retorno += "    \n\n\t\t\tset @vParm = '";
            foreach (var item in colunas)
            {
                if (item.Tipo == "varchar" || item.Tipo == "char")
                {
                    retorno += string.Format("@p{0} {1}({2}), ", item.Coluna, item.Tipo, item.Tamanho);
                }
                else if (item.Tipo == "decimal")
                {
                    retorno += string.Format("@p{0} {1}(18, 2), ", item.Coluna, item.Tipo);
                }
                else
                {
                    retorno += string.Format("@p{0} {1}, ", item.Coluna, item.Tipo);
                }
            }
            retorno = retorno.Remove(retorno.Length - 2) + "'";
            retorno += "    \n\n\t\t\tEXECUTE sp_executesql @vsql, @vParm,  ";

            foreach (var item in colunas)
                retorno += string.Format("@p{0} = @{0}, ", item.Coluna);
            retorno = retorno.Remove(retorno.Length - 2);

            retorno += "\n\t\tEND";
            return retorno;
        }

        public string ProcInsert()
        {
            var retorno = "";
            retorno = "\n\t\tINSERT INTO " + table + " (";
            var key = "";
            foreach (var item in colunas)
            {
                if (key != "")
                    retorno += item.Coluna + ", ";
                key = "1";
            }
            key = "";
            retorno = retorno.Remove(retorno.Length - 2) + ")";
            retorno += "\n\t\tSELECT ";
            foreach (var item in colunas)
            {
                if (key != "")
                    retorno += "@" + item.Coluna + ", ";
                key = "1";
            }
            retorno = retorno.Remove(retorno.Length - 2) + "";

            retorno += "\n\n\t\tDECLARE @res INT";
            retorno += "\n\n\t\tSELECT TOP 1 @res = " + colunas[0].Coluna + " FROM " + table + " ORDER BY " + colunas[0].Coluna + " DESC";

            retorno += "\n\n\t\tRETURN @res";

            //REMOÇÃO DO RETORNO DO IDENTITY, POIS ESTA RETORNANDO O ID DO LOG QUANDO O MESMO POSSUI TRIGGER
            //retorno += "\n\n\t\treturn @@identity";
            return retorno;
        }

        public string ProcUpdate()
        {
            var retorno = "";
            retorno = "\n\t\t\tUPDATE " + table + " SET ";
            var key = "";
            foreach (var item in colunas)
            {
                if (key != "")
                    retorno += string.Format("{0} = @{1}, ", item.Coluna, item.Coluna);
                else
                    key = item.Coluna;
            }

            retorno = retorno.Remove(retorno.Length - 2) + "";
            retorno += " \n\t\t\t\tWHERE " + key + " = @" + key;

            return retorno;
        }

        public string ProcDelete()
        {
            var retorno = "";
            retorno = "\n\t\t\tDELETE FROM  " + table + " ";

            retorno += " \n\t\t\t\tWHERE " + colunas[0].Coluna + " = @" + colunas[0].Coluna;

            return retorno;
        }

        public string ProcSelectName()
        {

            var retorno = "";


            retorno += "SET @s = \"SELECT \n\t\t//CAMPOS// \n\tFROM " + table + "\";\n";

            foreach (var item in colunas)
            {
                if (item.Tipo == "date" || item.Tipo == "datetime")
                    retorno += "\n\tIF v_" + item.Coluna + " IS NOT NULL THEN \n";
                else if (item.Tipo == "int" || item.Tipo == "integer")
                    retorno += "\n\tIF v_" + item.Coluna + " <> 0 AND v_" + item.Coluna + " IS NOT NULL THEN \n";
                else
                    retorno += "\n\tIF v_" + item.Coluna + " <> '' THEN \n";

                if (item.Tipo == "varchar")
                    retorno += "    \tSET @s = CONCAT(@s, \" AND " + item.Coluna + " like '%\", v_" + item.Coluna + ", \"%'\");  \n";
                else
                    retorno += "    \tSET @s = CONCAT(@s, \" AND " + item.Coluna + " = \", v_" + item.Coluna + ");  \n";

                retorno += "\n\tEND IF;\n";

            }
            retorno += "\n";
            foreach (var item in colunas)
                retorno += "    SET @" + item.Coluna + " = v_" + item.Coluna + ";\n";

            retorno += "\n\n    PREPARE stmt1 FROM @s;\n";
            retorno += "    EXECUTE stmt1;\n";
            retorno += "    DEALLOCATE PREPARE stmt1;\n";

            var columns = "";
            var var_columns = "";
            foreach (var item in colunas)
            {
                columns += item.Coluna + ", ";
                var_columns += "v_" + item.Coluna + ", ";
            }
            columns = columns.Remove(columns.Length - 2);
            var_columns = var_columns.Remove(var_columns.Length - 2);

            retorno = retorno.Replace("//CAMPOS//", columns) + "\n";

            retorno = MontaTitulo("SelectName").Replace("//Variaveis//", VariavelEntrada("")).Replace("//DADOS//", retorno);
            return retorno;
        }

        public string ProcSelectId()
        {
            var retorno = "\n\tSELECT \n\t\t//CAMPOS// \n\tFROM " + table + " \n\t\tWHERE //CAMPOCHAVE// = //VARIAVELCHAVE//; \n";
            var columns = "";
            var iditem = "";
            foreach (var item in colunas)
            {
                columns += item.Coluna + ", ";
                if (item.Key == "PRI")
                    iditem = item.Coluna;

            }
            columns = columns.Remove(columns.Length - 2);

            retorno = retorno.Replace("//CAMPOS//", columns).Replace("//CAMPOCHAVE//", iditem).Replace("//VARIAVELCHAVE//", "v_" + iditem) + "\n";
            retorno = MontaTitulo("SelectId").Replace("//Variaveis//", iditem + " int").Replace("//DADOS//", retorno);
            return retorno;
        }

        public string MontaTitulo(string nome)
        {
            var retorno = "CREATE PROCEDURE `proc_" + table + "_" + nome + "` (//Variaveis//)\n";
            retorno += "BEGIN \n";
            retorno += "//DADOS//";
            retorno += "END";

            if (nome == "Select")
            {
                HelpersSistema.Models.Base.Base.Select = "proc_" + table + "_" + nome;
            }
            else if (nome == "SelectId")
            {
                HelpersSistema.Models.Base.Base.SelectId = "proc_" + table + "_" + nome;
            }
            else if (nome == "SelectName")
            {
                HelpersSistema.Models.Base.Base.SelectName = "proc_" + table + "_" + nome;
            }
            else if (nome == "Insert")
            {
                HelpersSistema.Models.Base.Base.Insert = "proc_" + table + "_" + nome;
            }
            else if (nome == "Update")
            {
                HelpersSistema.Models.Base.Base.Update = "proc_" + table + "_" + nome;
            }
            else
            {
                HelpersSistema.Models.Base.Base.Delete = "proc_" + table + "_" + nome;
            }

            return retorno;
        }

        public string VariavelEntrada(string tipo)
        {
            string retorno = "";
            foreach (var item in colunas)
            {
                if (tipo != "INSERT" && item.Key == "PRI")
                {
                    retorno += "v_" + item.Coluna + " " + item.Tipo + ", ";
                }
                else
                {
                    if (item.Tipo.ToUpper() == "VARCHAR")
                        retorno += "v_" + item.Coluna + " " + item.Tipo + "(" + item.Tamanho + "), ";
                    else
                        retorno += "v_" + item.Coluna + " " + item.Tipo + ", ";
                }
            }
            retorno = retorno.Remove(retorno.Length - 2);
            return retorno;
        }
        public List<Table> CarregaColunas1()
        {
            var result = new List<Table>();
            result.Add(new Table { Key = "PRI", Coluna = "Id", Tamanho = "", Tipo = "int" });
            result.Add(new Table { Key = "", Coluna = "Nome", Tamanho = "255", Tipo = "varchar" });
            result.Add(new Table { Key = "", Coluna = "Data", Tamanho = "", Tipo = "date" });
            result.Add(new Table { Key = "", Coluna = "Valor", Tamanho = "18", Tipo = "double" });
            result.Add(new Table { Key = "", Coluna = "Ativo", Tamanho = "", Tipo = "bit" });
            result.Add(new Table { Key = "", Coluna = "Endereco", Tamanho = "100", Tipo = "varchar" });


            return result;
        }
        public List<Table> CarregaColunas()
        {
            string query = "SELECT column_name, data_type, character_maximum_length, '' as column_key FROM information_schema.columns WHERE table_name = '" + table + "'";
            var result = new List<Table>();

            ExecCommands b = new ExecCommands();
            var dr = b.ReturnDadosNew(query, null);

            if (dr != null)
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var linha = new Table
                        {
                            Key = dr["column_key"].ToString(),
                            Coluna = dr["column_name"].ToString(),
                            Tipo = dr["data_type"].ToString(),
                            Tamanho = dr["character_maximum_length"].ToString()
                        };
                        result.Add(linha);
                    };
                    dr.Close();
                    b.oConnection.Close();
                }
            }
            return result;
        }

        public List<TableName> ListaTodasTabelas()
        {

            string query = "select table_name from INFORMATION_SCHEMA.TABLES where table_type = 'BASE TABLE'";
            var result = new List<TableName>();

            ExecCommands b = new ExecCommands();
            var dr = b.ReturnDadosNew(query, null);

            if (dr != null)
            {
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var linha = new TableName
                        {
                            Nome = dr["table_name"].ToString(),
                        };
                        result.Add(linha);
                    };
                    dr.Close();
                    b.oConnection.Close();
                }
            }
            return result;

        }

        public void ExecutaProc(string query)
        {
            try
            {
                ExecCommands b = new ExecCommands();
                var res = b.ExecutaComandoSemTransaction(query, null);
            }
            catch (Exception ex)
            {
                var teste = ex;
            }
        }
    }



    public class Table
    {
        public string Key { get; set; }
        public string Coluna { get; set; }
        public string Tipo { get; set; }
        public string Tamanho { get; set; }
    }

    public class TableName
    {
        public string Nome { get; set; }
    }
}