namespace NFENotasFiscais.Helper
{
    public static class GlobaisNFENotasFiscais
    {
        public const string ConectionDataBaseGlobal = "SERVERHOSTGLOBAIS";

        /// <summary>
        /// Define o tempo de cada consulta ate obter respota do lote
        /// </summary>
        public const int TempoVerificacaoNotasLote = 3000;
        /// <summary>
        /// Define a quantidade de verificações padrão do lote
        /// </summary>
        public const int QuantidadeVerificacaoNotasLote = 3000;
    }
}
