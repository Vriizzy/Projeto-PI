using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private string connectionString = "Server=OSA0716358W11-1\\SQLEXPRESS; Integrated Security=True; ";
        public Form1()
        {
            InitializeComponent();
            CarregarDados(); //Chama  o método de carregar os dados e abrir no formulário
        }
        private void CarregarDados() {
            listBox1.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); //Abre a conexão com o banco
                SqlCommand cmd = new SqlCommand("USE MeuBanco SELECT Nome, Email From Usuarios", conn); //Cria um comando sql para selecionar os usuários da tabela.
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["Nome"].ToString() + " - " + reader["Email"].ToString());
                }
            }
           

        } // Carregar os dados do banco no listbox

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos(); //Chama o metodo para limpar os campos de entrada.
        }
        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            listBox1.ClearSelected();

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            // using (SqlConnection conn = new SqlConnection())
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "USE MeuBanco INSERT INTO Usuarios (Nome, Email) VALUES (@Nome, @Email)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.ExecuteNonQuery();
                }
                CarregarDados();
                LimparCampos();
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            string nomeSelecionado = listBox1.SelectedItems[0].ToString().Split('-')[0].Trim();
            //Obtem nome selecionado no listbox antes do traço
            txtNome.Text = nomeSelecionado;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "USE MeuBanco UPDATE Usuarios SET Nome = @Nome, Email = @Email Where Nome=@NomeAntigo";
                using (SqlCommand cmd = new SqlCommand(query, conn))

                {
                    if (listBox1.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Selecione um para atualizar.");
                        return;
                    }
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@NomeAntigo", nomeSelecionado);
                    cmd.ExecuteNonQuery();
                }
                CarregarDados();
                LimparCampos();
            }
           
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um para atualizar.");
                return;
            }
            if (listBox1.SelectedItems == null) { return; }
            string nomeSelecionado = listBox1.SelectedItem.ToString().Split('-')[0].Trim();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "USE MeuBanco DELETE FROM Usuarios WHERE Nome=@Nome";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nomeSelecionado);
                    cmd.ExecuteNonQuery();

                }
                CarregarDados();
                LimparCampos();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "Use MeuBanco SELECT Nome, Email FROM Usuarios WHERE Nome LIKE @Nome";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", "%" + txtNome.Text + "%");
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        listBox1.Items.Add(reader["Nome"].ToString() + " - " + reader["Email"].ToString());
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
