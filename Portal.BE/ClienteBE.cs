using System.Collections.Generic;
using Globais.BE;

namespace Portal.BE
{
    public class ClienteBE: GlobaisClienteBE
    {
        /// <summary>
        /// Lista os serviços adicionados no cliente sem o parcelamento
        /// </summary>
        public List<ClienteServicosBE> Servicos { get; set; }
        /// <summary>
        /// Carrega a lista de serviços da serem inseridos na nota
        /// Quando um item é parcelado é salvo linha a linha cada parcela
        /// </summary>
        public List<ClienteNotaBE> ServicosNotas { get; set; }
        /// <summary>
        /// Carrega a nota gerada do cliente
        /// </summary>
        public NotasBE Nota { get; set; }
    }

    /// <summary>
    /// Classe com as propriedades do item GerarNotaMes
    /// </summary>
    public class ClienteNotasContratoBE: GlobaisClienteBE
    {
        public int cliNot_contrato { get; set; }
        public decimal totalContrato { get; set; }
        /// <summary>
        /// Lista os serviços adicionados no cliente sem o parcelamento
        /// </summary>
        public List<ClienteServicosBE> Servicos { get; set; }
        /// <summary>
        /// Carrega a lista de serviços da serem inseridos na nota
        /// Quando um item é parcelado é salvo linha a linha cada parcela
        /// </summary>
        public List<ClienteNotaBE> ServicosNotas { get; set; }
        /// <summary>
        /// Carrega a nota gerada do cliente
        /// </summary>
        public NotasBE Nota { get; set; }
    }
}




