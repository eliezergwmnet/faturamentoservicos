using System.Collections.Generic;

namespace Portal.BE
{
    public class ServicosBE
    {
        public int serv_id { get; set; }
        public string serv_nome { get; set; }
        public string serv_descricao { get; set; }
        public bool serv_parcelado { get; set; }
        public int conf_id { get { return Globais.Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
        public int serv_idsub { get; set; }
        public List<ServicosBE> SubMenu { get; set; }
    }
}
