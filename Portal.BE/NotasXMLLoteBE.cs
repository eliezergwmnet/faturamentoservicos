using System;
using System.Collections.Generic;

namespace Portal.BE
{
    public class NotasXMLLoteBE
    {
        public int notXml_id { get; set; }
        public string notXml_nome { get; set; }
        public DateTime notXml_data { get; set; }
        public string notXml_xml { get; set; }
        public List<NotasXMLLoteItensBE> Itens { get; set; }
    }
}
