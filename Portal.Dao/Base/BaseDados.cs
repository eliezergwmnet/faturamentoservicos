﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Globais.Helper;

namespace Portal.Dao.Base
{
    public class BaseDados
    {
        protected List<SqlParameter> parametros = new List<SqlParameter>();
        protected string procedure;

        protected IList<T> RetornaListaDados<T>(object obj = null)
        {
            try
            {
                if (obj != null)
                    AdicionaParametro(obj);
                ExecCommands con = new ExecCommands();
                return con.RetornaLista<T>(procedure, parametros);
            }
            catch(Exception ex)
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
                return con.RetornaLista<T>(Procedure, Par);
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        protected int? ExecutaItem(object obj = null)
        {
            try
            {
                if (obj != null)
                    AdicionaParametro(obj);
                ExecCommands con = new ExecCommands();
                var retorno = con.ExecutaItem(procedure, parametros);
                return Convert.ToInt32(retorno);
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return null;
            }
        }

        void AdicionaParametro(object obj)
        {
            parametros.Clear();
            if (obj != null)
            {
                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    var name = prop.Name;
                    var type = prop.PropertyType.FullName;

                    var value = prop.GetValue(obj, null);

                    if (prop.PropertyType.FullName.Contains("Portal.BE") == false)
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