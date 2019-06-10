/*
********************************************
********************************************
********************************************
*** DESENVOLVIDO POR LEANDRO MARTINS *******
*** PLATAFORMA DE CRIAÇÃO DE TELAS**********
********************************************
********************************************
********************************************
*/


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Globais.Dao.Base;
using NFENotasFiscais.BE;
using NFENotasFiscais.Helper;

namespace NFENotasFiscais.Dao
{
    public class NTLoteErroDao : Functions
    {
        public NTLoteErroDao()
        {
            this.procedure = "proc_NTLoteErro";
            this.conectionString = GlobaisNFENotasFiscais.ConectionDataBaseGlobal;
        }

        public override IList<T> Select<T>()
        {
            return (IList<T>)this.CarregaDados(null);
        }

        public override IList<T> Select<T>(object obj)
        {
            return (IList<T>)this.CarregaDados((NTLoteErroBE)obj);
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


        List<NTLoteErroBE> CarregaDados(NTLoteErroBE obj)
        {
            List<NTLoteErroBE> result = new List<NTLoteErroBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    NTLoteErroBE item = new NTLoteErroBE();

                    item.LoteErr_id = DBNull.Value.Equals(dr["LoteErr_id "]) ? 0 : Convert.ToInt32(dr["LoteErr_id "]);
                    item.lote_id = DBNull.Value.Equals(dr["lote_id "]) ? 0 : Convert.ToInt32(dr["lote_id "]);
                    item.LoteErr_Codigo = dr["LoteErr_Codigo"].ToString();
                    item.LoteErr_Mensagem = dr["LoteErr_Mensagem"].ToString();
                    item.LoteErr_Correcao = dr["LoteErr_Correcao"].ToString();
                    item.LoteErr_Data = DBNull.Value.Equals(dr["LoteErr_Data"]) ? default(DateTime) : Convert.ToDateTime(dr["LoteErr_Data"]);

                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }
    }
}
