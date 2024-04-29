using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjLojaCarros.telas
{
    public partial class frmTipo : Form
    {
        int registrosAtual = 0;
        int totalRegistros = 0;
        String connectionString = @"";
        bool novo;
        DataTable dtTipo = new DataTable();
        public frmTipo()
        {
            InitializeComponent();
        }
        private void navegar()
        {
            txtCodTipo.Text = dtTipo.Rows[registrosAtual][0].ToString();
            txtTipo.Text = dtTipo.Rows[registrosAtual][1].ToString();
        }
        private void carregar()
        {
            dtTipo = new DataTable();
            string sql = "SELECT *FROM Tipo";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtTipo.Load(reader);
                    totalRegistros = dtTipo.Rows.Count;
                    registrosAtual = 0;
                    navegar();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally { con.Close(); }

        }

        private void frmTipo_Load(object sender, EventArgs e)
        {
            carregar();
            txtCodTipo.Enabled= false;
            txtTipo.Enabled= false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registrosAtual > 0)
            {
                registrosAtual = 0;
                navegar();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (registrosAtual > 0)
            {
                registrosAtual--;
                navegar();
            }
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            if (registrosAtual < totalRegistros - 1)
            {
                registrosAtual++;
                navegar();
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            if (registrosAtual < totalRegistros - 1)
            {
                registrosAtual = totalRegistros - 1;
                navegar();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                string sql = "INSERT INTO Tipo(Tipo)" +
                    $"VALUES('{txtTipo.Text}')";
                var con = new SqlConnection(connectionString);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("cadastrda com sucesso");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {
                    con.Close();
                }
             
            }
            else
            {
                string sql = $"UPDATE Tipo SET Tipo='{txtTipo.Text}' WHERE codTipo={txtCodTipo.Text}";
                var con = new SqlConnection(connectionString);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Tipo alterado com sucesso!!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro :" + ex.ToString());
                }
                finally
                {
                    con.Close();
                }

            }
            btnPrimeiro.Enabled = true;
            btnProximo.Enabled = true;
            btnUltimo.Enabled = true;
            btnAnterior.Enabled = true;
            btnAlterar.Enabled = true;
            btnExcluir.Enabled = true;
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            txtCodTipo.Enabled = false;
            txtTipo.Enabled = false;
            carregar();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            txtTipo.Enabled = true;
            txtCodTipo.Enabled = false;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnUltimo.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            txtCodTipo.Text = "";
            txtTipo.Text = "";
            txtTipo.Enabled = true;
            txtCodTipo.Enabled = false;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            btnAnterior.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            novo = true;
            txtTipo.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM Tipo WHERE" +
   $" codTipo={txtCodTipo.Text}";
            var con = new SqlConnection(connectionString);
            var cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Excluido com sucesso");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro :" + ex.ToString());
            }
            finally { con.Close(); }
            carregar();
            txtCodTipo.Enabled = false;
            txtTipo.Enabled = false;
        }
    }
}
