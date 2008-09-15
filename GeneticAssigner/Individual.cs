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
