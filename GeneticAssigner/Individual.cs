/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2020  Rodrigo Queipo <rodrigoq@gmail.com>
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
using GeneticAlgorithm;
using DataFactory;


namespace GeneticAssigner
{
	public class Individual : IIndividual
	{

		public Individual()
		{
			Students = new List<int>();
		}

		public int[] Options { get; set; }
		public int NotAssigned { get; set; }
		public int Assigned { get; set; }
		public double Fitness { get; set; }
		public double NormFitness { get; set; }
		public List<int> Students { get; set; }

		private void Swap(int i, int j)
		{
			int value = Students[i];
			Students[i] = Students[j];
			Students[j] = value;
		}

		public void Mutate(Random random, double mutationRate)
		{
			if (random.NextDouble() < mutationRate)
			{
				int src = random.Next(0, Students.Count);
				int dst = random.Next(0, Students.Count);
				Swap(src, dst);
			}
		}

		public override string ToString()
		{
			string SPACE = " ";
			string s = NotAssigned.ToString("d3") + SPACE;
			for (int i = 0; i < Options.Length; i++)
			{
				s += Options[i].ToString("d3") + SPACE;
			}
			s += Fitness.ToString() + SPACE + NormFitness.ToString();
			return s;
		}

		public double FitnessFunction()
		{
			int notAssigned = Students.Count;
			Options = new int[Context.Places];

			Context.Courses.ResetPlacesLeft();

			for (int i = 0; i < Students.Count; i++)
			{
				int id = Students[i];

				int opt = Math.Min(Context.Places, Context.Students[id].Options.Length);

				for (int j = 0; j < opt; j++)
				{
					Course actual = Context.Courses[Context.Students[id].Options[j]];
					if (actual.PlacesLeft > 0)
					{
						Options[j]++;
						notAssigned--;
						actual.AssignPlace();
						break;
					}
				}
			}
			Assigned = Students.Count - notAssigned;
			NotAssigned = notAssigned;

			long value = Assigned * (long)Math.Pow(10, Options.Length * 3);
			for (int i = 0; i < Options.Length; i++)
			{
				value += Options[i] * (long)Math.Pow(10, (Options.Length - 1 - i) * 3);
			}
			//return Math.Log10(value);
			return Math.Log(value);
		}


		public static List<IIndividual> FirstGeneration(int size, int seed)
		{
			Random random = new Random(seed + 1);
			List<int> students0 = new List<int>();
			foreach (Student student in Context.Students)
			{
				students0.Add(student.Id);
			}
			List<IIndividual> thisGeneration = new List<IIndividual>();
			for (int i = 0; i < size; i++)
			{
				Individual ind = new Individual();
				ind.Students = Shuffle(random, students0);
				thisGeneration.Add(ind);
			}
			return thisGeneration;
		}

		private static List<int> Shuffle(Random random, List<int> students)
		{
			int n = students.Count;
			while (n > 1)
			{
				int k = random.Next(n);
				n--;
				Swap(students, n, k);
			}
			return new List<int>(students);
		}

		private static void Swap(List<int> students, int i, int j)
		{
			int value = students[i];
			students[i] = students[j];
			students[j] = value;
		}

	}
}
