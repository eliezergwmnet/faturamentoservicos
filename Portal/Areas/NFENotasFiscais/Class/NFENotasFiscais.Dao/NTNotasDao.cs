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
using Globais.BE;
using Globais.Dao;
using Globais.Dao.Base;
using NFENotasFiscais.BE;
using NFENotasFiscais.Helper;
using PortalSCM.BE;
using PortalSCM.Dao;
using System.Linq;

namespace NFENotasFiscais.Dao
{
    public class NTNotasDao : Functions
    {
        public NTNotasDao()
        {
            this.procedure = "proc_NTNotas";
            this.conectionString = GlobaisNFENotasFiscais.ConectionDataBaseGlobal;
        }

        public override IList<T> Select<T>()
        {
            return (IList<T>)this.CarregaNotasEmitidas(null);
        }

        public List<NTNotasEmitidasBE> SelectPendentesBoleto()
        {
            return this.CarregaNotasEmitidas(null, "EmiBolPen");
        }

        public override IList<T> Select<T>(object obj)
        {
            return (IList<T>)this.CarregaNotasEmitidas((NTNotasBE)obj);
        }

        /// <summary>
        /// Carrega o numero da da proximo nota, não sendo necessário adcionar um contato para a primeira nota
        /// </summary>
        /// <param name="conf_id"></param>
        /// <returns></returns>
        public int NumeroNota(int conf_id)
        {
            int retorno = 1;
            SqlDataReader dr = base.RetornaDataReader(this.procedure, "ultNuNota", new NTNotasBE { conf_id = conf_id });
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    retorno = Convert.ToInt32(dr["NUMERO"]);
                }
            }
            dr.Close();
            return retorno;
        }

        public override int Insert(object obj)
        {
            return base.Insert(obj);
        }
        public override bool? Update(object obj)
        {
            return base.Update(obj).Value;
        }

        public bool? UpdateBoleto(object obj)
        {
            if (base.ExecutaFuncao(this.procedure, "BolPenAtu", obj).Value == 1)
                return true;
            else
                return false;
        }
        public override bool? Delete(object obj)
        {
            return base.Delete(obj).Value;
        }


        List<NTNotasBE> CarregaDados(NTNotasBE obj)
        {
            List<NTNotasBE> result = new List<NTNotasBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), this.conectionString, obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    NTNotasBE item = new NTNotasBE();

                    item.not_id = DBNull.Value.Equals(dr["not_id"]) ? 0 : Convert.ToInt32(dr["not_id"]);

                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    //item.user_id = DBNull.Value.Equals(dr["user_id"]) ? 0 : Convert.ToInt32(dr["user_id"]);
                    item.log_cadastro = Convert.ToDateTime(dr["log_cadastro"]);
                    item.log_alteracao = DBNull.Value.Equals(dr["log_alteracao"]) ? default(DateTime) : Convert.ToDateTime(dr["log_alteracao"]);
                    item.log_exclusao = DBNull.Value.Equals(dr["log_exclusao"]) ? default(DateTime) : Convert.ToDateTime(dr["log_exclusao"]);
                    item.log_ativo = DBNull.Value.Equals(dr["log_exclusao"]) ? false : Convert.ToBoolean(dr["log_exclusao"]);

                    item.not_numero = DBNull.Value.Equals(dr["not_numero"]) ? 0 : Convert.ToInt32(dr["not_numero"]);
                    item.cli_id = DBNull.Value.Equals(dr["cli_id"]) ? 0 : Convert.ToInt32(dr["cli_id"]);
                    item.cont_id = DBNull.Value.Equals(dr["cont_id"]) ? 0 : Convert.ToInt32(dr["cont_id"]);
                    item.conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]);
                    item.not_tipoReceita = dr["not_tipoReceita"].ToString();
                    item.not_codReceita = dr["not_codReceita"].ToString();
                    item.not_dtEmissao = DBNull.Value.Equals(dr["not_dtEmissao"]) ? default(DateTime) : Convert.ToDateTime(dr["not_dtEmissao"]);
                    item.not_dtVencimento = DBNull.Value.Equals(dr["not_dtVencimento"]) ? default(DateTime) : Convert.ToDateTime(dr["not_dtVencimento"]);
                    item.not_pis = DBNull.Value.Equals(dr["not_pis"]) ? 0 : Convert.ToDecimal(dr["not_pis"]);
                    item.not_confins = DBNull.Value.Equals(dr["not_confins"]) ? 0 : Convert.ToDecimal(dr["not_confins"]);
                    item.not_cssl = DBNull.Value.Equals(dr["not_cssl"]) ? 0 : Convert.ToDecimal(dr["not_cssl"]);
                    item.not_irrf = DBNull.Value.Equals(dr["not_irrf"]) ? 0 : Convert.ToDecimal(dr["not_irrf"]);
                    item.not_totalbruto = DBNull.Value.Equals(dr["not_totalbruto"]) ? 0 : Convert.ToDecimal(dr["not_totalbruto"]);
                    item.not_totalliquido = DBNull.Value.Equals(dr["not_totalliquido"]) ? 0 : Convert.ToDecimal(dr["not_totalliquido"]);
                    item.not_preenchida = DBNull.Value.Equals(dr["not_preenchida"]) ? 0 : Convert.ToInt32(dr["not_preenchida"]);
                    item.not_emitida = DBNull.Value.Equals(dr["not_emitida"]) ? false : Convert.ToBoolean(dr["not_emitida"]);
                    item.not_situacao = dr["not_situacao"].ToString();
                    item.not_chave = dr["not_chave"].ToString();

                    result.Add(item);
                }
                dr.Close();
            }
            dr.Close();
            return result;
        }

        public List<NTNotasEmitidasBE> CarregaNotasEmitidas(NTNotasBE obj, string tipoSelect = "Emitidas")
        {
            List<NTNotasEmitidasBE> result = new List<NTNotasEmitidasBE>();
            GlobaisClienteDao cliente = new GlobaisClienteDao();
            SCMContratoDao contrato = new SCMContratoDao();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, tipoSelect, this.conectionString, obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    NTNotasEmitidasBE item = new NTNotasEmitidasBE();

                    item.not_id = DBNull.Value.Equals(dr["not_id"]) ? 0 : Convert.ToInt32(dr["not_id"]);

                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    //item.user_id = DBNull.Value.Equals(dr["user_id"]) ? 0 : Convert.ToInt32(dr["user_id"]);
                    item.log_cadastro = Convert.ToDateTime(dr["log_cadastro"]);
                    item.log_alteracao = DBNull.Value.Equals(dr["log_alteracao"]) ? default(DateTime) : Convert.ToDateTime(dr["log_alteracao"]);
                    item.log_exclusao = DBNull.Value.Equals(dr["log_exclusao"]) ? default(DateTime) : Convert.ToDateTime(dr["log_exclusao"]);
                    item.log_ativo = DBNull.Value.Equals(dr["log_ativo"]) ? false : Convert.ToBoolean(dr["log_ativo"]);

                    item.not_numero = DBNull.Value.Equals(dr["not_numero"]) ? 0 : Convert.ToInt32(dr["not_numero"]);
                    item.cli_id = DBNull.Value.Equals(dr["cli_id"]) ? 0 : Convert.ToInt32(dr["cli_id"]);
                    item.cont_id = DBNull.Value.Equals(dr["cont_id"]) ? 0 : Convert.ToInt32(dr["cont_id"]);
                    item.conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]);
                    item.not_tipoReceita = dr["not_tipoReceita"].ToString();
                    item.not_codReceita = dr["not_codReceita"].ToString();
                    item.not_dtEmissao = DBNull.Value.Equals(dr["not_dtEmissao"]) ? default(DateTime) : Convert.ToDateTime(dr["not_dtEmissao"]);
                    item.not_dtVencimento = DBNull.Value.Equals(dr["not_dtVencimento"]) ? default(DateTime) : Convert.ToDateTime(dr["not_dtVencimento"]);
                    item.not_pis = DBNull.Value.Equals(dr["not_pis"]) ? 0 : Convert.ToDecimal(dr["not_pis"]);
                    item.not_confins = DBNull.Value.Equals(dr["not_confins"]) ? 0 : Convert.ToDecimal(dr["not_confins"]);
                    item.not_cssl = DBNull.Value.Equals(dr["not_cssl"]) ? 0 : Convert.ToDecimal(dr["not_cssl"]);
                    item.not_irrf = DBNull.Value.Equals(dr["not_irrf"]) ? 0 : Convert.ToDecimal(dr["not_irrf"]);
                    item.not_totalbruto = DBNull.Value.Equals(dr["not_totalbruto"]) ? 0 : Convert.ToDecimal(dr["not_totalbruto"]);
                    item.not_totalliquido = DBNull.Value.Equals(dr["not_totalliquido"]) ? 0 : Convert.ToDecimal(dr["not_totalliquido"]);
                    item.not_preenchida = DBNull.Value.Equals(dr["not_preenchida"]) ? 0 : Convert.ToInt32(dr["not_preenchida"]);
                    item.not_emitida = DBNull.Value.Equals(dr["not_emitida"]) ? false : Convert.ToBoolean(dr["not_emitida"]);
                    item.not_situacao = dr["not_situacao"].ToString();
                    item.not_chave = dr["not_chave"].ToString();

                    //Lote
                    item.Lote = new NTLoteBE();
                    item.Lote.lote_serie = dr["lote_serie"].ToString();
                    item.Lote.lote_modulo = dr["lote_modulo"].ToString();
                    item.Lote.lote_emissao = DBNull.Value.Equals(dr["lote_emissao"]) ? default(DateTime) : Convert.ToDateTime(dr["lote_emissao"]);

                    //Retorno
                    item.res_id = DBNull.Value.Equals(dr["res_id"]) ? 0 : Convert.ToInt32(dr["res_id"]);
                    item.res_numeroNota = DBNull.Value.Equals(dr["res_numeroNota"]) ? 0 : Convert.ToInt32(dr["res_numeroNota"]);
                    item.res_codigoVerificacao = DBNull.Value.Equals(dr["res_codigoVerificacao"]) ? 0 : Convert.ToInt32(dr["res_codigoVerificacao"]);
                    item.res_dataEmissao = dr["res_dataEmissao"].ToString();

                    item.Cliente = cliente.CarregaClienteCompleto(new GlobaisClienteBE { cli_id = item.cli_id }).FirstOrDefault<GlobaisClienteBE>();
                    item.Contrato = contrato.SelectId<SCMContratoBE>(new SCMContratoBE { cont_id = item.cont_id });

                    result.Add(item);
                }
                dr.Close();
            }
            dr.Close();
            return result;
        }
    }
}
