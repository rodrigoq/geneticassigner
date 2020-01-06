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
using System.Collections.Generic;

namespace GeneticAlgorithm
{
	public sealed class FitnessComparer : IComparer<IIndividual>
	{
		public bool UseOldComparer { get; private set; }

		public FitnessComparer(bool useOldComparer = false)
		{
			UseOldComparer = useOldComparer;
		}

		public int Compare(IIndividual x, IIndividual y)
		{
			var cmp = x.Fitness.CompareTo(y.Fitness);
			if (UseOldComparer || cmp != 0)
				return cmp;

			for (int i = 0; i < x.Students.Count; i++)
			{
				var cmpStudent = x.Students[i].CompareTo(y.Students[i]);
				if (cmpStudent != 0)
					return cmpStudent;
			}
			return cmp;
		}
	}
}
