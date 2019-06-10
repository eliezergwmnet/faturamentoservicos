using System.Collections.Generic;
using Globais.BE;
using Globais.Dao;
using System.Linq;
namespace Globais.BLL
{
    public class GlobaisPermissaoBLL
    {
        public List<GlobaisPermissaoBE> Select()
        {
            return new GlobaisPermissaoDao().CarregaPermissaoCompleto(null);
        }

        public List<GlobaisPermissaoBE> Select(GlobaisPermissaoBE obj)
        {
            return new GlobaisPermissaoDao().CarregaPermissaoCompleto(obj);
        }

        public GlobaisPermissaoBE SelectID(GlobaisPermissaoBE obj)
        {
            return new GlobaisPermissaoDao().CarregaPermissaoCompleto(obj).FirstOrDefault<GlobaisPermissaoBE>();
        }

        public int Insert(GlobaisPermissaoBE obj)
        {
            return new GlobaisPermissaoDao().Insert(obj);
        }

        public bool Update(GlobaisPermissaoBE obj)
        {
            return new GlobaisPermissaoDao().Update(obj).Value;
        }

        public bool Delete(GlobaisPermissaoBE obj)
        {
            return new GlobaisPermissaoDao().Delete(obj).Value;
        }
    }
}
