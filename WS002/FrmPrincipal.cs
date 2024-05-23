using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient; //adicionei

namespace WS002
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        public void CarregaDgv()
        {
            SqlConnection con = Conecta.Conexao();
            string query = "SELECT * FROM Produto";
            SqlCommand cmd = new SqlCommand(query, con);
            Conecta.Conexao();
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd); // diferença do Dr pro Da é p tamanho
            DataTable produto = new DataTable();
            da.Fill(produto);
            dgvProduto.DataSource = produto;
            Conecta.FecharConexao();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            CarregaDgv();
            this.ActiveControl = txtNome; //já vem direto pro txtNome;
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); //Mais recomendado que o This.Close();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conecta.Conexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO Produto(nome,tipo,preco,quantidade,total) VALUES(@nome,@tipo,@preco,@quantidade,@total)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                cmd.Parameters.AddWithValue("@preco", txtPreco.Text);
                cmd.Parameters.AddWithValue("@quantidade", txtQuantidade.Text);
                cmd.Parameters.AddWithValue("@total", txtTotal.Text);
                Conecta.Conexao(); // aqui eu vou me conectar com o banco
                cmd.ExecuteNonQuery(); // aqui é executado
                CarregaDgv();
                MessageBox.Show("Produto Cadastro com Sucesso");
                Conecta.FecharConexao();

                txtNome.Text = string.Empty;
                txtTipo.Text = string.Empty;
                txtPreco.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtTotal.Text = string.Empty;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPreco.Text) || string.IsNullOrEmpty(txtQuantidade.Text))
            {
                MessageBox.Show("Preencha os campos");
            }
            else
            {
                double preco, quantidade, total;
                preco = Convert.ToDouble(txtPreco.Text);
                quantidade = Convert.ToDouble(txtQuantidade.Text);
                total = preco * quantidade;
                txtTotal.Text = total.ToString();
            }
            
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conecta.Conexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Produto WHERE Id=@Id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                Conecta.Conexao();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    txtNome.Text = rd["nome"].ToString();
                    txtTipo.Text = rd["tipo"].ToString();
                    txtPreco.Text = rd["preco"].ToString();
                    txtQuantidade.Text = rd["quantidade"].ToString();
                    txtTotal.Text = rd["total"].ToString();
                    Conecta.FecharConexao();
                }
                else
                {
                    MessageBox.Show("Nenhum Registro Encontrado");
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conecta.Conexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE Produto SET  nome=@nome,tipo=@tipo,preco=@preco,quantidade=@quantidade,total=@total WHERE Id=@Id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text);
                cmd.Parameters.AddWithValue("@nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@tipo", txtTipo.Text);
                cmd.Parameters.AddWithValue("@preco", txtPreco.Text);
                cmd.Parameters.AddWithValue("@quantidade", txtQuantidade.Text);
                cmd.Parameters.AddWithValue("@total", txtTotal.Text);
                Conecta.Conexao(); // aqui eu vou me conectar com o banco
                cmd.ExecuteNonQuery(); // aqui é executado
                CarregaDgv();
                MessageBox.Show("Produto Editado com Sucesso!");
                Conecta.FecharConexao();

                txtNome.Text = string.Empty;
                txtTipo.Text = string.Empty;
                txtPreco.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtTotal.Text = string.Empty;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = Conecta.Conexao();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM Produto WHERE Id=@Id";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", this.txtId.Text); // especifica diretamente o txtId
                Conecta.Conexao(); // aqui eu vou me conectar com o banco
                cmd.ExecuteNonQuery(); // aqui é executado
                CarregaDgv();
                MessageBox.Show("Produto Excluido com Sucesso!");
                Conecta.FecharConexao();

                txtNome.Text = string.Empty;
                txtTipo.Text = string.Empty;
                txtPreco.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtTotal.Text = string.Empty;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
    }
}
