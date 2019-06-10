using System;

namespace Portal.BE
{
    public class RetornoBancoBoletoBE
    {
        public RetornoBancoBoletoBE() { }
        public RetornoBancoBoletoBE(int _seuBanco, string _nossoNumero, DateTime _vencimento, DateTime _dataLiquidado, decimal _valorTitulo, decimal _valorCobrado, decimal _ocilacao, string _pagador, string _contaCobranca, string _retBol_formapagamento, int _usu_id)
        {
            this.retBol_seuBanco = _seuBanco;
            this.retBol_nossoNumero = _nossoNumero;
            this.retBol_vencimento = _vencimento;
            this.retBol_dataLiquidado = _dataLiquidado;
            this.retBol_valorTitulo = _valorTitulo;
            this.retBol_valorCobrando = _valorCobrado;
            this.retBol_ociliacao = _ocilacao;
            this.retBol_pagador = _pagador;
            this.retBol_contaCobranca = _contaCobranca;
            this.retBol_formapagamento = _retBol_formapagamento;
            this.usu_id = _usu_id;
        }
        public RetornoBancoBoletoBE(string _seuBanco, string _nossoNumero, string _vencimento, string _dataLiquidado, string _valorTitulo, string _valorCobrado, string _ocilacao, string _pagador, string _contaCobranca, string _retBol_formapagamento, int _usu_id)
        {
            this.retBol_seuBanco = Convert.ToInt32(_seuBanco);
            this.retBol_nossoNumero = _nossoNumero;
            this.retBol_vencimento = Convert.ToDateTime(_vencimento);
            this.retBol_dataLiquidado = Convert.ToDateTime(_dataLiquidado);
            this.retBol_valorTitulo = Convert.ToDecimal(_valorTitulo);
            this.retBol_valorCobrando = Convert.ToDecimal(_valorCobrado);
            this.retBol_ociliacao = Convert.ToDecimal(_ocilacao);
            this.retBol_pagador = _pagador;
            this.retBol_contaCobranca = _contaCobranca;
            this.retBol_formapagamento = _retBol_formapagamento;
            this.usu_id = _usu_id;
        }

        public int retBol_id { get; set; }
        public int retBol_seuBanco { get; set; }
        public string retBol_nossoNumero { get; set; }
        public DateTime retBol_vencimento { get; set; }
        public DateTime retBol_dataLiquidado { get; set; }
        public decimal retBol_valorTitulo { get; set; }
        public decimal retBol_valorCobrando { get; set; }
        public decimal retBol_ociliacao { get; set; }
        public string retBol_pagador { get; set; }
        public string retBol_contaCobranca { get; set; }
        public string retBol_formapagamento { get; set; }
        public int usu_id { get; set; }
        public string retBol_comentarios { get; set; }

    }
}
