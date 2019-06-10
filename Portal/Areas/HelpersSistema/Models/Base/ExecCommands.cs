using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace HelpersSistema.Models.Base
{
    public class ExecCommands// : Base
    {
        private SqlDataAdapter bdAdapter;
        private DataSet bdDataSet; //MySQL
        public SqlConnection oConnection;
        /// <summary>
        /// Retorna Data Reader de Dados Usando Procedures Para Consulta e SQLParameter para a Procedure
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="Parametes"></param>
        /// <returns></returns>

        public SqlDataReader ReturnDadosNew(string Query, List<SqlParameter> Parameters)
        {
            if (string.IsNullOrEmpty(Query)) throw new Exception("Não foi informado o Nome da Procedure.");

            //using (MySqlConnection oConnection = new MySqlConnection(ConnectionString))
           // {
            oConnection = new SqlConnection(Base.ConnectionString);
                oConnection.Open();

                //Verifica se a conexão está aberta
                if (oConnection.State == ConnectionState.Open)
                {
                    using (SqlCommand command = oConnection.CreateCommand())
                    {
                        command.CommandText = Query;
                        command.CommandType = CommandType.Text;

                        if (Parameters != null && Parameters.Count > 0)
                        {
                            foreach (var item in Parameters)
                                command.Parameters.Add(item);
                        }
                        return command.ExecuteReader();
                    }
                }
                return null;
           // }
        }
        public IList<T> ReturnDados<T>(string Query, List<MySqlParameter> Parameters)
        {
            if (string.IsNullOrEmpty(Query)) throw new Exception("Não foi informado o Nome da Procedure.");

            IList<T> retorno = new List<T>();

            using (MySqlConnection oConnection = new MySqlConnection(Base.ConnectionString))
            {
                oConnection.Open();

                //Verifica se a conexão está aberta
                if (oConnection.State == ConnectionState.Open)
                {
                    using (MySqlCommand command = oConnection.CreateCommand())
                    {
                        command.CommandText = Query;
                        command.CommandType = CommandType.Text;

                        if (Parameters != null && Parameters.Count > 0)
                        {
                            foreach (var item in Parameters)
                                command.Parameters.Add(item);
                        }
                        retorno = ExecComandReturn<T>(command.ExecuteReader());
                    }

                    oConnection.Close();
                    return retorno;
                }
                else
                    return new List<T>();
            }
        }

        public object ExecutaComandoComTransaction(string Procedure, List<MySqlParameter> Parameters)
        {
            if (string.IsNullOrEmpty(Procedure)) throw new Exception("Não foi informado o Nome da Procedure.");

            using (SqlConnection oConnection = new SqlConnection(Base.ConnectionString))
            {
                oConnection.Open();
                using (SqlCommand command = oConnection.CreateCommand())
                {
                    SqlTransaction _transaction = oConnection.BeginTransaction("teste");
                    command.Transaction = _transaction;
                    command.CommandText = Procedure;
                    command.CommandText = Base.Owner + Procedure;
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

                    try
                    {
                        command.ExecuteNonQuery();
                        var retorno = Convert.ToInt32(command.Parameters["@return"].Value);
                        _transaction.Commit();
                        return retorno;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                        Console.WriteLine("  Message: {0}", ex.Message);

                        try
                        {
                            _transaction.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                            Console.WriteLine("  Message: {0}", ex2.Message);
                        }
                        return 0;
                    }
                }
                oConnection.Close();
            }
        }
        /// <summary>
        /// Executa comando no SQL usando Procedures
        /// </summary>
        /// <param name="Sql"></param>
        /// <returns>Retorna o total de linhas afetadas</returns>
        public object ExecutaComandoSemTransaction(string Query, List<MySqlParameter> Parameters)
        {

            if (string.IsNullOrEmpty(Query)) throw new Exception("Não foi informado o Nome da Procedure.");

            int retorno = 0;

            using (MySqlConnection oConnection = new MySqlConnection(Base.ConnectionString))
            {
                oConnection.Open();

                //Verifica se a conexão está aberta
                if (oConnection.State == ConnectionState.Open)
                {
                    using (MySqlCommand command = oConnection.CreateCommand())
                    {
                        command.CommandText = Query;
                        command.CommandType = CommandType.Text;

                        if (Parameters != null && Parameters.Count > 0)
                        {
                            foreach (var item in Parameters)
                                command.Parameters.Add(item);
                        }
                        retorno = command.ExecuteNonQuery();
                    }

                    oConnection.Close();
                    return retorno;
                }
                else
                    return retorno;

            }
        }
        public int ExecutaComandoSemTransactionId(string Query, List<MySqlParameter> Parameters)
        {

            if (string.IsNullOrEmpty(Query)) throw new Exception("Não foi informado o Nome da Procedure.");

            int retorno = 0;

            using (MySqlConnection oConnection = new MySqlConnection(Base.ConnectionString))
            {
                oConnection.Open();

                //Verifica se a conexão está aberta
                if (oConnection.State == ConnectionState.Open)
                {
                    using (MySqlCommand command = oConnection.CreateCommand())
                    {
                        try
                        {
                            command.CommandText = Query;
                            command.CommandType = CommandType.Text;

                            if (Parameters != null && Parameters.Count > 0)
                            {
                                foreach (var item in Parameters)
                                    command.Parameters.Add(item);
                            }
                            retorno = Convert.ToInt32(command.ExecuteScalar());
                        }
                        catch (Exception ex)
                        {
                            string teste = Query;
                            retorno = 0;
                        }
                    }

                    oConnection.Close();
                    return retorno;
                }
                else
                    return retorno;

            }
        }


        /// <summary>
        /// Recebe um DataReader e Transforma ele no List Generico
        /// E retorna os dados da lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        IList<T> ExecComandReturn<T>(MySqlDataReader dr)
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
                        if (!prop.GetMethod.ReturnType.Namespace.Contains("Svw.Domain") && !prop.GetMethod.ReturnType.Namespace.Contains("System.Collections.Generic"))
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
