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
using DataFactory;

namespace GeneticAssigner
{
	/// <summary>
	/// Antes de la asignación verifica:
	///  - Que el alumno no tenga opciones repetidas para un mismo curso.
	///  - Que el alumno no tenga opciones a cursos inexistentes.
	///  
	/// Luego con una asignación de alumnos y la cantidad de vacantes por centro,
	/// verifica que no haya inconsistencias:
	///  - Más alumnos que vacantes en una comisión.
	///  - Opción que no esté entre las del alumno.
	///  - Que no haya un alumno no asignado que tenga opciones en un centro con vacantes.
	/// </summary>
	public class Verifier
	{

		StudentCollection students;
		CourseCollection courses;

		public Verifier(CourseCollection courses, StudentCollection students)
		{
			this.courses = new CourseCollection(courses);
			this.students = new StudentCollection(students);
		}

		// Revisa que los estudiantes no tengan dos o más opciones para el mismo curso
		// ordena las opciones del alumno y compara las mismas.
		public void StudentsRepeatedOptions()
		{
			string repeated = string.Empty;
			foreach (Student student in students)
			{
				Array.Sort<int>(student.Options);
				for (int i = 1; i < student.Options.Length; i++)
				{
					if (student.Options[i] == student.Options[i - 1])
						repeated += student.ToString() + Environment.NewLine;
				}
			}
			if (string.IsNullOrEmpty(repeated) == false)
			{
				throw new Exception(repeated);
			}
		}

		public void StudentsInCourses()
		{
			string badCourses = string.Empty;

			List<int> courseIds = new List<int>();
			foreach (Course course in courses)
			{
				courseIds.Add(course.Id);
			}
			foreach (Student student in students)
			{
				for (int i = 0; i < student.Options.Length; i++)
				{
					if (courseIds.Contains(student.Options[i]) == false)
					{
						badCourses += student.ToString() + " BAD COURSE " + student.Options[i] + Environment.NewLine;
					}
				}
			}
			if (string.IsNullOrEmpty(badCourses) == false)
			{
				throw new Exception(badCourses);
			}
		}

		public void Verify()
		{
			Dictionary<int, int> coursePlaces = new Dictionary<int, int>();
			List<int> notAssignedOptions = new List<int>();
			foreach (Student student in students)
			{
				if (student.Assigned)
				{
					Predicate<int> contains = delegate (int value) { return value == student.AssignedCourse; };
					if (Array.Find<int>(student.Options, contains) < 0)
					{
						throw new Exception(student.ToString() + " asignado a una opción no posible");
					}
					if (coursePlaces.ContainsKey(student.AssignedCourse))
					{
						coursePlaces[student.AssignedCourse]++;
					}
					else
					{
						coursePlaces.Add(student.AssignedCourse, 1);
					}
				}
				else
				{
					int opt = Math.Min(Context.Places, student.Options.Length);

					for (int i = 0; i < opt; i++)
					{
						if (notAssignedOptions.Contains(student.Options[i]) == false)
						{
							notAssignedOptions.Add(student.Options[i]);
						}
					}
				}
			}

			foreach (Course course in courses)
			{
				if (coursePlaces.ContainsKey(course.Id))
				{
					if (coursePlaces[course.Id] > course.TotalPlaces)
					{
						throw new Exception("Más alumnos asignados que vacantes disponibles, centro [" + course.Id + "]");
					}
					if (coursePlaces[course.Id] < course.TotalPlaces && notAssignedOptions.Contains(course.Id))
					{
						throw new Exception("Centro [" + course.Id + "] con vacantes para alumno no asignado");
					}
				}
				else
				{
					if (course.TotalPlaces > 0 && notAssignedOptions.Contains(course.Id))
					{
						throw new Exception("Centro [" + course.Id + "] con vacantes para alumno no asignado");
					}
				}
			}
		}
	}
}
