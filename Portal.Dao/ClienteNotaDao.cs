using System.Collections.Generic;
using Portal.Dao.Base;

namespace Portal.Dao
{
    public class ClienteNotaDao : Functions
    {
        public override IList<T> Select<T>()
        {
            this.procedure = "proc_ClienteNota_Select";
            return base.Select<T>();
        }
        public IList<T> SelectNota<T>(object obj)
        {
            this.procedure = "proc_ClienteNota_FaturaServico";
            return base.Select<T>(obj);
        }
        public override IList<T> Select<T>(object obj)
        {
            this.procedure = "proc_ClienteNota_Select";
            return base.Select<T>(obj);
        }
        public override T SelectId<T>(object obj)
        {
            this.procedure = "proc_ClienteNota_Select";
            return base.SelectId<T>(obj);
        }
        public override int Insert(object obj)
        {
            this.procedure = "proc_ClienteNota_Insert";
            return base.Insert(obj);
        }
        public bool? BaixaParcela(object obj)
        {
            this.procedure = "proc_ClienteNota_BaixaParcela";
            return base.Update(obj).Value;
        }

        public override bool? Update(object obj)
        {
            this.procedure = "proc_ClienteNota_Update";
            return base.Update(obj).Value;
        }
        public override bool? Delete(object obj)
        {
            this.procedure = "proc_ClienteNota_Delete";
            return base.Delete(obj).Value;
        }
    }
}
