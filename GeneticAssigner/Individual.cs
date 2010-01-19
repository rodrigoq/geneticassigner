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
using GeneticAlgorithm;
using DataFactory;


namespace GeneticAssigner {
	public class Individual: IIndividual {

		public Individual() {
			Students = new List<int>();
		}

		public int[] Options { get; set; }
		public int NotAssigned { get; set; }
		public int Assigned { get; set; }
		public double Fitness { get; set; }
		public double NormFitness { get; set; }
		public List<int> Students { get; set; }

		private void Swap(int i, int j) {
			int value = Students[i];
			Students[i] = Students[j];
			Students[j] = value;
		}

		public void Mutate(Random random, double mutationRate) {
			if(random.NextDouble() < mutationRate) {
				int src = random.Next(0, Students.Count);
				int dst = random.Next(0, Students.Count);
				Swap(src, dst);
			}
		}

		public override string ToString() {
			string SPACE = " ";
			string s = NotAssigned.ToString("d3") + SPACE;
			for(int i = 0;i < Options.Length;i++)
				s += Options[i].ToString("d3") + SPACE;

			s += Fitness.ToString() + SPACE + NormFitness.ToString();
			return s;
		}

	}
}
