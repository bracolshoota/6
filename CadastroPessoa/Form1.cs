using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CadastroPessoa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validação
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Preencha o Nome.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !EmailValido(txtEmail.Text))
            {
                MessageBox.Show("Informe um Email válido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Preencha o Telefone.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefone.Focus();
                return;
            }

            if (cmbGenero.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o Gênero.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGenero.Focus();
                return;
            }

            // Formato bonito para o arquivo
            string dados = "========================================" + Environment.NewLine +
                           "CADASTRO REALIZADO EM: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + Environment.NewLine +
                           "----------------------------------------" + Environment.NewLine +
                           "Nome:     " + txtNome.Text.Trim() + Environment.NewLine +
                           "Email:    " + txtEmail.Text.Trim() + Environment.NewLine +
                           "Telefone: " + txtTelefone.Text.Trim() + Environment.NewLine +
                           "Gênero:   " + cmbGenero.SelectedItem.ToString() + Environment.NewLine +
                           "========================================" + Environment.NewLine + Environment.NewLine;

            try
            {
                string caminho = Path.Combine(Application.StartupPath, "cadastros.txt");
                File.AppendAllText(caminho, dados);

                MessageBox.Show("Dados salvos com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpa os campos
                txtNome.Clear();
                txtEmail.Clear();
                txtTelefone.Clear();
                cmbGenero.SelectedIndex = -1;
                txtNome.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool EmailValido(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}