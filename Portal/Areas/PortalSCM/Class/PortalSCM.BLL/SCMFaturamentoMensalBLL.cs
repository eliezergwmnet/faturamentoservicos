using System;
using System.Collections.Generic;
using System.Linq;
using Globais.BLL;
using Globais.Helper;
using GlobaisEmpresaDadosComplem.BLL;
using NFENotasFiscais.BE;
using NFENotasFiscais.BLL;
using NFENotasFiscais.BLL.NFEServicos;
using PortalSCM.BE;
using PortalSCM.Helper;

namespace PortalSCM.BLL
{
    /// <summary>
    /// Classe responsavel por carrega os dados de faturamento
    /// </summary>
    public class SCMFaturamentoMensalBLL
    {
        int conf_id;
        int avulso;
        int numeroNota = 0;
        List<SCMContratoBE> Contratos;
        SCMContratoFaturamentoBE Faturamento;

        NTLoteBE loteNota;

        /// <summary>
        /// Carrega dados para o faturamento
        /// </summary>
        /// <param name="_conf_id"></param>
        /// <param name="_avulso">
        /// 1 => Todos os Contratos
        /// 2 => Contratos Mensais
        /// 3 => Contratos Avulso
        /// </param>
        public SCMFaturamentoMensalBLL(int _conf_id, int _avulso = 3)
        {
            this.conf_id = _conf_id;
            this.avulso = _avulso;
            this.Contratos = new List<SCMContratoBE>();
            this.Faturamento = new SCMContratoFaturamentoBE();
            this.Faturamento.Contratos = new List<SCMContratoFaturamentoImpostosBE>();
        }

        public SCMContratoFaturamentoBE CalcularFaturamento()
        {
            this.CarregaEmpresa();
            this.CarregaContratosPendentes();
            this.CarregaFaturamento();

            return Faturamento;
        }

        public int CalcularFaturamento(string contratos)
        {
            try
            {
                this.CarregaEmpresa();
                this.CarregaContratosPendentes(contratos);
                this.CarregaFaturamento();
                this.CriarLoteNotaFiscal();
                if (this.SalvaNotas())
                {
                    NFEEnviarNota envNota = new NFEEnviarNota(this.loteNota, this.Faturamento.Empresa);
                    return envNota.EnviarNotaXML();
                }
                else
                    return 0;
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return 0;
            }
        }

        /// <summary>
        /// Carrega os dados da empresa para calcular impostos
        /// </summary>
        void CarregaEmpresa()
        {
            try
            {
                this.Faturamento.Empresa = new GlobaisEmpresaBLL().SelectId(new Globais.BE.GlobaisEmpresaBE { conf_id = this.conf_id });
                this.Faturamento.EmpresaTaxas = new GlobaisEmpresaComplementoBLL().SelectId(new Globais.BE.GlobaisEmpresaTaxasNotasBE { conf_id = this.conf_id });
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
            }
        }

        /// <summary>
        /// Carrega a lista de contratos pendentes no sistema para o faturamento
        /// </summary>
        void CarregaContratosPendentes()
        {
            try
            {
                if (this.avulso == 1)
                    this.Contratos = new SCMContratoBLL().SelectFaturamentoMensalPendente(new BE.SCMContratoBE { cont_avulso = true, conf_id = this.conf_id });
                else if (this.avulso == 2)
                    this.Contratos = new SCMContratoBLL().SelectFaturamentoMensalPendente(new BE.SCMContratoBE { cont_avulso = false, conf_id = this.conf_id });
                else
                    this.Contratos = new SCMContratoBLL().SelectFaturamentoMensalPendente(new BE.SCMContratoBE { conf_id = this.conf_id });
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
            }
        }

        /// <summary>
        /// Carrega a lista de faturamentos selecionados pelo usuario
        /// </summary>
        /// <param name="contratos"></param>
        void CarregaContratosPendentes(string contratos)
        {
            try
            {
                this.Contratos = new SCMContratoBLL().SelectFaturamentoMensalContratoPendente(new BE.SCMContratoBE { conf_id = this.conf_id, cont_descricao = contratos });
                this.numeroNota = new NTNotasBLL().NumeroNota(conf_id);
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
            }
        }

