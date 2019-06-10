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
    public class NTLoteDao : Functions
    {
        public NTLoteDao()
        {
            this.procedure = "proc_NTLote";
            this.conectionString = GlobaisNFENotasFiscais.ConectionDataBaseGlobal;
        }

        public override IList<T> Select<T>()
        {
            return (IList<T>)this.CarregaDados(null);
        }

        public override IList<T> Select<T>(object obj)
        {
            return (IList<T>)this.CarregaDados((NTLoteBE)obj);
        }

        public override int Insert(object obj)
        {
            return base.Insert(obj);
        }

        public bool UpdateStatus(NTLoteBE obj)
        {
            var retorno = ExecutaItem("altStatus", obj);
            if (retorno != null)
                return Convert.ToBoolean(retorno);
            else
                return false;
        }

        public bool UpdateProtocolo(NTLoteBE obj)
        {
            var retorno = ExecutaItem("altProto", obj);
            if (retorno != null)
                return Convert.ToBoolean(retorno);
            else
                return false;
        }

        public bool UpdateXmlEnvio(NTLoteBE obj)
        {
            var retorno = ExecutaItem("altUrlEnv", obj);
            if (retorno != null)
                return Convert.ToBoolean(retorno);
            else
                return false;
        }

        public bool UpdateXmlConsulta(NTLoteBE obj)
        {
            var retorno = ExecutaItem("altUrlCons", obj);
            if (retorno != null)
                return Convert.ToBoolean(retorno);
            else
                return false;
        }

        public override bool? Update(object obj)
        {
            return base.Update(obj).Value;
        }
        public override bool? Delete(object obj)
        {
            return base.Delete(obj).Value;
        }


        List<NTLoteBE> CarregaDados(NTLoteBE obj)
        {
            List<NTLoteBE> result = new List<NTLoteBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    NTLoteBE item = new NTLoteBE();

                    item.lote_id = DBNull.Value.Equals(dr["lote_id "]) ? 0 : Convert.ToInt32(dr["lote_id "]);

                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    item.user_id = DBNull.Value.Equals(dr["user_id"]) ? 0 : Convert.ToInt32(dr["user_id"]);
                    item.log_cadastro = Convert.ToDateTime(dr["log_cadastro"]);
                    item.log_alteracao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_alteracao"]);
                    item.log_exclusao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_exclusao"]);
                    item.log_ativo = DBNull.Value.Equals(dr["log_ativo"]) ? false : Convert.ToBoolean(dr["log_exclusao"]);

                    item.conf_id = DBNull.Value.Equals(dr["conf_id "]) ? 0 : Convert.ToInt32(dr["conf_id "]);
                    item.lote_serie = dr["lote_serie"].ToString();
                    item.lote_modulo = dr["lote_modulo"].ToString();
                    item.lote_emissao = DBNull.Value.Equals(dr["lote_emissao"]) ? default(DateTime) : Convert.ToDateTime(dr["lote_emissao"]);

                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }
    }
}
