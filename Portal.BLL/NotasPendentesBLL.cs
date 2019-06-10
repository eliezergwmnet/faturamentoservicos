using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class NotasPendentesBLL
    {
        public List<NotasPendentesBE> Select()
        {
            return new NotasPendentesDao().Select<NotasPendentesBE>().ToList();
        }
    }
}
