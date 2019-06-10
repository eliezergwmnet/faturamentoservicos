using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class NotasItensBLL
    {
        public List<NotasItensBE> Select()
        {
            return new NotasItensDao().Select<NotasItensBE>().ToList();
        }
        public List<NotasItensBE> Select(NotasItensBE obj)
        {
            return new NotasItensDao().Select<NotasItensBE>(obj).ToList();
        }

        public List<NotasItensBE> SelectIn(string notas)
        {
            return new NotasItensDao().SelectIn<NotasItensBE>(new { cli_id = notas }).ToList();
        }

        public NotasItensBE SelectId(NotasItensBE obj)
        {
            return new NotasItensDao().SelectId<NotasItensBE>(obj);
        }
        public NotasItensBE Insert(NotasItensBE obj)
        {
            obj.not_id = new NotasItensDao().Insert(obj);
            return obj;
        }
        public bool Update(NotasItensBE obj)
        {
            return new NotasItensDao().Update(obj).Value;
        }
        public bool Delete(NotasItensBE obj)
        {
            return new NotasItensDao().Delete(obj).Value;
        }
    }
}
