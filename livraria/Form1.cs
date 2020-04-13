using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace livraria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=root;persistsecurityinfo=True;database=dblivraria");

            String usuarioBanco = "";
            String senhaBanco = "";

            MySqlCommand comando = new MySqlCommand("SELECT nmUsuario, cdSenha FROM tbUsuario WHERE nmUsuario=@Usuario and cdSenha=@Senha", conexao);

            comando.Parameters.Add(new MySqlParameter("@Usuario", MySqlDbType.VarChar)).Value = txtLogin.Text;
            comando.Parameters.Add(new MySqlParameter("@Senha", MySqlDbType.VarChar)).Value = txtSenha.Text;

            MySqlDataReader dr;

            //abrindo conexao com o banco de dados
            conexao.Open();

            //executar a  leitura

            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                usuarioBanco = dr.GetString(0);
                senhaBanco = dr.GetString(1);


            }
            conexao.Close();

            if (usuarioBanco == txtLogin.Text && senhaBanco == txtSenha.Text)
            {
                if (Application.OpenForms.OfType<Form2>().Count() > 0)
                {
                    Form entra = Application.OpenForms["form2"];
                    entra.Show();
                    this.Hide();
                    MessageBox.Show("Acesso permitido!");
                }
                else
                {

                    Form2 entra = new Form2();
                    entra.Show();
                    this.Hide();

                }
            }
            else
            {
                MessageBox.Show("Acesso Negado!");
            }
        }



        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = '*';
        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            String conversao = "";
            conversao += e.ToString();
            e.ToString().Replace("s", "*");
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            txtLogin.Clear();
            txtSenha.Clear();
        }
    }
    }

