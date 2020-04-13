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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=root;persistsecurityinfo=True;database=dblivraria");

            String livroBanco = "";
            String codigoBanco = "";
            String ResenhaBanco = "";

            MySqlCommand comando = new MySqlCommand("SELECT * FROM tbLivro WHERE cdLivro=@Livro", conexao);

            comando.Parameters.Add(new MySqlParameter("@Livro", MySqlDbType.VarChar)).Value = txtCodigo.Text;

            MySqlDataReader dr;

            //abrindo conexao com o banco de dados
            conexao.Open();

            //executar a  leitura

            dr = comando.ExecuteReader();

            while (dr.Read())
            {
                codigoBanco = dr.GetString(0);
                livroBanco = dr.GetString(1);
                ResenhaBanco = dr.GetString(2);
            }
            conexao.Close();

            if (codigoBanco.Equals(txtCodigo.Text))
            {
                txtNomeLivro.Text = livroBanco;
                textBox1.Text = ResenhaBanco;

            }
            else
            {
                MessageBox.Show("Erro");
            }
            
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
           MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=root;database=dblivraria");
           MySqlCommand inclui = new MySqlCommand("insert into tbLivro values('"+txtCodigo.Text+"','"+ txtNomeLivro.Text+"','"+ textBox1.Text+"');",conexao);
            int linhas = 0;
            conexao.Open();

            linhas = inclui.ExecuteNonQuery();
            if (linhas != 1)
            {
                MessageBox.Show("Erro");
            }
            else
            {
                MessageBox.Show("Enviado com sucesso");
            }
            conexao.Close();
        }
    }
}
