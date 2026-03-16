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

namespace WindowsFormsApp2
{
    public partial class Form5 : Form

    {
        private string connectionString = "Server=OSA0716358W11-1\\SQLEXPRESS; Integrated Security=True; ";

        public Form5()
        {
            InitializeComponent();
            CarregarDados();

        }
        private void CarregarDados()
        {
            listBox1.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); //Abre a conexão com o banco
                SqlCommand cmd = new SqlCommand("USE MeuBanco SELECT Nome, CNPJ_CPF, Telefone, Email From Fornecedor", conn); //Cria um comando sql para selecionar os usuários da tabela.
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["Nome"].ToString() + " - " + reader["CNPJ_CPF"].ToString() + " - " + reader["Telefone"].ToString() + " - " + reader["Email"].ToString());
                }
            }
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "USE MeuBanco INSERT INTO Fornecedor (Nome, CNPJ_CPF, Telefone, Email) VALUES (@Nome, @CNPJ_CPF, @Telefone, @Email)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {


                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@CNPJ_CPF", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEm.Text);
                    cmd.ExecuteNonQuery();
                }
                CarregarDados();
                LimparCampos();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }
        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            txtDesc.Clear();
            listBox1.ClearSelected();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um fornecedor para atualizar.");
                return;
            }
            string nomeSelecionado = listBox1.SelectedItems[0].ToString().Split('-')[0].Trim();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"USE MeuBanco UPDATE Fornecedor 
                 SET Nome = @Nome, 
                     CNPJ_CPF = @CNPJ, 
                     Telefone = @Telefone,
                     Email = @Email
                 WHERE Nome = @NomeAntigo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@CNPJ", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Telefone", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEm.Text);
                    cmd.Parameters.AddWithValue("@NomeAntigo", nomeSelecionado);
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    if (linhasAfetadas == 0)
                        MessageBox.Show("Nenhum fornecedor encontrado com esse nome.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }




            }
            CarregarDados();
            LimparCampos();
        }
    

        private void button4_Click(object sender, EventArgs e)
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
                string query = "USE MeuBanco DELETE FROM Fornecedor WHERE Nome=@Nome";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nomeSelecionado);
                    cmd.ExecuteNonQuery();

                }
                CarregarDados();
                LimparCampos();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                listBox1.Items.Clear();

                {
                    conn.Open();

                    string query = @"USE MeuBanco SELECT Nome, CNPJ_CPF, Telefone, Email
FROM Fornecedor
WHERE Nome LIKE @Nome
   OR CNPJ_CPF LIKE @CNPJ_CPF
   OR Telefone LIKE @Telefone
   OR Email LIKE @Email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", "%" + txtNome.Text + "%");
                        cmd.Parameters.AddWithValue("@CNPJ_CPF", "%" + txtNome.Text + "%");
                        cmd.Parameters.AddWithValue("@Telefone", "%" + txtNome.Text + "%");
                        cmd.Parameters.AddWithValue("@Email", "%" + txtNome.Text + "%");

                       

                        listBox1.Items.Clear();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            listBox1.Items.Add(
                                reader["Nome"].ToString() + " - " +
                                reader["CNPJ_CPF"].ToString() + " - " +
                                 reader["Telefone"].ToString() + " - " +
                                  reader["Email"].ToString()
                            );
                        }

                        reader.Close();
                    }
                }
            }
        }
    }
}
