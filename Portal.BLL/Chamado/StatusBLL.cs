using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE.Chamado;
using Portal.Dao.Chamado;

namespace Portal.BLL.Chamado
{
    public class StatusBLL
    {
        public List<StatusBE> Select()
        {
            return new StatusDao().Select<StatusBE>().ToList();
        }
        public List<StatusBE> Select(StatusBE obj)
        {
            return new StatusDao().Select<StatusBE>(obj).ToList();
        }
        public StatusBE SelectId()
        {
            return new StatusDao().SelectId<StatusBE>(new { });
        }
        public StatusBE SelectId(StatusBE obj)
        {
            return new StatusDao().SelectId<StatusBE>(obj);
        }
        public bool Insert(StatusBE obj)
        {
            var result = new StatusDao().Insert(obj);
            return Convert.ToBoolean(result);
        }
        public bool Update(StatusBE obj)
        {
            var result = new StatusDao().Update(obj);
            return result.Value;
        }
        public bool Delete(StatusBE obj)
        {
            return new StatusDao().Delete(obj).Value;
        }
    }
}
