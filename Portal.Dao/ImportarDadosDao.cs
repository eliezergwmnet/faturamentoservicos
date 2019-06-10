using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.BE;
using Portal.Dao.Base;

namespace Portal.Dao
{
    public class ImportarDadosDao : Functions
    {
        

        public List<ImportarNotas> ImportarNotas()
        {
            this.procedure = "_Importarnotasantigas";
            return base.Select<ImportarNotas>().ToList();
        }

        public override IList<T> Select<T>()
        {
            this.procedure = "";
            return base.Select<T>();
        }
        public override IList<T> Select<T>(object obj)
        {
            this.procedure = "";
            return base.Select<T>(obj);
        }
        public override T SelectId<T>(object obj)
        {
            this.procedure = "";
            return base.SelectId<T>(obj);
        }
        public override int Insert(object obj)
        {
            this.procedure = "";
            return base.Insert(obj);
        }
        public override bool? Update(object obj)
        {
            this.procedure = "";
            return base.Update(obj).Value;
        }
        public override bool? Delete(object obj)
        {
            this.procedure = "";
            return base.Delete(obj).Value;
        }
    }
}
