namespace NFENotasFiscais.BE.NFEServicos
{
    public class NFeEnvioRetornoBE
    {
        public string NumeroLote { get; set; }
        public string DataRecebimento { get; set; }
        public string Protocolo { get; set; }
        public bool Erro { get; set; }
        public string ErroCodigo { get; set; }
        public string ErroMensagem { get; set; }
        public string ErroCorrecao { get; set; }
    }

    public class NFeConsultaRetorno
    {
        public string NumeroLote { get; set; }
        public string Situacao { get; set; }
    }
}
