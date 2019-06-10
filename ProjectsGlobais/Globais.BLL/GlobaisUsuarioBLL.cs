using System.Collections.Generic;
using Globais.BE;
using Globais.Dao;
using System.Linq;
using Globais.Helper;

namespace Globais.BLL
{
    public class GlobaisUsuarioBLL
    {
        public GlobaisUsuarioBE Login(GlobaisUsuarioBE obj)
        {
            return new GlobaisUsuarioDao().LoginUsuario(obj);
        }

        public GlobaisUsuarioBE SelectId(int id)
        {
            return new GlobaisUsuarioDao().LoginUsuarioID(new GlobaisUsuarioBE { usu_id = id });
        }

        public GlobaisUsuarioBE SelectEmail(string email)
        {
            return new GlobaisUsuarioDao().CarregaUsuarioCompleto(new GlobaisUsuarioBE { usu_email = email }).FirstOrDefault<GlobaisUsuarioBE>();
        }

        public List<GlobaisUsuarioBE> SelectBuscaLista(string usu_nome, string usu_email, string usu_telefone, int conf_id)
        {
            return new GlobaisUsuarioDao().CarregaUsuarioCompleto(new GlobaisUsuarioBE{ usu_nome = usu_nome, usu_email = usu_email, usu_telefone = usu_telefone, usu_celular = usu_telefone, conf_id = conf_id });
            //return new GlobaisUsuarioDao().Select<GlobaisUsuarioBE>(new { usu_nome = usu_nome, usu_email = usu_email, usu_telefone = usu_telefone, usu_celular = usu_telefone, conf_id = conf_id }).ToList();
        }

        public bool SenhaAlterar(int idUsuario, string senha, string senhanova)
        {
            return new GlobaisUsuarioDao().SenhaResetar(new { usu_id = idUsuario, usu_senha = senha, usu_novasenha = senhanova });
        }

        public bool SenhaResetar(int idUsuario, string senha)
        {
            return new GlobaisUsuarioDao().SenhaResetar(new { usu_id = idUsuario, usu_senha = senha});
        }


        public GlobaisUsuarioBE Insert(GlobaisUsuarioBE obj)
        {
            obj.end_id = new GlobaisEnderecoDao().Insert(obj.Endereco);
            
            obj.usu_id = new GlobaisUsuarioDao().Insert(obj);
            return obj;
        }

        public bool Update(GlobaisUsuarioBE obj)
        {
            //new GlobaisEnderecoDao().Update(obj.Endereco);
            return new GlobaisUsuarioDao().Update(obj).Value;
        }

        public bool Delete(GlobaisUsuarioBE obj)
        {
            return new GlobaisUsuarioDao().Delete(obj).Value;
        }
    }
}
