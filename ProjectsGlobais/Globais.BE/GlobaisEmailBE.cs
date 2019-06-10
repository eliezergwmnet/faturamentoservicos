using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globais.BE
{
    public class GlobaisEmailBE
    {
        public int ema_id { get; set; }
        public string ema_nome { get; set; }
        public string ema_email { get; set; }
        public string ema_smtp { get; set; }
        public string ema_senha { get; set; }
        public string ema_porta { get; set; }
        public string ema_referencia { get; set; }
        public int conf_id { get { return Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
        
        public string ema_html { get; set; }
        public string ema_camposnomes { get; set; }
    }
}
