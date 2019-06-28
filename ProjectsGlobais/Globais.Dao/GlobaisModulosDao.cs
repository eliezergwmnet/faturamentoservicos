using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Globais.BE;
using Globais.Dao.Base;
using Globais.Helper;

namespace Globais.Dao
{

    #region Modulos
    public class GlobaisModulosDao : Functions
    {
        public GlobaisModulosDao()
        {
            this.procedure = "proc_GlobaisModulos";
        }

        public List<GlobaisModulosBE> CarregaLista(GlobaisModulosBE obj)
        {
            List<GlobaisModulosBE> result = new List<GlobaisModulosBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisModulos", TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobaisModulosBE item = new GlobaisModulosBE();
                    item.Paginas = new List<GlobaisModulosPagesBE>();

                    item.mod_id = DBNull.Value.Equals(dr["mod_id"]) ? 0 : Convert.ToInt32(dr["mod_id"]);
                    item.mod_image = dr["mod_image"].ToString();
                    item.mod_nome = dr["mod_nome"].ToString();
                    item.mod_descricao = dr["mod_descricao"].ToString();

                    //Log
                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    item.user_id = DBNull.Value.Equals(dr["user_id"]) ? 0 : Convert.ToInt32(dr["user_id"]);
                    item.log_cadastro = Convert.ToDateTime(dr["log_cadastro"]);
                    item.log_alteracao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_alteracao"]);
                    item.log_exclusao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_exclusao"]);


                    var xml = dr["page"].ToString();
                    XmlDocument xm = new XmlDocument();
                    if (xml != "")
                    {
                        xm.LoadXml(@xml);

                        foreach (XmlElement linha in xm.GetElementsByTagName("row"))
                        {
                            item.Paginas.Add(new GlobaisModulosPagesBE
                            {
                                modPage_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "modPage_id")),
                                mod_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "mod_id")),
                                modPage_nome = Common.ConvertXMLItem(linha, "modPage_nome"),
                                modPage_descricao = Common.ConvertXMLItem(linha, "modPage_descricao"),
                                modPage_icone = Common.ConvertXMLItem(linha, "modPage_icone"),
                                modPage_cadTitulo = Common.ConvertXMLItem(linha, "modPage_cadTitulo"),
                                modPage_url = Common.ConvertXMLItem(linha, "modPage_url"),

                            });
                        }
                    }
                    result.Add(item);

                }
            }
            dr.Close();

            return result;
        }

        public List<GlobaisModulosBE> CarregaLista(object obj)
        {
            List<GlobaisModulosBE> result = new List<GlobaisModulosBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisModulos", TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobaisModulosBE item = new GlobaisModulosBE();
                    item.Paginas = new List<GlobaisModulosPagesBE>();

                    item.mod_id = DBNull.Value.Equals(dr["mod_id"]) ? 0 : Convert.ToInt32(dr["mod_id"]);
                    item.mod_image = dr["mod_image"].ToString();
                    item.mod_nome = dr["mod_nome"].ToString();
                    item.mod_descricao = dr["mod_descricao"].ToString();

                    //Log
                    item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                    item.user_id = DBNull.Value.Equals(dr["user_id"]) ? 0 : Convert.ToInt32(dr["user_id"]);
                    item.log_cadastro = Convert.ToDateTime(dr["log_cadastro"]);
                    item.log_alteracao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_alteracao"]);
                    item.log_exclusao = DBNull.Value.Equals(dr["user_id"]) ? default(DateTime) : Convert.ToDateTime(dr["log_exclusao"]);


                    var xml = dr["page"].ToString();
                    XmlDocument xm = new XmlDocument();
                    if (xml != "")
                    {
                        xm.LoadXml(@xml);

                        foreach (XmlElement linha in xm.GetElementsByTagName("row"))
                        {
                            item.Paginas.Add(new GlobaisModulosPagesBE
                            {
                                modPage_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "modPage_id")),
                                mod_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "mod_id")),
                                modPage_nome = Common.ConvertXMLItem(linha, "modPage_nome"),
                                modPage_descricao = Common.ConvertXMLItem(linha, "modPage_descricao"),
                                modPage_icone = Common.ConvertXMLItem(linha, "modPage_icone"),
                                modPage_cadTitulo = Common.ConvertXMLItem(linha, "modPage_cadTitulo"),
                                modPage_url = Common.ConvertXMLItem(linha, "modPage_url"),

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

    #endregion

    #region Paginas do Modulo
    public class GlobaisModulosPagesDao : Functions
    {
        public GlobaisModulosPagesDao()
        {
            this.procedure = "proc_GlobaisModulosPages";
        }
    }

    #endregion

    #region Modulos por Empresa

    public class GlobaisModulosEmpresaDao : Functions
    {
        public GlobaisModulosEmpresaDao()
        {
            this.procedure = "proc_GlobaisModulosEmpresa";
        }
    }

    #endregion
}
