namespace Globais.BE
{
    public class GlobaisConfiguracaoBE
    {
        public int Con_id { get; set; }
        public string Con_IdItem { get; set; }
        public string Con_Descricao { get; set; }
        public string Con_Tipo { get; set; }
        public int conf_id { get { return Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }

    }
}
