/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2010  Rodrigo Queipo <rodrigoq@gmail.com>
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

namespace GeneticAlgorithm
{

	#region Delegates

	public delegate void BestDelegate(object sender, GenerationEventArgs e);
	public delegate void GenerationDelegate(object sender, GenerationEventArgs e);
	public delegate void CompletedDelegate(object sender, EventArgs e);
	public delegate void StoppedDelegate(object sender, EventArgs e);

	#endregion

	public class GeneticAlgorithm<T> where T: IIndividual, new()
	{

		public event BestDelegate onBest;
		public event GenerationDelegate onGeneration;
		public event CompletedDelegate onComplete;
		public event StoppedDelegate onStop;

		private List<IIndividual> thisGeneration = new List<IIndividual>();
		private List<double> fitnessTable = new List<double>();

		private bool stop;
		private bool isRunning;
		private Random random;
		private double totalFitness;

		public IIndividual Best { get; private set; }
		public int Seed { get; private set; }
		public int Generation { get; private set; }
		public double MutationRate { get; set; }
		public int PopulationCount { get; set; }
		public int GenerationLength { get; set; }
		public bool Elitism { get; set; }

		public GeneticAlgorithm(List<IIndividual> firstGeneration, int seed)
		{
			this.thisGeneration =
				new List<IIndividual>(firstGeneration);

			this.MutationRate = 0.80;
			this.Elitism = true;
			this.Seed = seed;

			random = new Random(seed);
		}

		public void Start()
		{
			try
			{
				isRunning = true;
				RankPopulation();
				for(int i = 0;i < GenerationLength;i++)
				{
					if(stop)
					{
						if(onStop != null)
						{
							onStop(this, new EventArgs());
						}
						return;
					}

					CreateNextGeneration();

					RankPopulation();

					Generation = i + 1;

					SetBest();

					if(onGeneration != null)
					{
						onGeneration(this,
							new GenerationEventArgs(Generation,
								thisGeneration[PopulationCount - 1]));
					}
				}

				if(onComplete != null)
				{
					onComplete(this, new EventArgs());
				}
			}
			finally
			{
				isRunning = false;
				stop = false;
			}
		}

		public void Stop()
		{
			if(isRunning)
			{
				stop = true;
			}
		}


		private void SetBest()
		{
			IIndividual ind = thisGeneration[PopulationCount - 1];

			if(Best == null || Best.Fitness < ind.Fitness)
			{
				Best = new T();
				Best.Students = new List<int>(ind.Students);

				Best.NotAssigned = ind.NotAssigned;
				Best.Assigned = ind.Assigned;

				Best.Fitness = ind.Fitness;
				Best.NormFitness = ind.NormFitness;

				Best.Options = new List<int>(ind.Options).ToArray();

				if(onBest != null)
				{
					onBest(this, new GenerationEventArgs(Generation, Best));
				}
			}
		}

		//TODO: think how to add crossing over... and if it is a necesity.
		private void CreateNextGeneration()
		{
			List<IIndividual> nextGeneration = new List<IIndividual>();

			IIndividual ind = null;
			if(Elitism)
			{
				ind = new T();
				ind.Students =
					new List<int>(thisGeneration[PopulationCount - 1].Students);
			}
			for(int i = 0;i < PopulationCount;i++)
			{
				IIndividual parent = thisGeneration[RouletteSelection()];
				IIndividual child = new T();
				child.Students = new List<int>(parent.Students);
				child.Mutate(random, MutationRate);
				nextGeneration.Add(child);
			}
			thisGeneration = new List<IIndividual>(nextGeneration);

			if(Elitism && ind != null)
			{
				nextGeneration[0] = ind;
			}
		}

		private void RankPopulation()
		{
			for(int i = 0;i < PopulationCount;i++)
			{
				thisGeneration[i].Fitness = thisGeneration[i].FitnessFunction();
			}
			thisGeneration.Sort(new FitnessComparer());

			totalFitness = 0;
			fitnessTable.Clear();
			for(int i = 0;i < PopulationCount;i++)
			{
				if(thisGeneration[thisGeneration.Count - 1].Fitness - thisGeneration[0].Fitness > 0)
				{
					thisGeneration[i].NormFitness =
						(double)(thisGeneration[i].Fitness - thisGeneration[0].Fitness) /
						(double)(thisGeneration[thisGeneration.Count - 1].Fitness - thisGeneration[0].Fitness);
				}
				else
				{
					thisGeneration[i].NormFitness = 1.0;
				}
				totalFitness += thisGeneration[i].NormFitness;
				fitnessTable.Add(totalFitness);
			}

			/*
			//DEBUGGING
			using(System.IO.StreamWriter sw = new System.IO.StreamWriter("fitnesses.txt", true, Encoding.Default)) 
			{
				for(int i = 0;i < thisGeneration.Count;i++)
				{
					sw.WriteLine(generation + "\t" + i + "\t" + thisGeneration[i].ToString());
				}
			}*/
		}

		private int RouletteSelection()
		{
			int index =
				fitnessTable.BinarySearch(random.NextDouble() * totalFitness);
			return (index < 0) ? ~index : index;
		}

	}
}
