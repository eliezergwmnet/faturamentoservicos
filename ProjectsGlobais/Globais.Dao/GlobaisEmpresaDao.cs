using System;
using System.Data.SqlClient;
using Globais.BE;
using Globais.Dao.Base;

namespace Globais.Dao
{
    public class GlobaisEmpresaDao : Functions
    {
        public GlobaisEmpresaDao()
        {
            this.procedure = "proc_GlobaisEmpresa";
        }

        /*public GlobaisEmpresaCompletaBE SelectCompleto(int conf_id)
        {
            GlobaisEmpresaCompletaBE result = new GlobaisEmpresaCompletaBE();

            GlobaisEmpresaCompletaBE result = (GlobaisEmpresaCompletaBE)RetornaListaDados<GlobaisEmpresaCompletaBE>((this.procedure, "selComp", new { conf_id = conf_id }).FirstOrDefault<GlobaisEmpresaCompletaBE>();

            SqlDataReader dr = new BaseDados().RetornaDataReader("proc_GlobaisCliente", "selComp", );



            return result;
        }*/

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
    }
}
