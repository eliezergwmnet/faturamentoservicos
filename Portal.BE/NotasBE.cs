using System;
using System.Collections.Generic;

namespace Portal.BE
{
    public class NotasBE
    {
        public int not_id { get; set; }
        public int not_numero { get; set; }
        public int cli_id { get; set; }
        public string not_tipoReceita { get; set; }
        public string not_codReceita { get; set; }
        public DateTime not_dtEmissao { get; set; }
        public DateTime not_dtVencimento { get; set; }
        public decimal not_pis { get; set; }
        public decimal not_confins { get; set; }
        public decimal not_cssl { get; set; }
        public decimal not_irrf { get; set; }
        public decimal not_totalbruto { get; set; }
        public decimal not_totalliquido { get; set; }
        public int not_preenchida { get; set; }
        public bool not_emitida { get; set; }
        public string not_situacao { get; set; }
        public bool not_boleto { get; set; }
        public string not_chave { get; set; }
        public string not_linkNota { get; set; }
        public string not_linkBoleto { get; set; }
        public string not_statusPagamento { get; set; }
        public List<NotasItensBE> ItensNota { get; set; }
        public int conf_id { get { return Globais.Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
        public int not_contrato { get; set; }
    }
    public class NotasMesBE : NotasBE
    {

    }
    public class NotasClientesEmitidasMes: ClienteBE
    {
        public int not_id { get; set; }
        public int not_numero { get; set; }
        //public int cli_id { get; set; }
        public string not_tipoReceita { get; set; }
        public string not_codReceita { get; set; }
        public DateTime not_dtEmissao { get; set; }
        public DateTime not_dtVencimento { get; set; }
        public decimal not_pis { get; set; }
        public decimal not_confins { get; set; }
        public decimal not_cssl { get; set; }
        public decimal not_irrf { get; set; }
        public decimal not_totalbruto { get; set; }
        public decimal not_totalliquido { get; set; }
        public int not_preenchida { get; set; }
        public bool not_emitida { get; set; }
        public string not_situacao { get; set; }
        public bool not_boleto { get; set; }
        public string not_chave { get; set; }
        public string not_linkNota { get; set; }
        public string not_linkBoleto { get; set; }
        public string not_statusPagamento { get; set; }
    }
}
