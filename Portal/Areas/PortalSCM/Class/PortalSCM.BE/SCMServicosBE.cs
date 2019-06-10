using Globais.BE;

namespace PortalSCM.BE
{
    public class SCMServicosBE : GlobaisLogBE
    {
        public int ser_id { get; set; }
        public string cat_id { get; set; }
        public string ser_codigoServico { get; set; }
        public string ser_codigoAtividade { get; set; }
        public string ser_descricaoServico { get; set; }
        public int ser_aliquota { get; set; }
        public int conf_id { get { return Globais.Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
    }
}
