using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE.Chamado;
using Portal.Dao.Chamado;

namespace Portal.BLL.Chamado
{
    public class CategoriaBLL
    {
        public List<CategoriaBE> Select()
        {
            return CarregaLista(new CategoriaDao().Select<CategoriaBE>().ToList());
        }
        public List<CategoriaBE> Select(CategoriaBE obj)
        {
            return CarregaLista(new CategoriaDao().Select<CategoriaBE>(obj).ToList());
        }
        public CategoriaBE SelectId()
        {
            return new CategoriaDao().SelectId<CategoriaBE>(new { });
        }
        public CategoriaBE SelectId(CategoriaBE obj)
        {
            return new CategoriaDao().SelectId<CategoriaBE>(obj);
        }
        public bool Insert(CategoriaBE obj)
        {
            var result = new CategoriaDao().Insert(obj);
            return Convert.ToBoolean(result);
        }
        public bool Update(CategoriaBE obj)
        {
            var result = new CategoriaDao().Update(obj);
            return result.Value;
        }
        public bool Delete(CategoriaBE obj)
        {
            return new CategoriaDao().Delete(obj).Value;
        }

        List<CategoriaBE> CarregaLista(List<CategoriaBE> obj)
        {
            var result = new List<CategoriaBE>();

            foreach (var item in (from x in obj where x.cCa_categoria.Equals(0) || x.cCa_categoria.Equals(null) select x).ToList())
            {
                item.Categorias = (from x in obj where x.cCa_categoria.Equals(item.cCa_id) select x).ToList();
                result.Add(item);
            }

            return result;
        }
    }
}
