using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.ImportaAccess.Dados
{
    public class NotaFiscal
    {
        List<NotaFiscalModel> table = new List<NotaFiscalModel>();
        public void CarregaNotaFiscal()
        {
            var servicos = Conection.Select("select * from [notas fiscais]");

            while (servicos.Read())
            {
                table.Add(new NotaFiscalModel
                {
                    NumerodaNota = Convert.ToInt32(servicos["Numero da Nota"]),
                    Codcli = servicos["Codcli"].ToString(),
                    Tipodereceita = servicos["Tipo de receita"].ToString(),
                    Codreceita = servicos["Cod receita"].ToString(),
                    Datadeemissao = Convert.ToDateTime(servicos["Data de emissão"]),
                    DatadeVencimento = Convert.ToDateTime(servicos["Data de Vencimento"]),
                    Pis = Convert.IsDBNull(servicos["Pis"]) ? 0 : Convert.ToDouble(servicos["Pis"]),
                    Cofins = Convert.IsDBNull(servicos["Cofins"]) ? 0 : Convert.ToDouble(servicos["Cofins"]),
                    Cssl = Convert.IsDBNull(servicos["Cssl"]) ? 0 : Convert.ToDouble(servicos["Cssl"]),
                    Irrf = Convert.IsDBNull(servicos["Irrf"]) ? 0 : Convert.ToDouble(servicos["Irrf"]),
                    Total_bruto = Convert.IsDBNull(servicos["Total_bruto"]) ? 0 : Convert.ToDouble(servicos["Total_bruto"]),
                    Total_liquido = Convert.IsDBNull(servicos["Total_liquido"]) ? 0 : Convert.ToDouble(servicos["Total_liquido"]),
                    Preenchida = Convert.IsDBNull(servicos["Preenchida"]) ? false : Convert.ToBoolean(servicos["Preenchida"]),
                    Emitida = Convert.IsDBNull(servicos["Emitida"]) ? false : Convert.ToBoolean(servicos["Emitida"]),
                    Controle_impostos = Convert.ToInt32(servicos["Controle_impostos"]),

                });
            }
            CarregaNotaFiscalItens();
        }

        public void CarregaNotaFiscalItens()
        {
            foreach (var item in table)
            {
                var servicos = Conection.Select("select * from [itens da Nota] where [Numero da Nota] = " + item.NumerodaNota);

                item.itens = new List<NotaFiscalItensModel>();
                if (servicos != null)
                {
                    while (servicos.Read())
                    {

                        item.itens.Add(new NotaFiscalItensModel
                        {
                            NumerodaNota = Convert.ToInt32(servicos["Numero da Nota"]),
                            Itemdanota = Convert.ToInt32(servicos["Item da nota"]),
                            Quantidade = Convert.IsDBNull(servicos["Quantidade"]) ? 0 : Convert.ToInt32(servicos["Quantidade"]),
                            Descricao = servicos["Descrição"].ToString(),
                            PrecoUnitario = Convert.IsDBNull(servicos["Preço Unitário"]) ? 0 : Convert.ToDouble(servicos["Preço Unitário"]),
                            PreçoTotal = Convert.IsDBNull(servicos["Preço Total"]) ? 0 : Convert.ToDouble(servicos["Preço Total"]),
                            Tipo = servicos["Tipo"].ToString(),
                            EP = servicos["EP"].ToString(),
                            tipo_receita = servicos["tipo_receita"].ToString(),
                            Cssl = Convert.IsDBNull(servicos["Cssl"]) ? 0 : Convert.ToDouble(servicos["Cssl"]),
                            Irrf = Convert.IsDBNull(servicos["Irrf"]) ? 0 : Convert.ToDouble(servicos["Irrf"]),
                            Total_bruto = Convert.IsDBNull(servicos["Total_bruto"]) ? 0 : Convert.ToDouble(servicos["Total_bruto"]),
                            Total_liquido = Convert.IsDBNull(servicos["Total_liquido"]) ? 0 : Convert.ToDouble(servicos["Total_liquido"]),
                            Preenchida = Convert.IsDBNull(servicos["Preenchida"]) ? false : Convert.ToBoolean(servicos["Preenchida"]),
                            Emitida = Convert.IsDBNull(servicos["Emitida"]) ? false : Convert.ToBoolean(servicos["Emitida"]),
                            Emitida1 = Convert.IsDBNull(servicos["Emitida1"]) ? false : Convert.ToBoolean(servicos["Emitida1"]),


                        });


                    }
                }
            }
           
            var teste = table;


        }
    }
}
