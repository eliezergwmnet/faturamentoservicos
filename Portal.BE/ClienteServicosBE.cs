using System;
using System.Collections.Generic;
using Globais.BE;

namespace Portal.BE
{
    public class ClienteServicosContratoBE
    {
        public int Contrato { get; set; }
        public string NomeContrato { get; set; }
        public DateTime DataCriacao { get; set; }
        public List<ClienteServicosBE> ServicosContrato { get; set; }
    }
    public class ClienteServicosBE : GlobaisLogBE
    {
        public int servCli_id { get; set; }
        public int cli_id { get; set; }
        public int end_id { get; set; }
        public string servCli_nome { get; set; }
        public string servCli_descricao { get; set; }
        public decimal servCli_valor { get; set; }
        public bool servCli_parcelado { get; set; }
        public int servCli_parceladoQtd { get; set; }
        public int servCli_cobrarPorpor { get; set; }
        public int servCli_qtdDias { get; set; }
        public int serv_id { get; set; }
        public DateTime? servCli_dataAtivacao { get; set; }
        public GlobaisEnderecoBE Endereco { get; set; }
        public int servCli_contrato { get; set; }
        public string servCli_contratonome { get; set; }
        public DateTime servCli_data { get; set; }
    }

    /// <summary>
    /// Classe usada para desconto de serviços
    /// </summary>
    public class ClienteServicosDescontoBE
    {
        public int cli_id { get; set; }
        public string cli_nome { get; set; }
        public string servCli_nome { get; set; }
        public string horas { get; set; }
        public string servicos { get; set; }
        public decimal servCli_valorServicos { get; set; }
        public decimal servCli_valorDesconto { get; set; }
        public decimal servCli_valorTotal { get; set; }
    }
}
