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
using System.Text;
using DataFactory;
using GA;


namespace DataFactory {
	public class Individual: IIndividual {

		public Individual() { }

		List<int> students = new List<int>();
		int notAssigned;
		double fitness;
		double normFitness;
		int assigned;
		int[] options;

		public int[] Options {
			get { return options; }
			set { options = value; }
		}

		public List<int> Students {
			get { return students; }
			set { students = value; }
		}

		public int NotAssigned {
			get { return notAssigned; }
			set { notAssigned = value; }
		}
		public int Assigned {
			get { return assigned; }
			set { assigned = value; }
		}


		public double Fitness {
			get { return fitness; }
			set { fitness = value; }
		}
		public double NormFitness {
			get { return normFitness; }
			set { normFitness = value; }
		}

		private void Swap(int i, int j) {
			int value = students[i];
			students[i] = students[j];
			students[j] = value;
		}

		public void Mutate(Random random, double mutationRate) {
			if(random.NextDouble() < mutationRate) {
				int src = random.Next(0, students.Count);
				int dst = random.Next(0, students.Count);
				Swap(src, dst);
			}
		}

		public override string ToString() {
			string s = NotAssigned.ToString("d3") + " ";
			for(int i = 0;i < options.Length;i++)
				s += options[i].ToString("d3") + " ";

			s += fitness.ToString() + " " + normFitness.ToString();
			return s;
		}

	}
}
