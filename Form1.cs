using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace desenvolvimento_interface
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            txtNome.Text = "NOME";
            txtNome.ForeColor = Color.Gray;
            txtNome.TextAlign = HorizontalAlignment.Center;

            txtSenha.Text = "SENHA";
            txtSenha.ForeColor = Color.Gray;
            txtSenha.TextAlign = HorizontalAlignment.Center;

        }

        private void txtNome_Enter_1(object sender, EventArgs e)
        {
            if (txtNome.Text == "NOME")
            {
                txtNome.Text = "";
                txtNome.ForeColor = Color.Black;
            }
        }

        private void txtNome_Leave_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                txtNome.Text = "NOME";
                txtNome.ForeColor = Color.Gray;
            }
        }

        private void txtSenha_Enter(object sender, EventArgs e)
        {
            if (txtSenha.Text == "SENHA")
            {
                txtSenha.Text = "";
                txtSenha.ForeColor = Color.Black;
            }
          
        }

        private void txtSenha_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                txtSenha.Text = "SENHA";
                txtSenha.ForeColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtSenha.Text == "1234" && txtNome.Text == "ADMIN")
            {
                MessageBox.Show("Bem Vindo!");
            }
            else
            {
                MessageBox.Show("Voce não é autorizado");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    


}

