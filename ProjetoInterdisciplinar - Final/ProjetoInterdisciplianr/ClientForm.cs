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
    public partial class ClientForm : Form
    {
        int cont = 0;
        public int ClientID;

        public ClientForm()
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
                string valor = txbCPF.Text;
                validacao x = new validacao();
                if (x.validation_cpf(valor))
                {
                    cont++;
                }

                if (cont == 1)
                {
                    MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                    conn.Open();

                    MySqlCommand comm = new MySqlCommand("ClientAddOrEdit", conn);

                    comm.Parameters.AddWithValue("_ClientID", ClientID);
                    comm.Parameters.AddWithValue("_ClientNAME", txbName.Text);
                    comm.Parameters.AddWithValue("_ClientCPF", txbCPF.Text);
                    comm.Parameters.AddWithValue("_ClientADRESS", txbAdress.Text);
                    comm.Parameters.AddWithValue("_ClientCITY", txbCity.Text);
                    comm.Parameters.AddWithValue("_ClientSTATE", cmbState.SelectedItem.ToString());
                    comm.Parameters.AddWithValue("_clientCAR", txbCar.Text);
                    comm.Parameters.AddWithValue("_clientCARTAG", txbCarTag.Text);

                    comm.CommandType = CommandType.StoredProcedure;
                    comm.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Cadastro de cliente realizado com sucesso!!");
                }

                else
                {
                    throw new CPFInvalidoException(valor);
                }
            }

            catch (CPFInvalidoException)
            {
                MessageBox.Show("CPF inválido.");
                txbCPF.Focus();
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception)
            {
                MessageBox.Show("Erro ao cadastrar o cliente.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("ClientAddOrEdit", conn);

                comm.Parameters.AddWithValue("_ClientNAME", txbName.Text);
                comm.Parameters.AddWithValue("_ClientADRESS", txbAdress.Text);
                comm.Parameters.AddWithValue("_ClientCPF", txbCPF.Text);
                comm.Parameters.AddWithValue("_ClientCITY", txbAdress.Text);
                comm.Parameters.AddWithValue("_ClientSTATE", cmbState.SelectedItem.ToString());
                comm.Parameters.AddWithValue("_ClientCAR", txbCar.Text);
                comm.Parameters.AddWithValue("_ClientCARTAG", txbCarTag.Text);
                comm.Parameters.AddWithValue("_ClientID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Dados alterados.");
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception)
            {
                MessageBox.Show("Falha ao atualizar os dados.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("ClientDeleteByID", conn);

                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("_ClientID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;
                comm.ExecuteNonQuery();

                MessageBox.Show("Cliente retirado no sistema.");
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception)
            {
                MessageBox.Show("Erro ao apagar dados do cliente.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {         
            txbCode.Text = "";
            txbName.Text = "";
            txbCPF.Text = "";
            txbAdress.Text = "";
            txbCity.Text = "";
            cmbState.Text = "-";
            txbCar.Text = "";
            txbCarTag.Text = "";           
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("ClientViewByID", conn);
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("_ClientID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;

                MySqlDataReader dr;
                dr = comm.ExecuteReader();
                dr.Read();

                txbName.Text = dr.GetString(1);
                txbCPF.Text = dr.GetString(2);
                txbAdress.Text = dr.GetString(3);
                txbCity.Text = dr.GetString(4);
                cmbState.SelectedItem = dr.GetString(5);
                txbCar.Text = dr.GetString(6);
                txbCarTag.Text = dr.GetString(7);

                conn.Close();
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados desse código inválidos ou inexistentes.");
            }

            catch (Exception)
            {
                MessageBox.Show("Erro ao procurar os dados do cliente.");
            }
        }
    }
}
