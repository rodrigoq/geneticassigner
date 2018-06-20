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
using System.Collections.Generic;

namespace DataFactory
{
	public class StudentFactory : Factory
	{

		public static StudentCollection CreateFromFile(string file)
		{
			return CreateFromArray(GetLineArray(file));
		}

		public static StudentCollection CreateFromAssignedFile(
			string file, string assignedFile)
		{

			StudentCollection assignedStudents =
				CreateFromAssignedArray(GetLineArray(assignedFile));

			StudentCollection students = CreateFromArray(GetLineArray(file));
			int maxOptions = 0;

			foreach (Student student in students)
			{
				student.AssignOption(assignedStudents[student.Id].AssignedOption);
				if (student.Options.Length > maxOptions)
					maxOptions = student.Options.Length;
			}
			students.MaxOptions = maxOptions;
			return students;
		}

		private static StudentCollection CreateFromAssignedArray(string[] lines)
		{
			StudentCollection ac = new StudentCollection();

			for (int i = 0; i < lines.Length; i++)
			{
				string[] tokens = lines[i].Split(
					new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

				if (tokens.Length != 4)
					throw new Exception("Malformed line " + i);

				ac.Add(new Student(int.Parse(tokens[0]), tokens[1], int.Parse(tokens[3])));
			}
			return ac;
		}

		private static StudentCollection CreateFromArray(string[] lines)
		{
			StudentCollection sc = new StudentCollection();

			int maxOptions = 0;

			for (int i = 0; i < lines.Length; i++)
			{
				List<int> options = new List<int>();
				string[] tokens = lines[i].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
				if (tokens.Length < 3)
					throw new Exception("Malformed line " + i);

				for (int j = 2; j < tokens.Length; j++)
					options.Add(int.Parse(tokens[j]));

				sc.Add(new Student(int.Parse(tokens[0]), tokens[1], options.ToArray()));

				if (options.Count > maxOptions)
					maxOptions = options.Count;
			}

			sc.MaxOptions = maxOptions;

			return sc;
		}

		public static StudentCollection CreateRandomizedFromArray(Student[] students, Random rand)
		{
			StudentCollection sc = new StudentCollection();

			List<int> indices = new List<int>();
			while (indices.Count != students.Length)
			{
				int num = rand.Next(0, students.Length);
				if (indices.Contains(num) == false)
				{
					indices.Add(num);
					sc.Add(students[num]);
				}
			}
			return sc;
		}
	}
}
