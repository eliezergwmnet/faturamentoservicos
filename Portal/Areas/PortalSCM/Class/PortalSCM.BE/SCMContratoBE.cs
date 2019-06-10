using System;
using System.Collections.Generic;
using Globais.BE;

namespace PortalSCM.BE
{
    public class SCMContratoBE : GlobaisLogBE
    {
        public int cont_id { get; set; }
        public int cli_id { get; set; }
        public GlobaisClienteBE Cliente { get; set; }
        public List<SCMClienteServicosBE> Servicos { get; set; }
        public string cont_numero { get; set; }
        public string cont_nome { get; set; }
        public string cont_descricao { get; set; }
        public DateTime cont_data { get; set; }
        public bool cont_fatura { get; set; }
        public bool cont_avulso { get; set; }//Falso Contrato mensa, True Contrato avulso
        public int conf_id { get { return Globais.Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
    }

    /// <summary>
    /// Classe responsavel por carrega os dados dos Contratos para efetuar o Faturamento
    /// </summary>
    public class SCMContratoFaturamentoBE
    {
        public Globais.BE.GlobaisEmpresaBE Empresa { get; set; }
        public Globais.BE.GlobaisEmpresaTaxasNotasBE EmpresaTaxas { get; set; }
        public List<SCMContratoFaturamentoImpostosBE> Contratos { get; set; }
    }

    public class SCMContratoFaturamentoImpostosBE
    {
        public SCMContratoBE Contrato { get; set; }

        /*decimal _iss = 0;
        public decimal iss
        {
            get { return _iss; }
            set
            {
                if (value < 1)
                    _iss = 0;
                else
                    _iss = value;
            }
        }*/

        decimal _pis = 0;
        public decimal pis
        {
            get { return _pis; }
            set
            {
                if (value < 1)
                    _pis = 0;
                else
                    _pis = value;
            }
        }

        decimal _confins = 0;
        public decimal confins
        {
            get { return _confins; }
            set
            {
                if (value < 1)
                    _confins = 0;
                else
                    _confins = value;
            }
        }

        decimal _cssl = 0;
        public decimal cssl
        {
            get { return _cssl; }
            set
            {
                if (value < 1)
                    _cssl = 0;
                else
                    _cssl = value;
            }
        }

        decimal _irrf = 0;
        public decimal irrf
        {
            get { return _irrf; }
            set
            {
                if (value < 1)
                    _irrf = 0;
                else
                    _irrf = value;
            }
        }

        decimal _valorLiquito = 0;
        public decimal valorLiquito
        {
            get { return _valorLiquito; }
            set
            {
                if (value < 1)
                    _valorLiquito = 0;
                else
                    _valorLiquito = value;
            }
        }

        decimal _valorBruto = 0;
        public decimal valorBruto
        {
            get { return _valorBruto; }
            set
            {
                if (value < 1)
                    _valorBruto = 0;
                else
                    _valorBruto = value;
            }
        }
    }
}
