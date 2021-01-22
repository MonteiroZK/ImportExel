using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllBankBitWs
{
    public class BankBitWsBO
    {
       

        public double MostrarTotalBitWs()
        {
            double resut = 0;
            BankBitWsDAL bankdal = new BankBitWsDAL();

            DataTable tb =  bankdal.listar("CadastroProdutos");

            foreach (DataRow row in tb.Rows)
            {
                if (!row.IsNull("Bitws"))
                {
                    try
                    {
                        resut = resut + Convert.ToDouble(row.ItemArray.GetValue(2));
                    }
                    catch (Exception)
                    {

                    }
                    
                }
            }

            return resut;
        }

        public double CalcularTotalBITWS()
        {
            double resut = 0;
            BankBitWsDAL bankdal = new BankBitWsDAL();

            DataTable tb = bankdal.listar("CadastroFuncionarios");

            foreach (DataRow row in tb.Rows)
            {
                if (!row.IsNull("qtde familia"))
                {
                    if (Convert.ToDouble(row.ItemArray.GetValue(2)) != 0)
                    {
                        try
                        {
                            resut = resut + (250 * Convert.ToDouble(row.ItemArray.GetValue(3)));
                        }
                        catch (Exception)
                        {

                        }
                       
                    }
                    resut = resut + 1000;
                }
            }

            return resut;
        }

    }
}
