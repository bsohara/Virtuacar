using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoInterdisciplianr
{
    class ValorTotal
    {
        private double maodeobra;
        private double valorproduto;
        private int quantidade;

        public ValorTotal()
        {
            maodeobra = 0.0;
            valorproduto = 0.0;
            quantidade = 0;
        }

        //Calcula o valor total do serviço (mão-de-obra, quantidade do produto e impostos)
        public double Calculo(double maodeobra, double valorproduto, int quantidade)
        {
            double total = (maodeobra + (valorproduto * quantidade));
            double totalcomimposto = total - (total * 0.0775);
            return totalcomimposto;
        }
    }
}
