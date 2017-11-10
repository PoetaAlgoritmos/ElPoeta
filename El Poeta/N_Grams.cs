using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace El_Poeta
{
    class N_Grams
    {
        public void generateNGram()
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
                    Char[] pattern = { ',', '\n', '.', ' ', ':', ';', '!', '?', '\r', '\"', '[', ']', '(', ')' };
                    string[] elements = text.Split(pattern);
                    Dictionary<int, string> dict = new Dictionary<int, string>();
                    int key = 0;
                    List<String> values = new List<string>();
                    List<Int32> apariciones = new List<int>();
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
                        //Console.WriteLine(temp_string);
                        if (dict.ContainsValue(temp_string))
                        {
                            int position = values.BinarySearch(temp_string);
                            apariciones[position] = apariciones[position] + 1;
                            Console.WriteLine("El valor ya se encuentra en el diccionario");
                        }
                        else
                        {
                            apariciones.Add(1);
                            values.Add(temp_string);
                            dict.Add(key, temp_string);
                        }
                        key++;
                    }
                    foreach (var element in dict)
                    {
                        Console.WriteLine(element);
                    }
                    foreach (var num in apariciones)
                    {
                        Console.WriteLine(num);
                    }

                }
                else if (userinput == "2")
                {
                    Console.Write("Gracias");
                    break;
                }
            }
        }

        public void Evelio()
        {
            string h = "En un lugar de la Mancha, de cuyo nombre no quiero acordarme, no ha mucho tiempo que vivía un hidalgo de los de lanza en astillero, adarga antigua, rocín flaco y galgo corredor. Una olla de algo más vaca que carnero, salpicón las más noches, duelos y quebrantos los sábados, lantejas los viernes, algún palomino de añadidura los domingos, consumían las tres partes de su hacienda. El resto della concluían sayo de velarte, calzas de velludo para las fiestas, con sus pantuflos de lo mesmo, y los días de entresemana se honraba con su vellorí de lo más fino. Tenía en su casa una ama que pasaba de los cuarenta, y una sobrina que no llegaba a los veinte, y un mozo de campo y plaza, que así ensillaba el rocín como tomaba la podadera. Frisaba la edad de nuestro hidalgo con los cincuenta años; era de complexión recia, seco de carnes, enjuto de rostro, gran madrugador y amigo de la caza. Quieren decir que tenía el sobrenombre de Quijada, o Quesada";
            string[] separators = { ",", "\n", ".", " ", ":", ";", "!", "?", "\r", "\"", "[", "]", "(", ")" };
            string[] fields = h.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            foreach (var grouping in fields.GroupBy(t => t).Where(t => t.Count() >= 1))
            {
                Console.WriteLine(string.Format("{0} está repetido {1} veces.", grouping.Key, grouping.Count()));
            }
        }

    }
}
