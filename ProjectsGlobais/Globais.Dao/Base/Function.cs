using System;
using System.Collections.Generic;
using System.Linq;

namespace Globais.Dao.Base
{
    public class Functions : BaseDados, IDisposable
    {
        public virtual IList<T> Select<T>(object obj)
        {
            return RetornaListaDados<T>(Helper.TipoSql.Select, obj).ToList();
        }

        public virtual IList<T> Select<T>()
        {
            return RetornaListaDados<T>(Helper.TipoSql.Select).ToList();
        }

        public virtual T SelectId<T>(object obj)
        {
            return RetornaListaDados<T>(Helper.TipoSql.Select, obj).FirstOrDefault<T>(); 
        }

        public virtual int Insert(object obj)
        {
            var retorno = ExecutaItem(Helper.TipoSql.Insert.ToString(), obj);
            if (retorno != null)
                return Convert.ToInt32(retorno);
            else
                return 0;
        }

        public virtual bool? Update(object obj)
        {
            var retorno = ExecutaItem(Helper.TipoSql.Update.ToString(), obj);
            if (retorno != null)
                return Convert.ToBoolean(retorno);
            else
                return false;
        }

        public virtual bool? Delete(object obj)
        {
            var retorno = ExecutaItem(Helper.TipoSql.Delete.ToString(), obj);

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
