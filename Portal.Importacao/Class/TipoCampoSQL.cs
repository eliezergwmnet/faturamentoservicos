using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Importacao.Class
{
    public class TipoCampoSQL
    {
        public string name { get; set; }
        public bool temtamanho { get; set; }
        public string tamanhodefault { get; set; }

        public TipoCampoSQL()
        {

        }
        public List<TipoCampoSQL> TipoCamposSQLLista()
        {
            var result = new List<TipoCampoSQL>();
            result.Add(new TipoCampoSQL { name = "bigint", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "binary", temtamanho = false, tamanhodefault = "50" });
            result.Add(new TipoCampoSQL { name = "bit", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "char(10)", temtamanho = false, tamanhodefault = "10" });
            result.Add(new TipoCampoSQL { name = "datetime", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "decimal", temtamanho = false, tamanhodefault = "18, 0" });
            result.Add(new TipoCampoSQL { name = "float", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "image", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "int", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "money", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "nchar", temtamanho = false, tamanhodefault = "10" });
            result.Add(new TipoCampoSQL { name = "ntext", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "numeric", temtamanho = false, tamanhodefault = "18, 0" });
            result.Add(new TipoCampoSQL { name = "nvarchar", temtamanho = false, tamanhodefault = "50" });
            result.Add(new TipoCampoSQL { name = "nvarchar", temtamanho = false, tamanhodefault = "MAX" });
            result.Add(new TipoCampoSQL { name = "real", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "smalldatetime", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "smallint", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "smallmoney", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "sql_variant", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "text", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "timestamp", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "tinyint", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "uniqueidentifier", temtamanho = false, tamanhodefault = "0" });
            result.Add(new TipoCampoSQL { name = "varbinary", temtamanho = false, tamanhodefault = "50" });
            result.Add(new TipoCampoSQL { name = "varbinary", temtamanho = false, tamanhodefault = "MAX" });
            result.Add(new TipoCampoSQL { name = "varchar", temtamanho = false, tamanhodefault = "50" });
            result.Add(new TipoCampoSQL { name = "varchar", temtamanho = false, tamanhodefault = "MAX" });
            result.Add(new TipoCampoSQL { name = "xml", temtamanho = false, tamanhodefault = "0" });
            return result;
        }
    }

}
