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

    public partial class CarPieceForm : Form
    {
        public int ProductID;

        public CarPieceForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("ProductAddOrEdit", conn);

                comm.Parameters.AddWithValue("_ProductID", ProductID);
                comm.Parameters.AddWithValue("_ProductNAME", txbProduct.Text);
                comm.Parameters.AddWithValue("_ProductMARK", txbMark.Text);
                comm.Parameters.AddWithValue("_ProductPRICE", txbPrice.Text);
                comm.Parameters.AddWithValue("_ProductAMMOUNT", txbAmmount.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
                MessageBox.Show("Produto cadastrado com sucesso!!");

                conn.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Erro ao cadastrar produto. \n\nPossóvel erro: " + error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("ProductAddOrEdit", conn);

                comm.Parameters.AddWithValue("_ProductID", txbCode.Text);
                comm.Parameters.AddWithValue("_ProductNAME", txbProduct.Text);
                comm.Parameters.AddWithValue("_ProductMARK", txbMark.Text);
                comm.Parameters.AddWithValue("_ProductPRICE", txbPrice.Text);
                comm.Parameters.AddWithValue("_ProductAMMOUNT", txbAmmount.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
                MessageBox.Show("Dados alterados.");

                conn.Close();
            }

            catch (Exception error)
            {
                MessageBox.Show("Erro ao atualizar dados. \n\nErro: " + error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbCode.Text = "";
            txbProduct.Text = "";
            txbMark.Text = "";
            txbPrice.Text = "";
            txbAmmount.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("ProductDeleteByID", conn);
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("_ProductID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();

                MessageBox.Show("Produto removido na base de dados.");
            }

            catch (Exception error)
            {
                MessageBox.Show("Erro ao remover dados. \n\nErro: " + error);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("ProductViewByID", conn);
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("_ProductID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;

                MySqlDataReader dr;
                dr = comm.ExecuteReader();
                dr.Read();

                txbProduct.Text = dr.GetString(1);
                txbMark.Text = dr.GetString(2);
                txbPrice.Text = dr.GetString(3);
                txbAmmount.Text = dr.GetString(4);

                conn.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Erro na busca de produtos. \n\nErro: " + error);
            }
        }
    }
}
