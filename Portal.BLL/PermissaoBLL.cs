using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class PermissaoBLL
    {
        public List<PermissaoBE> Select()
        {
            return this.CarregaPages(new PermissaoDao().Select<PermissaoBE>().ToList());
        }
        public List<PermissaoBE> Select(PermissaoBE obj)
        {
            return this.CarregaPages(new PermissaoDao().Select<PermissaoBE>(obj).ToList());
        }
        public PermissaoBE SelectId(PermissaoBE obj)
        {
            var result = new PermissaoDao().SelectId<PermissaoBE>(obj);
            result.Paginas = new PermissaoPaginaBLL().SelectPermissao(result.perm_id);
            return result;
        }
        public PermissaoBE Insert(PermissaoBE obj)
        {
            obj.perm_id = new PermissaoDao().Insert(obj);
            return obj;
        }
        public bool Update(PermissaoBE obj)
        {
            return new PermissaoDao().Update(obj).Value;
        }
        public bool Delete(PermissaoBE obj)
        {
            return new PermissaoDao().Delete(obj).Value;
        }
        List<PermissaoBE> CarregaPages(List<PermissaoBE> _permissao)
        {
            var _page = new PermissaoPaginaBLL();
            foreach (var item in _permissao)
                item.Paginas = _page.SelectPermissao(item.perm_id);
            return _permissao;
        }
    }
}
