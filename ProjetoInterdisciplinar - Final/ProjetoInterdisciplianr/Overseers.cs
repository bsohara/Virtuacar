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
    public partial class Overseers : Form
    {
        public Overseers()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void GridFill()
        {

            MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
            conn.Open();

            MySqlDataAdapter data = new MySqlDataAdapter("OverseersViewAll", conn);
            data.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable table = new DataTable();
            data.Fill(table);

            dgvOverseers.DataSource = table;
        }
        
        private void Overseers_Load(object sender, EventArgs e)
        {
            GridFill();
        }
    }
}
