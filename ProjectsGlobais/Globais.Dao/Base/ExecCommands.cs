using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Globais.Helper;

namespace Globais.Dao.Base
{
    public class ExecCommands
    {
        /// <summary>
        /// Retorna Data Reader de Dados Usando Procedures Para Consulta e SQLParameter para a Procedure
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Parametes"></param>
        /// <returns></returns>
        public IList<T> RetornaLista<T>(string Procedure, List<SqlParameter> Parameters, string connection)
        {
            if (string.IsNullOrEmpty(Procedure)) throw new Exception("Não foi informado o Nome da Procedure.");
            using (SqlConnection oConnection = new SqlConnection(Common.GetConnectionStringSqlServer(connection)))
            {
                oConnection.Open();
                using (SqlCommand command = oConnection.CreateCommand())
                {
                    command.CommandText = Procedure;
                    command.CommandType = CommandType.StoredProcedure;

                    if (Parameters != null && Parameters.Count > 0)
                    {
                        foreach (var item in Parameters)
                            command.Parameters.Add(item);
                    }
                    return ExecComandReturn<T>(command.ExecuteReader());
                }
            }
        }


        public SqlDataReader RetornaDatareader(string Procedure, List<SqlParameter> Parameters, string connection)
        {
            if (string.IsNullOrEmpty(Procedure)) throw new Exception("Não foi informado o Nome da Procedure.");
            SqlConnection oConnection = new SqlConnection(Common.GetConnectionStringSqlServer(connection));

            oConnection.Open();
            SqlCommand command = oConnection.CreateCommand();

            command.CommandText = Procedure;
            command.CommandType = CommandType.StoredProcedure;

            if (Parameters != null && Parameters.Count > 0)
            {
                foreach (var item in Parameters)
                    command.Parameters.Add(item);
            }
            return command.ExecuteReader();
        }


        /// <summary>
        /// Executa comando no SQL usando Procedures
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns>Retorna o total de linhas afetadas</returns>
        public object ExecutaItem(string Procedures, List<SqlParameter> Parameters, string connection)
        {
            if (string.IsNullOrEmpty(Procedures)) throw new Exception("Não foi informado o Nome da Procedure.");
            using (SqlConnection oConnection = new SqlConnection(Common.GetConnectionStringSqlServer(connection)))
            {
                oConnection.Open();
                using (SqlCommand command = oConnection.CreateCommand())
                {
                    command.CommandText = Procedures;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@return",
                        Direction = ParameterDirection.ReturnValue
                    });
                    if (Parameters.Count > 0)
                    {
                        foreach (var item in Parameters)
                            command.Parameters.Add(item);
                    }
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(command.Parameters["@return"].Value);
                }
                oConnection.Close();
            }
        }


        /// <summary>
        /// Recebe um DataReader e Transforma ele no List Generico
        /// E retorna os dados da lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        IList<T> ExecComandReturn<T>(System.Data.SqlClient.SqlDataReader dr)
        {
            IList<T> list = new List<T>();
            if (dr.HasRows)
            {
                T obj = Activator.CreateInstance<T>();
                var props = obj.GetType().GetProperties();
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    
                    foreach (System.Reflection.PropertyInfo prop in props)
                    {
                        if (!prop.GetMethod.ReturnType.Namespace.Contains(".BE") && !prop.GetMethod.ReturnType.Namespace.Contains("Portal.Helper") && !prop.GetMethod.ReturnType.Namespace.Contains("System.Collections.Generic"))
                        {
                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                            {
                                var _value = dr[prop.Name];
                                prop.SetValue(obj, _value, null);
                            }
                                
                        }
                    }
                    list.Add(obj);
                }
            }
            return list;
        }

    }
}
