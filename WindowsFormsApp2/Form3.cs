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
    public partial class Form3 : Form
    {
        private string connectionString = "Server=OSA0716358W11-1\\SQLEXPRESS; Integrated Security=True; ";

        public Form3()
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
                SqlCommand cmd = new SqlCommand("USE MeuBanco SELECT Nome, Preço, Descrição From PRODUTO", conn); //Cria um comando sql para selecionar os usuários da tabela.
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["Nome"].ToString() + " - " + reader["Preço"].ToString() + " - " + reader["Descrição"].ToString());
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            // using (SqlConnection conn = new SqlConnection())
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "USE MeuBanco INSERT INTO PRODUTO (Nome, Preço, Descrição) VALUES (@Nome, @Preço, @Descrição)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    decimal preco = Convert.ToDecimal(txtEmail.Text);

                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Preço", preco);
                    cmd.Parameters.AddWithValue("@Descrição", txtDesc.Text);
                    cmd.ExecuteNonQuery();
                }
                CarregarDados();
                LimparCampos();
            }
        }
        private void LimparCampos()
        {
            txtNome.Clear();
            txtEmail.Clear();
            listBox1.ClearSelected();

        }
        private void button5_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void label3_Click(object sender, EventArgs e)
        {

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
                string query = "USE MeuBanco DELETE FROM Produto WHERE Nome=@Nome";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nome", nomeSelecionado);
                    cmd.ExecuteNonQuery();

                }
                CarregarDados();
                LimparCampos();
            }
        }

        private void button3_Click(object sender, EventArgs e)

        {
            if (listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um item para atualizar.");
                return; // sai do método
            } else { 
                string nomeSelecionado = listBox1.SelectedItems[0].ToString().Split('-')[0].Trim();
            //Obtem nome selecionado no listbox antes do traço
            txtNome.Text = nomeSelecionado;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"USE MeuBanco UPDATE Produto 
                 SET Nome = @Nome, 
                     Preço = @Preço, 
                     Descrição = @Descrição
                 WHERE Nome = @NomeAntigo";
                using (SqlCommand cmd = new SqlCommand(query, conn))

                {
                        if (listBox1.SelectedItems.Count == 0)
                        {
                            MessageBox.Show("Selecione um para atualizar.");
                            return;
                        }
                        decimal preco = Convert.ToDecimal(txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Preço", preco);
                    cmd.Parameters.AddWithValue("@Descrição", txtDesc.Text);
                    cmd.Parameters.AddWithValue("@NomeAntigo", nomeSelecionado);
                    cmd.ExecuteNonQuery();
                }
                CarregarDados();
                LimparCampos();
            }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
 
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                listBox1.Items.Clear();
          
                {
                    conn.Open();
                    //string query = "Use MeuBanco SELECT Nome, Preço FROM Produto WHERE Nome LIKE @Nome OR Preço = @Preço";
                    string query = @"USE MeuBanco SELECT Nome, [Preço]
                 FROM Produto
                 WHERE Nome LIKE @Nome OR [Preço] = @Preço";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", "%" + txtNome.Text + "%");

                        decimal preco;
                        decimal.TryParse(txtEmail.Text, out preco);
                        cmd.Parameters.AddWithValue("@Preço", preco);

                        listBox1.Items.Clear();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            listBox1.Items.Add(
                                reader["Nome"].ToString() + " - " +
                                reader["Preço"].ToString()
                            );
                        }

                        reader.Close();
                    }
                }
            }
            }
        }
    }

