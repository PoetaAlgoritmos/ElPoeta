using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace El_Poeta
{
    class GeneticAlgorithm<T>
    {
        //Para empezar se crea una lista con la poblacion inicial
        public List<Individual<T>> Population { get; private set; }
        //Cuantas generacion han pasado 
        public int Generation { get; private set; }
        //Almacena la mejor adaptabilidad de la polacion 
        public float BestFitness { get; private set; }
        //Almacena los mejores genes
        public T[] BestGenes { get; private set; }

        public int Elitism;
        //Almacena la tasa de mutacion
        public float MutationRate;
        //Crea una nueva poblacion de individuos
        private List<Individual<T>> newPopulation;
        private Random random;
        //Suma de toda la adaptablidad de cada individuo de la poblacion
        private float fitnessSum;
        private int dnaSize;
        private Func<T> getRandomGene;
        private Func<int, float> fitnessFunction;

        //Constructor tiene el tamaño de la poblacion, el array de los genes para cada elemento de la poblacion
        //un random, la funcion para obtener un gen aleatorio, la funcion de adaptabilidad, la tasa de mutacion inicial
        public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, 
            Func<int, float> fitnessFunction, int elitism, float mutationRate = 0.01f)
        {
            //Empieza con la primera generacion 
            Generation = 1;
            Elitism = elitism;
            MutationRate = mutationRate;
            //Lista de individuos que componen la poblacion
            Population = new List<Individual<T>>(populationSize);
            newPopulation = new List<Individual<T>>(populationSize);
            this.random = random;
            this.dnaSize = dnaSize;
            this.getRandomGene = getRandomGene;
            this.fitnessFunction = fitnessFunction;

            BestGenes = new T[dnaSize];
            //Crea los elementos de la poblacion 
            for (int i = 0; i < populationSize; i++)
            {
                Population.Add(new Individual<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
            }
        }

        //Funcion principal de la clase, crear nuevas generaciones 
        public void NewGeneration(int numNewIndividual = 0, bool crossoverNewIndividual = false)
        {
            int finalCount = Population.Count + numNewIndividual;
            //Si el tamaño de la poblacion es 0, se sale 
            if (finalCount <= 0)
            {
                return;
            }
      
            if (Population.Count > 0)
            {
                //Calculo de funcion de adaptabilidad
                CalculateFitness();
                Population.Sort(CompareIndividual);
            }
            newPopulation.Clear();

            for (int i = 0; i < Population.Count; i++)
            {
                if (i < Elitism && i < Population.Count)
                {
                    newPopulation.Add(Population[i]);
                }
                else if (i < Population.Count || crossoverNewIndividual)
                {
                    //Se hace el cruce de genes de los padres y nace un nuevo hijo
                    Individual<T> parent1 = ChooseParent();
                    Individual<T> parent2 = ChooseParent();

                    Individual<T> child = parent1.Crossover(parent2);
                    //Se muta el nuevo individuo dependiendo de la tasa de mutacion 
                    child.Mutate(MutationRate);
                    //Se agrega el nuevo individuo a la nueva poblacion 
                    newPopulation.Add(child);
                }
                else
                {
                    newPopulation.Add(new Individual<T>(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
                }
            }

            List<Individual<T>> tmpList = Population;
            Population = newPopulation;
            newPopulation = tmpList;
            //La generacion se aumenta en uno
            Generation++;
        }

        public int CompareIndividual(Individual<T> a, Individual<T> b)
        {
            if (a.Fitness > b.Fitness)
            {
                return -1;
            }
            else if (a.Fitness < b.Fitness)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //Funcion de adaptabilidad
        public void CalculateFitness()
        {
            fitnessSum = 0;
            Individual<T> best = Population[0];
            //Para toda la poblacion calcula la adaptabilidad de cada individuo
            for (int i = 0; i < Population.Count; i++)
            {
                fitnessSum += Population[i].CalculateFitness(i);
                //Si la adaptabilidad del individuo actual es mejor que la que considerabamos mejor anteriormente
                //Esta pasa a ser la mejor ahora
                if (Population[i].Fitness > best.Fitness)
                {
                    best = Population[i];
                }
            }
            //Copiamos los genes del nuevo mejor individuo a la variable que contiene los mejores genes

            BestFitness = best.Fitness;
            best.Genes.CopyTo(BestGenes, 0);
        }

        //Escoge un padre de acuerdo a su adaptabilidad 
        private Individual<T> ChooseParent()
        {
            double randomNumber = random.NextDouble() * fitnessSum;
            //Para cada individuo de la poblacion si el numero aleatorio es menor que la adaptabilidad del individuo
            //retorna ese individuo
            for (int i = 0; i < Population.Count; i++)
            {
                if (randomNumber < Population[i].Fitness)
                {
                    return Population[i];
                }

                randomNumber -= Population[i].Fitness;
            }

            return null;
        }

    }
}
