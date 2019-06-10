using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE.Chamado;
using Portal.Dao.Chamado;

namespace Portal.BLL.Chamado
{
    public class ArquivosBLL
    {
        public List<ArquivosBE> Select()
        {
            return new ArquivosDao().Select<ArquivosBE>().ToList();
        }
        public List<ArquivosBE> Select(ArquivosBE obj)
        {
            return new ArquivosDao().Select<ArquivosBE>(obj).ToList();
        }
        public ArquivosBE SelectId()
        {
            return new ArquivosDao().SelectId<ArquivosBE>(new { });
        }
        public ArquivosBE SelectId(ArquivosBE obj)
        {
            return new ArquivosDao().SelectId<ArquivosBE>(obj);
        }
        public bool Insert(ArquivosBE obj)
        {
            var result = new ArquivosDao().Insert(obj);
            return Convert.ToBoolean(result);
        }
        public bool Update(ArquivosBE obj)
        {
            var result = new ArquivosDao().Update(obj);
            return result.Value;
        }
        public bool Delete(ArquivosBE obj)
        {
            return new ArquivosDao().Delete(obj).Value;
        }
    }
}
