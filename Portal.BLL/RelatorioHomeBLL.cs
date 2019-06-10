using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class RelatorioHomeBLL
    {
        /// <summary>
        /// Retorna lista com o faturamento por mes do ano atual
        /// </summary>
        /// <returns></returns>
        public List<RelatorioHomeMesBE> SelectFaturamentoPorMesAtual()
        {
            return new RelatorioHomeDao().Select<RelatorioHomeMesBE>(new { conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }

        /// <summary>
        /// Retorna lista com o faturamento por mes do ano passado como parametro
        /// </summary>
        /// <param name="mes"></param>
        /// <returns></returns>
        public List<RelatorioHomeMesBE> SelectFaturamentoPorMesAtual(int ano)
        {
            return new RelatorioHomeDao().Select<RelatorioHomeMesBE>(new { mes = ano, conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }

        /// <summary>
        /// Retorna lista com o faturamento de todos os anos
        /// </summary>
        /// <returns></returns>
        public List<RelatorioHomeMesBE> SelectFaturamentoPorAno()
        {
            return new RelatorioHomeDao().Select<RelatorioHomeMesBE>(new { tipoexecucao = "ANO", conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }

        /// <summary>
        /// Retorna lista dos pagamento em aberto
        /// </summary>
        /// <returns></returns>
        public List<RelatorioHomeValoresBE> SelectPagamentoPendentes()
        {
            return new RelatorioHomeDao().Select<RelatorioHomeValoresBE>(new { tipoexecucao = "PAGPE", conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }

        /// <summary>
        /// Retorna lista dos pagamento atrasados
        /// </summary>
        /// <returns></returns>
        public List<RelatorioHomeValoresBE> SelectPagamentoAtrasados()
        {
            return new RelatorioHomeDao().Select<RelatorioHomeValoresBE>(new { tipoexecucao = "PAGAT", conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }

        /// <summary>
        /// Retorna a lista de todos os pagamentos liquitados do mes
        /// </summary>
        /// <returns></returns>
        public List<RelatorioHomeMesBE> SelectPagamentosPagoMes()
        {
            return new RelatorioHomeDao().Select<RelatorioHomeMesBE>(new { tipoexecucao = "PAGBA", conf_id = Globais.Helper.Common.GetEmpresaSelecionada() }).ToList();
        }
    }
}
