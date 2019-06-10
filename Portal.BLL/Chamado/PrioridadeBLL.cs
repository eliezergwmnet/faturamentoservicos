using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE.Chamado;
using Portal.Dao.Chamado;

namespace Portal.BLL.Chamado
{
    public class PrioridadeBLL
    {
        public List<PrioridadeBE> Select()
        {
            return new PrioridadeDao().Select<PrioridadeBE>().ToList();
        }
        public List<PrioridadeBE> Select(PrioridadeBE obj)
        {
            return new PrioridadeDao().Select<PrioridadeBE>(obj).ToList();
        }
        public PrioridadeBE SelectId()
        {
            return new PrioridadeDao().SelectId<PrioridadeBE>(new { });
        }
        public PrioridadeBE SelectId(PrioridadeBE obj)
        {
            return new PrioridadeDao().SelectId<PrioridadeBE>(obj);
        }
        public bool Insert(PrioridadeBE obj)
        {
            var result = new PrioridadeDao().Insert(obj);
            return Convert.ToBoolean(result);
        }
        public bool Update(PrioridadeBE obj)
        {
            var result = new PrioridadeDao().Update(obj);
            return result.Value;
        }
        public bool Delete(PrioridadeBE obj)
        {
            return new PrioridadeDao().Delete(obj).Value;
        }
    }
}
