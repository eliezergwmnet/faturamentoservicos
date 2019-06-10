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
using Globais.Dao.Base;


namespace Globais.Dao
{
    public class GlobaisEmpresaDadosComplemDao : Functions
    {
        public GlobaisEmpresaDadosComplemDao()
        {
            this.procedure = "proc_GlobaisEmpresaDadosComplem";
        }

        public override IList<T> Select<T>()
        {
            return (IList<T>)this.CarregaDados(null);
        }

        public override IList<T> Select<T>(object obj)
        {
            return (IList<T>)this.CarregaDados((GlobaisEmpresaTaxasNotasBE)obj);
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


        List<GlobaisEmpresaTaxasNotasBE> CarregaDados(GlobaisEmpresaTaxasNotasBE obj)
        {
            List<GlobaisEmpresaTaxasNotasBE> result = new List<GlobaisEmpresaTaxasNotasBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobaisEmpresaTaxasNotasBE item = new GlobaisEmpresaTaxasNotasBE();

                    item.confCom_id = DBNull.Value.Equals(dr["confCom_id "]) ? 0 : Convert.ToInt32(dr["confCom_id "]);
                    item.conf_id = DBNull.Value.Equals(dr["conf_id "]) ? 0 : Convert.ToInt32(dr["conf_id "]);
                    item.confCom_logo = dr["confCom_logo"].ToString();
                    item.confCom_calcularTributos = DBNull.Value.Equals(dr["confCom_calcularTributos"]) ? false : Convert.ToBoolean(dr["confCom_calcularTributos"]);
                    item.confCom_valormaior = DBNull.Value.Equals(dr["confCom_valormaior"]) ? 0 : Convert.ToDecimal(dr["confCom_valormaior"]);
                    item.confCom_pis = DBNull.Value.Equals(dr["confCom_pis"]) ? 0 : Convert.ToDecimal(dr["confCom_pis"]);
                    item.confCom_confins = DBNull.Value.Equals(dr["confCom_confins"]) ? 0 : Convert.ToDecimal(dr["confCom_confins"]);
                    item.confCom_cssl = DBNull.Value.Equals(dr["confCom_cssl"]) ? 0 : Convert.ToDecimal(dr["confCom_cssl"]);
                    item.confCom_irrf = DBNull.Value.Equals(dr["confCom_irrf"]) ? 0 : Convert.ToDecimal(dr["confCom_irrf"]);
                    item.confCom_boleto = DBNull.Value.Equals(dr["confCom_boleto"]) ? false : Convert.ToBoolean(dr["confCom_boleto"]);
                    item.confCom_nota = DBNull.Value.Equals(dr["confCom_nota"]) ? false : Convert.ToBoolean(dr["confCom_nota"]);

                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }
    }
}
