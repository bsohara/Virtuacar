using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoInterdisciplianr
{
    class validacao
    {
        private int counter;

        
        public bool validation_cpf(string value)
        {
            int i, j, k, seq = 10, seq2 = 11, ac = 0, ac2 = 0, verifica = 0,
                counter1 = 0, counter2 = 0, verifica2 = 0;

            char[] array = value.ToCharArray();
            int[] cpf = new int[array.Length];

            for (i = 0; i < array.Length; i++)
            {
                char caractere = array[i];
                string aux = caractere.ToString();
                cpf[i] = int.Parse(aux);
            }

            for (j = 0; j < 9; j++)
            {
                ac = cpf[j] * seq;
                counter1 += ac;
                seq--;
            }

            double resto1 = (counter1 * 10) % 11;
            if (resto1 == cpf[9])
            {
                verifica++;
            }
            else if (resto1 == 10)
            {
                resto1 = 0;
                if (resto1 == cpf[9])
                {
                    verifica++;
                }
            }
            else
            {
                verifica--;
            }

            for (k = 0; k < 10; k++)
            {
                ac2 = cpf[k] * seq2;
                counter2 += ac2;
                seq2--;
            }

            double resto2 = (counter2 * 10) % 11;
            if (resto2 == cpf[10])
            {
                verifica++;
            }
            else if (resto2 == 10)
            {
                resto2 = 0;
                if (resto2 == cpf[10])
                {
                    verifica++;
                }
            }
            else
            {
                verifica--;
            }

            for (i = 0; i < 11; i++)
            {
                if (cpf[i] == 1 && cpf[i] == 2 && cpf[i] == 3 && cpf[i] == 4 && cpf[i] == 5
                    && cpf[i] == 6 && cpf[i] == 7 && cpf[i] == 8 && cpf[i] == 9)
                {
                    verifica2++;
                }
            }

            if (verifica == 2 && verifica2 < 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void counterAnimal(int x)
        {
            counter = x;
        }

        public bool verAnimal()
        {
            if (counter == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
