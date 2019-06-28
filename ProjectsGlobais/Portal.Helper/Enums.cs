using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Globais.Helper
{
    public static class Globais
    {
        public const string NameCookie = "gwmnetSystemFaturamento";
        public const string SessionSistemaConfId = "sessionSistemaConfId";
        public const string SessionSistema = "sessionSistemaFaturamento";
        public const string SessionSistemaEmpresaSelecionada = "SessionSistemaEmpresaSelecionada";
        public const string SessionSistemaPermissao = "sessionSistemaFaturamentoPermissao";
        public const int CookieDias = 10;
        public const bool CoolieAtivo = true;
        public const int TamanhoSenhaPadrao = 8;
        public const string ChaveAcessoSenha = "SWSistemaOnline@DadosSys";
        public const string ConectionDataBaseGlobal = "SERVERHOSTGLOBAIS";
    }


    public class EnumTipoEndereco
    {
        private EnumTipoEndereco(string value) { Value = value; }

        public string Value { get; set; }

        public static EnumTipoEndereco Principal { get { return new EnumTipoEndereco("EP"); } }
        public static EnumTipoEndereco Cobranca { get { return new EnumTipoEndereco("EC"); } }
        public static EnumTipoEndereco Matriz { get { return new EnumTipoEndereco("EM"); } }
    }

    public enum TipoSql
    {
        Select,
        Insert,
        Update,
        Delete
    }

    public enum TipoCampo
    {
        String,
        Inteiro,
        Booleano,
        Double
    }

    public enum TipoPagePermissao
    {
        InsertItem,
        UpdateItem,
        DeletarItem
    }

    public enum TipoEmail
    {
        [Description("NFE")]
        NotasFiscal,
        [Description("SEN")]
        EsqueciSenha,
        [Description("NOV")]
        NovoCadastro

    }

    public enum TipoConfiguracao
    {
        [Description("Tipo Vencimento")]
        TPVenc,
        [Description("Codigo IBGE")]
        IBGE,
        [Description("CFOP")]
        CFOP,
        [Description("Paginas do Sistema")]
        PAGE,
        [Description("Tipo de Endereço")]
        TIPEND,
        [Description("Instruções do Boleto")]
        INSTBOL

    }

    public enum TipoImagem
    {
        [Description("Empresa")]
        Empresa,
        [Description("Usuario")]
        Usuario,
        [Description("Representante")]
        Representante,
        [Description("Produto")]
        Produto,
        [Description("Categoria")]
        Categoria,
        [Description("Modulos")]
        Modulos,
    }

    public enum ModeloTemplate
    {
        [Description("Vermelho")]
        Vermelho,
        [Description("Rosa")]
        Rosa,
        [Description("Purpura")]
        Purpura,
        [Description("Roxo")]
        Roxo,
        [Description("Azul Escuro")]
        AzulEscuro,
        [Description("Azul")]
        Azul,
        [Description("Azul Claro")]
        AzulClaro,
        [Description("Ciano")]
        Ciano,
        [Description("Verde Escuro")]
        VerdeEscuro,
        [Description("Verde")]
        Verde,
        [Description("Verde Claro")]
        VerdeClaro,
        [Description("Amarelo")]
        Amarelo,
        [Description("Laranja")]
        Laranja,
        [Description("Marron")]
        Marron,
        [Description("Cinza")]
        Cinza,
        [Description("Cinza Escuro")]
        CinzaEscuro,
        [Description("Preto")]
        Preto
    }

    public enum TituloPageEnum
    {
        TituloPagina,
        TituloModuloPagina
    }

    public enum TipoServicosCadastrados
    {
        [Description("SCM")]
        Sistema_SCM
    }

    public enum StatusNotaNFe
    {
        [Description("NOV")]
        LoteNovo,
        [Description("GER")]
        LoteGerado,
        [Description("ENV")]
        LoteEnviado,
        [Description("FIN")]
        LoteFinalizado,
        [Description("ERR")]
        LoteErro
    }

    /// <summary>
    /// Enum com o retorno do status da nota
    /// </summary>
    public enum StatusNotaNFeRetorno
    {
        [Description("1")]
        Nao_Recebido,
        [Description("2")]
        Nao_Processado,
        [Description("3")]
        Processado_Com_Erro,
        [Description("4")]
        Processado_Com_Sucesso
    }
}


public static class EnumExtensions
{
    public static string GetDescription<T>(this T e) where T : IConvertible
    {
        if (e is Enum)
        {
            Type type = e.GetType();
            Array values = System.Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }
        }
        return null;
    }
}