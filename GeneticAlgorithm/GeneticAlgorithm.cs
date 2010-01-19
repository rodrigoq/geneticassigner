/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008  Rodrigo Queipo <rodrigoq@gmail.com>
*
*   This program is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   This program is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;

namespace GeneticAlgorithm {

	#region Delegates

	public delegate void BestDelegate(object sender, GenerationEventArgs e);
	public delegate void GenerationDelegate(object sender, GenerationEventArgs e);
	public delegate void CompletedDelegate(object sender, EventArgs e);
	public delegate double FitnessDelegate(IIndividual individual);

	#endregion

	public class GeneticAlgorithm<T> where T: IIndividual, new() {

		public event BestDelegate onBest;
		public event GenerationDelegate onGeneration;
		public event CompletedDelegate onComplete;
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
		private int seed;

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
		public int Seed {
			get { return seed; }
		}


		public GeneticAlgorithm(FitnessDelegate fitnessFunction, List<IIndividual> firstGeneration, int seed) {
			this.fitnessFunction = fitnessFunction;
			this.thisGeneration = new List<IIndividual>(firstGeneration);

			this.mutationRate = 0.80;
			this.elitism = true;
			this.seed = seed;

			random = new Random(seed);
		}

		public void Start() {
			RankPopulation();
			for(int i = 0;i < generationLength;i++) {
				CreateNextGeneration();

				RankPopulation();

				generation = i + 1;

				SetBest();

				if(onGeneration != null)
					onGeneration(this, new GenerationEventArgs(this.generation, thisGeneration[populationCount - 1]));
			}

			if(onComplete != null)
				onComplete(this, new EventArgs());
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
					onBest(this, new GenerationEventArgs(this.generation, best));
			}
		}

		//TODO: think how to add crossing over... and if it is a necesity.
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
			using(System.IO.StreamWriter sw = new System.IO.StreamWriter("fitnesses.txt", true, Encoding.Default)) {
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
