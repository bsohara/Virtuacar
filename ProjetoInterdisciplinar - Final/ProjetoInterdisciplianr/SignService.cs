using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoInterdisciplianr
{
    public partial class SignService : Form
    {
        public int OrderID;
        public SignService()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("OrderAdd", conn);

                comm.Parameters.AddWithValue("_ClientID", txbCliCode.Text);
                comm.Parameters.AddWithValue("_EmployeeID", txbEmpCode.Text);
                comm.Parameters.AddWithValue("_ProductID", txbProdCode.Text);
                comm.Parameters.AddWithValue("_OrderDATE", dtpOrder.Value);
                comm.Parameters.AddWithValue("_OrderHOUR", mtxbHour.Text);
                comm.Parameters.AddWithValue("_OrderVALUE", txbValService.Text);
                comm.Parameters.AddWithValue("_ProductUNIT", txbUnit.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Dados inseridos com sucesso!!");
            }

            catch (MySqlException)
            {
                MessageBox.Show("Erro ao inserir os dados. \nDica: digite o código de alguns dados existentes.");
            }

            catch (FormatException)
            {
                MessageBox.Show("Alguns dados foram inválidos.");
            }
        }
    }
}
