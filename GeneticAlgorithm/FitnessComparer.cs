using System;
using System.Collections.Generic;
using System.Text;

namespace GA {
	public sealed class FitnessComparer: IComparer<IIndividual> {

		public int Compare(IIndividual x, IIndividual y) {
			if (x.Fitness > y.Fitness)
				return 1;
			else if (x.Fitness == y.Fitness)
				return 0;
			else
				return -1;
		}

	}
}
