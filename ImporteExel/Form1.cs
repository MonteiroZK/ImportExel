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
using System.Data.OleDb;
using System.Data.Common;

namespace ImporteExel
{
    public partial class Form1 : Form
    {


       

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dllBankBitWs.BankBitWsDAL bankdal = new dllBankBitWs.BankBitWsDAL();
            DataTable tb =bankdal.listar("CadastroProdutos");
            datagridview.DataSource = tb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dllBankBitWs.BankBitWsDAL bankdal = new dllBankBitWs.BankBitWsDAL();
            DataTable tb = bankdal.GerarTabelaComAutenticacao();

            datagridview.DataSource = tb;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormMetodos formOn = new FormMetodos();
            Hide();
            formOn.Show();
        }
    }
}
