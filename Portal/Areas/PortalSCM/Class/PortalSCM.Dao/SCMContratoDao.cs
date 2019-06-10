using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Globais.BE;
using Globais.Dao.Base;
using Globais.Helper;
using PortalSCM.BE;
using PortalSCM.Helper;

namespace PortalSCM.Dao
{
    public class SCMContratoDao : Functions
    {
        public SCMContratoDao()
        {
            this.procedure = "proc_SCMContrato";
            this.conectionString = GlobaisSCM.ConectionDataBaseGlobal;
        }

        /// <summary>
        /// Retorna todos os serviços que ainda não foram faturados no mes
        /// o parametro "cont_avulso" indica se vai fatura os serviços mensais ou os serviços avulso
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<SCMContratoBE> SelectFaturamentoMensalPendente(SCMContratoBE obj)
        {
            return this.CarregaContrato(obj, "faturmes");
        }

        /// <summary>
        /// Carrega os itens que ainda nao foram faturados filtrados pelo contrato
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<SCMContratoBE> SelectFaturamentoMensalContratoPendente(SCMContratoBE obj)
        {
            return this.CarregaContrato(obj, "faturmcont");
        }

        public override IList<T> Select<T>()
        {
            return (IList<T>)this.CarregaContrato(null, TipoSql.Select.ToString());
        }

        public override IList<T> Select<T>(object obj)
        {
            return (IList<T>)this.CarregaContrato((SCMContratoBE)obj, TipoSql.Select.ToString());
        }

        List<SCMContratoBE> CarregaContrato(SCMContratoBE obj, string select)
        {
            List<SCMContratoBE> result = new List<SCMContratoBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_SCMContrato", select, GlobaisSCM.ConectionDataBaseGlobal, obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    SCMContratoBE item = new SCMContratoBE();
                    item.Servicos = new List<SCMClienteServicosBE>();
                    item.Cliente = new GlobaisClienteBE();

                    item.cont_id = DBNull.Value.Equals(dr["cont_id"]) ? 0 : Convert.ToInt32(dr["cont_id"]);
                    item.cli_id = DBNull.Value.Equals(dr["cli_id"]) ? 0 : Convert.ToInt32(dr["cli_id"]);
                    item.cont_data = Convert.ToDateTime(dr["cont_data"]);
                    item.conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]);
                    item.cont_fatura = DBNull.Value.Equals(dr["cont_fatura"]) ? false : Convert.ToBoolean(dr["cont_fatura"]);
                    item.cont_avulso = DBNull.Value.Equals(dr["cont_avulso"]) ? false : Convert.ToBoolean(dr["cont_avulso"]);
                    item.cont_numero = dr["cont_numero"].ToString();
                    item.cont_nome = dr["cont_nome"].ToString();
                    item.cont_descricao = dr["cont_descricao"].ToString();

                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    item.user_id = DBNull.Value.Equals(dr["user_id"]) ? 0 : Convert.ToInt32(dr["user_id"]);
                    item.log_cadastro = Convert.ToDateTime(dr["log_cadastro"]);
                    item.log_alteracao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_alteracao"]);
                    item.log_exclusao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_exclusao"]);


                    var xml = dr["servico"].ToString();
                    XmlDocument xm = new XmlDocument();
                    if (xml != "")
                    {
                        xm.LoadXml(@xml);

                        foreach (XmlElement linha in xm.GetElementsByTagName("row"))
                        {
                            item.Servicos.Add(new SCMClienteServicosBE
                            {
                                servCli_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "servCli_id")),
                                cli_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "cli_id")),
                                serv_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "serv_id")),
                                cont_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "cont_id")),
                                servCli_nome = Common.ConvertXMLItem(linha, "servCli_nome"),
                                servCli_descricao = Common.ConvertXMLItem(linha, "servCli_descricao"),
                                servCli_valor = Convert.ToDecimal(Common.ConvertXMLItem(linha, "servCli_valor")),
                                servCli_parcelado = Common.ConvertXMLItem(linha, "servCli_parcelado") == "1" ? true : false,
                                servCli_parceladoQtd = Convert.ToInt32(Common.ConvertXMLItem(linha, "servCli_parceladoQtd")),
                                servCli_cobrarPorpor = Convert.ToInt32(Common.ConvertXMLItem(linha, "servCli_cobrarPorpor")),
                                servCli_qtdDias = Convert.ToInt32(Common.ConvertXMLItem(linha, "servCli_qtdDias")),
                                servCli_dataAtivacao = Convert.ToDateTime(Common.ConvertXMLItem(linha, "servCli_dataAtivacao")),
                                servCli_data = Convert.ToDateTime(Common.ConvertXMLItem(linha, "servCli_data"))
                            });
                        }
                    }

                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }
        /*public override IList<T> Select<T>()
         {
             //this.procedure = "proc_Cliente_Select";
             return base.Select<T>();
         }

         public override IList<T> Select<T>(object obj)
         {
             //this.procedure = "proc_Cliente_Select";
             return base.Select<T>(obj);
         }
         public override T SelectId<T>(object obj)
         {
             //this.procedure = "proc_Cliente_Select";
             return base.SelectId<T>(obj);
         }
         public override int Insert(object obj)
         {
             //this.procedure = "proc_Cliente_Insert";
             return base.Insert(obj);
         }
         public override bool? Update(object obj)
         {
             //this.procedure = "proc_Cliente_Update";
             return base.Update(obj).Value;
         }
         public override bool? Delete(object obj)
         {
             //this.procedure = "proc_Cliente_Delete";
             return base.Delete(obj).Value;
         }*/
    }
}
