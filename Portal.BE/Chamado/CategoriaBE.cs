using System;
using System.Collections.Generic;

namespace Portal.BE.Chamado
{
    public class CategoriaBE
    {
        public int cCa_id { get; set; }
        public string cCa_nome { get; set; }
        public int cCa_categoria { get; set; }
        public List<CategoriaBE> Categorias { get; set; }
        public DateTime cCa_data { get; set; }
        public bool cCa_ativo { get; set; }
    }
}
