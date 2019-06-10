using Globais.Dao.Base;
using PortalSCM.Helper;

namespace PortalSCM.Dao
{
    public class SCMClienteServicosDao : Functions
    {
        public SCMClienteServicosDao()
        {
            this.procedure = "proc_SCMServicoCliente";
            this.conectionString = GlobaisSCM.ConectionDataBaseGlobal;
        }
    }
}
