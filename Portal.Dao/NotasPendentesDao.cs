using System.Collections.Generic;
using Portal.Dao.Base;

namespace Portal.Dao
{
    public class NotasPendentesDao: Functions
    {
        public IList<T> NotasPendentes<T>()
        {
            this.procedure = "prod_EmitirNotasPendentes";
            return base.Select<T>();
        }
    }
}
