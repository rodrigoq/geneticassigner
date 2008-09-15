using System;
using System.Collections.Generic;
using System.Text;

namespace GA {

	#region Delegates

	public delegate void BestDelegate(int generation, IIndividual best);
	public delegate void GenerationDelegate(int generation, IIndividual individual);
	public delegate double FitnessDelegate(IIndividual individual);

	#endregion

	public class GeneticAlgorithm<T> where T: IIndividual, new() {

		public GeneticAlgorithm(FitnessDelegate fitnessFunction, List<IIndividual> firstGeneration, int seed) {
			this.fitnessFunction = fitnessFunction;
			this.thisGeneration = new List<IIndividual>(firstGeneration);

			this.mutationRate = 0.80;
			this.elitism = true;

			random = new Random(seed);
		}

		public event BestDelegate onBest;
		public event GenerationDelegate onGeneration;
		public FitnessDelegate fitnessFunction;

		private List<IIndividual> thisGeneration = new List<IIndividual>();
		private List<double> fitnessTable = new List<double>();

		private Random random;
		private IIndividual best;
		private double totalFitness;
		private int generation;
		private double mutationRate;
		private int populationCount;
		private int generationLength;
		private bool elitism;

		public double MutationRate {
			get { return mutationRate; }
			set { mutationRate = value; }
		}
		public int PopulationCount {
			get { return populationCount; }
			set { populationCount = value; }
		}
		public int GenerationLength {
			get { return generationLength; }
			set { generationLength = value; }
		}
		public bool Elitism {
			get { return elitism; }
			set { elitism = value; }
		}
		public IIndividual Best {
			get { return best; }
		}
		public int Generation {
			get { return generation; }
		}

		public void Start() {
			RankPopulation();
			for(int i = 0;i < generationLength;i++) {
				CreateNextGeneration();

				RankPopulation();

				generation = i + 1;

				SetBest();

				if(onGeneration != null)
					onGeneration(this.generation, thisGeneration[populationCount - 1]);
			}
		}

		private void SetBest() {
			IIndividual ind = thisGeneration[populationCount - 1];

			if(best == null || best.Fitness < ind.Fitness) {
				best = new T();
				best.Students = new List<int>(ind.Students);

				best.NotAssigned = ind.NotAssigned;
				best.Assigned = ind.Assigned;

				best.Fitness = ind.Fitness;
				best.NormFitness = ind.NormFitness;

				best.Options = new List<int>(ind.Options).ToArray();

				if(onBest != null)
					onBest(this.generation, best);
			}
		}

		//TODO: think how to add crossing over... and if is a necesity.
		private void CreateNextGeneration() {
			List<IIndividual> nextGeneration = new List<IIndividual>();

			IIndividual ind = null;
			if(elitism) {
				ind = new T();
				ind.Students = new List<int>(thisGeneration[populationCount - 1].Students);
			}
			for(int i = 0;i < populationCount;i++) {
				IIndividual parent = thisGeneration[RouletteSelection()];
				IIndividual child = new T();
				child.Students = new List<int>(parent.Students);
				child.Mutate(random, mutationRate);
				nextGeneration.Add(child);
			}
			thisGeneration = new List<IIndividual>(nextGeneration);

			if(elitism && ind != null)
				nextGeneration[0] = ind;

		}

		private void RankPopulation() {
			for(int i = 0;i < populationCount;i++)
				thisGeneration[i].Fitness = fitnessFunction(thisGeneration[i]);

			thisGeneration.Sort(new FitnessComparer());

			totalFitness = 0;
			fitnessTable.Clear();
			for(int i = 0;i < populationCount;i++) {
				if(thisGeneration[thisGeneration.Count - 1].Fitness - thisGeneration[0].Fitness > 0)
					thisGeneration[i].NormFitness = (double)(thisGeneration[i].Fitness - thisGeneration[0].Fitness) / (double)(thisGeneration[thisGeneration.Count - 1].Fitness - thisGeneration[0].Fitness);
				else
					thisGeneration[i].NormFitness = 1.0;

				totalFitness += thisGeneration[i].NormFitness;
				fitnessTable.Add(totalFitness);
			}

			/*//DEBUGGING
			using(System.IO.StreamWriter sw = new System.IO.StreamWriter("c:\\fitnesses.txt", true, Encoding.Default)) {
				for(int i = 0;i < thisGeneration.Count;i++)
					sw.WriteLine(generation + "\t" + i + "\t" + thisGeneration[i].ToString());
			}*/


		}

		private int RouletteSelection() {
			int index = fitnessTable.BinarySearch(random.NextDouble() * totalFitness);
			return (index < 0) ? ~index : index;
		}

	}
}
