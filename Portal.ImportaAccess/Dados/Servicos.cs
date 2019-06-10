using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.ImportaAccess.Dados
{
    public class Servicos
    {
        List<servicosModel> serv = new List<servicosModel>();

        public void CarregaServicos()
        {
           var servicos = Conection.Select("select * from Serviços");
            
            while (servicos.Read())
            {
                serv.Add(new servicosModel
                {
                    Codserv = servicos["Codserv"].ToString(),
                    servico = servicos["Serviço"].ToString(),
                    descricaonota = servicos["Título na NF"].ToString(),
                });
            }

            CarregaServicosAplicativos();
        }

        public void CarregaServicosAplicativos()
        {
            foreach (var item in serv)
            {
                var servicos = Conection.Select("select * from [Sistemas Aplicativos] where CodigoServico = '" + item.Codserv + "'");
                List<ServicosAplicativoModel> itens = new List<ServicosAplicativoModel>();
                while (servicos.Read())
                {
                    itens.Add(new ServicosAplicativoModel
                    {
                        Codsis = servicos["Codsis"].ToString(),
                        Sistema = servicos["Sistema"].ToString(),
                        CodigoServico = servicos["CodigoServico"].ToString(),
                        vl_anatel = Convert.IsDBNull(servicos["vl_anatel"]) ? 0 : Convert.ToDouble(servicos["vl_anatel"]),
                        link = servicos["link"].ToString(),
                    });
                }
                if(itens.Count > 0)
                {
                    item.ServicosAplicativos = itens;
                }

            }
            
        }
    }

    
}
