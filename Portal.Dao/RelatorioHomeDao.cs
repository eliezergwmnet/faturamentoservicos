using System.Collections.Generic;
using Portal.Dao.Base;

namespace Portal.Dao
{
    public class RelatorioHomeDao: Functions
    {
        public override IList<T> Select<T>()
        {
            this.procedure = "proc_Relatorio_Total";
            return base.Select<T>();
        }
        public override IList<T> Select<T>(object obj)
        {
            this.procedure = "proc_Relatorio_Total";
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
            return base.Update(obj);
        }

        public override bool? Delete(object obj)
        {
            this.procedure = "";
            return base.Delete(obj);
        }
    }
}
