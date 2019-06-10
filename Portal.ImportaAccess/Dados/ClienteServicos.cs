using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.ImportaAccess.Dados
{
    public class ClienteServicos
    {
        List<Clientes_x_ServicosModel> serv = new List<Clientes_x_ServicosModel>();
        public void CarregaDados()
        {
            var servicos = Conection.Select("select * from [Clientes x Serviços]");

            while (servicos.Read())
            {
                serv.Add(new Clientes_x_ServicosModel
                {
                    Codserv = servicos["Codcli"].ToString(),
                    Codcli = servicos["Codserv"].ToString(),
                    CodTabeladePrecos = servicos["Cód Tabela de Preços"].ToString(),
                });
            }
        }
    }
}
