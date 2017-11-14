using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace El_Poeta
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            N_Grams test = new N_Grams();
            test.GenerateHistogram("Hola me llamo manuel hola me", 2);
            Distance test_2 = new Distance();
            List<Int32> histogram_1 = new List<int>(new int[] {1,2,3,4,5,6});
            List<Int32> histogram_2 = new List<int>(new int[] {1,2,3,4,5,6});
            Console.WriteLine("Manhattan Distance: ");
            Console.WriteLine(test_2.Manhattan(histogram_1, histogram_2));
            Console.WriteLine("Chebyshev Distance: ");
            Console.WriteLine(test_2.Chebyshev(histogram_1, histogram_2));
            Console.WriteLine("Personal Distance: ");
            Console.WriteLine(test_2.Personal(histogram_1, histogram_2));
            Console.ReadLine();
        }
    }
}
