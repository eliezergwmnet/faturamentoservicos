/*
********************************************
********************************************
********************************************
*** DESENVOLVIDO POR LEANDRO MARTINS *******
*** PLATAFORMA DE CRIAÇÃO DE TELAS**********
********************************************
********************************************
********************************************
*/


using System;
using System.Collections.Generic;
using Globais.BE;
using PortalSCM.BE;

namespace NFENotasFiscais.BE
{
    public class NTNotasBE : GlobaisLogBE
    {
        public int not_id { get; set; }
        public int not_numero { get; set; }
        public int cli_id { get; set; }
        public int cont_id { get; set; }
        public int conf_id { get; set; }
        public int lote_id { get; set; }
        public string not_tipoReceita { get; set; }
        public string not_codReceita { get; set; }
        public DateTime not_dtEmissao { get; set; }
        public DateTime not_dtVencimento { get; set; }
        public decimal not_pis { get; set; }
        public decimal not_confins { get; set; }
        public decimal not_cssl { get; set; }
        public decimal not_irrf { get; set; }
        //public decimal not_iss { get; set; }
        public decimal not_totalbruto { get; set; }
        public decimal not_totalliquido { get; set; }
        public int not_preenchida { get; set; }
        public bool not_emitida { get; set; }
        public string not_situacao { get; set; }
        public string not_chave { get; set; }
        public string not_tipopagamento { get; set; }
        public List<NTNotasItensBE> NotaItens { get; set; }
        public SCMContratoBE Contrato { get; set; }
    }

    public class NTNotasEmitidasBE: NTNotasBE
    {
        public NTLoteBE Lote { get; set; }
        public GlobaisClienteBE Cliente { get; set; }
        public int res_id { get; set; }
        public int res_numeroNota { get; set; }
        public int res_codigoVerificacao { get; set; }
        public string res_dataEmissao { get; set; }
    }
}