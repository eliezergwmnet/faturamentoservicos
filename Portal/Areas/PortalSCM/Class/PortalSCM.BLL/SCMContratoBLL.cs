using System.Collections.Generic;
using System.Linq;
using Globais.BE;
using Globais.BLL;
using PortalSCM.BE;
using PortalSCM.Dao;

namespace PortalSCM.BLL
{
    public class SCMContratoBLL
    {
        /// <summary>
        /// Retorna todos os serviços que ainda não foram faturados no mes
        /// o parametro "cont_avulso" indica se vai fatura os serviços mensais ou os serviços avulso
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<SCMContratoBE> SelectFaturamentoMensalPendente(SCMContratoBE obj)
        {
            return this.CarregaCliente(new SCMContratoDao().SelectFaturamentoMensalPendente(obj).ToList());
        }

        /// <summary>
        /// Carrega os itens que ainda nao foram faturados filtrados pelo contrato
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<SCMContratoBE> SelectFaturamentoMensalContratoPendente(SCMContratoBE obj)
        {
            return this.CarregaCliente(new SCMContratoDao().SelectFaturamentoMensalContratoPendente(obj).ToList());
        }
        

        public List<SCMContratoBE> Select()
        {
            return this.CarregaCliente(new SCMContratoDao().Select<SCMContratoBE>().ToList());
        }

        public List<SCMContratoBE> Select(SCMContratoBE obj)
        {
            return this.CarregaCliente(new SCMContratoDao().Select<SCMContratoBE>(obj).ToList());
        }

        public SCMContratoBE SelectID(SCMContratoBE obj)
        {
            SCMContratoBE contrato = new SCMContratoDao().Select<SCMContratoBE>(obj).FirstOrDefault<SCMContratoBE>();
            contrato.Cliente = new GlobaisClienteBLL().SelectID(new GlobaisClienteBE { cli_id = contrato.cli_id });
            return contrato;
        }

        List<SCMContratoBE> CarregaCliente(List<SCMContratoBE> obj)
        {
            GlobaisClienteBLL cli = new GlobaisClienteBLL();
            for (int i = 0; i < obj.Count; i++)
            {
                var cliente = cli.SelectID(new GlobaisClienteBE { cli_id = obj[i].cli_id });
                if (cliente != null)
                    obj[i].Cliente = cliente;
                else
                    obj[i].Cliente = new GlobaisClienteBE();
            }
            return obj;
        }

        public int Insert(SCMContratoBE obj)
        {
            return new SCMContratoDao().Insert(obj);
        }

        public bool Update(SCMContratoBE obj)
        {
            return new SCMContratoDao().Update(obj).Value;
        }

        public bool Delete(SCMContratoBE obj)
        {
            return new SCMContratoDao().Delete(obj).Value;
        }
    }
}