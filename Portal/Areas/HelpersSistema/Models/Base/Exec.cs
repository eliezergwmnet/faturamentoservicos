using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpersSistema.Models.Base
{
    public class BaseDados : IGenericAbstract
    {
        public BaseDados()
        {
        }

        /// <summary>
        /// Estancia o Objeto com a procedure a ser executada.
        /// </summary>
        /// <param name="proc"></param>
        public BaseDados(string proc)
        {
            query = proc;
        }

        /// <summary>
        /// Executa comando SQL com Procedure e retorna uma lista de Generic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <param name="obj"></param>
        /// <param name="funcao"></param>
        /// <returns></returns>
        public IList<T> ExecutarComandoComParametro<T>(object obj)
        {
            AdicionaParametro(obj);
            return ExecutaComandoComRetorno<T>(query).ToList();
        }

        /// <summary>
        /// Executa item e retorna lista de Generico sem parametro
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IList<T> ExecutarComandoSemParametro<T>()
        {
            return ExecutaComandoComRetorno<T>(query).ToList();
        }

        /// <summary>
        /// Executa comando SQL co Procedure e retorma um inteiro com o resultado.
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="obj"></param>
        /// <param name="funcao"></param>
        /// <returns></returns>
        public long ExecutaComandoSemTransaction(object obj)
        {
            AdicionaParametro(obj);
            return ExecutaComandoInternoSemTransaction(query, parametros);
        }

        /// <summary>
        /// Executa Comando sem parametro e sem transaction
        /// </summary>
        /// <returns></returns>
        public long ExecutaComandoSemTransaction()
        {
            return ExecutaComandoInternoSemTransaction(query, parametros);
        }

        /// <summary>
        /// Executa comando usando transaction com parametros
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public long ExecutaComandoComTransaction(object obj)
        {
            AdicionaParametro(obj);
            return ExecutaComandoInternoComTransaction(query, parametros);
        }
        /// <summary>
        /// Executa Comando usando transaction sem parametros
        /// </summary>
        /// <returns></returns>
        public long ExecutaComandoComTransaction()
        {
            return ExecutaComandoInternoComTransaction(query);
        }

        /// <summary>
        /// Executa o Commando e Retorna um Inteiro sem transaction
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Par"></param>
        /// <returns></returns>
        protected long ExecutaComandoInternoSemTransaction(string Procedure, List<MySqlParameter> Par)
        {
            ExecCommands con = new ExecCommands();
            var retorno = con.ExecutaComandoSemTransaction(query, Par);
            return Convert.ToInt64(retorno);
        }
        /// <summary>
        /// Executa Comando Interno sem transaction
        /// </summary>
        /// <param name="Procedurer"></param>
        /// <returns></returns>
        protected long ExecutaComandoInternoSemTransaction(string Procedurer)
        {
            ExecCommands con = new ExecCommands();
            var retorno = con.ExecutaComandoSemTransaction(query, null);
            return Convert.ToInt64(retorno);
        }

        /// <summary>
        /// Executa comando interno com transaction e parametro
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Par"></param>
        /// <returns></returns>
        protected long ExecutaComandoInternoComTransaction(string Procedure, List<MySqlParameter> Par)
        {
            ExecCommands con = new ExecCommands();
            var retorno = con.ExecutaComandoComTransaction(query, Par);
            return Convert.ToInt64(retorno);
        }
        /// <summary>
        /// Executa comando interno com transaction e sem parametro
        /// </summary>
        /// <param name="Procedure"></param>
        /// <param name="Par"></param>
        /// <returns></returns>
        protected long ExecutaComandoInternoComTransaction(string Procedure)
        {
            ExecCommands con = new ExecCommands();
            var retorno = con.ExecutaComandoSemTransaction(query, parametros);
            return Convert.ToInt64(retorno);
        }

        /// <summary>
        /// Executa o comando e chama a função para transforma o DataReader em List, retornando o mesmo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Procedure"></param>
        /// <param name="Par"></param>
        /// <returns></returns>
        public IList<T> ExecutaComandoComRetorno<T>(string Procedure)
        {
            ExecCommands con = new ExecCommands();
            return con.ReturnDados<T>(Procedure, parametros);
        }

        /// <summary>
        /// Recebe um DataReader e Transforma ele no List Generico
        /// E retorna os dados da lista
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        protected IList<T> CriarObjetoRetorno<T>(string Procedure, SqlDataReader dr)
        {
            if (dr != null)
            {
                IList<T> list = new List<T>();
                T obj = default(T);
                int i = 0;
                while (dr.Read())
                {
                    obj = Activator.CreateInstance<T>();
                    foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
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

        /// <summary>
        /// Adicionar todos os parametros no list gererico de parametros.
        /// Quando o item for fazio, o parametro nao é adicionardo na lista.
        /// </summary>
        /// <param name="obj"></param>
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

                    if (type == "System.Int32")
                    {
                        if (value.ToString() != "0")
                            AdicionarParametros(name, value);
                    }
                    else if (type == "System.Decimal")
                    {
                        if (value.ToString() != "0")
                            AdicionarParametros(name, value);
                    }
                    else if (type == "System.DateTime")
                    {
                        if (value.ToString() != "01/01/0001 00:00:00")
                            AdicionarParametros(name, value);
                    }
                    else if (value != null)
                        AdicionarParametros(name, value);
                }
            }

        }

    }

    /// <summary>
    /// Cria todos os itens que são genericos para o envio de dados para a base de dados;
    /// </summary>
    public class IGenericAbstract
    {
        public List<MySqlParameter> parametros = new List<MySqlParameter>();
        public string query;

        public string retornoprocedure;
        /// <summary>
        /// Limpa a lista de parametros.
        /// </summary>
        public void LimpaParametros()
        {
            parametros.Clear();
        }

        /// <summary>
        /// adicionar um novo item como parametro.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AdicionarParametros(string name, object value)
        {
            parametros.Add(new MySqlParameter(name, value));
        }
    }

}
