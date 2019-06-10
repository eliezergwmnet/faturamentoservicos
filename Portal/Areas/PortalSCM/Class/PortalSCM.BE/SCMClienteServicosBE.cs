using System;
using Globais.BE;

namespace PortalSCM.BE
{
    public class SCMClienteServicosBE : GlobaisLogBE
    {
        public int servCli_id { get; set; }
        public int cli_id { get; set; }
        public int serv_id { get; set; }
        public int cont_id { get; set; }
        public string servCli_nome { get; set; }
        public string servCli_descricao { get; set; }
        public decimal servCli_valor { get; set; }
        public bool servCli_parcelado { get; set; }
        public int servCli_parceladoQtd { get; set; }
        public int servCli_cobrarPorpor { get; set; }
        public int servCli_qtdDias { get; set; }
        public DateTime servCli_dataAtivacao { get; set; }
        public DateTime servCli_data { get; set; }

    }
}
