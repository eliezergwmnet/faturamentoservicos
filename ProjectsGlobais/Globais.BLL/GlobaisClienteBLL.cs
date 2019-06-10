using System.Collections.Generic;
using Globais.BE;
using Globais.Dao;
using System.Linq;
namespace Globais.BLL
{
    public class GlobaisClienteBLL
    {
        public List<GlobaisClienteBE> SelectNovoTeste()
        {
            return new GlobaisClienteTesteDao().Select<GlobaisClienteBE>().ToList();
        }
        public List<GlobaisClienteBE> Select()
        {
            return new GlobaisClienteDao().CarregaClienteCompleto(new GlobaisClienteBE { });
        }

        public List<GlobaisClienteBE> Select(GlobaisClienteBE obj)
        {
            return new GlobaisClienteDao().CarregaClienteCompleto(obj);
        }

        public GlobaisClienteBE SelectID(GlobaisClienteBE obj)
        {
            return new GlobaisClienteDao().CarregaClienteCompleto(obj).FirstOrDefault<GlobaisClienteBE>();
        }

        public int Insert(GlobaisClienteBE obj)
        {
            return new GlobaisClienteDao().Insert(obj);
        }

        public bool Update(GlobaisClienteBE obj)
        {
            return new GlobaisClienteDao().Update(obj).Value;
        }

        public bool Delete(GlobaisClienteBE obj)
        {
            return new GlobaisClienteDao().Delete(obj).Value;
        }
    }
}
