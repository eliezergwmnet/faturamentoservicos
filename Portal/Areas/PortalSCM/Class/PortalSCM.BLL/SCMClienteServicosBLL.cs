using System.Collections.Generic;
using System.Linq;
using PortalSCM.BE;
using PortalSCM.Dao;

namespace PortalSCM.BLL
{
    public class SCMClienteServicosBLL
    {
        public List<SCMClienteServicosBE> Select()
        {
            return new SCMContratoDao().Select<SCMClienteServicosBE>().ToList();
        }

        public List<SCMClienteServicosBE> Select(SCMClienteServicosBE obj)
        {
            return new SCMClienteServicosDao().Select<SCMClienteServicosBE>(obj).ToList();
        }

        public SCMClienteServicosBE SelectID(SCMClienteServicosBE obj)
        {
            return new SCMClienteServicosDao().Select<SCMClienteServicosBE>(obj).FirstOrDefault<SCMClienteServicosBE>();
        }

        public int Insert(SCMClienteServicosBE obj)
        {
            return new SCMClienteServicosDao().Insert(obj);
        }

        public bool Update(SCMClienteServicosBE obj)
        {
            return new SCMClienteServicosDao().Update(obj).Value;
        }

        public bool Delete(SCMClienteServicosBE obj)
        {
            return new SCMClienteServicosDao().Delete(obj).Value;
        }
    }
}