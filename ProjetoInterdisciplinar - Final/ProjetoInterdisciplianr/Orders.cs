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
    public partial class Orders : Form
    {
        public const int WM_NCLBUTTOONDOWN = 0xA1;
        public const int HT_LOCATION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        public Orders()
        {
            InitializeComponent();
        }

        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTOONDOWN, HT_LOCATION, 0);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm neworder = new OrderForm();
            neworder.ShowDialog();
        }

        void GridFill()
        {

            MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
            conn.Open();

            MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM servicos2020", conn);
            data.SelectCommand.CommandType = CommandType.Text;
            DataTable table = new DataTable();
            data.Fill(table);

            dgvOrders.DataSource = table;
        }

        private void Orders_Load(object sender, EventArgs e)
        {
            GridFill();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignService ss = new SignService();
            ss.ShowDialog();
        }
    }
}
