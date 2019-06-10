using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class ClienteServicosBLL: ClienteNotaCalcularNota
    {
        public List<ClienteServicosBE> Select()
        {
            return new ClienteServicosDao().Select<ClienteServicosBE>().ToList();
        }
        public List<ClienteServicosBE> Select(ClienteServicosBE obj)
        {
            return new ClienteServicosDao().Select<ClienteServicosBE>(obj).ToList();
        }

        /// <summary>
        /// Carrega a lsta de serviços agrupada por contrato
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<ClienteServicosContratoBE> SelectContratoServicos(ClienteServicosBE obj)
        {
            var servicos = new ClienteServicosDao().Select<ClienteServicosBE>(obj).ToList();
            if (servicos.Count == 0)
                return new List<ClienteServicosContratoBE>();
            else
            {
                servicos = servicos.OrderBy(x => x.servCli_contrato).ToList();
                var retorno = new List<ClienteServicosContratoBE>();

                foreach (var item in servicos)
                {
                    if((from x in retorno where x.Contrato.Equals(item.servCli_contrato) select x).Count() == 0){
                        var novocontrato = new ClienteServicosContratoBE { Contrato = item.servCli_contrato, NomeContrato = item.servCli_contratonome, DataCriacao = item.servCli_data, ServicosContrato = new List<ClienteServicosBE>() };
                        novocontrato.ServicosContrato.Add(item);
                        retorno.Add(novocontrato);
                    }
                    else
                    {
                        for (int i = 0; i < retorno.Count; i++)
                        {
                            if(retorno[i].Contrato.Equals(item.servCli_contrato))
                            {
                                retorno[i].ServicosContrato.Add(item);
                                break;
                            }
                        }
                    }
                }
                return retorno;
            }
        }

        public ClienteServicosBE SelectId(ClienteServicosBE obj)
        {
            return new ClienteServicosDao().SelectId<ClienteServicosBE>(obj);
        }
        public ClienteServicosBE Insert(ClienteServicosBE obj, int conf_id)
        {
            var _serv = new ClienteServicosDao();
            obj.servCli_id = _serv.Insert(obj);
            obj = _serv.SelectId<ClienteServicosBE>(new ClienteServicosBE { servCli_id = obj.servCli_id, log_ativo = true });
            var servico = new ServicosBLL().SelectId(new ServicosBE { serv_id = obj.serv_id, conf_id = conf_id });
            if(servico != null && servico.serv_parcelado == false)
                this.CalcularProporcional(obj);
            else
                this.CalcularParcelas(obj);
            return obj;
        }
        public bool Update(ClienteServicosBE obj)
        {
            return new ClienteServicosDao().Update(obj).Value;
        }
        public bool Delete(ClienteServicosBE obj)
        {
            return new ClienteServicosDao().Delete(new { servCli_id=obj.servCli_id, cli_id = obj.cli_id }).Value;
        }

        

    }
}
