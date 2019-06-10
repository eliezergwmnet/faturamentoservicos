using System.Collections.Generic;

namespace Globais.BE
{
    public class GlobaisClienteBE : GlobaisLogBE
    {
        public int cli_id { get; set; }
        public int usu_id { get; set; }
        public decimal cli_CFOP { get; set; }
        public bool cli_SCM { get; set; }
        public bool cli_MNT { get; set; }
        public string cli_tipoVencimento { get; set; }
        public string cli_parametroVencimento { get; set; }
        public string cli_nomeFantasia { get; set; }
        public string cli_razaoSocial { get; set; }
        public string cli_tipoInscricao { get; set; }
        public string cli_CGC { get; set; }
        public string cli_CPF { get; set; }
        public string cli_CodCidIBGE { get; set; }
        public string cli_incricaoEstadual { get; set; }
        public List<GlobaisClienteEnderecoBE> Endereco { get; set; }
        public List<GlobaisClienteContatoBE> Contato { get; set; }
        public int conf_id { get { return Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
    }
    



    public class GlobaisClienteListaBE
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
    }
}
