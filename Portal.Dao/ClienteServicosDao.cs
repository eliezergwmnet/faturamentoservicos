using System.Collections.Generic;
using Portal.Dao.Base;

namespace Portal.Dao
{
    public class ClienteServicosDao : Functions
    {
        public override IList<T> Select<T>()
        {
            this.procedure = "proc_ClienteServicos_Select";
            return base.Select<T>();
        }
        public override IList<T> Select<T>(object obj)
        {
            this.procedure = "proc_ClienteServicos_Select";
            return base.Select<T>(obj);
        }
        public override T SelectId<T>(object obj)
        {
            this.procedure = "proc_ClienteServicos_Select";
            return base.SelectId<T>(obj);
        }
        public override int Insert(object obj)
        {
            this.procedure = "proc_ClienteServicos_Insert";
            return base.Insert(obj);
        }
        public override bool? Update(object obj)
        {
            this.procedure = "proc_ClienteServicos_Update";
            return base.Update(obj).Value;
        }
        public override bool? Delete(object obj)
        {
            this.procedure = "proc_ClienteServicos_Delete";
            return base.Delete(obj).Value;
        }
    }
}
