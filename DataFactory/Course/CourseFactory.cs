/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2018  Rodrigo Queipo <rodrigoq@gmail.com>
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

namespace DataFactory
{
	public class CourseFactory : Factory
	{

		public static CourseCollection CreateFromFile(string file)
		{
			return CreateFromArray(GetLineArray(file));
		}

		private static CourseCollection CreateFromArray(string[] lines)
		{
			CourseCollection cc = new CourseCollection();

			int totalPlaces = 0;
			for (int i = 0; i < lines.Length; i++)
			{
				string[] tokens = lines[i].Split(
					new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

				if (tokens.Length != 3)
					throw new Exception("Malformed line " + i);

				int places = int.Parse(tokens[2]);
				cc.Add(new Course(int.Parse(tokens[0]), tokens[1], places));

				totalPlaces += places;

			}
			cc.TotalPlaces = totalPlaces;
			return cc;
		}
	}
}
