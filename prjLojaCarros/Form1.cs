using prjLojaCarros.telas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLojaCarros
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var marca =new frmMarca();
            marca.Show();
        }

        private void veiculoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var veiculo = new Veiculo();
            veiculo.Show();
        }

        private void tipoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tipo=new frmTipo();
            tipo.Show();
        }
    }
}
