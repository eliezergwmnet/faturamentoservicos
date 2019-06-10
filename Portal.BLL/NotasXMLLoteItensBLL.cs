using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class NotasXMLLoteItensItensBLL
    {
        public List<NotasXMLLoteItensBE> Select()
        {
            return new NotasXMLLoteItensDao().Select<NotasXMLLoteItensBE>().ToList();
        }
        public List<NotasXMLLoteItensBE> Select(NotasXMLLoteItensBE obj)
        {
            return new NotasXMLLoteItensDao().Select<NotasXMLLoteItensBE>(obj).ToList();
        }
        public NotasXMLLoteItensBE SelectId()
        {
            return new NotasXMLLoteItensDao().SelectId<NotasXMLLoteItensBE>(new { });
        }
        public NotasXMLLoteItensBE SelectId(NotasXMLLoteItensBE obj)
        {
            return new NotasXMLLoteItensDao().SelectId<NotasXMLLoteItensBE>(obj);
        }
        public NotasXMLLoteItensBE Insert(NotasXMLLoteItensBE obj)
        {
            obj.notXml_id = new NotasXMLLoteItensDao().Insert(obj);
            return obj;
        }
        public bool Update(NotasXMLLoteItensBE obj)
        {
            new NotasXMLLoteItensDao().Update(obj);
            return true;
        }
        public bool Delete(NotasXMLLoteItensBE obj)
        {
            return new NotasXMLLoteItensDao().Delete(obj).Value;
        }
    }
}
