using System;
using System.Collections.Generic;
using System.Text;

namespace GA {
	public interface IIndividual {

		int[] Options { get; set; }

		List<int> Students { get; set; }

		int NotAssigned { get; set; }
		int Assigned { get; set; }

		double Fitness { get; set; }
		double NormFitness { get; set; }

		void Mutate(Random random, double mutationRate);

	}
}
