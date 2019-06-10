using System.Collections.Generic;

namespace Globais.BE
{
    public class GlobaisModulosBE : GlobaisLogBE
    {
        public int mod_id { get; set; }
        public string mod_image { get; set; }
        public string mod_nome { get; set; }
        public string mod_descricao { get; set; }
        public List<GlobaisModulosPagesBE> Paginas { get; set; }
    }

    public class GlobaisModulosPagesBE : GlobaisLogBE
    {
        public int modPage_id { get; set; }
        public int mod_id { get; set; }
        public string modPage_nome { get; set; }
        public string modPage_descricao { get; set; }
        public string modPage_icone { get; set; }
        public string modPage_cadTitulo { get; set; }
        public string modPage_url { get; set; }
    }
}
