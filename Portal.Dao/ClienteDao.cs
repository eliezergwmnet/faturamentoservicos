using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Dao.Base;

namespace Portal.Dao
{
    public class ClienteDao: Functions
    {
        /*public override IList<T> Select<T>()
        {
            this.procedure = "proc_Cliente_Select";
            return base.Select<T>();
        }*/
        public IList<T> SelectNotas<T>(object obj)
        {
            this.procedure = "proc_Cliente_Notas";
            return base.Select<T>(obj);
        }
        public IList<T> SelectNotasContrato<T>(object obj)
        {
            this.procedure = "proc_Cliente_NotasContrato";
            return base.Select<T>(obj);
        }
        /*public override IList<T> Select<T>(object obj)
        {
            this.procedure = "proc_Cliente_Select";
            return base.Select<T>(obj);
        }*/

        public IList<T> SelectClienteIn<T>(object obj)
        {
            this.procedure = "proc_ClienteNotas_IN_Not_Id";
            return base.Select<T>(obj);
        }

        public IList<T> SelectClienteNotasArquivos<T>(object obj)
        {
            this.procedure = "proc_NotasGerarArquivo";
            return base.Select<T>(obj);
        }

        /*public override T SelectId<T>(object obj)
        {
            this.procedure = "proc_Cliente_Select";
            return base.SelectId<T>(obj);
        }
        public override int Insert(object obj)
        {
            this.procedure = "proc_Cliente_Insert";
            return base.Insert(obj);
        }
        public override bool? Update(object obj)
        {
            this.procedure = "proc_Cliente_Update";
            return base.Update(obj).Value;
        }
        public override bool? Delete(object obj)
        {
            this.procedure = "proc_Cliente_Delete";
            return base.Delete(obj).Value;
        }*/
    }
}
