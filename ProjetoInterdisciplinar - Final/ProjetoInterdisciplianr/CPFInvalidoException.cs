using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoInterdisciplianr
{
    class CPFInvalidoException : Exception
    {
        private string cpf;

        //Trata a exceção de verificação do CPF
        public CPFInvalidoException(string cpf)
        {
            this.cpf = cpf;
        }

        public string getCPF()
        {
            return cpf;
        }

    }
}
