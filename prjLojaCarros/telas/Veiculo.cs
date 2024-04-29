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
    public partial class Veiculo : Form
    {
        int registrosAtual = 0;
        int totalRegistros = 0;
        String connectionString = @"";
        bool novo;
        DataTable dtVeiculo = new DataTable();
        DataTable dtTipo=new DataTable();
        DataTable dtMarca = new DataTable();

        public Veiculo()
        {
            InitializeComponent();
        }
        private void navegar()
        {
            carregarComboMarca();
            carregarComboTipo();
            txtCodVeiculo.Text = dtVeiculo.Rows[registrosAtual][0].ToString();
            txtModelo.Text = dtVeiculo.Rows[registrosAtual][1].ToString();
            txtAno.Text = dtVeiculo.Rows[registrosAtual][2].ToString();
            cmbMarca.Text = dtVeiculo.Rows[registrosAtual][3].ToString();
            cmbTipo.Text = dtVeiculo.Rows[registrosAtual][4].ToString();
        }
        private void carregar()
        {
            dtVeiculo = new DataTable();
            string sql = "SELECT codVeiculo, modeloVeiculo, anoVeiculo, Marca, Tipo FROM Veiculoo v " +
                         "INNER JOIN Marca m ON m.codMarca = v.codMarca " +
                         "INNER JOIN Tipo t ON t.codTipo = v.codTipo";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            con.Open();
            try
            {
                using (reader = cmd.ExecuteReader())
                {
                    dtVeiculo.Load(reader);
                    totalRegistros = dtVeiculo.Rows.Count;
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
        private void carregarComboMarca()
        {
            dtMarca = new DataTable();
            string sql = "SELECT * FROM Marca";
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

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            finally { con.Close(); }
            cmbMarca.DataSource = dtMarca;
            cmbMarca.DisplayMember = "Marca";
            cmbMarca.ValueMember = "codMarca";

        }
        private void carregarComboTipo()
        {
            dtTipo = new DataTable();
            string sql = "SELECT * FROM Tipo";
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

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message);
            }
            finally { con.Close(); }
            cmbTipo.DataSource = dtTipo;
            cmbTipo.DisplayMember = "Tipo";
            cmbTipo.ValueMember = "codTipo";
        }

        private void Veiculo_Load(object sender, EventArgs e)
        {
            carregar();
            txtCodVeiculo.Enabled = false;
            txtModelo.Enabled = false;  
            txtAno.Enabled = false;
            cmbTipo.Enabled = false;
            cmbMarca.Enabled = false;
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            txtCodVeiculo.Text = "";
            txtAno.Text = "";
            btnProximo.Enabled = false;
            btnUltimo.Enabled = false;
            btnAnterior.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnPrimeiro.Enabled = false;
            novo = true;
            txtCodVeiculo.Enabled = false;
            txtModelo.Enabled = true;
            txtAno.Enabled = true;
            cmbTipo.Enabled = true;
            cmbMarca.Enabled = true;
            txtModelo.Focus();
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (novo)
            {
                string sql = "INSERT INTO Veiculoo(modeloVeiculo, anoVeiculo, codMarca, codTipo)" +
                    $"VALUES('{txtModelo.Text}','{txtAno.Text}','{cmbMarca.SelectedValue}','{cmbTipo.SelectedValue}')";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
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
              string sql = $"UPDATE Veiculoo SET modeloVeiculo='{txtModelo.Text}',anoVeiculo='{txtAno.Text}',codMarca={cmbMarca.SelectedValue},codTipo={cmbTipo.SelectedValue} WHERE codVeiculo={txtCodVeiculo.Text}";
                var con = new SqlConnection(connectionString);
                var cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Veiculo alterado com sucesso!!!");
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
            txtCodVeiculo.Enabled = false;
            txtModelo.Enabled = false;
            txtAno.Enabled = false;
            cmbTipo.Enabled = false;
            cmbMarca.Enabled = false;
            carregar();
            navegar();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            novo = false; 
            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnSalvar.Enabled = true;
            btnPrimeiro.Enabled = false;
            btnAnterior.Enabled = false; 
            btnUltimo.Enabled = false;
            txtCodVeiculo.Enabled = true;
            txtModelo.Enabled = true;
            txtAno.Enabled = true;
            cmbTipo.Enabled = true;
            cmbMarca.Enabled = true;
            
            
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            string sql = $"DELETE FROM Veiculoo WHERE" +
   $" codVeiculo={txtCodVeiculo.Text}";
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
        }
    }
}
