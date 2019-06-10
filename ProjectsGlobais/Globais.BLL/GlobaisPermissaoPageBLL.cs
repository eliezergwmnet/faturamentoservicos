using System.Collections.Generic;
using Globais.BE;
using Globais.Dao;
using System.Linq;
using Globais.Helper;

namespace Globais.BLL
{
    public class GlobaisPermissaoPageBLL
    {
        public List<GlobaisPermissaoPaginaBE> Select()
        {
            return new GlobaisPermissaoPageDao().Select<GlobaisPermissaoPaginaBE>(null).ToList();
        }

        public List<GlobaisPermissaoPaginaBE> Select(GlobaisPermissaoPaginaBE obj)
        {
            return new GlobaisPermissaoPageDao().Select<GlobaisPermissaoPaginaBE>(obj).ToList();
        }

        public GlobaisPermissaoPaginaBE SelectID(GlobaisPermissaoPaginaBE obj)
        {
            return new GlobaisPermissaoPageDao().Select<GlobaisPermissaoPaginaBE>(obj).FirstOrDefault<GlobaisPermissaoPaginaBE>();
        }

        public int Insert(TipoPagePermissao tipoPage, string page, int perfil, int idpage)
        {
            var obj = new GlobaisPermissaoPaginaBE();
            if (idpage != 0)
                obj = this.SelectID(new GlobaisPermissaoPaginaBE { permPag_id = idpage });
            else
                obj = this.SelectID(new GlobaisPermissaoPaginaBE { permPag_url = page, perm_id = perfil });

            if (obj == null)
            {
                obj = new GlobaisPermissaoPaginaBE();
                obj.permPag_url = page;
                obj.perm_id = perfil;
            }

            switch (tipoPage)
            {
                case TipoPagePermissao.InsertItem:
                    obj.permPag_inserir = obj.permPag_inserir ? false : true;
                    break;
                case TipoPagePermissao.UpdateItem:
                    obj.permPag_alterar = obj.permPag_alterar ? false : true;
                    break;
                case TipoPagePermissao.DeletarItem:
                    obj.permPag_excluir = obj.permPag_excluir ? false : true;
                    break;
                default:
                    obj.permPag_inserir = obj.permPag_inserir ? false : true;
                    break;
            }

            return new GlobaisPermissaoPageDao().Insert(obj);
        }

        public bool Update(GlobaisPermissaoPaginaBE obj)
        {
            return new GlobaisPermissaoPageDao().Update(obj).Value;
        }

        public bool Delete(GlobaisPermissaoPaginaBE obj)
        {
            return new GlobaisPermissaoPageDao().Delete(obj).Value;
        }
    }
}
