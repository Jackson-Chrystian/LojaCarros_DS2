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
    public partial class frmMarca : Form
    {
        int registrosAtual = 0;
        int totalRegistros = 0;
        String connectionString = @"";
        bool novo;
        DataTable dtMarca = new DataTable();
        public frmMarca()
        {
            InitializeComponent();
        }
        private void navegar()
        {
            txtCodMarca.Text = dtMarca.Rows[registrosAtual][0].ToString();
            txtMarca.Text = dtMarca.Rows[registrosAtual][1].ToString();
        }
        private void carregar()
        {
            dtMarca = new DataTable();
            string sql = "SELECT *FROM Marca";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtMarca.Load(reader);
                    totalRegistros = dtMarca.Rows.Count;
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

        private void frmMarca_Load(object sender, EventArgs e)
        {
            carregar();
            btnSalvar.Enabled = false;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                string sql = "INSERT INTO Marca(Marca)" +
                    $"VALUES('{txtMarca.Text}')";
                // MessageBox.Show(sql);
                var con = new SqlConnection(connectionString);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Marca cadastrda com sucesso");
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
                string sql = $"UPDATE Marca SET Marca='{txtMarca.Text}' WHERE codMarca={txtCodMarca.Text}";
                var con = new SqlConnection(connectionString);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Produtora alterada com sucesso!!!");
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
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
            carregar();
        }
        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false;
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = true;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false;
            btnUltimo.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            txtMarca.Text = "";
            txtCodMarca.Text = "";
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = true;
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            btnAnterior.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            novo = true;
            txtMarca.Focus();
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM Marca WHERE" +
               $" codMarca={txtCodMarca.Text}";
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
            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;
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

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (registrosAtual > 0)
            {
                registrosAtual--;
                navegar();
            }
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            if (registrosAtual > 0)
            {
                registrosAtual = 0;
                navegar();
            }
        }
    }
}
