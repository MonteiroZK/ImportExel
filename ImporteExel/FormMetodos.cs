using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ImporteExel
{
    public partial class FormMetodos : Form
    {
        public FormMetodos()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dllBankBitWs.BankBitWsBO bankbo = new dllBankBitWs.BankBitWsBO();
            double resut = bankbo.MostrarTotalBitWs();

            MessageBox.Show("Total de Bitws:" + resut, "Aviso", MessageBoxButtons.OK);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dllBankBitWs.BankBitWsBO bankbo = new dllBankBitWs.BankBitWsBO();

            double resut = bankbo.CalcularTotalBITWS();
            double resut2 = bankbo.MostrarTotalBitWs();

            if (resut < resut2)
            {
                MessageBox.Show("<<cadastrar mais produtos {urgente}>>", "Aviso",MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Total de Bitws:" + resut, "Aviso", MessageBoxButtons.OK);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dllBankBitWs.BankBitWsDAL bankbo = new dllBankBitWs.BankBitWsDAL();
            textBox1.Text = bankbo.GerarCodigoUsuarios();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dllBankBitWs.BankBitWsDAL bankbo = new dllBankBitWs.BankBitWsDAL();
      
            textBox1.Text = bankbo.GerarCodigoProdutos();
         
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Escolha a pasta em que quer salva o .JSON");
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string diretorio = folderBrowserDialog1.SelectedPath.ToString();

                File.WriteAllText(diretorio +"/jsonCreate.json",textBox1.Text.ToString());
                MessageBox.Show("JSON CRIADO COM SUCESSO");

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            openFileDialog1.OpenFile();
        }
    }
}
