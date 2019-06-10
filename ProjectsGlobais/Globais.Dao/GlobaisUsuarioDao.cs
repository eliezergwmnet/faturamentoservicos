using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Globais.BE;
using Globais.Dao.Base;
using System.Linq;

namespace Globais.Dao
{
    public class GlobaisUsuarioDao : Functions
    {
        public GlobaisUsuarioDao()
        {
            this.procedure = "proc_GlobaisUsuario";
        }

        public GlobaisUsuarioBE LoginUsuario(GlobaisUsuarioBE obj)
        {
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisUsuario", "login", obj);

            if (dr.HasRows)
                return this.ConvertDR(dr).FirstOrDefault<GlobaisUsuarioBE>();
            else
                return null;
        }

        public GlobaisUsuarioBE LoginUsuarioID(GlobaisUsuarioBE obj)
        {
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisUsuario", "loginid", obj);

            if (dr.HasRows)
                return this.ConvertDR(dr).FirstOrDefault<GlobaisUsuarioBE>();
            else
                return null;
        }

        public List<GlobaisUsuarioBE> CarregaUsuarioCompleto(GlobaisUsuarioBE obj)
        {
            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisUsuario", Helper.TipoSql.Select.ToString(), obj);

            if (dr.HasRows)
                return this.ConvertDR(dr);
            else
                return null;
        }

        public bool SenhaAlterar(object obj)
        {
            int retorno = new BaseDados().ExecutaFuncao("proc_GlobaisUsuario", "senha", obj).Value;
            return retorno == 1 ? true : false;
        }

        public bool SenhaResetar(object obj)
        {
            int retorno = new BaseDados().ExecutaFuncao("proc_GlobaisUsuario", "retsenha", obj).Value;
            return retorno == 1 ? true : false;
        }


        List<GlobaisUsuarioBE> ConvertDR(SqlDataReader dr)
        {
            List<GlobaisUsuarioBE> retorno = new List<GlobaisUsuarioBE>();
            GlobaisUsuarioBE item;
            while (dr.Read())
            {
                item = new GlobaisUsuarioBE();
                item.Endereco = new GlobaisEnderecoBE();
                item.Permissao = new GlobaisPermissaoBE();

                item.usu_id = DBNull.Value.Equals(dr["usu_id"]) ? 0 : Convert.ToInt32(dr["usu_id"]);
                item.end_id = DBNull.Value.Equals(dr["end_id"]) ? 0 : Convert.ToInt32(dr["end_id"]);
                item.perm_id = DBNull.Value.Equals(dr["perm_id"]) ? 0 : Convert.ToInt32(dr["perm_id"]);
                item.log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]);
                item.usu_nome = dr["usu_nome"].ToString();
                item.usu_email = dr["usu_email"].ToString();
                item.usu_senha = dr["usu_senha"].ToString();
                item.usu_telefone = dr["usu_telefone"].ToString();
                item.usu_celular = dr["usu_celular"].ToString();
                item.usu_imagem = dr["usu_imagem"].ToString();
                item.conf_id = Convert.ToInt32(dr["conf_id"]);

                item.Endereco = new GlobaisEnderecoBE
                {
                    end_id = DBNull.Value.Equals(dr["end_id"]) ? 0 : Convert.ToInt32(dr["end_id"]),
                    end_cep = dr["end_cep"].ToString(),
                    end_logradouro = dr["end_logradouro"].ToString(),
                    end_numero = dr["end_numero"].ToString(),
                    end_complemento = dr["end_complemento"].ToString(),
                    end_bairro = dr["end_bairro"].ToString(),
                    end_cidade = dr["end_cidade"].ToString(),
                    end_estado = dr["end_estado"].ToString(),
                    end_latitude = dr["end_latitude"].ToString(),
                    end_longetitude = dr["end_longetitude"].ToString(),
                };
                item.Permissao = new GlobaisPermissaoBE
                {
                    perm_id = DBNull.Value.Equals(dr["perm_id"]) ? 0 : Convert.ToInt32(dr["perm_id"]),
                    log_id = DBNull.Value.Equals(dr["log_id"]) ? 0 : Convert.ToInt32(dr["log_id"]),
                    conf_id = DBNull.Value.Equals(dr["conf_id"]) ? 0 : Convert.ToInt32(dr["conf_id"]),
                    perm_nome = dr["perm_nome"].ToString(),
                    per_adminisrador = DBNull.Value.Equals(dr["per_adminisrador"]) ? false : Convert.ToBoolean(dr["per_adminisrador"])
                };
                var xml = dr["Paginas"].ToString();
                System.Xml.XmlDocument xm = new XmlDocument();
                /*xm.LoadXml(@xml);
                item.Permissao.Paginas = new List<GlobaisPermissaoPaginaBE>();

                foreach (XmlElement linha in xm.GetElementsByTagName("row"))
                {
                    item.Permissao.Paginas.Add(new GlobaisPermissaoPaginaBE
                    {
                        permPag_id = Convert.ToInt32(linha.GetElementsByTagName("permPag_id").Item(0).InnerText),
                        perm_id = Convert.ToInt32(linha.GetElementsByTagName("perm_id").Item(0).InnerText),
                        permPag_url = linha.GetElementsByTagName("permPag_url").Item(0).InnerText.ToString(),
                        permPag_inserir = linha.GetElementsByTagName("permPag_inserir").Item(0).InnerText == "1" ? true : false,
                        permPag_alterar = linha.GetElementsByTagName("permPag_alterar").Item(0).InnerText == "1" ? true : false,
                        permPag_excluir = linha.GetElementsByTagName("permPag_excluir").Item(0).InnerText == "1" ? true : false,
                    });
                }*/
                retorno.Add(item);
            }
            //dr.Close();

            return retorno;
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
