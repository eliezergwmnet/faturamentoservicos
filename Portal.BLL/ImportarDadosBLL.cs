using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.BE;
using Portal.Dao;

namespace Portal.BLL
{
    public class ImportarDadosBLL
    {
        public void ImportarNotas()
        {
            ImportarDadosDao _i = new ImportarDadosDao();
            NotasBLL _n = new NotasBLL();
            ClienteBLL _c = new ClienteBLL();
            foreach (var item in _i.ImportarNotas())
            {
                var nota = item.a.Split('.');
                //===========================================
                //-----ADICIONAR O NOME DA EMPRESA--
                //===========================================
                var cliente = _c.SelectBuscaNomeFantasia(item.b, 1);


                NotasBE n = new NotasBE();
                n.not_numero = Convert.ToInt32(nota[0]);
                n.cli_id = cliente.cli_id;
                n.not_tipoReceita = item.c;
                n.not_codReceita = item.d;
                n.not_dtEmissao = Convert.ToDateTime(item.e);
                n.not_dtVencimento = Convert.ToDateTime(item.f);
                n.not_pis = Convert.ToDecimal(item.g);
                n.not_confins = Convert.ToDecimal(item.h);
                n.not_cssl = Convert.ToDecimal(item.i);
                n.not_irrf = Convert.ToDecimal(item.j);
                n.not_totalbruto = Convert.ToDecimal(item.k);
                n.not_totalliquido = Convert.ToDecimal(item.l);
                n.not_preenchida = item.m == "1" ? 1 : 0;
                n.not_emitida = item.n == "1" ? true : false;
                n.not_situacao = item.o;


                _n.Insert(n);
            }
        }
    }
}
