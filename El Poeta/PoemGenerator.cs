using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace El_Poeta
{
    class PoemGenerator
    {
         string targetString = "To be, or not to be, that is the question.";
         string validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,.|!#$%&/()=? ";
         int populationSize = 200;
         float mutationRate = 0.01f;
         int elitism = 5;
         private GeneticAlgorithm<char> ga;
         private System.Random random;

        void Start()
        {
            random = new System.Random();
            ga = new GeneticAlgorithm<char>(populationSize, targetString.Length, random, GetRandomCharacter, 
                FitnessFunction, elitism, mutationRate);
        }

        void Update()
        {
            ga.NewGeneration();
            if (ga.BestFitness == 1)
            {
            }
        }

        private char GetRandomCharacter()
        {
            int i = random.Next(validCharacters.Length);
            return validCharacters[i];
        }


        //Funcion de adaptabilidad, aqui va la similitud de documentos
        private float FitnessFunction(int index)
        {
            float score = 0;
            DNA<char> dna = ga.Population[index];

            for (int i = 0; i < dna.Genes.Length; i++)
            {
                if (dna.Genes[i] == targetString[i])
                {
                    score += 1;
                }
            }

            score /= targetString.Length;

            score = ((float)Math.Pow(2, score) - 1) / (2 - 1);

            return score;
        }
    }
}
