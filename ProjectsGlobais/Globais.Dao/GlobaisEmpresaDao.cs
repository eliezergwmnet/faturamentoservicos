using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Globais.BE;
using Globais.Dao.Base;
using Globais.Helper;
using System.Linq;

namespace Globais.Dao
{
    public class GlobaisEmpresaDao : Functions
    {
        public GlobaisEmpresaDao()
        {
            this.procedure = "proc_GlobaisEmpresa";
        }

        public override IList<T> Select<T>()
        {
            return (IList<T>)this.CarregaDados(null);
        }

        public override IList<T> Select<T>(object obj)
        {
            return (IList<T>)this.CarregaDados((GlobaisEmpresaBE)obj);
        }



        public int InsertEmpresa(GlobaisEmpresaBE item)
        {
            var obj = new
            {
                conf_nomefantasia = item.conf_nomefantasia,
                conf_razaosocial = item.conf_razaosocial,
                conf_cnpj = item.conf_cnpj,
                conf_inscricaoestadual = item.conf_inscricaoestadual,
                conf_telefone = item.conf_telefone,
                conf_celular = item.conf_celular,
                conf_celularwhats = item.conf_celularwhats,
                conf_email = item.conf_email,
                conf_dominio = item.conf_dominio,
                conf_imagempequena = item.conf_imagempequena,
                conf_imagemgrande = item.conf_imagemgrande,
                conf_layoutcor = item.conf_layoutcor,
                end_id = item.end_id
            };
            var retorno = ExecutaItem(Helper.TipoSql.Insert.ToString(), obj);
            if (retorno != null)
                return Convert.ToInt32(retorno);
            else
                return 0;
        }

        List<GlobaisEmpresaBE> CarregaDados(GlobaisEmpresaBE obj)
        {
            List<GlobaisEmpresaBE> result = new List<GlobaisEmpresaBE>();
            SqlDataReader dr = new BaseDados().RetornaDataReader(this.procedure, Globais.Helper.TipoSql.Select.ToString(), obj);
            GlobaisModulosDao _modulo = new GlobaisModulosDao();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    GlobaisEmpresaBE item = new GlobaisEmpresaBE();

                    item.conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]);
                    item.end_id = DBNull.Value.Equals(dr["end_id"]) ? 0 : Convert.ToInt32(dr["end_id"]);
                    item.conf_nomefantasia = dr["conf_nomefantasia"].ToString();
                    item.conf_razaosocial = dr["conf_razaosocial"].ToString();
                    item.conf_cnpj = dr["conf_cnpj"].ToString();
                    item.conf_inscricaoestadual = dr["conf_inscricaoestadual"].ToString();
                    item.conf_telefone = dr["conf_telefone"].ToString();
                    item.conf_celular = dr["conf_celular"].ToString();
                    item.conf_celularwhats = DBNull.Value.Equals(dr["conf_celularwhats"]) ? false : Convert.ToBoolean(dr["conf_celularwhats"]);
                    item.conf_email = dr["conf_email"].ToString();
                    item.conf_dominio = dr["conf_dominio"].ToString();
                    item.conf_imagempequena = dr["conf_imagempequena"].ToString();
                    item.conf_imagemgrande = dr["conf_imagemgrande"].ToString();
                    item.conf_layoutcor = dr["conf_layoutcor"].ToString();

                    item.end_cep = dr["end_cep"].ToString();
                    item.end_logradouro = dr["end_logradouro"].ToString();
                    item.end_numero = dr["end_numero"].ToString();
                    item.end_complemento = dr["end_complemento"].ToString();
                    item.end_bairro = dr["end_bairro"].ToString();
                    item.end_cidade = dr["end_cidade"].ToString();
                    item.end_estado = dr["end_estado"].ToString();
                    item.end_latitude = dr["end_latitude"].ToString();
                    item.end_longetitude = dr["end_longetitude"].ToString();

                    var xml = dr["modulos"].ToString();
                    XmlDocument xm = new XmlDocument();
                    item.Modulos = new List<GlobaisModulosBE>();
                    if (xml != "")
                    {
                        xm.LoadXml(@xml);

                        foreach (XmlElement linha in xm.GetElementsByTagName("row"))
                        {
                            item.Modulos.Add(
                                _modulo.CarregaLista(
                                    new GlobaisModulosBE
                                    {
                                        mod_id = Convert.ToInt32(Common.ConvertXMLItem(linha, "mod_id"))
                                    }
                               ).FirstOrDefault<GlobaisModulosBE>()
                            );
                        }
                    }

                    result.Add(item);
                }
                
            }
            dr.Close();

            return result;
        }
    }
}
