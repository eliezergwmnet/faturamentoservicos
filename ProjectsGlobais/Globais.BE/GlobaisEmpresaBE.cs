using System;
using System.Collections.Generic;
using Globais.Helper;

namespace Globais.BE
{
    public class GlobaisEmpresaBE : GlobaisEnderecoBE
    {
        public int conf_id { get; set; }
        //public int conf_id { get { return Globais.Helper.Common.GetEmpresaSelecionada(); } set { value = conf_id; } }
        public string conf_nomefantasia { get; set; }
        public string conf_razaosocial { get; set; }
        public string conf_cnpj { get; set; }
        public string conf_inscricaoestadual { get; set; }
        public string conf_telefone { get; set; }
        public string conf_celular { get; set; }
        public bool conf_celularwhats { get; set; }
        public string conf_email { get; set; }
        public string conf_dominio { get; set; }
        public string conf_imagempequena { get; set; }
        public string conf_imagemgrande { get; set; }
        public string conf_layoutcor { get; set; }

        public List<GlobaisModulosBE> Modulos { get; set; }
        /*ModeloTemplate _conf_layoutcor;
        public ModeloTemplate conf_layoutcor { get { return _conf_layoutcor; }
            set
            {
                foreach (Globais.Helper.ModeloTemplate item in Enum.GetValues(typeof(Globais.Helper.ModeloTemplate)))
                {
                    if (value == item)
                        _conf_layoutcor = item;
                }

            }
        }*/
    }

    public class GlobaisEmpresaBancoBE{
        public int conf_banco { get; set; }
        public string conf_agencia { get; set; }
        public string conf_agenciadiito { get; set; }
        public string conf_conta { get; set; }
        public string conf_digito { get; set; }
        public string conf_codigo { get; set; }
    }

    public class GlobaisEmpresaTaxasNotasBE
    {
        public int conf_id { get; set; }
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
}
