using System;
using Globais.Helper;
using Portal.BE;

namespace Portal.BLL
{
    public class ClienteNotaCalcularNota
    {
        #region Calcular Valor Proporcional
        public virtual bool CalcularProporcional(ClienteServicosBE obj)
        {
            bool result = false;
            switch (obj.servCli_cobrarPorpor)
            {
                case 1:
                    result = this.CalculaSemProporcional(obj);
                    break;
                case 2:
                    result = this.CalculaProporcionalMesAnterior(obj);
                    break;
                case 3:
                    result = this.CalculaProporcionalMesAtual(obj);
                    break;
                default:
                    result = this.CalculaSemProporcional(obj);
                    break;
            }
            return result;
        }
        public virtual bool CalculaSemProporcional(ClienteServicosBE obj)
        {
            try
            {
                ClienteNotaBE nota = new ClienteNotaBE(obj.cli_id, obj.servCli_descricao, obj.servCli_valor, null, false, true, obj.serv_id, obj.servCli_contrato, obj.servCli_contratonome);
                var ins = new ClienteNotaBLL().Insert(nota);

                return true;
            }
            catch(Exception ex)
            {
                Common.TratarLogErro(ex);
                return false;
            }
        }
        public virtual bool CalculaProporcionalMesAnterior(ClienteServicosBE obj)
        {
            try
            {
                int mes = DateTime.Now.Month == 1 ? 12: DateTime.Now.Month - 1;
                int dias = DateTime.Now.Month == 1 ? dias = DateTime.DaysInMonth((DateTime.Now.Year - 1), mes) : DateTime.DaysInMonth((DateTime.Now.Year), mes);
                decimal valor = (obj.servCli_valor / dias) * obj.servCli_qtdDias;
                valor = Decimal.Round(valor, 2);


                ClienteNotaBE notaAnterior = new ClienteNotaBE(obj.cli_id, obj.servCli_descricao + " Valor Proporional ao Mês " + Common.GetNameMonth(mes), valor, null, false, true, obj.serv_id, obj.servCli_contrato, obj.servCli_contratonome);
                ClienteNotaBE notaAtual = new ClienteNotaBE(obj.cli_id, obj.servCli_descricao, obj.servCli_valor, null, false, true, obj.serv_id, obj.servCli_contrato, obj.servCli_contratonome);

                var ins = new ClienteNotaBLL().Insert(notaAnterior);
                var ins2 = new ClienteNotaBLL().Insert(notaAtual);
                return true;
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return false;
            }
        }
        public virtual bool CalculaProporcionalMesAtual(ClienteServicosBE obj)
        {
            try
            {
                int dias = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                decimal valor = (obj.servCli_valor / dias) * obj.servCli_qtdDias;
                valor = Decimal.Round(valor, 2);

                ClienteNotaBE notaAtual = new ClienteNotaBE(obj.cli_id, obj.servCli_descricao, valor, null, false, true, obj.serv_id, obj.servCli_contrato, obj.servCli_contratonome);
                var ins = new ClienteNotaBLL().Insert(notaAtual);
                return true;
            }
            catch (Exception ex)
            {
                Common.TratarLogErro(ex);
                return false;
            }
        }
        #endregion

        public virtual bool CalcularParcelas(ClienteServicosBE obj)
        {
            if (obj.servCli_parcelado)
            {
                if (obj.servCli_parceladoQtd > 1)
                {
                    var parcelas = Decimal.Round(obj.servCli_valor / obj.servCli_parceladoQtd, 3);
                    var primeiraparcela = parcelas;
                    var _string = parcelas.ToString();
                    var _array = _string.Split(',');

                    if (_array.Length > 1 && _array[1].Length > 2)
                    {
                        var _ultimocampos = Convert.ToInt32(_string.Substring(_string.Length - 1));
                        if (_ultimocampos > 0)
                        {
                            parcelas = Convert.ToDecimal(_string.Substring(0, _string.Length - 1));
                            primeiraparcela = parcelas + (obj.servCli_valor - (parcelas * obj.servCli_parceladoQtd));
                        }
                    }

                    var cliNota = new ClienteNotaBLL();
                    ClienteNotaBE valorParcelas = new ClienteNotaBE(obj.cli_id, obj.servCli_descricao, primeiraparcela, null, false, true, obj.serv_id, obj.servCli_contrato, obj.servCli_contratonome);

                    for (int i = 0; i < obj.servCli_parceladoQtd; i++)
                    {
                        if (i > 0)
                            valorParcelas.cliNot_valor = parcelas;

                        valorParcelas.cliNot_parcela = i + 1;
                        valorParcelas.cliNot_descricao = obj.servCli_descricao + " " + (i + 1) + " / " + obj.servCli_parceladoQtd;
                        valorParcelas.cliNot_id = 0;
                        cliNota.Insert(valorParcelas);
                    }
                }
                else
                {
                    new ClienteNotaBLL().Insert(new ClienteNotaBE(obj.cli_id, obj.servCli_descricao, obj.servCli_valor, 1, false, true, obj.serv_id, obj.servCli_contrato, obj.servCli_contratonome));
                }
            }
            else
            {
                new ClienteNotaBLL().Insert(new ClienteNotaBE(obj.cli_id, obj.servCli_descricao, obj.servCli_valor, 1, false, true, obj.serv_id, obj.servCli_contrato, obj.servCli_contratonome));
            }
            return true;
        }
    }
}
