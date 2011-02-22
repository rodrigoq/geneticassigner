/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2011  Rodrigo Queipo <rodrigoq@gmail.com>
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
	public class Course: Identifiable
	{

		internal Course(int id, string name, int totalPlaces)
		{
			if(totalPlaces < 0)
				throw new Exception("totalPlaces has to be greater than zero.");

			this.id = id;
			this.name = name;
			this.totalPlaces = totalPlaces;
			this.placesLeft = totalPlaces;
		}

		int id;
		string name;
		int totalPlaces;
		int placesLeft;

		public int Id
		{
			get { return id; }
		}
		public string Name
		{
			get { return name; }
		}
		public int TotalPlaces
		{
			get { return totalPlaces; }
		}
		public int PlacesLeft
		{
			get { return placesLeft; }
		}

		internal void ResetPlacesLeft()
		{
			placesLeft = totalPlaces;
		}

		public void AssignPlace()
		{
			if(placesLeft == 0)
				throw new Exception("No more places available.");

			placesLeft--;
		}

		public override string ToString()
		{
			return Id + " | " + Name + " | total: " + TotalPlaces + " | left: " + PlacesLeft;
		}


		internal Course Clone()
		{
			Course course = new Course(this.id, this.name, this.totalPlaces);
			course.placesLeft = this.placesLeft;
			return course;
		}

	}
}
