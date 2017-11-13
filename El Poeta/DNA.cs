using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Poeta
{
    //Cada individuo de la poblacion 
    class DNA<T>
    {
        //Se crea un array que va a contener los genes
        public T[] Genes { get; private set; }
        //Almacena la adaptabilidad de cada individuo
        public float Fitness { get; private set; }

        private Random random;
        //Retorna un objeto de tipo Gen
        private Func<T> getRandomGene;
        private Func<int, float> fitnessFunction;
        //El tamaño del array de genes, el random del crossover, referencia a una funcion que devuelve un objeto de tipo T
        //La funcion de adaptabilidad
        public DNA(int size, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, bool shouldInitGenes = true)
        {
            //Crea el array del tamaño que dice el constructor
            Genes = new T[size];
            this.random = random;
            this.getRandomGene = getRandomGene;
            this.fitnessFunction = fitnessFunction;
            //Los genes de cada individuo al principio tienen que generarse de forma aleatoria
            if (shouldInitGenes)
            {
                for (int i = 0; i < Genes.Length; i++)
                {
                    Genes[i] = getRandomGene();
                }
            }
        }
        //Funcion que calcula la adaptabilidad de cada individuo
        //Ve si el individuo se puede reproducir o no 
        public float CalculateFitness(int index)
        {
            Fitness = fitnessFunction(index);
            return Fitness;
        }
        //Cruce de genes, retorna un objeto DNA
        //Cruza con otro individuo
        public DNA<T> Crossover(DNA<T> otherParent)
        {
            //Se crea un nuevo hijo de igual tipo y con el largo del len de los padres
            DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, fitnessFunction, shouldInitGenes: false);
            //Combina los genes de os padres para generar los del hijo
            //Flip a coin
            for (int i = 0; i < Genes.Length; i++)
            {
                child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
            }

            return child;
        }

        //Que tan probable es mutar el gen
        public void Mutate(float mutationRate)
        {
            //Recorre cada gen para saber si va a sufrir una mutacion o no 
            //Si es menor a la tasa de mutacion se le asigna un nuevo gen aleatorio
            for (int i = 0; i < Genes.Length; i++)
            {
                if (random.NextDouble() < mutationRate)
                {
                    Genes[i] = getRandomGene();
                }
            }
        }
    }
}
