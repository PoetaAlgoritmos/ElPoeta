using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Poeta
{
    class N_Grams
    {
        public void generateNGram(string text, int n_size)
        {
            Char[] pattern = { ',', '\n', '.', ' ', ':', ';', '!', '?', '\r', '\"', '[', ']', '(', ')' };
            string[] elements = text.Split(pattern);
            var temp_elements = new List<String>(elements);
            temp_elements.RemoveAll(IsNull);
            elements = temp_elements.ToArray();
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
                        temp_string += elements[x + i].ToLower();
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
                    int position = GetPosition(values, temp_string);
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
        public static bool IsNull(String s)
        {
            if (s == String.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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

        public void readPoems(string Poems)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(Poems))
                {
                    // Read the stream to a string, and write the string to the console.
                    String all_Poems = sr.ReadToEnd();
                    char separator = '*';
                    string[] elements = all_Poems.Split(separator);
                    for (int i = 0; i < elements.Length; i++)
                    {
                        generateNGram(elements[i], 3);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }
}
