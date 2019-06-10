using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class PermissaoPaginaBLL
    {
        public List<PermissaoPaginaBE> Select()
        {
            return new PermissaoPaginaDao().Select<PermissaoPaginaBE>().ToList();
        }
        public List<PermissaoPaginaBE> SelectPermissao(int _perm_id)
        {
            return new PermissaoPaginaDao().SelectPermissao(_perm_id).ToList();
        }

        public List<PermissaoPaginaBE> Select(PermissaoPaginaBE obj)
        {
            return new PermissaoPaginaDao().Select<PermissaoPaginaBE>(obj).ToList();
        }
        public PermissaoPaginaBE SelectId(PermissaoPaginaBE obj)
        {
            return new PermissaoPaginaDao().SelectId<PermissaoPaginaBE>(obj);
        }
        public PermissaoPaginaBE Insert(string tipo, string page, int perfil, int idpage)
        {
            var obj = new PermissaoPaginaBE();
            if (idpage != 0)
                obj = this.SelectId(new PermissaoPaginaBE { permPag_id = idpage });
            else
                obj = this.SelectId(new PermissaoPaginaBE { permPag_url = page, perm_id = perfil });

            if(obj == null)
            {
                obj = new PermissaoPaginaBE();
                obj.permPag_url = page;
                obj.perm_id = perfil;
            }

            if (tipo == "inseriitem")
                obj.permPag_inserir = obj.permPag_inserir ? false : true;
            if (tipo == "alteraritem")
                obj.permPag_alterar = obj.permPag_alterar ? false : true;
            if (tipo == "removeritem")
                obj.permPag_excluir = obj.permPag_excluir ? false : true;

            obj.perm_id = new PermissaoPaginaDao().Insert(obj);
            return new PermissaoPaginaBE();
        }

    }
}
