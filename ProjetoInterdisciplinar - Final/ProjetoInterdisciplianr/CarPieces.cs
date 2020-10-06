using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ProjetoInterdisciplianr
{
    public partial class CarPieces : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        public CarPieces()
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
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        void GridFill()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlDataAdapter data = new MySqlDataAdapter("ProductViewAll", conn);
                data.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable table = new DataTable();
                data.Fill(table);

                dgvProduct.DataSource = table;
            }

            catch (Exception error)
            {
                MessageBox.Show("Não foi possível conectar as tabelas. \n\nErro: " + error);
            }
        }

        private void CarPieces_Load(object sender, EventArgs e)
        {
            GridFill();
        }

        private void btnClientForm_Click(object sender, EventArgs e)
        {
            CarPieceForm cpform = new CarPieceForm();
            cpform.ShowDialog();
        }
    }
}
