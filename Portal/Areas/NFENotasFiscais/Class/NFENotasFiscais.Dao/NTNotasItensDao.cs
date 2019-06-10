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
    public class NTNotasItensDao : Functions
    {
        public NTNotasItensDao()
        {
            this.procedure = "proc_NTNotasItens";
            this.conectionString = GlobaisNFENotasFiscais.ConectionDataBaseGlobal;
        }

        public override IList<T> Select<T>()
        {
            return (IList<T>)this.CarregaDados(null);
        }

        public override IList<T> Select<T>(object obj)
        {
            return (IList<T>)this.CarregaDados((NTNotasItensBE)obj);
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


        List<NTNotasItensBE> CarregaDados(NTNotasItensBE obj)
        {
            List<NTNotasItensBE> result = new List<NTNotasItensBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    NTNotasItensBE item = new NTNotasItensBE();

                    item.notI_ind = DBNull.Value.Equals(dr["notI_ind "]) ? 0 : Convert.ToInt32(dr["notI_ind "]);
                    item.not_id = DBNull.Value.Equals(dr["not_id "]) ? 0 : Convert.ToInt32(dr["not_id "]);
                    item.not_numero = DBNull.Value.Equals(dr["not_numero "]) ? 0 : Convert.ToInt32(dr["not_numero "]);
                    item.notI_qtde = DBNull.Value.Equals(dr["notI_qtde "]) ? 0 : Convert.ToInt32(dr["notI_qtde "]);
                    item.notI_descricao = dr["notI_descricao"].ToString();
                    item.notI_precoUnitario = DBNull.Value.Equals(dr["notI_precoUnitario"]) ? 0 : Convert.ToDecimal(dr["notI_precoUnitario"]);
                    item.notI_total = DBNull.Value.Equals(dr["notI_total"]) ? 0 : Convert.ToDecimal(dr["notI_total"]);
                    item.notI_tipo = dr["notI_tipo"].ToString();
                    item.notI_ep = dr["notI_ep"].ToString();
                    item.notI_tipoReceita = dr["notI_tipoReceita"].ToString();

                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }
    }
}
