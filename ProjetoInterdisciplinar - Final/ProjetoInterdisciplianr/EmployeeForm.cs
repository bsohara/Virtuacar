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
    public partial class EmployeeForm : Form
    {
        public int EmployeeID;

        public EmployeeForm()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("EmployeeAddOrEdit", conn);

                comm.Parameters.AddWithValue("_EmployeeID", EmployeeID);
                comm.Parameters.AddWithValue("_EmployeeNAME", txbName.Text);
                comm.Parameters.AddWithValue("_EmployeeFUNCTION", txbFunction.Text);
                comm.Parameters.AddWithValue("_EmployeeSALARY", txbSalary.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Funcionário cadastrado.");
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception)
            {
                MessageBox.Show("Erro ao adicionar funcionário.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("EmployeeAddOrEdit", conn);

                comm.Parameters.AddWithValue("_EmployeeNAME", txbName.Text);
                comm.Parameters.AddWithValue("_EmployeeFUNCTION", txbFunction.Text);
                comm.Parameters.AddWithValue("_EmployeeSALARY", txbSalary.Text);
                comm.Parameters.AddWithValue("_EmployeeID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Dados alterados.");
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception error)
            {
                MessageBox.Show("Não foi possível atualizar os dados do funcionário. \n\nErro: " + error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("EmployeeViewByID", conn);
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("_EmployeeID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();

                MySqlDataReader dr;
                dr = comm.ExecuteReader();
                dr.Read();

                txbName.Text = dr.GetString(1);
                txbFunction.Text = dr.GetString(2);
                txbSalary.Text = dr.GetString(3);

                conn.Close();
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception error)
            {
                MessageBox.Show("Não foi possível buscar os dados do empregado. \n\nErro: " + error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("EmployeeDeletebyID", conn);
                comm.Parameters.AddWithValue("_EmployeeID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Dado removido do sistema.");
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception error)
            {
                MessageBox.Show("Não foi possivel remover este funcionário. \n\nErro: " + error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbCode.Text = "";
            txbName.Text = "";
            txbFunction.Text = "";
            txbSalary.Text = "";
        }
    }
}
