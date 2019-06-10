using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Linq;

namespace HelpersSistema.Models.Base
{
    public class ExecCommandsParametrizavel// : Base
    {
        /// <summary>
        /// Retorna Data Reader de Dados Usando Procedures Para Consulta e SQLParameter para a Procedure
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Parametes"></param>
        /// <returns></returns>
        public IList<T> ReturnDados<T>(string Procedure, List<SqlParameter> Parameters, string[] saida)
        {
            try{
                if (string.IsNullOrEmpty(Procedure)) throw new Exception("Não foi informado o Nome da Procedure.");
                using (SqlConnection oConnection = new SqlConnection(Base.ConnectionString))
                {
                    oConnection.Open();
                    using (SqlCommand command = oConnection.CreateCommand())
                    {
                        command.CommandText = Base.Owner + Procedure;
                        command.CommandType = CommandType.StoredProcedure;

                        if (Parameters.Count > 0)
                        {
                            foreach (var item in Parameters)
                                command.Parameters.Add(item);
                        }
                        return ExecComandReturn<T>(command.ExecuteReader(), saida);
                    }
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception((ex.Message + "--" + ex.StackTrace + 
                        " -- procedure " + Procedure +
                        " -- saida:['" + string.Join("','", saida) + "']" 
                ),ex);
            }
        }
        /// <summary>
        /// Executa comando no SQL usando Procedures
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns>Retorna o total de linhas afetadas</returns>
        public object ExecutaComando(string Procedure, List<SqlParameter> Parameters)
        {
            try
            {
                if (string.IsNullOrEmpty(Procedure)) throw new Exception("Não foi informado o Nome da Procedure.");
                using (SqlConnection oConnection = new SqlConnection(Base.ConnectionString))
                {
                    oConnection.Open();
                    using (SqlCommand command = oConnection.CreateCommand())
                    {
                        command.CommandText = Base.Owner + Procedure;
                        command.CommandType = CommandType.StoredProcedure;

                        if (Parameters.Count > 0)
                        {
                            foreach (var item in Parameters)
                                command.Parameters.Add(item);
                        }
                        var retorno = "";
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                            retorno = reader[0].ToString();
                        return retorno;
                    }
                    oConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception((ex.Message + "--" + ex.StackTrace +
                        " -- procedure " + Procedure
                ), ex);
            }
        }


        /// <summary>
        /// Recebe um DataReader e Transforma ele no List Generico
        /// E retorna os dados da lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public IList<T> ExecComandReturn<T>(System.Data.SqlClient.SqlDataReader dr, string[] saida)
        {
            try
            {
                if (dr != null)
                {
                    IList<T> list = new List<T>();
                    T obj = default(T);
                    int i = 0;
                    var pInfo = obj.GetType().GetProperties().Where(a => saida.Contains(a.Name));

                    while (dr.Read())
                    {
                        foreach (var prop in pInfo)
                        {
                            if (!object.Equals(dr[prop.Name], DBNull.Value))
                                prop.SetValue(obj, dr[prop.Name], null);
                        }
                        list.Add(obj);
                        i++;
                    }
                    return list;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception((ex.Message + "--" + ex.StackTrace 
                ), ex);
            }
        }

        /// <summary>
        /// Executa o Commando e Retorna um Long
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Par"></param>
        /// <returns></returns>
        public long? ExecCommandIntern(string Procedure, List<System.Data.SqlClient.SqlParameter> Parameters)
        {
            try
            {
                long? retorno = null;
                if (string.IsNullOrEmpty(Procedure)) throw new Exception("Não foi informado o Nome da Procedure.");
                using (SqlConnection oConnection = new SqlConnection(Base.ConnectionString))
                {
                    oConnection.Open();
                    using (SqlCommand command = oConnection.CreateCommand())
                    {
                        command.CommandText = Procedure;
                        command.CommandType = CommandType.StoredProcedure;

                        if (Parameters.Count > 0)
                        {
                            foreach (var item in Parameters)
                                command.Parameters.Add(item);
                        }

                        command.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = "@return",
                            Direction = ParameterDirection.ReturnValue
                        });

                        try
                        {
                            command.ExecuteNonQuery();
                            retorno = Convert.ToInt64(command.Parameters["@return"].Value);
                        }
                        catch (Exception e)
                        {
                            //LogAppRepository.Insert(new Domain.Entities.App.Seguranca.LogApp()
                            //{
                            //    lor_Enviado = "",
                            //    lor_Ip = "",
                            //    lor_Recebimento = "Não Houve retorno! --" + JsonConvert.SerializeObject(Parameters),
                            //    lor_Requisicao = Procedure
                            //});
                        }

                    }
                    oConnection.Close();
                }

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception((ex.Message + "--" + ex.StackTrace +
                        " -- procedure " + Procedure
                ), ex);
            }
        }


    }
}
