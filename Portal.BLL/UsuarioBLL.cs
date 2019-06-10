using System;
using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Helper;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class UsuarioBLL
    {
       /* public List<UsuarioBE> Select(int conf_id)
        {
            return this.CarregaUsuario(new UsuarioDao().Select<UsuarioBE>(new { conf_id = conf_id }).ToList());
        }*/
       /* public List<UsuarioBE> Select(UsuarioBE obj)
        {
            return this.CarregaUsuario(new UsuarioDao().Select<UsuarioBE>(obj).ToList());
        }*/

       /* public List<UsuarioBE> SelectBuscaLista(string usu_nome, string usu_email, string usu_telefone, int conf_id)
        {
            List<UsuarioBE> retorno;
            if (!String.IsNullOrEmpty(usu_nome) || !String.IsNullOrEmpty(usu_email) || !String.IsNullOrEmpty(usu_telefone))
                retorno = this.CarregaUsuario(new UsuarioDao().Select<UsuarioBE>(new { usu_nome = usu_nome, usu_email = usu_email, usu_telefone = usu_telefone, usu_celular = usu_telefone, conf_id = conf_id }).ToList());
            else
                retorno = this.CarregaUsuario(new UsuarioDao().Select<UsuarioBE>(new { conf_id  = conf_id }).ToList());
            return retorno;
        }*/

        /*public UsuarioBE SelectId(UsuarioBE obj)
        {
            obj = new UsuarioDao().SelectId<UsuarioBE>(obj);
            if (obj != null)
            {
                obj.Endereco = new EnderecoBLL().SelectId(new GlobaisEnderecoBE { end_id = obj.end_id });
                obj.Permissao = new PermissaoBLL().SelectId(new PermissaoBE { perm_id = obj.perm_id });
                obj.Empresas = new EmpresaUsuarioEmpresaDao().Select<UsuarioEmpresaBE>(new UsuarioEmpresaBE { useemp_user = obj.usu_id }).ToList();
            }
            return obj;
        }*/
        /*public UsuarioBE Insert(UsuarioBE obj)
        {
            if(obj.usu_imagem != null && obj.usu_imagem != null)
                obj.usu_imagem = new ConverterImagem().Base64ToImage(obj.usu_imagem.Replace("/Imagem/Usuario/", ""));
            obj.end_id = new EnderecoDao().Insert(obj.Endereco);
            obj.usu_id = new UsuarioDao().Insert(obj);
            return obj;
        }
        public bool Update(UsuarioBE obj)
        {
            if (obj.usu_imagem != null && obj.usu_imagem != "")
                obj.usu_imagem = new ConverterImagem().Base64ToImage(obj.usu_imagem);
            new EnderecoDao().Update(obj.Endereco);
            return new UsuarioDao().Update(obj).Value;
        }
        public bool Delete(UsuarioBE obj)
        {
            return new UsuarioDao().Delete(obj).Value;
        }*/
      /*  List<UsuarioBE> CarregaUsuario(List<UsuarioBE> obj)
        {
            EnderecoBLL _bll = new EnderecoBLL();
            PermissaoBLL _con = new PermissaoBLL();
            EmpresaUsuarioEmpresaDao _emp = new EmpresaUsuarioEmpresaDao();

            foreach (var item in obj)
            {
                item.Endereco = _bll.SelectId(new GlobaisEnderecoBE { end_id = item.end_id });
                item.Permissao = _con.SelectId(new PermissaoBE { perm_id = item.perm_id });
                item.Empresas = _emp.Select<UsuarioEmpresaBE>(new UsuarioEmpresaBE { useemp_user = item.usu_id }).ToList();
            }
            return obj;
        }*/

       /* public bool AddRemoveEmpresa(int user, int empresa, int tipo)
        {
            if (tipo == 0)
                new EmpresaUsuarioEmpresaDao().Delete(new UsuarioEmpresaBE { useemp_empresa = empresa, useemp_user = user });
            else
                new EmpresaUsuarioEmpresaDao().Insert(new UsuarioEmpresaBE { useemp_empresa = empresa, useemp_user = user });
            return true;
        }*/
    }
}
