using System.Data.Common;
using System.Data.OleDb;
using System.IO;

namespace Portal.ImportaAccess.Dados
{
    public class Conection
    {
        public static DbDataReader Select(string query)
        {
            //var urlBase = Common.GetCaminhoAplicacao("BaseAccess");

            string urlBase = Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "\\BaseAccess");
            OleDbConnection aConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + urlBase + "\\Faturamento_GWMNET.mdb");
            OleDbCommand aCommand = new OleDbCommand(query, aConnection);
            try
            {
                aConnection.Open();
                OleDbDataReader aReader = aCommand.ExecuteReader();
                return aReader;
               
            }
            catch (OleDbException e)
            {
                return null;
            }
        }
    }
}
