using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Globais.Helper;

namespace Globais.Dao.Base
{
    public class BaseDados
    {
        protected List<SqlParameter> parametros = new List<SqlParameter>();
        protected string procedure;
        protected string conectionString = Globais.Helper.Globais.ConectionDataBaseGlobal;

        protected IList<T> RetornaListaDados<T>(TipoSql sql, object obj = null)
        {
            try
            {
                if (obj != null)
                    AdicionaParametro(obj, sql.ToString());
                else
                    AdicionaParametro(null, sql.ToString());
                ExecCommands con = new ExecCommands();
                return con.RetornaLista<T>(procedure, parametros, conectionString);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        public SqlDataReader RetornaDataReader(string proc, string sqlTipo, object obj = null)
        {
            try
            {
                procedure = proc;

                AdicionaParametro(obj, sqlTipo);
                ExecCommands con = new ExecCommands();
                return con.RetornaDatareader(procedure, parametros, conectionString);
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        public SqlDataReader RetornaDataReader(string proc, string sqlTipo,string connection, object obj = null)
        {
            try
            {
                procedure = proc;

                AdicionaParametro(obj, sqlTipo);
                ExecCommands con = new ExecCommands();
                return con.RetornaDatareader(procedure, parametros, connection);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        public int? ExecutaFuncao(string proc, string sqlTipo, object obj = null)
        {
            try
            {
                procedure = proc;
                AdicionaParametro(obj, sqlTipo);
                ExecCommands con = new ExecCommands();
                return Convert.ToInt32(con.ExecutaItem(procedure, parametros, conectionString));
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        protected IList<T> RetornaListaDados<T>(string Procedure, List<SqlParameter> Par)
        {
            try
            {
                ExecCommands con = new ExecCommands();
                return con.RetornaLista<T>(Procedure, Par, conectionString);
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        protected int? ExecutaItem(string sql, object obj = null)
        {
            try
            {
                AdicionaParametro(obj, sql);
                
                ExecCommands con = new ExecCommands();
                var retorno = con.ExecutaItem(procedure, parametros, conectionString);
                return Convert.ToInt32(retorno);
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        void AdicionaParametro(object obj, string selTipo = "")
        {
            parametros.Clear();
            if(selTipo != "")
                parametros.Add(new SqlParameter("selTipo", selTipo));
            if (obj != null)
            {
                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    var name = prop.Name;
                    var type = prop.PropertyType.FullName;

                    var value = prop.GetValue(obj, null);

                    if (type.Contains("System.Collections.Generic") == false && prop.PropertyType.FullName.Contains("PortalSCM.BE") == false && prop.PropertyType.FullName.Contains("Portal.BE") == false && prop.PropertyType.FullName.Contains("Globais.BE") == false)
                    {
                        if (type == "System.Int32")
                        {
                            if (value.ToString() != "0")
                                parametros.Add(new System.Data.SqlClient.SqlParameter(name, value));
                        }
                        else if(type == "System.String")
                        {
                            if (!String.IsNullOrEmpty(Convert.ToString(value)))
                                parametros.Add(new System.Data.SqlClient.SqlParameter(name, value));
                        }
                        else if (type == "System.Decimal")
                        {
                            if (value.ToString() != "0")
                            {
                                var par = new System.Data.SqlClient.SqlParameter(name, System.Data.SqlDbType.Decimal);
                                par.Value = value;
                                parametros.Add(par);
                            }
                        }
                        else if (type == "System.Double")
                        {
                            if (value.ToString() != "0")
                            {
                                var par = new System.Data.SqlClient.SqlParameter(name, System.Data.SqlDbType.Decimal);
                                par.Value = value;
                                parametros.Add(par);
                            }
                        }
                        else if (type == "System.DateTime")
                        {
                            //if (value.ToString() != "01/01/0001 00:00:00")
                            if (default(DateTime) != (DateTime)value)
                                parametros.Add(new System.Data.SqlClient.SqlParameter(name, value));
                        }
                        else if (value != null)
                            parametros.Add(new System.Data.SqlClient.SqlParameter(name, value));
                    }
                }
            }

        }
    }
}