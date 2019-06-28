using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HelpersSistema.Models.Base;
using Portal.Importacao.Class;
using Portal.Importacao.Model;

namespace Portal.Importacao
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //new ClienteImport().CarregaDados();
            //new ServicosImport().CarregaDados();
            new ContratoImport().CarregaDados();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregaLista();
        }
        private void CarregaLista()
        {
            var lista = new TipoCampoSQL().TipoCamposSQLLista();
            for (var i = 0; i<lista.Count; i++)
            {
                if (lista[i].temtamanho)
                    lista[i].name = lista[i].name + "(" + lista[i].tamanhodefault + ")";
            }
            cboTipoCampo.DataSource = lista;
            cboTipoCampo.DisplayMember = "name";
            cboTipoCampo.ValueMember = "name";
        }
        private void btnAdicionar_Click(object sender, EventArgs e)
        {

        }
    }
}
