using DataManipulation.Interfaces;
using KnowledgeRepresentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManipulation.DataManipulation
{
    class Individual
    {
        public int FitnessValue { get; set; }
        public string Chromosome { get; set; }
        public Individual() { }

        public override string ToString()
        {
            return "Chromosome: " + Chromosome + "   FitnessValue: " + FitnessValue;
        }
    }

    class Genetic
    {
        public static int typeOfBomb;
        public static string typeOfBombBin;
        public static readonly int lengthOfChromosome = 5; // set how long chromosome should be

        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            max += 1; // to make this set closed on right side <min,max) -> <min,max>
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }

        public static double GetRandomNumber(double min, double max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.NextDouble() * (max - min) + min;  // Random.NextDouble returns a double between 0 and 1. You then multiply that by the range you need to go into (difference between maximum and minimum) and then add that to the base (minimum).
            }
        }

        public static string randomChromosome()
        {
            string tempString = "";

            for (int i = 0; i < lengthOfChromosome; i++)
            {
                tempString += GetRandomNumber(0, 1);
                //Console.WriteLine("Value of {0} gen equals = {1}", i, GetRandomNumber(0,1));
            }
            return tempString;
        }

        // Generate population
        public List<Individual> Initialization()
        {
            //The population size depends on the nature of the problem,
            //Often, the initial population is generated randomly, allowing the entire range of possible solutions.
            List<Individual> listOfIndividuals = new List<Individual>();

            for (int i = 0; i < 8; i++)
                listOfIndividuals.Add(new Individual()
                {
                    Chromosome = randomChromosome()
                });
            return listOfIndividuals;
        }

        public List<Individual> FitnessFunction(List<Individual> selectedPopulation)
        {
            foreach (Individual individual in selectedPopulation)
            {
                //Console.WriteLine("Pierwsza czesc = {0}", int.Parse(individual.Chromosome[typeOfBomb - 1].ToString()) * 100);
                //Console.WriteLine("Druga czesc = {0}", individual.Chromosome.Count(x => x == '1'));
                individual.FitnessValue = int.Parse(individual.Chromosome[typeOfBomb - 1].ToString()) == 1
                                                        ? int.Parse(individual.Chromosome[typeOfBomb - 1].ToString()) * 100 - individual.Chromosome.Count(x => x == '1') * 15
                                                        : 5;
            }
            return selectedPopulation;
        }

        public List<Individual> Selection(List<Individual> parentalPopulation, List<Individual> posterityPopulation)
        {
            // Add all fitness values
            int sum = 0, i = 0;
            int[] upperlimits = new int[parentalPopulation.Count];
            foreach (Individual individual in parentalPopulation)
            {
                sum += individual.FitnessValue;
                upperlimits[i] = sum;
                i++;
            }

            /*for (int j = 0; j < upperlimits.Length; j++)
            {
                Console.WriteLine("uperlimits {0} = {1}", j, upperlimits[j]);
            }*/

            // Randomize rulete values
            int lotteryNumber = 0;
            string choosenChromosome;
            for (int k = 0; k < parentalPopulation.Count; k++)
            {
                choosenChromosome = ""; i = 0;
                lotteryNumber = GetRandomNumber(1, sum);
                /*Console.WriteLine("Lottery numbers = {0}", lotteryNumber);*/

                foreach (Individual individual in parentalPopulation)
                {
                    if (upperlimits[i] >= lotteryNumber)
                    {
                        //Console.WriteLine("Choosen Chromosome = {0}", choosenChromosome);
                        choosenChromosome = individual.Chromosome;
                        break;
                    }
                    i++;
                }
                posterityPopulation.Add(new Individual { Chromosome = choosenChromosome });
            }

            return posterityPopulation;
        }

        public List<Individual> Crossover(List<Individual> parentsList)
        {
            List<Individual> crossoverList = new List<Individual>();
            int nParents = parentsList.Count;
            int locus;

            string firstPartOfMothersChromosome = "";
            string secondPartOfMothersChromosome = "";

            string firstPartOfFathersChromosome = "";
            string secondPartOfFathersChromosome = "";

            if (nParents % 2 == 0)
                for (int nCounter = 0; nCounter < nParents; nCounter = nCounter + 2)
                {
                    if (nCounter + 1 < nParents)
                    {
                        Individual oMother = parentsList[nCounter];
                        Individual oFather = parentsList[nCounter + 1];

                        locus = GetRandomNumber(1, oMother.Chromosome.Length - 1); // 10101- find locus and set it as first gen to crossover

                        firstPartOfMothersChromosome = oMother.Chromosome.Substring(0, locus);
                        //Console.WriteLine("firstPartOfMothersChromosome = {0}", firstPartOfMothersChromosome);
                        secondPartOfMothersChromosome = oMother.Chromosome.Substring(locus, oMother.Chromosome.Length - locus);
                        //Console.WriteLine("lgh - locus = {0}", oMother.Chromosome.Length - locus);
                        /*Console.WriteLine("locus = {0}", locus);*/
                        firstPartOfFathersChromosome = oFather.Chromosome.Substring(0, locus);
                        secondPartOfFathersChromosome = oFather.Chromosome.Substring(locus, oMother.Chromosome.Length - locus);

                        crossoverList.Add(new Individual { Chromosome = firstPartOfMothersChromosome + secondPartOfFathersChromosome });
                        crossoverList.Add(new Individual { Chromosome = firstPartOfFathersChromosome + secondPartOfMothersChromosome });
                    }

                }
            return crossoverList;
        }

        public List<Individual> Mutation(List<Individual> currentPopulation)
        {
            int mutationChance = 33; // random int
            int randomNumber;
            foreach (Individual individual in currentPopulation)
            {
                for (int i = 0; i < individual.Chromosome.Length; i++)
                {
                    randomNumber = GetRandomNumber(1, 50); // both inclusive
                    if (randomNumber == mutationChance)
                    {
                        /*Console.WriteLine("YATTA, MUTATION!!!");*/

                        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder(individual.Chromosome);
                        if (individual.Chromosome[i] == '1')
                            strBuilder[i] = '0';
                        else
                            strBuilder[i] = '1';
                        individual.Chromosome = strBuilder.ToString();
                    }
                }
            }
            return currentPopulation;
        }

        public bool checkCondition(List<Individual> posterityPopulation)
        {
            bool isItDone = false;
            foreach (Individual individual in posterityPopulation)
            {
                isItDone = individual.Chromosome == typeOfBombBin ? true : false;
                /*Console.WriteLine("MAMY ZWYCIEZCE : {0}", isItDone);*/
            }
            return isItDone;
        }

        public string bestChromosome(List<Individual> posterityPopulation)
        {
            string result = "";
            foreach (Individual individual in posterityPopulation)
            {
                if (individual.Chromosome == typeOfBombBin) result = individual.Chromosome;
            }
            return result;
        }

        public string searchForResult(string valueStr, int valueInt) // value = currentBomb
        {
            typeOfBomb = valueInt;
            typeOfBombBin = valueStr;

            bool alreadyFound = false;
            Genetic processAlgorithm = new Genetic();
            List<Individual> parentalPopulation;

            parentalPopulation = processAlgorithm.Initialization();
            parentalPopulation = processAlgorithm.FitnessFunction(parentalPopulation);

            do
            {
                // !!! WARUNEK STOPU
                alreadyFound = processAlgorithm.checkCondition(parentalPopulation);

                if (alreadyFound == false)
                {
                    List<Individual> posterityPopulation = new List<Individual>();
                    posterityPopulation = processAlgorithm.Selection(parentalPopulation, posterityPopulation);

                    List<Individual> crossoverdPopulation = new List<Individual>();
                    crossoverdPopulation = processAlgorithm.Crossover(posterityPopulation);
                    crossoverdPopulation = processAlgorithm.Mutation(crossoverdPopulation);
                    crossoverdPopulation = processAlgorithm.FitnessFunction(crossoverdPopulation);

                    parentalPopulation.Clear();
                    parentalPopulation = crossoverdPopulation;
                }
            }
            while (alreadyFound == false);

            string resultOfAlg = "";
            resultOfAlg = processAlgorithm.bestChromosome(parentalPopulation);

            //Console.WriteLine("WYNIK = {0} ", resultOfAlg);
            return resultOfAlg;
        }
    }

    public class GeneticAlg : IDataManipulation
    {
        private readonly Task _learnGenTask;
        public GeneticAlg() 
        {
            _learnGenTask = GenericGen();
        }
        public async Task<Tuple<Disarming, Disarming, Disarming>> GetDisarmingProcedure(int beepsLevel)
        {
                await _learnGenTask;
                Genetic findResultForBomb = new Genetic();
                bool foundResult = false;
                string currentBomb = "";
                var result = new Disarming[3];

                switch (beepsLevel)
                {
                    case 1: currentBomb = "10000"; break;
                    case 2: currentBomb = "01000"; break;
                    case 3: currentBomb = "00100"; break;
                    case 4: currentBomb = "00010"; break;
                    case 5: currentBomb = "00001"; break;
                    default: currentBomb = "00000"; break;
                }

                //Console.WriteLine("Wkładam = {0}, {1}", currentBomb, beepsLevel);
                string resultOfFunction = findResultForBomb.searchForResult(currentBomb, beepsLevel);
                
                if(resultOfFunction == currentBomb)
                    foundResult = true;

                if(foundResult == true)
                {
                    switch (resultOfFunction)
                    {
                        case "10000": //Ball
                            result[0] = Disarming.CutControlWire;
                            result[1] = Disarming.CutYellowWire;
                            result[2] = Disarming.CutRedWire;
                            break;
                        case "01000": //DemolitionBomb
                            result[0] = Disarming.CutBlueWire;
                            result[1] = Disarming.CutGreenWire;
                            result[2] = Disarming.CutRedWire;
                            break;
                        case "00100": //DemolitionExplosiveBomb
                            result[0] = Disarming.CutGreenWire;
                            result[1] = Disarming.CutControlWire;
                            result[2] = Disarming.CutYellowWire;
                            break;
                        case "00010": //ExplosiveBomb
                            result[0] = Disarming.CutRedWire;
                            result[1] = Disarming.CutGreenWire;
                            result[2] = Disarming.CutBlueWire;
                            break;
                        case "00001": //Mine
                            result[0] = Disarming.CutRedWire;
                            result[1] = Disarming.CutGreenWire;
                            result[2] = Disarming.CutBlueWire;
                            break;
                    }
                }

               return Tuple.Create(result[0], result[1], result[2]);

        }

        async Task GenericGen() { }
    }
}
