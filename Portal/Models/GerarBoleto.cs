/*using System;
using System.Globalization;
using BoletoNet;
using Globais.BLL;
using Portal.BE;
using Portal.BLL;

namespace Portal.Models
{
    public enum Bancos
    {
        BancodoBrasil = 1,
        Banrisul = 41,
        Basa = 3,
        Bradesco = 237,
        BRB = 70,
        Caixa = 104,
        HSBC = 399,
        Itau = 341,
        Real = 356,
        Safra = 422,
        Santander = 33,
        Sicoob = 756,
        Sicred = 748,
        Sudameris = 347,
        Unibanco = 409,
        Semear = 743
    }

    public class GerarBoleto
    {
        public GerarBoleto(int Banco)
        {
            boletoBancario = new BoletoBancario();
            boletoBancario.CodigoBanco = (short)Banco;
        }

        public BoletoBancario boletoBancario { get; set; }

        public BoletoBancario BancodoBrasil()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);


            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "1", "123456", "1");
            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01m, "16", "09876543210", c);

            b.NumeroDocumento = "12415487";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //Adiciona as instruções ao boleto
            #region Instruções
            //Protestar
            Instrucao_BancoBrasil item = new Instrucao_BancoBrasil(9, 5);
            b.Instrucoes.Add(item);
            //ImportanciaporDiaDesconto
            item = new Instrucao_BancoBrasil(30, 0);
            b.Instrucoes.Add(item);
            //ProtestarAposNDiasCorridos
            item = new Instrucao_BancoBrasil(81, 15);
            b.Instrucoes.Add(item);
            #endregion Instruções

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            boletoBancario.RemoveSimboloMoedaValorDocumento = true;
            boletoBancario.AjustaTamanhoFonte(12, 10, 14, 14);

            return boletoBancario;
        }

        public BoletoBancario Banrisul()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "12345678", "9");

            c.Codigo = "00000000504";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 45.50m, "18", "12345678901", c);

            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "DF";

            //Adiciona as instruções ao boleto
            #region Instruções
            //Protestar
            Instrucao_Banrisul item = new Instrucao_Banrisul(9, 10, 0);
            b.Instrucoes.Add(item);
            #endregion Instruções

            b.NumeroDocumento = "12345678901";


            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Basa()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "12345678", "9");

            c.Codigo = "12548";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 45.50m, "CNR", "125478", c);

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";


            b.NumeroDocumento = "12345678901";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            boletoBancario.Cedente.Endereco = new Endereco()
            {
                End = "Endereço do Cedente",
                Bairro = "Bairro",
                Cidade = "Cidade",
                CEP = "70000000",
                UF = "DF"

            };

            //boletoBancario.MostrarEnderecoCedente = true;

            //boletoBancario.AjustaTamanhoFonte(12, tamanhoFonteInstrucaoImpressao: 14);
            boletoBancario.RemoveSimboloMoedaValorDocumento = false;

            return boletoBancario;
        }

        public BoletoBancario Bradesco()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Instrucao_Bradesco item = new Instrucao_Bradesco(9, 5);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "1234", "5", "123456", "7");
            c.Codigo = "13000";


            //Carteiras 
            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1.00m, "09", "01000000001", c);
            b.ValorMulta = 0.10m;
            b.ValorCobrado = 1.10m;
            b.NumeroDocumento = "01000000001";
            b.DataVencimento = new DateTime(2015, 09, 12);

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            item.Descricao += " após " + item.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item); //"Não Receber após o vencimento");

            Instrucao i = new Instrucao(237);
            i.Descricao = "Nova Instrução";
            b.Instrucoes.Add(i);

            /* 
             * A data de vencimento não é usada
             * Usado para mostrar no lugar da data de vencimento o termo "Contra Apresentação";
             * Usado na carteira 06
             */
             /*
            boletoBancario.MostrarContraApresentacaoNaDataVencimento = true;

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario BRB()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "208", "", "010357", "6");
            c.Codigo = "13000";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.01m, "COB", "119964", c);
            b.NumeroDocumento = "119964";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Caixa()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("000.000.000-00", "Boleto.net ILTDA", "1234", "12345678", "9");

            c.Codigo = "112233";


            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 20.00m, "2", "0123456789", c);

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //Adiciona as instruções ao boleto
            #region Instruções
            Instrucao_Caixa item;
            //ImportanciaporDiaDesconto
            item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.Multa, Convert.ToDecimal(0.40));
            b.Instrucoes.Add(item);
            item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.JurosdeMora, Convert.ToDecimal(0.01));
            b.Instrucoes.Add(item);
            item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.NaoReceberAposNDias, 90);
            b.Instrucoes.Add(item);
            #endregion Instruções

            EspecieDocumento_Caixa espDocCaixa = new EspecieDocumento_Caixa();
            b.EspecieDocumento = new EspecieDocumento_Caixa(espDocCaixa.getCodigoEspecieByEnum(EnumEspecieDocumento_Caixa.DuplicataMercantil));
            b.NumeroDocumento = "00001";
            b.DataProcessamento = DateTime.Now;
            b.DataDocumento = DateTime.Now;

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario HSBC()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Minha empresa", "0000", "", "00000", "00");
            // Código fornecido pela agencia, NÃO é o numero da conta
            c.Codigo = "0000000"; // 7 posicoes

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 2, "CNR", "1330001490684", c); //cod documento
            b.NumeroDocumento = "9999999999999"; // nr documento

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Itau()
        {
            DateTime vencimento = DateTime.Now.AddDays(1);

            Instrucao_Itau item1 = new Instrucao_Itau(9, 5);
            Instrucao_Itau item2 = new Instrucao_Itau(81, 10);
            Cedente c = new Cedente("10.823.650/0001-90", "SAFIRALIFE", "4406", "22324");
            //Na carteira 198 o código do Cedente é a conta bancária
            c.Codigo = "13000";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.1m, "176", "00000001", c, new EspecieDocumento(341, "1"));
            b.NumeroDocumento = "00000001";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "2541245"));

            item2.Descricao += " " + item2.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);
            b.Instrucoes.Add(item2);

            // juros/descontos

            if (b.ValorDesconto == 0)
            {
                Instrucao_Itau item3 = new Instrucao_Itau(999, 1);
                item3.Descricao += ("1,00 por dia de antecipação.");
                b.Instrucoes.Add(item3);
            }

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Real()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Coloque a Razão Social da sua empresa aqui", "1234", "12345");
            c.Codigo = "12345";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.1m, "57", "123456", c, new EspecieDocumento(356, "9"));
            b.NumeroDocumento = "1234567";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Real");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");
            boletoBancario.Boleto = b;

            EspeciesDocumento ed = EspecieDocumento_Real.CarregaTodas();
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Safra()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0542", "5413000");

            c.Codigo = "13000";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1642, "198", "02592082835", c);
            b.NumeroDocumento = "1008073";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Bradesco");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");

            Instrucao_Safra instrucao = new Instrucao_Safra();
            instrucao.Descricao = "Instrução 1";

            b.Instrucoes.Add(instrucao);


            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Santander(ClienteBE obj)
        {
            if (obj.Nota != null)
            {
                var empresa = new EmpresaBLL().SelectId(new EmpresaBE());

                Cedente c = new Cedente(empresa.conf_cnpj, empresa.conf_razaosocial, empresa.conf_agencia, empresa.conf_agenciadiito, empresa.conf_conta, empresa.conf_digito);
                c.Codigo = "3247120";// obj.Nota.not_id.ToString();

                BoletoNet.Boleto b = new BoletoNet.Boleto(obj.Nota.not_dtVencimento, obj.Nota.not_totalliquido, "101", obj.Nota.not_numero.ToString(), c);


                var juros = string.Format(new CultureInfo("pt-BR", false), "{0:c}", (obj.Nota.not_totalliquido * Convert.ToDecimal(0.02)));
                var varjuros = string.Format(new CultureInfo("pt-BR", false), "{0:c}", ((obj.Nota.not_totalliquido / 30) * Convert.ToDecimal(0.01)));

                var inst = new GlobaisConfiguracaoBLL().SelectInstrucaoBoleto("");
                if (inst.Count > 0)
                {
                    foreach (var item in inst)
                    {
                        item.Con_IdItem = item.Con_IdItem.Replace("//NUMERONOTA//", obj.Nota.not_numero.ToString());
                        item.Con_IdItem = item.Con_IdItem.Replace("//RAZAOSOCIAL//", obj.cli_razaoSocial);
                        item.Con_IdItem = item.Con_IdItem.Replace("//VALORJUROS//", juros);
                        item.Con_IdItem = item.Con_IdItem.Replace("//JUROS//", varjuros);
                        b.Instrucoes.Add(new Instrucao_Santander { Descricao = item.Con_IdItem });
                    }
                }

                b.NumeroDocumento = obj.Nota.not_numero.ToString().PadLeft(6, '0');
                b.Sacado = new Sacado(obj.cli_CGC, obj.cli_razaoSocial);
                if (obj.Endereco != null && obj.Endereco.Count > 0)
                {
                    b.Sacado.Endereco.End = obj.Endereco[0].end_logradouro + " " + obj.Endereco[0].end_numero + " " + obj.Endereco[0].end_complemento;
                    b.Sacado.Endereco.Bairro = obj.Endereco[0].end_bairro;
                    b.Sacado.Endereco.Cidade = obj.Endereco[0].end_cidade;
                    b.Sacado.Endereco.CEP = obj.Endereco[0].end_cep;
                    b.Sacado.Endereco.UF = obj.Endereco[0].end_estado;
                }

                b.EspecieDocumento = new EspecieDocumento_Santander("6");

                boletoBancario.Boleto = b;
                boletoBancario.MostrarCodigoCarteira = true;

                boletoBancario.Boleto.Valida();

                return boletoBancario;
            }
            else
                return null;
        }

        public BoletoBancario Santander()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "GwmnetNet", "3247", "", "324712", "0");
            //string cpfcnpj, string nome, string agencia, string digitoAgencia, string conta, string digitoConta
            c.Codigo = "9848";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.20m, "101", "9848", c);

            //NOSSO NÚMERO
            //############################################################################################################################
            //Número adotado e controlado pelo Cliente, para identificar o título de cobrança.
            //Informação utilizada pelos Bancos para referenciar a identificação do documento objeto de cobrança.
            //Poderá conter número da duplicata, no caso de cobrança de duplicatas, número de apólice, no caso de cobrança de seguros, etc.
            //Esse campo é devolvido no arquivo retorno.
            b.NumeroDocumento = "9848";

            b.Sacado = new Sacado("000.000.000-00", "Fulano de Silva");
            b.Sacado.Endereco.End = "SSS 154 Bloco J Casa 23";
            b.Sacado.Endereco.Bairro = "Testando";
            b.Sacado.Endereco.Cidade = "Testelândia";
            b.Sacado.Endereco.CEP = "70000000";
            b.Sacado.Endereco.UF = "DF";

            //Espécie Documento - [R] Recibo
            b.EspecieDocumento = new EspecieDocumento_Santander("17");

            boletoBancario.Boleto = b;
            boletoBancario.MostrarCodigoCarteira = true;

            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Sicoob()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);


            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "4444", "", "", "");

            c.Codigo = "123456";
            c.DigitoCedente = 7;
            c.Carteira = "1";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 10, "1", "897654321", c);
            b.NumeroDocumento = "119964";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Sicred()
        {

            DateTime vencimento = DateTime.Now.AddDays(1);

            Instrucao_Sicredi item1 = new Instrucao_Sicredi(9, 5);
            Instrucao_Sicredi item2 = new Instrucao_Sicredi();

            Cedente c = new Cedente("10.823.650/0001-90", "SAFIRALIFE", "0811", "81111");
            c.Codigo = "08111081111";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 0.1m, "1", "00000001", c);
            b.NumeroDocumento = "00000001";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            // Exemplo de como adicionar mais informações ao sacado
            b.Sacado.InformacoesSacado.Add(new InfoSacado("TÍTULO: " + "2541245"));

            item2.Descricao += " " + item1.QuantidadeDias.ToString() + " dias corridos do vencimento.";
            b.Instrucoes.Add(item1);



            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Sudameris()
        {
            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Empresa de Atacado", "0501", "6703255");

            c.Codigo = "13000";

            //Nosso número com 7 dígitos
            string nn = "0003020";
            //Nosso número com 13 dígitos
            //nn = "0000000003025";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1642, "198", nn, c);// EnumEspecieDocumento_Sudameris.DuplicataMercantil);
            b.NumeroDocumento = "1008073";

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            //b.Instrucoes.Add("Não Receber após o vencimento");
            //b.Instrucoes.Add("Após o Vencimento pague somente no Sudameris");
            //b.Instrucoes.Add("Instrução 2");
            //b.Instrucoes.Add("Instrução 3");

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Unibanco()
        {
            // ----------------------------------------------------------------------------------------
            // Exemplo 1

            //DateTime vencimento = new DateTime(2001, 12, 31);

            //Cedente c = new Cedente("00.000.000/0000-00", "Next Consultoria", "1234", "5", "123456", "7");
            //c.Codigo = 123456;

            //BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 1000.00, "20", "1803029901", c);
            //b.NumeroDocumento = b.NossoNumero;

            // ----------------------------------------------------------------------------------------
            // Exemplo 2

            DateTime vencimento = DateTime.Now.AddDays(10);

            Cedente c = new Cedente("00.000.000/0000-00", "Next Consultoria Ltda.", "0123", "100618", "9");
            c.Codigo = "203167";

            BoletoNet.Boleto b = new BoletoNet.Boleto(vencimento, 2952.95m, "20", "1803029901", c);
            b.NumeroDocumento = b.NossoNumero;

            // ----------------------------------------------------------------------------------------

            //b.Instrucoes.Add("Não receber após o vencimento");
            //b.Instrucoes.Add("Após o vencimento pague somente no Unibanco");
            //b.Instrucoes.Add("Taxa bancária - R$ 2,95");
            //b.Instrucoes.Add("Emitido pelo componente Boleto.NET");

            // ----------------------------------------------------------------------------------------

            b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
            b.Sacado.Endereco.End = "Endereço do seu Cliente ";
            b.Sacado.Endereco.Bairro = "Bairro";
            b.Sacado.Endereco.Cidade = "Cidade";
            b.Sacado.Endereco.CEP = "00000000";
            b.Sacado.Endereco.UF = "UF";

            boletoBancario.Boleto = b;
            boletoBancario.Boleto.Valida();

            return boletoBancario;
        }

        public BoletoBancario Semear()
        {
            var boleto = new BoletoNet.Boleto();

            boleto.Cedente = new Cedente()
            {
                Codigo = "743",
                MostrarCNPJnoBoleto = true,
                Nome = "BANCO SEMEAR",
                CPFCNPJ = "65825481000110",
                Carteira = "2",
                DigCedente = "9",
                ContaBancaria = new ContaBancaria()
                {
                    Agencia = "001",
                    DigitoAgencia = "0",
                    Conta = "65456",
                    DigitoConta = "5"
                },
                Endereco = new Endereco()
            };

            boleto.LocalPagamento = "Este boleto poderá ser pago em toda a Rede Bancária até 5 dias após o vencimento. ";
            boleto.Instrucoes.Add(new Instrucao_Bradesco()
            {
                Descricao = "Não receber após o vencimento",
                QuantidadeDias = 3
            });

            boleto.ValorBoleto = 251.51M;
            boleto.ValorCobrado = 251.51M;
            boleto.NossoNumero = "35148373401";
            boleto.NumeroDocumento = "051483734";
            boleto.DataVencimento = new DateTime(2017, 12, 4);
            boleto.DataProcessamento = DateTime.Now;
            boleto.DataDocumento = DateTime.Now;
            boleto.Carteira = "03";

            boleto.Sacado = new Sacado()
            {
                CPFCNPJ = "05461883893",
                Nome = "Joãozinho Testador",
                Endereco = new Endereco()
                {
                    Complemento = "Bla bla",
                    Numero = "80",
                    Bairro = "",
                    CEP = "32310535",
                    Cidade = "Contagem",
                    UF = "Minas Gerais",
                }
            };

            boleto.CodigoBarra.CodigoBanco = "743";
            boleto.CodigoBarra.Moeda = 9;
            boleto.CodigoBarra.CampoLivre = "0001023514837340110996818";
            boleto.CodigoBarra.ValorDocumento = "0000025151";
            boleto.CodigoBarra.FatorVencimento = 7363;

            var linhaDigitavel = boleto.CodigoBarra.LinhaDigitavelFormatada;
            boleto.CodigoBarra.Codigo = boleto.CodigoBarra.LinhaDigitavelFormatada.Trim().Replace(".", string.Empty).Replace(" ", string.Empty);

            var boletoBancario = new BoletoBancario
            {
                CodigoBanco = 743,
                Boleto = boleto,
                MostrarEnderecoCedente = true,
                MostrarContraApresentacaoNaDataVencimento = false,
                GerarArquivoRemessa = true
            };

            return boletoBancario;
        }
    }
}*/