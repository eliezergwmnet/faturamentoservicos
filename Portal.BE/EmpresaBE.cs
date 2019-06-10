
using Globais.BE;

namespace Portal.BE
{
    public class EmpresaBE: GlobaisEnderecoBE
    {
        public int conf_id { get { return Globais.Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
        public string conf_nomefantasia { get; set; }
        public string conf_razaosocial { get; set; }
        public string conf_cnpj { get; set; }
        public int conf_banco { get; set; }
        public string conf_agencia { get; set; }
        public string conf_agenciadiito { get; set; }
        public string conf_conta { get; set; }
        public string conf_digito { get; set; }
        public string conf_inscricaoestadual { get; set; }
        public string conf_ccm { get; set; }
        public int confCom_id { get; set; }
        public string confCom_logo { get; set; }
        public bool confCom_calcularTributos { get; set; }
        public decimal confCom_valormaior { get; set; }
        public decimal confCom_pis { get; set; }
        public decimal confCom_confins { get; set; }
        public decimal confCom_cssl { get; set; }
        public decimal confCom_irrf { get; set; }
        public bool confCom_boleto { get; set; }
        public bool confCom_nota { get; set; }


    }



    public class EmpresaComplementarBE: GlobaisEnderecoBE
    {
    }
}
