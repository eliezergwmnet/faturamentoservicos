using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.Dao;

namespace Globais.BLL
{
    public class GlobaisEnderecoBLL
    {
        public List<GlobaisEnderecoBE> Select()
        {
            return new GlobaisEnderecoDao().Select<GlobaisEnderecoBE>().ToList();
        }
        public List<GlobaisEnderecoBE> Select(GlobaisEnderecoBE obj)
        {
            return new GlobaisEnderecoDao().Select<GlobaisEnderecoBE>(obj).ToList();
        }
        public GlobaisEnderecoBE SelectId(GlobaisEnderecoBE obj)
        {
            return new GlobaisEnderecoDao().SelectId<GlobaisEnderecoBE>(obj);
        }
        public GlobaisEnderecoBE Insert(GlobaisEnderecoBE obj)
        {
            obj.end_id = new GlobaisEnderecoDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisEnderecoBE obj)
        {
            return new GlobaisEnderecoDao().Update(obj).Value;
        }
        public bool Delete(GlobaisEnderecoBE obj)
        {
            return new GlobaisEnderecoDao().Delete(obj).Value;
        }
    }
}
