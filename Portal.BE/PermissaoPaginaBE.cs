namespace Portal.BE
{
    public class PermissaoPaginaBE
    {
        public int permPag_id { get; set; }
        public int perm_id { get; set; }
        public string permPag_url { get; set; }
        public bool permPag_inserir { get; set; }
        public bool permPag_alterar { get; set; }
        public bool permPag_excluir { get; set; }
    }
}
