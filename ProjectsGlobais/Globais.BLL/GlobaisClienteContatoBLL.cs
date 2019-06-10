using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;

namespace Globais.BLL
{
    public class GlobaisClienteContatoBLL
    {
        public List<GlobaisClienteContatoBE> Select(GlobaisClienteContatoBE obj)
        {
            return new ClienteContatoDao().Select<GlobaisClienteContatoBE>(obj).ToList();
        }
        public GlobaisClienteContatoBE SelectId(GlobaisClienteContatoBE obj)
        {
            return new ClienteContatoDao().SelectId<GlobaisClienteContatoBE>(obj);
        }
        public GlobaisClienteContatoBE Insert(GlobaisClienteContatoBE obj)
        {
            obj.cont_id = new ClienteContatoDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisClienteContatoBE obj)
        {
            return new ClienteContatoDao().Update(obj).Value;
        }
        public bool Delete(GlobaisClienteContatoBE obj)
        {
            return new ClienteContatoDao().Delete(obj).Value;
        }
    }
}