        /// <summary>
        /// Cria novo lote para enviar a receita
        /// </summary>
        void CriarLoteNotaFiscal()
        {
            NTLoteBLL _lote = new NTLoteBLL();
            loteNota = new NTLoteBE
            {
                conf_id = this.conf_id,
                lote_serie = GlobaisSCM.NFeSerie,
                lote_modulo = GlobaisSCM.Modulo,
                lote_emissao = DateTime.Now,
                lote_status = StatusNotaNFe.LoteNovo.GetDescription()
            };
            loteNota = _lote.Insert(loteNota);
        }

        /// <summary>
        /// Calula as Taxas do Faturamento usando os parametros definido na empresa
        /// </summary>
        void CarregaFaturamento()
        {

            try
            {
                foreach (var item in this.Contratos)
                {
                    SCMContratoFaturamentoImpostosBE obj = new SCMContratoFaturamentoImpostosBE();
                    obj.Contrato = item;
                    obj.valorBruto = (from x in item.Servicos select x.servCli_valor).Sum();

                    if (this.Faturamento.EmpresaTaxas.confCom_calcularTributos && !item.Cliente.cli_simplesNacional)
                    {
                        /*var pis = Math.Round(((obj.valorBruto * GlobaisSCM.TaxaPis) / 100), 2);
                        var confins = Math.Round(((obj.valorBruto * GlobaisSCM.TaxaConfins) / 100), 2);
                        var cssl = Math.Round(((obj.valorBruto * GlobaisSCM.TaxaCssl) / 100), 2);*/


                        var pis = this.ValorTruncado(((obj.valorBruto * GlobaisSCM.TaxaPis) / 100), 2);
                        var confins = this.ValorTruncado(((obj.valorBruto * GlobaisSCM.TaxaConfins) / 100), 2);
                        var cssl = this.ValorTruncado(((obj.valorBruto * GlobaisSCM.TaxaCssl) / 100), 2);

                       /* pis = Math.Round(Convert.ToDecimal(pis.ToString().Substring(0, pis.ToString().Length - 1)), 2);
                        confins = Math.Round(Convert.ToDecimal(confins.ToString().Substring(0, confins.ToString().Length - 1)), 2);
                        cssl = Math.Round(Convert.ToDecimal(cssl.ToString().Substring(0, cssl.ToString().Length - 1)), 2);*/

                        decimal somaTaxas = Math.Round(pis + confins + cssl);

                        if(somaTaxas > GlobaisSCM.TaxaOutrosCobrarAcimaDe)
                        {
                            obj.pis = pis;
                            obj.confins = confins;
                            obj.cssl = cssl;

                            var irrf = Math.Round(((obj.valorBruto * GlobaisSCM.TaxaIR) / 100), 2);
                            if (irrf > GlobaisSCM.TaxaCobrarAcimaDe)
                                obj.irrf = irrf;

                            obj.valorLiquito = Math.Round(obj.valorBruto - (obj.pis + obj.confins + obj.cssl + obj.irrf), 2);
                        }
                        else
                        {
                            obj.valorLiquito = obj.valorBruto;
                        }
                    }
                    else
                        obj.valorLiquito = obj.valorBruto;

                    this.Faturamento.Contratos.Add(obj);
                }
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
            }
        }

        public decimal ValorTruncado(decimal valor, int precisao)
        {
            decimal fator = (decimal)Math.Pow(10d, precisao);
            decimal valorTruncado = Math.Floor(valor * fator);
            return Math.Floor((Math.Round(valorTruncado, precisao))) / fator;
        }

