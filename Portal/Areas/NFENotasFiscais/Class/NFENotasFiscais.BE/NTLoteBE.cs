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
namespace NFENotasFiscais.BE
{
    public class NTLoteBE : GlobaisLogBE
    {
        public int lote_id { get; set; }
        public int conf_id { get; set; }
        public string lote_serie { get; set; }
        public string lote_modulo { get; set; }
        public DateTime lote_emissao { get; set; }
        public string lote_status { get; set; }
        public string lote_protocolo { get; set; }
        public string lote_urlXmlEnvio { get; set; }
        public string lote_urlXmlConsulta { get; set; }
        public List<NTNotasBE> Notas { get; set; }
    }
}