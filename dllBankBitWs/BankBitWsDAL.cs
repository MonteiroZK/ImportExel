using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace dllBankBitWs
{
    public class BankBitWsDAL 
    {

        public DataTable listar(string nome)
        {
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;
            //define um objeto range
            Excel.Range range;
            int column = 0;
            int row = 0;
     
            //string _Arquivo = ();
            // String _StringConexao = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0 Xml; HDR = YES';", _Arquivo);

            var excelApp = new Excel.Application();
           
            workbook = excelApp.Workbooks.Open(Directory.GetCurrentDirectory() + "\\" + nome + ".xlsx");
          
            if(nome == "CadastroFuncionarios")
            {
                worksheet = (Excel.Worksheet)workbook.Sheets["FUNCIONARIO"];
            }
            else
            {
                worksheet = (Excel.Worksheet)workbook.Sheets["Planilha1"];
            }
          

            // OleDbDataAdapter adapter = new OleDbDataAdapter("select * from [Planilha1$]",);
            range = worksheet.UsedRange;
            DataTable tb = new DataTable() ;
            // DataTable tb  = new DataTable();
            //tb = worksheet.ListObjects() ;




           

            for (row = 1; row <= range.Rows.Count; row++)
            {
                if(row == 1)
                {
                    string coluna;
                    for (column = 1; column <= range.Columns.Count; column++)
                    {                    
                       coluna= range.ToString();                           
                        tb.Columns.Add((range.Cells[row, column] as Excel.Range).Value2 != null ?
                         (range.Cells[row, column] as Excel.Range).Value2.ToString() : "");
                    }
                    
                }
                else
                {
                    DataRow dr = tb.NewRow();
                    for (column = 1; column <= range.Columns.Count; column++)
                    {

                        dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2 != null ?
                        (range.Cells[row, column] as Excel.Range).Value2.ToString() : "";
                    }
                    tb.Rows.Add(dr);
                    tb.AcceptChanges();
                }
               
            }
            excelApp.Quit();
            return tb;

        }



        public DataTable GerarTabelaComAutenticacao()
        {
  
            DataTable tb = listar("CadastroFuncionarios");
            BankBitWsVO.Funcionarios func = new BankBitWsVO.Funcionarios();
            tb.Columns.Add("PIXWS");
            

            string codigo = "";

            foreach (DataRow row in tb.Rows)
            {
                string cpf = row.ItemArray.GetValue(1).ToString();
                string nome = row.ItemArray.GetValue(0).ToString();
                string email = row.ItemArray.GetValue(3).ToString();
                char[] charEmail = email.ToCharArray();
                string provedor = "";

                bool tf = false;
                for (int ctr = 0; ctr < charEmail.Length; ctr++)
                {
                    if (tf == false)
                    {
                        if (charEmail[ctr].ToString() == "@")
                        {
                            tf = true;
                        }
                    }
                    else
                    {
                        provedor = provedor + charEmail[ctr];
                    }
                }
                codigo = nome[0].ToString() + cpf[0].ToString() + cpf[1].ToString().ToUpper() + provedor[0].ToString() + provedor[1].ToString(); ;

                row.SetField(6,codigo);
                // row.
               // tb.Rows.Add(row, row.ItemArray.GetValue(0)) ;
                
              
            }
            tb.AcceptChanges();
        

            return tb;
        }


        public string GerarCodigoUsuarios()
        {
            //List<BankBitWsVO.Funcionarios> ListFuncionarion = new List<BankBitWsVO.Funcionarios>();
            var funci = JsonConvert.DeserializeObject<List<BankBitWsVO.Funcionarios>>("[]");

            DataTable tb = listar("CadastroFuncionarios");

            foreach (DataRow row in tb.Rows)
            {
                BankBitWsVO.Funcionarios funcionarios = new BankBitWsVO.Funcionarios();

               
                    string cpf = row.ItemArray.GetValue(1).ToString();
                    string nome = row.ItemArray.GetValue(0).ToString();
                    string email = row.ItemArray.GetValue(3).ToString();
                    char[] charEmail = email.ToCharArray();
                    string provedor = "";
                    bool tf = false;
                    for (int ctr = 0; ctr < charEmail.Length; ctr++)
                    {
                        if (tf == false)
                        {
                            if (charEmail[ctr].ToString() == "@")
                            {
                                tf = true;
                            }
                        }
                        else
                        {
                            provedor = provedor + charEmail[ctr];
                        }


                    }

                    string codigo = nome[0].ToString() + cpf[0].ToString() + cpf[1].ToString().ToUpper() + provedor[0]+provedor[1];

                 
                    funcionarios.Nome = nome;
                    funcionarios.Cpf = cpf;
                    funcionarios.QtdeFamilia = Convert.ToInt32(row.ItemArray.GetValue(2));
                    funcionarios.Email = email;
                    funcionarios.Departamento = row.ItemArray.GetValue(4).ToString(); ;
                    funcionarios.Cargo = row.ItemArray.GetValue(5).ToString(); ;
                    funcionarios.PIXWS = codigo;

                    funci.Add(funcionarios);

                

             


            }

            var jsonSerializado = JsonConvert.SerializeObject(funci);
            return jsonSerializado.ToString();
        }


        public string GerarCodigoProdutos()
        {
            //List<BankBitWsVO.Funcionarios> ListFuncionarion = new List<BankBitWsVO.Funcionarios>();
            var prodList = JsonConvert.DeserializeObject<List<BankBitWsVO.Produto>>("[]");

            DataTable tb = listar("CadastroProdutos");

            foreach (DataRow row in tb.Rows)
            {
                BankBitWsVO.Produto prod = new BankBitWsVO.Produto();

              
                    prod.Nome = row.ItemArray.GetValue(0).ToString();
                    prod.Valor = (row.ItemArray.GetValue(1).ToString() != "")? Convert.ToInt32(row.ItemArray.GetValue(1)) : (0) ;
                    prod.BitWs = (row.ItemArray.GetValue(2).ToString() != "") ? Convert.ToDouble(row.ItemArray.GetValue(2)) : (0);
                    prod.dept_indicado = (row.ItemArray.GetValue(3).ToString() != "") ? row.ItemArray.GetValue(3).ToString():"";
                    prod.Qtde = (row.ItemArray.GetValue(4).ToString() != "")? Convert.ToInt32(row.ItemArray.GetValue(4)): 0;
                    prod.ValorTotal = (row.ItemArray.GetValue(5).ToString() != "")? Convert.ToInt32(row.ItemArray.GetValue(5)):0;
                    prod.Descricao = (row.ItemArray.GetValue(6).ToString() != "")? row.ItemArray.GetValue(6).ToString():"";
       

                    prodList.Add(prod);

                




            }

            var jsonSerializado = JsonConvert.SerializeObject(prodList);
            return jsonSerializado.ToString();
        }




    }
}
