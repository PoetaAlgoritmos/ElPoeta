using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Poeta
{
    class Distance
    {

        public double Manhattan(List<Int32> histograma_1, List<Int32> histograma_2)
        {
            double result = 0;
            for (int x = 0; x < histograma_1.Capacity; x++)
            {
                double temp_result = histograma_1.ElementAt(x) + histograma_2.ElementAt(x);
                temp_result = Math.Pow(temp_result, 2);
                result += temp_result;
            }
            result = Math.Sqrt(result);
            return result;
        }

        public int Chebyshev(List<Int32> histograma_1, List<Int32> histograma_2)
        {
            List<Int32> result = new List<int>();
            for (int x = 0; x < histograma_1.Capacity; x++)
            {
                int temp_result = histograma_1.ElementAt(x) - histograma_2.ElementAt(x);
                temp_result = Math.Abs(temp_result);
                result.Add(temp_result);
            }
            return result.Max();
        }

        /* Funcion que crea un histograma para el poema de la lista_1 
         * 
         * 
         * 
         */
        public List<Int32> CreateHistogram(List<String> lista_1, List<String> lista_2, List<Int32> posicion)
        {
            //Se crea la lista con la union de los n-grams de ambos poemas
            List<String> union = lista_1.Union(lista_2).ToList();

            //Ahora creamos el nuevo histograma para un poema
            List<Int32> result = new List<int>(union.Capacity);

            //Ahora asignamos los valores correspondientes a cada posicion
            for (int x = 0; x < lista_1.Capacity; x++)
            {
                int pos = GetPosition(union, lista_1[x]);
                if (pos >= 0)
                {
                    result[pos] = posicion[x]; 
                }
            }

            return result;

        }

        //Funcion para obtener la posicion de un n-gram en la lista
        public int GetPosition(List<String> lista, String valor)
        {
            Int32 result = -1;
            for (int x = 0; x < lista.Capacity; x++)
            {
                if (lista[x] == valor)
                {
                    return x;
                }
            }
            return result;
        }


    }
}
