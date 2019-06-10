namespace Globais.BE
{
    public class GlobaisUsuarioBE : GlobaisLogBE
    {
        public int usu_id { get; set; }
        public int end_id { get; set; }
        public int perm_id { get; set; }
        public string usu_nome { get; set; }
        public string usu_email { get; set; }
        public string usu_senha { get; set; }
        public string usu_telefone { get; set; }
        public string usu_celular { get; set; }
        string _usu_imagem { get; set; }
        public string usu_imagem
        {
            get
            {
                return _usu_imagem;
            }
            set
            {
                if (value == "")
                    this._usu_imagem = "/Imagem/Usuario/User.jpg";
                 else
                    this._usu_imagem = value.Replace("~", "");// Helper.ConverterImagem.RetornaDiretorioImagem() + "\\" + value;
            }
        }
        public GlobaisEnderecoBE Endereco { get; set; }
        public GlobaisPermissaoBE Permissao { get; set; }
        //public List<UsuarioEmpresaBE> Empresas { get; set; }
        public int conf_id { get; set; }
    }
}
