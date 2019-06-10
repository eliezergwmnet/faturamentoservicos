//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Portal.Dao.Base;
//using Portal.Helper;

//namespace Portal.Dao
//{
//    public class CriarProcedures
//    {
//        string dataBase;
//        string criarproc = "_usp_CriaProcedure";
//        public CriarProcedures(string _database)
//        {
//            this.dataBase = _database;
//            if (!ValidaProcedure(criarproc))
//                CriarProcProcedures();
//        }
//        public bool Atualizacao()
//        {
//            var carregaitens = Update.Updates.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true);
//            foreach (DictionaryEntry item in carregaitens)
//            {
//                if(item.Key.ToString() != criarproc)
//                {
//                    if (item.Key.ToString().Substring(0, 3) == "tbl")
//                    {
//                        if (!ValidaTable(item.Key.ToString()))
//                        {
//                            this.ExecutaItem(item.Value.ToString());
//                        }
//                        var parametros = new List<SqlParameter>();
//                        parametros.Add(new SqlParameter("GenerateProcsFor", item.Key.ToString()));
//                        parametros.Add(new SqlParameter("PrintOrExecute", "Execute"));
//                        new ExecCommands().ExecutaItem(criarproc, parametros);
//                    }
//                    else if (item.Key.ToString().Substring(0, 3) == "pro")
//                    {
//                        if (!ValidaProcedure(item.Key.ToString()))
//                        {
//                            this.ExecutaItem(item.Value.ToString());
//                        }
//                    }
//                    else
//                    {
//                        if (!ValidaFunction(item.Key.ToString()))
//                        {
//                            this.ExecutaItem(item.Value.ToString());
//                        }
//                    }
//                }

//            }
//            return true;
//        }


//        public bool ValidaTable(string nome)
//        {
//            var tb = this.RetornarItem("SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_NAME = '" + nome + "'");
//            if (tb.Rows.Count == 0)
//                return false;
//            else
//                return true;
//        }
//        public bool ValidaProcedure(string nome)
//        {
//            var tb = this.RetornarItem("SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = '" + nome + "'");
//            if (tb.Rows.Count == 0)
//                return false;
//            else
//                return true;
//        }
//        public bool ValidaFunction(string nome)
//        {
//            var tb = this.RetornarItem("SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME = '" + nome + "'");
//            if (tb.Rows.Count == 0)
//                return false;
//            else
//                return true;
//        }

//        public void CriarProcProcedures()
//        {
//            this.ExecutaItem(Update.Updates._usp_CriaProcedure.Replace("//@NomeBanco//", dataBase));
//        }


//        public object ExecutaItem(string query)
//        {
//            using (SqlConnection oConnection = new SqlConnection(Common.GetConnectionStringSqlServer()))
//            {
//                oConnection.Open();
//                using (SqlCommand command = oConnection.CreateCommand())
//                {
//                    command.CommandText = query;
//                    command.CommandType = CommandType.Text;
//                    command.Parameters.Add(new SqlParameter()
//                    {
//                        ParameterName = "@return",
//                        Direction = ParameterDirection.ReturnValue
//                    });
                    

//                    command.ExecuteNonQuery();
//                    return Convert.ToInt32(command.Parameters["@return"].Value);
//                }
//                oConnection.Close();
//            }
//        }
//        public DataTable RetornarItem(string query)
//        {
//            using (SqlConnection oConnection = new SqlConnection(Common.GetConnectionStringSqlServer()))
//            {
//                oConnection.Open();
//                using (SqlCommand command = oConnection.CreateCommand())
//                {
//                    command.CommandText = query;
//                    command.CommandType = CommandType.Text;
//                    command.Parameters.Add(new SqlParameter()
//                    {
//                        ParameterName = "@return",
//                        Direction = ParameterDirection.ReturnValue
//                    });

//                    SqlDataReader reader = command.ExecuteReader();
//                    var retorno = new DataTable();
//                    retorno.Load(reader);
//                    oConnection.Close();
//                    return retorno;
//                }   
//            }
//        }


      
//    }
//}
