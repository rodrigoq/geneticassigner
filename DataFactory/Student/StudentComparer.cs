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

namespace DataFactory
{
	public class StudentComparer : IComparer<Student>
	{
		public enum ComparisonType
		{
			Name,
			AssignedCourseAndName
		}
		public ComparisonType CompType { get; set; }

		public StudentComparer(ComparisonType comparisonType)
		{
			CompType = comparisonType;
		}

		private static int CompareAssignedCourseAndName(Student x, Student y)
		{
			if (x.AssignedCourse == y.AssignedCourse)
			{
				return CompareName(x, y);
			}
			else
			{
				return x.AssignedCourse.CompareTo(y.AssignedCourse);
			}
		}

		private static int CompareName(Student x, Student y)
		{
			return x.Name.CompareTo(y.Name);
		}

		public int Compare(Student x, Student y)
		{
			switch (CompType)
			{
				case ComparisonType.Name:
					return CompareName(x, y);
				case ComparisonType.AssignedCourseAndName:
					return CompareAssignedCourseAndName(x, y);
				default:
					throw new Exception("unknown comparsion type.");
			}
		}
	}
}
