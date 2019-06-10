using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.ImportaAccess.Dados
{
    public class TabelaManutencao
    {
        List<TabelaManutencaoModel> table = new List<TabelaManutencaoModel>();
        public void CarregaTabela()
        {
            var servicos = Conection.Select("select * from [Tabela de manutenções]");

            while (servicos.Read())
            {
                table.Add(new TabelaManutencaoModel
                {
                    Codcli = servicos["Codcli"].ToString(),
                    Codsis = servicos["Codsis"].ToString(),
                    ValorManutencao = Convert.IsDBNull(servicos["Valor Manutenção"]) ? 0 : Convert.ToDouble(servicos["Valor Manutenção"]),
                    CodServico = servicos["CodServico"].ToString(),
                });
            }
        }

        public void CarregaTabelaPrecoServico()
        {

            List<TabelaPrecoServicoModel> table = new List<TabelaPrecoServicoModel>();
            var servicos = Conection.Select("select * from [Tabela de Preços de Serviços]");

            while (servicos.Read())
            {
                table.Add(new TabelaPrecoServicoModel
                {
                    CodTabeladePrecos = Convert.ToInt32(servicos["Cod Tabela de Preços"]),
                    Itemdatabela = Convert.ToInt32(servicos["Item da tabela"]),
                    Descricao = servicos["Descrição"].ToString(),
                    Tipo = servicos["Tipo"].ToString(),
                    Preco = Convert.IsDBNull(servicos["Preço"]) ? 0 : Convert.ToDouble(servicos["Preço"]),
                    EP = servicos["EP"].ToString(),
                });
            }
        }
    }
}
