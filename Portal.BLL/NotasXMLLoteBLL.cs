using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class NotasXMLLoteBLL
    {
        public List<NotasXMLLoteBE> Select()
        {
            return new NotasXMLLoteDao().Select<NotasXMLLoteBE>().ToList();
        }
        public List<NotasXMLLoteBE> Select(NotasXMLLoteBE obj)
        {
            return new NotasXMLLoteDao().Select<NotasXMLLoteBE>(obj).ToList();
        }
        public NotasXMLLoteBE SelectId()
        {
            return new NotasXMLLoteDao().SelectId<NotasXMLLoteBE>(new { });
        }
        public NotasXMLLoteBE SelectId(NotasXMLLoteBE obj)
        {
            return new NotasXMLLoteDao().SelectId<NotasXMLLoteBE>(obj);
        }
        public NotasXMLLoteBE Insert(NotasXMLLoteBE obj)
        {
            obj.notXml_id = new NotasXMLLoteDao().Insert(obj);
            return obj;
        }
        public bool Update(NotasXMLLoteBE obj)
        {
            new NotasXMLLoteDao().Update(obj);
            return true;
        }
        public bool Delete(NotasXMLLoteBE obj)
        {
            return new NotasXMLLoteDao().Delete(obj).Value;
        }
    }
}
