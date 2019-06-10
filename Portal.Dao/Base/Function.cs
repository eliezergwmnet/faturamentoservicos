using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.Dao.Base
{
    public class Functions : BaseDados, IDisposable
    {
        public virtual IList<T> Select<T>(object obj)
        {
            return RetornaListaDados<T>(obj).ToList();
        }

        public virtual IList<T> Select<T>()
        {

            return RetornaListaDados<T>().ToList();
        }
        public virtual T SelectId<T>(object obj)
        {
            return RetornaListaDados<T>(obj).FirstOrDefault<T>(); ;
        }

        public virtual int Insert(object obj)
        {
            var retorno = ExecutaItem(obj);
            if (retorno != null)
                return Convert.ToInt32(retorno);
            else
                return 0;
        }

        public virtual bool? Update(object obj)
        {
            var retorno = ExecutaItem(obj);
            if (retorno != null)
                return Convert.ToBoolean(retorno);
            else
                return false;
        }

        public virtual bool? Delete(object obj)
        {
            var retorno = ExecutaItem(obj);
            if (retorno != null)
                return Convert.ToBoolean(retorno);
            else
                return false;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
