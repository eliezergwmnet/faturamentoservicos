/*using System;
using System.Collections.Generic;
using System.Linq;
using Portal.BE;
using Portal.BLL;

namespace Portal.Notas
{
    public class GerarNotas
    {
        int _numeroNotaProvavel = 0;
        int conf_id;

        public GerarNotas(int _conf_id)
        {
            conf_id = _conf_id;
        }

        public List<ClienteNotasContratoBE> GerarNotasMes(string CriarNotas)
        {
            _numeroNotaProvavel = new NotasBLL().SelecionarUltimaNova();
            var list = this.CarregarClientesServicosContrato();
            if (CriarNotas != "")
            {
                var array = CriarNotas.Split(',');
                var newList = new List<ClienteNotasContratoBE>();
                foreach (var item in array)
                {
                    var arrayitem = item.Split('|');
                    var cliItem = (from x in list where x.cli_id.Equals(Convert.ToInt32(arrayitem[0])) && x.cliNot_contrato.Equals(Convert.ToInt32(arrayitem[1])) select x).FirstOrDefault<ClienteNotasContratoBE>();
                    if (cliItem != null)
                        newList.Add(cliItem);
                }
                CadastraItens(newList);
                
            }
            return list;
        }

        public List<ClienteBE> GerarNotasMes()
        {
            _numeroNotaProvavel = new NotasBLL().SelecionarUltimaNova();
            return this.CarregarClientesServicos();
        }
        public List<ClienteNotasContratoBE> GerarNotasMesContrato()
        {
            _numeroNotaProvavel = new NotasBLL().SelecionarUltimaNova();
            return this.CarregarClientesServicosContrato();
        }

        void CadastraItens(List<ClienteNotasContratoBE> obj)
        {
            var _nota = new NotasBLL();
            var _notaitens = new NotasItensBLL();
            var _cliNota = new ClienteNotaBLL();
            
            foreach (var item in obj)
            {
                item.Nota.not_chave = new GerarChave().CarregarCampoChaveCodificacaoNF(item);
                item.Nota.conf_id = conf_id;
                item.Nota = _nota.Insert(item.Nota);
                item.Nota.not_numero = _nota.SelecionarUltimaNova();


                if (item.Nota.not_id > 0)
                {
                    foreach (var itemItens in item.Nota.ItensNota)
                    {
                        itemItens.not_id = item.Nota.not_id;
                        itemItens.not_numero = item.Nota.not_numero;
                        _notaitens.Insert(itemItens);
                    }
                }
                if(item.ServicosNotas.Count > 0)
                {
                    foreach (var itemServ in item.ServicosNotas)
                    {
                        if (itemServ.cliNot_parcela > 0)
                            _cliNota.BaixaParcela(itemServ);
                    }
                }
            }
        }

        List<ClienteBE> CarregarClientesServicos()
        {
            ClienteBLL _cli = new ClienteBLL();
            var listacliente  = _cli.SelectNotas(conf_id);
            var empresa = new EmpresaBLL().SelectId(new EmpresaBE { conf_id = conf_id });
            foreach (var item in listacliente)
            {
                item.ServicosNotas = this.CarregarServicosClientes(item.cli_id);
                item.Nota = this.CarregarItensNota(item, empresa);
            }
            return listacliente;
        }
        List<ClienteNotasContratoBE> CarregarClientesServicosContrato()
        {
            ClienteBLL _cli = new ClienteBLL();
            var listacliente = _cli.SelectNotasContrato(conf_id);
            var empresa = new EmpresaBLL().SelectId(new EmpresaBE { conf_id = conf_id });
            foreach (var item in listacliente)
            {
                item.ServicosNotas = this.CarregarServicosClientes(item.cli_id, item.cliNot_contrato);
                item.Nota = this.CalculaTributosNota(this.CarregarItensNota(item, empresa), empresa);
            }
            return listacliente;
        }
        List<ClienteNotaBE> CarregarServicosClientes(int id)
        {
            ClienteNotaBLL _not = new ClienteNotaBLL();
            return _not.SelectNota(id);
        }

        List<ClienteNotaBE> CarregarServicosClientes(int id, int cliNot_contrato)
        {
            ClienteNotaBLL _not = new ClienteNotaBLL();
            return _not.SelectNota(id, cliNot_contrato);
        }

        NotasBE CarregarItensNota(ClienteNotasContratoBE obj, EmpresaBE empresa)
        {
            _numeroNotaProvavel++;
            List<NotasItensBE> _notasItens = new List<NotasItensBE>();
            foreach (var item in obj.ServicosNotas)
                _notasItens.Add(new NotasItensBE
                {
                    not_id = 0,
                    not_numero = _numeroNotaProvavel,
                    notI_qtde = 1,
                    notI_descricao = item.cliNot_descricao,
                    notI_precoUnitario = item.cliNot_valor,
                    notI_total = item.cliNot_valor,
                    notI_tipo = "F",
                    notI_ep = "P",
                    notI_tipoReceita = "",
                    cliNot_id = item.cliNot_id
                });

            decimal valorTotal = obj.ServicosNotas.Sum(x => x.cliNot_valor);

            NotasBE _nota = new NotasBE();
            _nota.not_numero = _numeroNotaProvavel;
            _nota.cli_id = obj.cli_id;
            _nota.not_tipoReceita = "M";
            _nota.not_codReceita = "SBL";
            _nota.not_dtEmissao = DateTime.Now.Date;
            _nota.not_dtVencimento = this.DataVencimento(obj.cli_tipoVencimento, obj.cli_parametroVencimento);
            _nota.not_pis = 0;
            _nota.not_confins = 0;
            _nota.not_cssl = 0;
            _nota.not_irrf = 0;
            _nota.not_totalbruto = valorTotal;
            _nota.not_totalliquido = valorTotal;
            _nota.not_preenchida = 0;
            _nota.not_emitida = false;
            _nota.not_situacao = "";
            _nota.not_boleto = false;
            _nota.not_contrato = obj.cliNot_contrato;

            _nota.ItensNota = _notasItens;

            this.CalculaTributosNota(_nota, empresa);

            return _nota;
        }
        NotasBE CarregarItensNota(ClienteBE obj, EmpresaBE empresa)
        {
            _numeroNotaProvavel++;
            List<NotasItensBE> _notasItens = new List<NotasItensBE>();
            foreach (var item in obj.ServicosNotas)
                _notasItens.Add(new NotasItensBE
                {
                    not_id = 0,
                    not_numero = _numeroNotaProvavel,
                    notI_qtde = 1,
                    notI_descricao = item.cliNot_descricao,
                    notI_precoUnitario = item.cliNot_valor,
                    notI_total = item.cliNot_valor,
                    notI_tipo = "F",
                    notI_ep = "P",
                    notI_tipoReceita = "",
                    cliNot_id = item.cliNot_id
                });

            decimal valorTotal = obj.ServicosNotas.Sum(x => x.cliNot_valor);

            NotasBE _nota = new NotasBE();
            _nota.not_numero = _numeroNotaProvavel;
            _nota.cli_id = obj.cli_id;
            _nota.not_tipoReceita = "M";
            _nota.not_codReceita = "SBL";
            _nota.not_dtEmissao = DateTime.Now.Date;
            _nota.not_dtVencimento = this.DataVencimento(obj.cli_tipoVencimento, obj.cli_parametroVencimento);
            _nota.not_pis = 0;
            _nota.not_confins = 0;
            _nota.not_cssl = 0;
            _nota.not_irrf = 0;
            _nota.not_totalbruto = valorTotal;
            _nota.not_totalliquido = valorTotal;
            _nota.not_preenchida = 0;
            _nota.not_emitida = false;
            _nota.not_situacao = "";
            _nota.not_boleto = false;

            _nota.ItensNota = _notasItens;

            this.CalculaTributosNota(_nota, empresa);

            return _nota;
        }

        NotasBE CalculaTributosNota(NotasBE nota, EmpresaBE empresa)
        {
            if (empresa.confCom_calcularTributos)
            {
                decimal pis = 0;
                decimal confins = 0;
                decimal cssl = 0;
                decimal irrf = 0;

                pis = Decimal.Round(nota.not_totalbruto * (empresa.confCom_pis / 100), 2);
                confins = Decimal.Round(nota.not_totalbruto * (empresa.confCom_confins / 100), 2);
                cssl = Decimal.Round(nota.not_totalbruto * (empresa.confCom_cssl / 100), 2);
                irrf = Decimal.Round(nota.not_totalbruto * (empresa.confCom_irrf / 100), 2);

                if (irrf > 10)
                {
                    nota.not_irrf = irrf;
                    nota.not_totalliquido = nota.not_totalliquido - irrf;
                }

                decimal totalTributos = pis + confins + cssl;
                if (totalTributos > 10)
                {
                    nota.not_pis = pis;
                    nota.not_confins = confins;
                    nota.not_cssl = cssl;

                    nota.not_totalliquido = nota.not_totalliquido - totalTributos;
                }
            }
            return nota;
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
                    retorno = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Convert.ToInt32(objParametroVencimento));
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
*/