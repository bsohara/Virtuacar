using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ProjetoInterdisciplianr
{
    public partial class OrderForm : Form
    {
        public const int WM_NCLBUTTOONDOWN = 0xA1;
        public const int HT_LOCATION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();

        public OrderForm()
        {
            InitializeComponent();
        }

        //Interação com o cabeçalho
        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTOONDOWN, HT_LOCATION, 0);
            }
        }

        //Fecha a janela
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        //Seleciona o serviço através do código
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost; username=bruno; database=mecanica; password=dbadmin");
                conn.Open();

                MySqlCommand comm = new MySqlCommand("OrderViewByID", conn);
                comm.Parameters.Clear();
                comm.Parameters.AddWithValue("_OrderID", txbCode.Text);

                comm.CommandType = CommandType.StoredProcedure;

                MySqlDataReader dr;
                dr = comm.ExecuteReader();
                dr.Read();

                txbEmployeeName.Text = dr.GetString(1);
                txbClientName.Text = dr.GetString(2);
                txbCPF.Text = dr.GetString(3);
                txbAdress.Text = dr.GetString(4);
                txbCity.Text = dr.GetString(5);
                cmbState.Text = dr.GetString(6);
                txbCar.Text = dr.GetString(7);
                txbCartag.Text = dr.GetString(8);
                dtpStartDate.Text = dr.GetString(9);
                txbProduct.Text = dr.GetString(11);
                txbPrice.Text = dr.GetString(12);
                txbUnit.Text = dr.GetString(13);
                txbSubTotal.Text = dr.GetString(14);
            }

            catch (FormatException)
            {
                MessageBox.Show("Um dos dados foram digitados de forma inválida.");
            }

            catch (MySqlException)
            {
                MessageBox.Show("Dados inválidos.");
            }

            catch (Exception erro)
            {
                MessageBox.Show("Erro ao executar. \nPossível erro: " + erro.Message);
            }
        }

        //Botão que irá realizar o cálculo e gerar o código
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                ValorTotal valor = new ValorTotal();
                gerCodigo codigo = new gerCodigo();

                double valor1 = double.Parse(txbSubTotal.Text);
                double valor2 = double.Parse(txbPrice.Text);
                int valor3 = int.Parse(txbUnit.Text);

                double valor4 = valor.Calculo(valor1, valor2, valor3);
                txtOrderTotal.Text = string.Format("{0:c}", Convert.ToDouble(valor4));

                string cod = codigo.GerarCodigo();
                txtReceiptNumber.Text = cod.ToUpper();
            }

            catch (FormatException err)
            {
                MessageBox.Show("Erro ao calcular os valores. \nPossível erro: " + err.Message);
            }

            catch(Exception)
            {
                MessageBox.Show("Erro nos cálculos.");
            }
        }

        //Botão que gera ,em PDF, o comprovante de pagamento
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Document doc = new Document(PageSize.A4);
            try
            {
                doc.SetMargins(40, 40, 40, 80);
                doc.AddCreationDate();

                string path = @"C:\Users\bruno\Desktop\Recibo\Recibo.pdf";
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path, FileMode.Create));

                doc.Open();
                Paragraph titulo = new Paragraph();
                titulo.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 35);
                titulo.Alignment = Element.ALIGN_CENTER;
                titulo.Add("Virtuacar\n\n");
                doc.Add(titulo);

                Paragraph subtitulo = new Paragraph();
                subtitulo.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 25);
                titulo.Alignment = Element.ALIGN_LEFT;
                subtitulo.Add("Comprovante de pagamento");
                doc.Add(subtitulo);

                Paragraph recibo = new Paragraph();
                recibo.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                recibo.Alignment = Element.ALIGN_LEFT;
                recibo.Add("Código do recibo: #" + txtReceiptNumber.Text + "\n\n");
                doc.Add(recibo);

                //Nome do funcionário que realizou a mão-de-obra
                Paragraph empregado = new Paragraph();
                empregado.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                empregado.Alignment = Element.ALIGN_LEFT;
                empregado.Add("Funcionário responsável: " + txbEmployeeName.Text + "\n");
                doc.Add(empregado);

                //Nome do cliente atendido
                Paragraph cliente = new Paragraph();
                cliente.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                cliente.Alignment = Element.ALIGN_LEFT;
                cliente.Add("Cliente atendido: " + txbClientName.Text);
                doc.Add(cliente);

                //Parte que insere os dados do endereço do cliente
                Phrase endereco = new Phrase();
                endereco.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                endereco.Add("Endereço: " + txbAdress.Text + ", ");
                doc.Add(endereco);

                Phrase cidade = new Phrase();
                cidade.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                cidade.Add(txbCity.Text + " - ");
                doc.Add(cidade);

                Phrase uf = new Phrase();
                uf.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                uf.Add(cmbState.Text + "\n");
                doc.Add(uf);

                //Carro e placa do cliente
                Phrase carro = new Phrase();
                carro.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                carro.Add("Carro do cliente: " + txbCar.Text + " | ");
                doc.Add(carro);

                Phrase placa = new Phrase();
                placa.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                placa.Add("Placa: " + txbCartag.Text + "\n\n");
                doc.Add(placa);

                //Produto, seu preço e quantidade utilizada
                Phrase produto = new Phrase();
                produto.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                produto.Add("Produto: " + txbProduct.Text);
                doc.Add(produto);

                Phrase preco = new Phrase();
                preco.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                preco.Add(" | Preço: " + txbPrice.Text + "\n");
                doc.Add(preco);

                Phrase quantidade = new Phrase();
                quantidade.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                quantidade.Add("Quantidade do produto utilizada: " + txbUnit.Text + " unidade(s)\n");
                doc.Add(quantidade);

                //Data do serviço
                Paragraph data = new Paragraph();
                data.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                data.Alignment = Element.ALIGN_LEFT;
                data.Add("Data do serviço: " + dtpStartDate.Text + "\n\n");
                doc.Add(data);

                //Valores do serviço, imposto e o valor total
                Phrase maodeobra = new Phrase();
                maodeobra.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                maodeobra.Add("Mão-de-obra: R$" + string.Format("{0}", txbSubTotal.Text) + " | ");
                doc.Add(maodeobra);

                Phrase imposto = new Phrase();
                imposto.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10);
                imposto.Add("Valor dos impostos: 7,75%");
                doc.Add(imposto);

                Paragraph total = new Paragraph();
                total.Font = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20);
                total.Alignment = Element.ALIGN_RIGHT;
                total.Add("Valor total: " + string.Format("{0:c}", txtOrderTotal.Text));
                doc.Add(total);

                doc.Close();

                MessageBox.Show("Impressão realizada com sucesso!!");
            }

            catch (FormatException error)
            {
                MessageBox.Show("Algo deu errado ao imprimir o comprovante. \nPossível erro: " + error.Message);
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
