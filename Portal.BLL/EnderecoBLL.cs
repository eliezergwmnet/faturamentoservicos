using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Globais.BE;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class EnderecoBLL
    {
        public List<GlobaisEnderecoBE> Select()
        {
            return new EnderecoDao().Select<GlobaisEnderecoBE>().ToList();
        }
        public List<GlobaisEnderecoBE> Select(GlobaisEnderecoBE obj)
        {
            return new EnderecoDao().Select<GlobaisEnderecoBE>(obj).ToList();
        }
        public GlobaisEnderecoBE SelectId(GlobaisEnderecoBE obj)
        {
            return new EnderecoDao().SelectId<GlobaisEnderecoBE>(obj);
        }
        public GlobaisEnderecoBE Insert(GlobaisEnderecoBE obj)
        {
            obj.end_id = new EnderecoDao().Insert(obj);
            return obj;
        }
        public bool Update(GlobaisEnderecoBE obj)
        {
            return new EnderecoDao().Update(obj).Value;
        }
        public bool Delete(GlobaisEnderecoBE obj)
        {
            return new EnderecoDao().Delete(obj).Value;
        }
    }
}
