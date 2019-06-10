namespace Portal.BE
{
    /// <summary>
    /// Retorna da lista de notas pendentes
    /// </summary>
    public class NotasPendentesBE
    {
        public int not_numero { get; set; }
        public string notI_descricao { get; set; }
        public double notI_total { get; set; }
        public double not_totalbruto { get; set; }
        public double not_totalliquido { get; set; }
    }
    public class NotasPendentesChaveBE: NotasPendentesBE
    {
        public string strChaveCodificacaoNotaFiscal { get; set; }
    }

}
