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

namespace NFENotasFiscais.BE
{
    public class NTLoteErroBE
    {
        public int LoteErr_id { get; set; }
        public int lote_id { get; set; }
        public string LoteErr_Codigo { get; set; }
        public string LoteErr_Mensagem { get; set; }
        public string LoteErr_Correcao { get; set; }
        public DateTime LoteErr_Data { get; set; }
    }
}
