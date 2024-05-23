using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data; //adicionei
using System.Data.SqlClient; //adicionei para conexão com BD
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace WS002
{
    public class Conecta
    {
        private static string str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Aluno\\source\\repos\\WS002\\WS002\\DbProduto.mdf;Integrated Security=True";
        private static SqlConnection con = null;

        public static SqlConnection Conexao()
        {
            con = new SqlConnection(str);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            try
            {
                con.Open();
            }
            catch (Exception sqle)
            {
                con = null;
                MessageBox.Show(sqle.Message);
            }
            return con;
        }
        public static void FecharConexao()
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }  
}
