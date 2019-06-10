using System.Collections.Generic;
using Portal.Dao.Base;
using System.Linq;
using Portal.BE;

namespace Portal.Dao
{
    public class PermissaoPaginaDao : Functions
    {
        public override IList<T> Select<T>()
        {
            this.procedure = "proc_PermissaoPage_Select";
            return base.Select<T>();
        }
        public List<PermissaoPaginaBE> SelectPermissao(int _perm_id)
        {
            this.procedure = "proc_PermissaoPage_Select";
            return base.Select<PermissaoPaginaBE>(new {perm_id = _perm_id }).ToList();
        }
        public override IList<T> Select<T>(object obj)
        {
            this.procedure = "proc_PermissaoPage_Select";
            return base.Select<T>(obj);
        }
        public override T SelectId<T>(object obj)
        {
            this.procedure = "proc_PermissaoPage_Select";
            return base.SelectId<T>(obj);
        }
        public override int Insert(object obj)
        {
            this.procedure = "proc_PermissaoPage_Insert";
            return base.Insert(obj);
        }
        public override bool? Update(object obj)
        {
            this.procedure = "proc_PermissaoPage_Update";
            return base.Update(obj).Value;
        }
        public override bool? Delete(object obj)
        {
            this.procedure = "proc_PermissaoPage_Delete";
            return base.Delete(obj).Value;
        }
    }
}
