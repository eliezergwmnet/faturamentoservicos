using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Globais.BE;
using Globais.Dao.Base;
using Globais.Helper;

namespace Globais.Dao
{
    public class GlobaisPermissaoDao : Functions
    {
        public GlobaisPermissaoDao()
        {
            this.procedure = "proc_GlobaisPermissao";
        }
        public List<GlobaisPermissaoBE> CarregaPermissaoCompleto(GlobaisPermissaoBE obj)
        {
            List<GlobaisPermissaoBE> result = new List<GlobaisPermissaoBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisPermissao", Helper.TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobaisPermissaoBE item = new GlobaisPermissaoBE();
                    item.Paginas = new List<GlobaisPermissaoPaginaBE>();

                    item.conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]);
                    item.perm_id = DBNull.Value.Equals(dr["perm_id"])? 0 : Convert.ToInt32(dr["perm_id"]);
                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    item.per_adminisrador = DBNull.Value.Equals(dr["per_adminisrador"]) ? false : Convert.ToBoolean(dr["per_adminisrador"]);
                    item.perm_nome = dr["perm_nome"].ToString();

                    var xml = dr["pages"].ToString();
                    XmlDocument xm = new XmlDocument();
                    if (xml != "")
                    {
                        xm.LoadXml(@xml);

                        foreach (XmlElement linha in xm.GetElementsByTagName("row"))
                        {
                            item.Paginas.Add(new GlobaisPermissaoPaginaBE
                            {
                                permPag_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "permPag_id")),
                                perm_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "perm_id")),
                                permPag_url = Common.ConvertXMLItem(linha, "permPag_url"),
                                permPag_inserir = Common.ConvertXMLItem(linha, "permPag_inserir") == "1" ? true : false,
                                permPag_alterar = Common.ConvertXMLItem(linha, "permPag_alterar") == "1" ? true : false,
                                permPag_excluir = Common.ConvertXMLItem(linha, "permPag_excluir") == "1" ? true : false,
                            });
                        }
                    }

                    result.Add(item);
                }
                dr.Close();
            }
            return result;
        }
    }
}
