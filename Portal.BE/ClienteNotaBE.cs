namespace Portal.BE
{
    public class ClienteNotaBE
    {
        public ClienteNotaBE() { }
        public ClienteNotaBE(int _cli_id, string _cliNot_descricao, decimal _cliNot_valor, int? _cliNot_parcela, bool _cliNot_faturado, bool _cliNot_ativo, int _servCli_id, int _cliNot_contrato, string _cliNot_contratonome)
        {
            this.cli_id = _cli_id;
            this.cliNot_descricao = _cliNot_descricao;
            this.cliNot_valor = _cliNot_valor;
            this.cliNot_parcela = _cliNot_parcela;
            this.cliNot_faturado = _cliNot_faturado;
            this.cliNot_ativo = _cliNot_ativo;
            this.servCli_id = _servCli_id;
            this.cliNot_contrato = _cliNot_contrato;
            this.cliNot_contratonome = _cliNot_contratonome;
        }

        public int cliNot_id { get; set; }
        public int cli_id { get; set; }
        public string cliNot_descricao { get; set; }
        public decimal cliNot_valor { get; set; }
        public int? cliNot_parcela { get; set; }
        public bool cliNot_faturado { get; set; }
        public bool cliNot_ativo { get; set; }
        public int servCli_id { get; set; }
        public int cliNot_contrato { get; set; }
        public string cliNot_contratonome { get; set; }

    }
}