        bool SalvaNotas()
        {
            try
            {
                List<NTNotasBE> notas = new List<NTNotasBE>();
                NTNotasBLL _notaDao = new NTNotasBLL();
                NTNotasItensBLL _notaItensDao = new NTNotasItensBLL();
                foreach (var item in this.Faturamento.Contratos)
                {
                    var obj = new NTNotasBE();
                    obj.not_numero = this.numeroNota;
                    obj.cli_id = item.Contrato.cli_id;
                    obj.cont_id = item.Contrato.cont_id;
                    obj.conf_id = conf_id;
                    obj.not_tipoReceita = "N";
                    obj.not_codReceita = "N";
                    obj.not_dtEmissao = DateTime.Now;
                    obj.not_dtVencimento = this.DataVencimento(item.Contrato.Cliente.cli_tipoVencimento, item.Contrato.Cliente.cli_parametroVencimento); // DateTime.Now.AddDays(10);//Ajustes com os parametros do sistema
                    obj.not_pis = item.pis;
                    obj.not_confins = item.confins;
                    obj.not_cssl = item.cssl;
                    obj.not_irrf = item.irrf;
                    obj.not_totalbruto = item.valorBruto;
                    obj.not_totalliquido = item.valorLiquito;
                    obj.not_preenchida = 0;//Muda o status quando gera a cobrança
                    obj.not_emitida = false;//Muda o status quando gera a nota fiscal
                    obj.not_situacao = "N";
                    obj.not_chave = "";
                    obj.lote_id = this.loteNota.lote_id;
                    obj.not_tipopagamento = "";
                    obj.Contrato = item.Contrato;

                    obj = _notaDao.Insert(obj);
                    obj.NotaItens = new List<NTNotasItensBE>();
                    this.numeroNota++;

                    obj.NotaItens = new List<NTNotasItensBE>();

                    foreach (var itemContrato in item.Contrato.Servicos)
                    {
                        var objItem = new NTNotasItensBE();

                        objItem.not_id = obj.not_id;
                        objItem.not_numero = obj.not_numero;
                        objItem.notI_qtde = 1;
                        objItem.notI_descricao = itemContrato.servCli_nome;
                        objItem.notI_precoUnitario = itemContrato.servCli_valor;
                        objItem.notI_total = itemContrato.servCli_valor;
                        objItem.notI_tipo = "F";
                        objItem.notI_ep = "P";
                        objItem.notI_tipoReceita = "-";

                        objItem = _notaItensDao.Insert(objItem);
                        obj.NotaItens.Add(objItem);
                    }
                    notas.Add(obj);
                }

                this.loteNota.Notas = notas;

                return true;
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return false;
            }
        }

        DateTime DataVencimento(string tipovencimento, string objParametroVencimento)
        {
            DateTime retorno;
            if (objParametroVencimento == "*")
                objParametroVencimento = "20";
            //Vencimento no mesmo mes
            if (tipovencimento == "1")
            {
                //retorno = Convert.ToDateTime(objParametroVencimento + "/08/2018").Date;//.AddDays(Convert.ToInt32(objParametroVencimento));
                if (Convert.ToInt32(objParametroVencimento) > DateTime.Now.Day)
                {
                    if (DateTime.Now.Month == 2 && Convert.ToInt32(objParametroVencimento) > 28)
                        retorno = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 28);
                    else
                        retorno = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(objParametroVencimento));
                }
                else
                {
                    if (DateTime.Now.Month == 12)
                        retorno = new DateTime((DateTime.Now.Year + 1), 1, Convert.ToInt32(objParametroVencimento));
                    else
                        retorno = new DateTime(DateTime.Now.Year, (DateTime.Now.Month + 1), Convert.ToInt32(objParametroVencimento));
                }
            }
            //Vencimento no mes seguinte
            else if (tipovencimento == "2")
            {
                //retorno = Convert.ToDateTime(objParametroVencimento + "/09/2018").Date;// (Convert.ToInt32(objParametroVencimento));
                if (DateTime.Now.Month == 12)
                    retorno = new DateTime((DateTime.Now.Year + 1), 1, Convert.ToInt32(objParametroVencimento));
                else
                    retorno = new DateTime(DateTime.Now.Year, (DateTime.Now.Month + 1), Convert.ToInt32(objParametroVencimento));
            }
            //Vencimento tantos dias apos a geração da nota
            else
            {
                //retorno = Convert.ToDateTime("02/08/2018").Date.AddDays(Convert.ToInt32(objParametroVencimento));
                retorno = DateTime.Now.AddDays(Convert.ToInt32(objParametroVencimento));
            }
            return retorno;
        }
    }
}
