using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form4 : Form

    {
        private string connectionString = "Server=OSA0716358W11-1\\SQLEXPRESS; Integrated Security=True; ";
        public Form4()
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
                SqlCommand cmd = new SqlCommand("USE MeuBanco SELECT NomeServiço, Preço, Descrição From Serviços", conn); //Cria um comando sql para selecionar os usuários da tabela.
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    listBox1.Items.Add(reader["NomeServiço"].ToString() + " - " + reader["Preço"].ToString() + " - " + reader["Descrição"].ToString());
                }
            }
        }



        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "USE MeuBanco INSERT INTO Serviços (NomeServiço, Preço, Descrição) VALUES (@NomeServiço, @Preço, @Descrição)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    decimal preco = Convert.ToDecimal(txtEmail.Text);

                    cmd.Parameters.AddWithValue("@NomeServiço", txtNome.Text);
                    cmd.Parameters.AddWithValue("@Preço", preco);
                    cmd.Parameters.AddWithValue("@Descrição", txtDesc.Text);
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
            string nomeSelecionado = listBox1.SelectedItems[0].ToString().Split('-')[0].Trim();
            //Obtem nome selecionado no listbox antes do traço
            txtNome.Text = nomeSelecionado;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"USE MeuBanco UPDATE Serviços 
                 SET NomeServiço = @NomeServiço, 
                     Preço = @Preço, 
                     Descrição = @Descrição
                 WHERE NomeServiço = @NomeAntigo";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (listBox1.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Selecione um para atualizar.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtNome.Text))
                        cmd.Parameters.AddWithValue("@NomeServiço", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@NomeServiço", txtNome.Text);


                    if (decimal.TryParse(txtEmail.Text, out decimal preco))
                        cmd.Parameters.AddWithValue("@Preço", preco);
                    else
                        cmd.Parameters.AddWithValue("@Preço", DBNull.Value);


                    if (string.IsNullOrWhiteSpace(txtDesc.Text))
                        cmd.Parameters.AddWithValue("@Descrição", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Descrição", txtDesc.Text);

                    cmd.Parameters.AddWithValue("@NomeAntigo", nomeSelecionado);

                    cmd.ExecuteNonQuery();


                }
                CarregarDados();
                LimparCampos();
            }
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
                string query = "USE MeuBanco DELETE FROM SERVIÇOS WHERE NomeServiço=@Nome";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (listBox1.SelectedItems.Count == 0)
                    {
                        MessageBox.Show("Selecione um para atualizar.");
                        return;
                    }
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

                    string query = @"USE MeuBanco SELECT NomeServiço, [Preço]
                 FROM Serviços
                 WHERE NomeServiço LIKE @NomeServiço OR [Preço] = @Preço";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NomeServiço", "%" + txtNome.Text + "%");

                        decimal preco;
                        decimal.TryParse(txtEmail.Text, out preco);
                        cmd.Parameters.AddWithValue("@Preço", preco);

                        listBox1.Items.Clear();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            listBox1.Items.Add(
                                reader["NomeServiço"].ToString() + " - " +
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
