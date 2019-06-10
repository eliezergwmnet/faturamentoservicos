using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Globais.BE;
using Globais.Dao.Base;

namespace Globais.Dao
{
    public class GlobaisClienteTesteDao : Functions
    {
        public GlobaisClienteTesteDao()
        {
            this.procedure = "proc_GlobaisCliente";
        }
        
         public override IList<T> Select<T>()
         {
             return (IList<T>)this.CarregaDados(null);
        }

         public override IList<T> Select<T>(object obj)
         {
            return (IList<T>)this.CarregaDados((GlobaisClienteBE)obj);
        }

         public override int Insert(object obj)
         {
             return base.Insert(obj);
         }
         public override bool? Update(object obj)
         {
             return base.Update(obj).Value;
         }
         public override bool? Delete(object obj)
         {
             return base.Delete(obj).Value;
         }


        List<GlobaisClienteBE> CarregaDados(GlobaisClienteBE obj)
        {
            List<GlobaisClienteBE> result = new List<GlobaisClienteBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobaisClienteBE item = new GlobaisClienteBE();

                    item.conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]);
                    item.cli_id = DBNull.Value.Equals(dr["cli_id"]) ? 0 : Convert.ToInt32(dr["cli_id"]);
                    item.usu_id = DBNull.Value.Equals(dr["usu_id"]) ? 0 : Convert.ToInt32(dr["usu_id"]);
                    item.cli_CFOP = DBNull.Value.Equals(dr["cli_CFOP"]) ? 0 : Convert.ToDecimal(dr["cli_CFOP"]);
                    item.cli_SCM = DBNull.Value.Equals(dr["cli_SCM"]) ? false : Convert.ToBoolean(dr["cli_SCM"]);
                    item.cli_MNT = DBNull.Value.Equals(dr["cli_MNT"]) ? false : Convert.ToBoolean(dr["cli_MNT"]);
                    item.cli_tipoVencimento = dr["cli_tipoVencimento"].ToString();
                    item.cli_parametroVencimento = dr["cli_parametroVencimento"].ToString();
                    item.cli_nomeFantasia = dr["cli_nomeFantasia"].ToString();
                    item.cli_razaoSocial = dr["cli_razaoSocial"].ToString();
                    item.cli_tipoInscricao = dr["cli_tipoInscricao"].ToString();
                    item.cli_CGC = dr["cli_CGC"].ToString();
                    item.cli_CPF = dr["cli_CPF"].ToString();
                    item.cli_CodCidIBGE = dr["cli_CodCidIBGE"].ToString();
                    item.cli_incricaoEstadual = dr["cli_incricaoEstadual"].ToString();

                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    item.user_id = DBNull.Value.Equals(dr["user_id"]) ? 0 : Convert.ToInt32(dr["user_id"]);
                    item.log_cadastro = Convert.ToDateTime(dr["log_cadastro"]);
                    item.log_alteracao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_alteracao"]);
                    item.log_exclusao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_exclusao"]);


                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }

    }
}
