using System;
using System.Collections.Generic;
using System.Text;
using DataFactory;

namespace GeneticAssigner {
	/// <summary>
	/// Toma una asignación de alumnos y la cantidad de vacantes por centro
	/// y verifica que no haya inconsistencias:
	///  - Más alumnos que vacantes en una comisión.
	///  - Opción que no esté entre las del alumno.
	///  - Que no haya un alumno no asignado que tenga opciones en un centro con vacantes.
	/// </summary>
	public class Verifier {

		StudentCollection students;
		CourseCollection courses;

		public Verifier(CourseCollection courses, StudentCollection students) {
			this.courses = new CourseCollection(courses);
			this.students = new StudentCollection(students);
		}

		public void StudentsRepeatedOptions() {
			string repeated = "";
			foreach(Student student in students) {
				Array.Sort<int>(student.Options);
				for(int i = 1;i < student.Options.Length;i++) {
					if(student.Options[i] == student.Options[i - 1])
						repeated += student.ToString() + Environment.NewLine;
				}
			}
			if(!string.IsNullOrEmpty(repeated))
				throw new Exception(repeated);
		}

		public void StudentsInCourses() {
			string repeated = "";

			List<int> cour = new List<int>();
			foreach(Course course in courses)
				cour.Add(course.Id);

			foreach(Student student in students) {
				for(int i = 0;i < student.Options.Length;i++) {
					if(!cour.Contains(student.Options[i]))
						repeated += student.ToString() + " BAD COURSE " + student.Options[i] + Environment.NewLine;
				}
			}
			if(!string.IsNullOrEmpty(repeated))
				throw new Exception(repeated);
		}

		public void Verify() {
			Dictionary<int, int> coursePlaces = new Dictionary<int, int>();
			List<int> notAssignedOptions = new List<int>();
			foreach(Student student in students) {
				if(student.Assigned) {
					Predicate<int> contains = delegate(int value) { return value == student.AssignedCourse; };
					if(Array.Find<int>(student.Options, contains) < 0)
						throw new Exception(student.ToString() + " asignado a una opción no posible");

					if(coursePlaces.ContainsKey(student.AssignedCourse))
						coursePlaces[student.AssignedCourse]++;
					else
						coursePlaces.Add(student.AssignedCourse, 1);

				} else {
					for(int i = 0;i < student.Options.Length;i++) {
						if(!notAssignedOptions.Contains(student.Options[i]))
							notAssignedOptions.Add(student.Options[i]);
					}
				}
			}

			foreach(Course course in courses) {
				if(coursePlaces.ContainsKey(course.Id)) {
					if(coursePlaces[course.Id] > course.TotalPlaces)
						throw new Exception("Más alumnos asignados que vacantes disponibles, centro [" + course.Id + "]");

					if(coursePlaces[course.Id] < course.TotalPlaces && notAssignedOptions.Contains(course.Id))
						throw new Exception("Centro [" + course.Id + "] con vacantes para alumno no asignado");
				} else {
					if(course.TotalPlaces > 0 && notAssignedOptions.Contains(course.Id))
						throw new Exception("Centro [" + course.Id + "] con vacantes para alumno no asignado");
				}
			}
		}
	}
}
