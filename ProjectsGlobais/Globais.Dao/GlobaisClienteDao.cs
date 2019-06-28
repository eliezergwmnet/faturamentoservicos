using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Globais.BE;
using Globais.Dao.Base;
using Globais.Helper;

namespace Globais.Dao
{
    public class GlobaisClienteDao: Functions
    {
        public GlobaisClienteDao()
        {
            this.procedure = "proc_GlobaisCliente";
        }
        public List<GlobaisClienteBE> CarregaClienteCompleto(GlobaisClienteBE obj)
        {
            List<GlobaisClienteBE> result = new List<GlobaisClienteBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisCliente", "cliComp", obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobaisClienteBE item = new GlobaisClienteBE();
                    item.Endereco = new List<GlobaisClienteEnderecoBE>();
                    item.Contato = new List<GlobaisClienteContatoBE>();

                    item.conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]);
                    item.cli_id = DBNull.Value.Equals(dr["cli_id"])? 0 : Convert.ToInt32(dr["cli_id"]);
                    item.usu_id = DBNull.Value.Equals(dr["usu_id"]) ? 0 : Convert.ToInt32(dr["usu_id"]);
                    item.cli_CFOP = DBNull.Value.Equals(dr["cli_CFOP"]) ? 0 : Convert.ToDecimal(dr["cli_CFOP"]);
                    item.cli_SCM = DBNull.Value.Equals(dr["cli_SCM"])? false : Convert.ToBoolean(dr["cli_SCM"]);
                    item.cli_MNT = DBNull.Value.Equals(dr["cli_MNT"]) ? false : Convert.ToBoolean(dr["cli_MNT"]);
                    item.cli_simplesNacional = DBNull.Value.Equals(dr["cli_simplesNacional"]) ? false : Convert.ToBoolean(dr["cli_simplesNacional"]);
                    item.cli_tipoVencimento = dr["cli_tipoVencimento"].ToString();
                    item.cli_parametroVencimento = dr["cli_parametroVencimento"].ToString();
                    item.cli_nomeFantasia = dr["cli_nomeFantasia"].ToString();
                    item.cli_razaoSocial = dr["cli_razaoSocial"].ToString();
                    item.cli_tipoInscricao = dr["cli_tipoInscricao"].ToString();
                    item.cli_CGC = dr["cli_CGC"].ToString();
                    item.cli_CPF = dr["cli_CPF"].ToString();
                    item.cli_CodCidIBGE = dr["cli_CodCidIBGE"].ToString();
                    item.cli_incricaoEstadual = dr["cli_incricaoEstadual"].ToString();


                    var xml = dr["endereco"].ToString();
                    XmlDocument xm = new XmlDocument();
                    if (xml != "")
                    {
                        xm.LoadXml(@xml);

                        foreach (XmlElement linha in xm.GetElementsByTagName("row"))
                        {
                            item.Endereco.Add(new GlobaisClienteEnderecoBE
                            {
                                cliEnd_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "cliEnd_id")),
                                cliEnd_Tipo = Common.ConvertXMLItem(linha, "cliEnd_Tipo"),
                                cli_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "cli_id")),
                                end_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "end_id")),
                                end_cep = Common.ConvertXMLItem(linha, "end_cep"),
                                end_logradouro = Common.ConvertXMLItem(linha, "end_logradouro"),
                                end_numero = Common.ConvertXMLItem(linha, "end_numero"),
                                end_complemento = Common.ConvertXMLItem(linha, "end_complemento"),
                                end_bairro = Common.ConvertXMLItem(linha, "end_bairro"),
                                end_cidade = Common.ConvertXMLItem(linha, "end_cidade"),
                                end_estado = Common.ConvertXMLItem(linha, "end_estado"),
                                end_latitude = Common.ConvertXMLItem(linha, "end_latitude"),
                                end_longetitude = Common.ConvertXMLItem(linha, "end_longetitude"),

                            });
                        }
                    }

                    xml = dr["contato"].ToString();
                    if (xml != "")
                    {
                        XmlDocument xm1 = new XmlDocument();
                        xm1.LoadXml(@xml);

                        foreach (XmlElement linha in xm1.GetElementsByTagName("row"))
                        {
                            item.Contato.Add(new GlobaisClienteContatoBE
                            {
                                cont_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "cont_id")),
                                cli_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "cli_id")),
                                cont_nome = Common.ConvertXMLItem(linha, "cont_nome"),
                                cont_ddd = Common.ConvertXMLItem(linha, "cont_fone"),
                                cont_fone = Common.ConvertXMLItem(linha, "cont_fone"),
                                cont_departamento = Common.ConvertXMLItem(linha, "cont_departamento"),
                                cont_email = Common.ConvertXMLItem(linha, "cont_email"),

                            });
                        }
                    }

                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }
        /* public override IList<T> Select<T>()
         {
             this.procedure = "proc_Cliente_Select";
             return base.Select<T>();
         }

         public override IList<T> Select<T>(object obj)
         {
             this.procedure = "proc_Cliente_Select";
             return base.Select<T>(obj);
         }
         public override T SelectId<T>(object obj)
         {
             this.procedure = "proc_Cliente_Select";
             return base.SelectId<T>(obj);
         }
         public override int Insert(object obj)
         {
             this.procedure = "proc_Cliente_Insert";
             return base.Insert(obj);
         }
         public override bool? Update(object obj)
         {
             this.procedure = "proc_Cliente_Update";
             return base.Update(obj).Value;
         }
         public override bool? Delete(object obj)
         {
             this.procedure = "proc_Cliente_Delete";
             return base.Delete(obj).Value;
         }*/
    }
}
