using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpersSistema.Models.Base
{
    public class BaseDadosParametrizavel : BaseDados
    {

        /// <summary>
        /// Estancia o Objeto com a procedure a ser executada.
        /// </summary>
        /// <param name="proc"></param>
        public BaseDadosParametrizavel(string proc) : base(proc) { }

        /// <summary>
        /// Executa comando SQL com Procedure e retorna uma lista de Generic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <param name="obj"></param>
        /// <param name="funcao"></param>
        /// <returns></returns>
        public IList<T> ExecComandReturn<T>(object obj, string[] entrada, string[] saida)
        {
            List<SqlParameter> Par = null;
            try
            {
                Par = this.AdicionaParametro(obj, entrada);
                ExecCommandsParametrizavel con = new ExecCommandsParametrizavel();
                return con.ReturnDados<T>(base.query, Par, saida);
            }
            catch (Exception ex)
            {
                throw new Exception((ex.Message + "--" + ex.StackTrace +
                        " -- procedure " + base.query +
                        " -- entrada:['" + string.Join("','", entrada) + "'] " +
                        " -- saida:['" + string.Join("','", saida) + "']"
                ), ex);
            }

        }

        /// <summary>
        /// Executa comando SQL com Procedure e retorna uma lista de Generic.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="procedure"></param>
        /// <param name="obj"></param>
        /// <param name="funcao"></param>
        /// <returns></returns>
        public IList<T> ExecComandReturn<T>(List<SqlParameter> entrada, string[] saida)
        {
            try
            {
                ExecCommandsParametrizavel con = new ExecCommandsParametrizavel();
                return con.ReturnDados<T>(base.query, entrada, saida);
            }
            catch (Exception ex)
            {
                throw new Exception((ex.Message + "--" + ex.StackTrace +
                        " -- procedure " + base.query +
                        " -- saida:['" + string.Join("','", saida) + "']"
                ), ex);
            }

        }

        /// <summary>
        /// Executa comando SQL co Procedure e retorma um inteiro com o resultado.
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="obj"></param>
        /// <param name="funcao"></param>
        /// <returns></returns>
        public long? ExecComandReturn(object obj, string[] parametros)
        {
            List<SqlParameter> Par = null;
            try
            {
                Par = this.AdicionaParametro(obj, parametros);
                ExecCommandsParametrizavel con = new ExecCommandsParametrizavel();
                return con.ExecCommandIntern(base.query, Par);
            }
            catch (Exception ex)
            {
                //LogAppRepository.Insert(new Domain.Entities.App.Seguranca.LogApp() { 
                    //lor_Enviado = ex.Message + "--" + ex.StackTrace +
                    //    " -- procedure " + base.procedure + 
                    //    " -- parametros:" + JsonConvert.SerializeObject(Par), 
                    //lor_Ip = "", 
                    //lor_Recebimento = JsonConvert.SerializeObject(obj),
                    //lor_Requisicao = base.procedure 
                //});
                throw ex;
            }
        }



        public List<SqlParameter> AdicionaParametro(object obj, string[] parametros)
        {
            List<SqlParameter> par = new List<SqlParameter>();
            if (obj != null)
            {
                var pInfo = new List<System.Reflection.PropertyInfo>();
                foreach (var p in obj.GetType().GetProperties())
                {
                    foreach (var pp in parametros)
                    {
                        if (p.Name == pp)
                            pInfo.Add(p);
                    }
                }

                foreach (System.Reflection.PropertyInfo prop in pInfo)
                {
                    var name = prop.Name;
                    var type = prop.PropertyType.FullName;

                    var value = prop.GetValue(obj, null);

                    if (type == "System.Int32")
                    {
                        if (value.ToString() != "0")
                            par.Add(new System.Data.SqlClient.SqlParameter(name, value));
                    }
                    else if (type == "System.Decimal")
                    {
                        if (value.ToString() != "0")
                            par.Add(new System.Data.SqlClient.SqlParameter(name, value));
                    }
                    else if (type == "System.DateTime")
                    {
                        if (value.ToString() != "01/01/0001 00:00:00")
                            par.Add(new System.Data.SqlClient.SqlParameter(name, value));
                    }
                    else if (value != null)
                        par.Add(new System.Data.SqlClient.SqlParameter(name, value));
                }
            }
            return par;
        }
    }
}
