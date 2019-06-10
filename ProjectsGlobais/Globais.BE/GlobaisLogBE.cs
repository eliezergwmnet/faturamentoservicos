using System;

namespace Globais.BE
{
    public class GlobaisLogBE
    {
        public int log_id { get; set; }
        public int? user_id { get; set; }
        public DateTime log_cadastro { get; set; }
        public DateTime? log_alteracao { get; set; }
        public DateTime? log_exclusao { get; set; }
        bool _log_ativo = true;
        public bool log_ativo {
            get { return _log_ativo;  }
            set { _log_ativo = value; }
        }
    }
}
