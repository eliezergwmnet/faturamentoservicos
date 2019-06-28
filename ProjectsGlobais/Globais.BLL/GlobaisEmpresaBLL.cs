using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;
using Globais.Helper;

namespace Globais.BLL
{
    public class GlobaisEmpresaBLL
    {
        public List<GlobaisEmpresaBE> Select()
        {
            return new GlobaisEmpresaDao().Select<GlobaisEmpresaBE>().ToList();
        }
        public List<GlobaisEmpresaBE> Select(GlobaisEmpresaBE obj)
        {
            return new GlobaisEmpresaDao().Select<GlobaisEmpresaBE>(obj).ToList();
        }
        public GlobaisEmpresaBE SelectId(GlobaisEmpresaBE obj)
        {
            return new GlobaisEmpresaDao().Select<GlobaisEmpresaBE>(obj).FirstOrDefault<GlobaisEmpresaBE>();
        }
        public GlobaisEmpresaBE Insert(GlobaisEmpresaBE obj, GlobaisEnderecoBE _endereco)
        {
            _endereco = new GlobaisEnderecoBLL().Insert(_endereco);
            obj.end_id = _endereco.end_id;
            obj.conf_id = new GlobaisEmpresaDao().InsertEmpresa(obj);

            string senha = Common.GeradorDeSenha();
            var userNovo = new GlobaisUsuarioBLL().Insert(new GlobaisUsuarioBE
            {
                end_id = obj.conf_id,
                perm_id = 4,//Ajusta um perfil padrão para cada modulo
                usu_nome = obj.conf_nomefantasia,
                usu_email = obj.conf_email,
                usu_telefone = obj.conf_telefone,
                usu_celular = obj.conf_celular,
                usu_senha = Common.CriptografiaSenha(Common.GeradorDeSenha()),
                Endereco = _endereco

            });
            new GlobaisEmailBLL().EmailNovoCadastro(obj.conf_email, senha);

            return obj;
        }
        public bool Update(GlobaisEmpresaBE obj)
        {
            var endereco = new GlobaisEnderecoBE
            {
                end_id = obj.end_id,
                end_bairro = obj.end_bairro,
                end_cep = obj.end_cep,
                end_cidade = obj.end_cidade,
                end_complemento = obj.end_complemento,
                end_estado = obj.end_estado,
                end_latitude = obj.end_latitude,
                end_logradouro = obj.end_logradouro,
                end_longetitude = obj.end_longetitude,
                end_numero = obj.end_numero
            };
            new GlobaisEnderecoDao().Update(endereco);
            return new GlobaisEmpresaDao().Update(obj).Value;
        }
        public bool Delete(GlobaisEmpresaBE obj)
        {
            return new GlobaisEmpresaDao().Delete(obj).Value;
        }
    }
}
