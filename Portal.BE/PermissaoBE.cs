using System.Collections.Generic;
using Globais.Helper;

namespace Portal.BE
{
    public class PermissaoBE
    {
        public int perm_id { get; set; }
        public int log_id { get; set; }
        public string perm_nome { get; set; }
        public bool per_adminisrador { get; set; }
        public int conf_id { get { return Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
        public List<PermissaoPaginaBE> Paginas { get; set; }
    }
}
