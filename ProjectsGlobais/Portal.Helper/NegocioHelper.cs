using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Globais.Helper
{
    public class NegocioHelper
    {
        public bool ValidarEmail(string strEntrada) {
            Regex rg = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            if (rg.IsMatch(strEntrada))
                return true;
            else
                return false;
        }
        public static string FormataCampoSaidaValor(string strValor, int intQtdeCasas) {
            string strSaida = strValor;

            if (!string.IsNullOrEmpty(strSaida)) { strSaida = strSaida.Replace(",", "").Replace(".", "").Replace("/", "").Replace("-", ""); }
            strSaida = strSaida.PadLeft(intQtdeCasas, '0');
            // strSaida = strSaida.PadRight(strSaida, intQtdeCasas)
            return strSaida;
        }
        public static string FormataCampoSaidaInscricaoEstadual(string strTexto, int intQtdeCasas)
        {
            string strSaida = "";
            if (!string.IsNullOrEmpty(strTexto))
            {
                strSaida = strTexto;
                strSaida = strSaida.Replace(".", "");
                strSaida = strSaida.Replace("-", "");
            }
            strSaida = strSaida.PadRight(intQtdeCasas, ' ').ToUpper();
            return strSaida.Substring(0, intQtdeCasas);
        }


        public static string FormataCampoSaidaAlfanumerico(string strTexto, int intQtdeCasas)
        {
            strTexto = strTexto == null ? "" : strTexto;
            string strSaida = strTexto;
            strSaida = strSaida.PadRight(intQtdeCasas, ' ');
            return strSaida.Substring(0, intQtdeCasas);
        }

        public static string FormataCampoSaidaData(DateTime dteEntrada) {
            return dteEntrada.ToString("yyyyMMdd");
        }

    }
}
