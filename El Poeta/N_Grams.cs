using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace El_Poeta
{
    class N_Grams
    {
        public void getNGram()
        {
            while (true)
            {
                Console.WriteLine("N-Grams");
                Console.WriteLine();
                Console.WriteLine("1. Nuevo n-gram");
                Console.WriteLine("2. Salir");
                string userinput = Console.ReadLine();
                if (userinput == "1")
                {
                    Console.WriteLine("Porfavor ingrese el texto: ");
                    string text = Console.ReadLine();
                    Console.WriteLine("Porfavor ingrese el n de los N-Grams");
                    int n_size = Int32.Parse(Console.ReadLine());
                    //String pattern = @"\s-\s?[+*]?\s?-\s";
                    string[] elements = text.Split(new Char[] { ' ', ',', '.', ':', '\t' });
                    IDictionary<int, string> dict = new Dictionary<int, string>();
                    int key = 0;
                    for (int x = 0; x < elements.Length; x++)
                    {
                        string temp_string = "";
                        for (int i = 0; i < n_size; i++)
                        {
                            try
                            {
                                temp_string += elements[x + i];
                                temp_string += " ";
                            }
                            catch (IndexOutOfRangeException)
                            {
                                temp_string += " ";
                            }
                        }
                        //Console.WriteLine(element);
                        dict.Add(key, temp_string);
                        key++;
                    }

                }
                else if (userinput == "2")
                {
                    Console.Write("Gracias");
                    break;
                }
            }
        }
    }
}
