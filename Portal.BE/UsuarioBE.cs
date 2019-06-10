using System.Collections.Generic;
using Globais.BE;

namespace Portal.BE
{
    public class UsuarioBE : GlobaisLogBE
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
        public string usu_imagem {
            get
            {
                return _usu_imagem;
            }
            set
            {
                //if (value == "")
                    this._usu_imagem = "/Imagem/Usuario/User.jpg";
               // else
               //     this._usu_imagem = "/Imagem/Usuario/" + value;// Helper.ConverterImagem.RetornaDiretorioImagem() + "\\" + value;
            }
        }
        public GlobaisEnderecoBE Endereco { get; set; }
        public PermissaoBE Permissao { get; set; }
        public List<UsuarioEmpresaBE> Empresas { get; set; }
        public int conf_id { get { return Globais.Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
    }

    public class UsuarioListaBE
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Perfil { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        private string _Image;
        public string Imagem
        {
            get
            {
                return _Image;
            }
            set
            {
                if (value == "")
                    this._Image = "/Imagem/Usuario/User.jpg";
                else
                    this._Image = "/Imagem/Usuario/" + value;// Helper.ConverterImagem.RetornaDiretorioImagem() + "\\" + value;
            }
        }
        public List<UsuarioEmpresaBE> Empresas { get; set; }
    }

    public class UsuarioEmpresaBE
    {
        public int useemp_id { get; set; }
        public int useemp_user { get; set; }
        public int useemp_empresa { get; set; }
        public string confCom_logo { get; set; }
        public string conf_nomefantasia { get; set; }
    }

    public class UsuarioConfiguracao
    {
        public int userconf_id { get; set; }
        public int userconf_user { get; set; }
        public string userconf_referecia { get; set; }
    }
}
