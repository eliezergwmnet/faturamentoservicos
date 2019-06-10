using System;
using System.Collections.Generic;

namespace Portal.BE.Chamado
{
    public class ChamadoBE
    {
        public int cOs_id { get; set; }
        public int cli_id { get; set; }
        public int usu_id { get; set; }
        public int cCa_id { get; set; }
        public int cSt_id { get; set; }
        public int cPr_id { get; set; }
        public ClienteBE Cliente { get; set; }
        public UsuarioBE Usuario { get; set; }
        public CategoriaBE Categoria { get; set; }
        public StatusBE Status { get; set; }
        public PrioridadeBE Prioridade { get; set; }
        public List<MensagemBE> Mensagens { get; set; }
        public string cOs_numero { get; set; }
        public string cOs_titulo { get; set; }
        public DateTime cOs_data { get; set; }
        public bool cOs_ativo { get; set; }
    }
}
