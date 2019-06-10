using System.Collections.Generic;

namespace HelpersSistema.Models.Base
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IList<T> Search<T>();
        IList<T> Search<T>(object obj);
        T Find<T>(object obj);
        T Insert<T>(object obj);
        bool? Update(object obj);
        bool? Delete(object obj);
    }
}
