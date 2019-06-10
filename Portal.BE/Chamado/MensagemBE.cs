using System;

namespace Portal.BE.Chamado
{
    public class MensagemBE
    {
        public int cOsMes_id { get; set; }
        public int cOs_id { get; set; }
        public string cOsMes_mensagem { get; set; }
        public DateTime cOsMes_data { get; set; }
    }
}
