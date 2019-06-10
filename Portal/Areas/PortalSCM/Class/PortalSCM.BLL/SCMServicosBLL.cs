using System.Collections.Generic;
using System.Linq;
using PortalSCM.BE;
using PortalSCM.Dao;

namespace PortalSCM.BLL
{
    public class SCMServicosBLL
    {
        public List<SCMServicosBE> Select()
        {
            return new SCMServicosDao().Select<SCMServicosBE>(new SCMServicosBE { }).ToList();
        }

        public List<SCMServicosBE> Select(SCMServicosBE obj)
        {
            return new SCMServicosDao().Select<SCMServicosBE>(obj).ToList();
        }

        public SCMServicosBE SelectID(SCMServicosBE obj)
        {
            return new SCMServicosDao().Select<SCMServicosBE>(obj).FirstOrDefault<SCMServicosBE>();
        }

        public int Insert(SCMServicosBE obj)
        {
            return new SCMServicosDao().Insert(obj);
        }

        public bool Update(SCMServicosBE obj)
        {
            return new SCMServicosDao().Update(obj).Value;
        }

        public bool Delete(SCMServicosBE obj)
        {
            return new SCMServicosDao().Delete(obj).Value;
        }
    }
}