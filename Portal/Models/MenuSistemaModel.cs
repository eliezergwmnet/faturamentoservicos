using System.Collections.Generic;


public class HelpSistemaModel
{
    public static List<HelpSistemaModel> ListamenuItem()
    {
        var result = new List<HelpSistemaModel>();
        return result;
    }

    public static List<MenuSistemaModel> CarregaMenuProfile()
    {
        var result = new List<MenuSistemaModel>();
        result.Add(new MenuSistemaModel { id = 0, idPai = 0, icon = "", link = "/MeusDados", nome = "Meus Dados", referencia = MenuReferenciaitemEnum.MenuProfileMeusDados, tipo = MenuLateralTipoEnum.MenuLateral });
        result.Add(new MenuSistemaModel { id = 0, idPai = 0, icon = "", link = "Notificacao", nome = "Notificação", referencia = MenuReferenciaitemEnum.MenuProfileNotificacao, tipo = MenuLateralTipoEnum.MenuLateral });
        result.Add(new MenuSistemaModel { id = 0, idPai = 0, icon = "", link = "/Configuracao", nome = "Configuração", referencia = MenuReferenciaitemEnum.MenuProfileConfiguracao, tipo = MenuLateralTipoEnum.MenuLateral });
        //result.Add(new MenuSistemaModel { id = 0, idPai = 0, icon = "", link = "", nome = "Sair", referencia = MenuReferenciaitemEnum.MenuProfileSair, tipo = MenuLateralTipoEnum.MenuLateral });
        return result;
    }

    public static SistemaDadosConfiguracao Configuracao()
    {
        SistemaDadosConfiguracao configuracao = new SistemaDadosConfiguracao()
        {
            Logo = "/images/logo-header.png",
            Logo2 = "/images/logo-header2.png",
            Titulo = "GWMNet - Faturamento",
            Descricao = "GWMNet - Faturamento",
            Icon = "/favicon.ico",
            IconTouch = "/favicon.png"
        };
        return configuracao;
    }
}

#region Classes

public class SistemaDadosConfiguracao
{
    public string Logo { get; set; }
    public string Logo2 { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Icon { get; set; }
    public string IconTouch { get; set; }
}

public class MenuSistemaModel
{
    public int id { get; set; }
    public int idPai { get; set; }
    public string nome { get; set; }
    public string icon { get; set; }
    public MenuLateralTipoEnum tipo { get; set; }
    public MenuReferenciaitemEnum referencia { get; set; }
    public string link { get; set; }
}

#endregion

#region Enums

public enum MenuLateralTipoEnum {
    MenuLateral,
    MenuTopo
}

public enum MenuReferenciaitemEnum
{
    MenuProfileMeusDados,
    MenuProfileNotificacao,
    MenuProfileConfiguracao,
    MenuProfileSair
}



#endregion