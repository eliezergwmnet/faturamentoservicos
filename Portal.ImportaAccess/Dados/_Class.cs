using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.ImportaAccess.Dados
{
    public class servicosModel
    {
        public string Codserv { get; set; }
        public string servico { get; set; }
        public string descricaonota { get; set; }
        public List<ServicosAplicativoModel> ServicosAplicativos { get; set; }
    }
    public class ServicosAplicativoModel
    {
        public string Codsis { get; set; }
        public string Sistema { get; set; }
        public string CodigoServico { get; set; }
        public double vl_anatel { get; set; }
        public string link { get; set; }
    }
    /// <summary>
    /// Valor dos serviços cliente
    /// </summary>
    public class TabelaManutencaoModel
    {
        public string Codcli { get; set; }
        public string Codsis { get; set; }
        public double ValorManutencao { get; set; }
        public string CodServico { get; set; }
    }

    /// <summary>
    /// Tabela com al ista de serviços do cliente
    /// </summary>
    public class TabelaPrecoServicoModel
    {
        public int CodTabeladePrecos { get; set; }
        public int Itemdatabela { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string Tipo { get; set; }
        public string EP { get; set; }
    }

    /// <summary>
    /// Tabela referencia Cliente e Lista de Serviços
    /// </summary>
    public class Clientes_x_ServicosModel
    {
        public string Codcli { get; set; }
        public string Codserv { get; set; }
        public string CodTabeladePrecos { get; set; }
    }

    public class NotaFiscalModel
    {
        public int NumerodaNota { get; set; }
        public string Codcli { get; set; }
        public string Tipodereceita { get; set; }
        public string Codreceita { get; set; }
        public DateTime Datadeemissao { get; set; }
        public DateTime DatadeVencimento { get; set; }
        public double Pis { get; set; }
        public double Cofins { get; set; }
        public double Cssl { get; set; }
        public double Irrf { get; set; }
        public double Total_bruto { get; set; }
        public double Total_liquido { get; set; }
        public bool Preenchida { get; set; }
        public bool Emitida { get; set; }
        public int Controle_impostos { get; set; }
        public List<NotaFiscalItensModel> itens { get; set; }
    }

    public class NotaFiscalItensModel
    {
        public int NumerodaNota { get; set; }
        public int Itemdanota { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public double PrecoUnitario { get; set; }
        public double PreçoTotal { get; set; }
        public string Tipo { get; set; }
        public string EP { get; set; }
        public string tipo_receita { get; set; }
        public double Cssl { get; set; }
        public double Irrf { get; set; }
        public double Total_bruto { get; set; }
        public double Total_liquido { get; set; }
        public bool Preenchida { get; set; }
        public bool Emitida { get; set; }
        public bool Emitida1 { get; set; }

    }
}
