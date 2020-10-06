using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoInterdisciplianr
{
    public partial class Employees : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_LOCATION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        public Employees()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_LOCATION, 0);
            }
        }

        void GridFill()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlDataAdapter data = new MySqlDataAdapter("EmployeeViewAll", conn);
                data.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable table = new DataTable();
                data.Fill(table);

                dgvEmployees.DataSource = table;
            }

            catch (MySqlException)
            {
                MessageBox.Show("Ocorreu um erro ao visualizar os dados.");
            }
        }

        private void Employees_Load(object sender, EventArgs e)
        {
            GridFill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmployeeForm eform = new EmployeeForm();
            eform.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Overseers ovs = new Overseers();
                ovs.ShowDialog();
            }

            catch (MySqlException)
            {
                MessageBox.Show("Ocorreu um erro ao visualizar os dados.");
            }
        }
    }
}
