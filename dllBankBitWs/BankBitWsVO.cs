using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dllBankBitWs
{
    public class BankBitWsVO
    {

        public class Funcionarios
        {

            public string Nome { get; set; }
            public string Cpf { get; set; }
            public int QtdeFamilia { get; set; }
            public string Email { get; set; }
            public string Departamento { get; set; }
            public string Cargo { get; set; }
            public string PIXWS { get; set; }
        }

        public class Produto
        {

            public string Nome { get; set; }
            public int Valor { get; set; }
            public double BitWs { get; set; }
            public string dept_indicado { get; set; }
            public int Qtde { get; set; }
            public int ValorTotal { get; set; }
            public string Descricao { get; set; }
        }


    }
}
