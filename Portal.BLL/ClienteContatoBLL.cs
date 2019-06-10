using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class ClienteContatoBLL
    {
        public List<GlobaisClienteContatoBE> Select(GlobaisClienteContatoBE obj)
        {
            return new ClienteContatoDao().Select<GlobaisClienteContatoBE>(obj).ToList();
        }
        public GlobaisClienteContatoBE SelectId(GlobaisClienteContatoBE obj)
        {
            return new ClienteContatoDao().SelectId<GlobaisClienteContatoBE>(obj);
        }
        public int Insert(GlobaisClienteContatoBE obj)
        {
            var cli_id = new ClienteContatoDao().Insert(obj);
            return cli_id;
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
