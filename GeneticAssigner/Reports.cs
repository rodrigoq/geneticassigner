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
using System.Text;
using DataFactory;
using System.IO;
using GeneticAlgorithm;

namespace GeneticAssigner
{
	public class Reports
	{

		StudentCollection students;
		CourseCollection courses;
		string fileNamePrefix;
		string outPath;
		Ordinal ordinales = new Ordinal();

		public Reports(CourseCollection courses, StudentCollection students, string outPath, string fileNamePrefix)
		{
			this.courses = new CourseCollection(courses);
			this.students = new StudentCollection(students);
			this.fileNamePrefix = fileNamePrefix;
			this.outPath = outPath;
		}

		public void AlphabeticOrder()
		{
			StudentComparer studentComparer =
				new StudentComparer(StudentComparer.ComparisonType.Name);
			students.Sort(studentComparer);

			bool hayNoAssig = false;
			StringBuilder sb = new StringBuilder();
			sb.Append("<table border='1'><tr><th>Libreta</th><th>Nombre</th><th>Comisi�n</th></tr>");
			//StringBuilder sbNoAssig = new StringBuilder(sb.ToString());
			StringBuilder sbNoAssig = new StringBuilder();
			sbNoAssig.Append("<table border='1'><tr><th colspan='2'>No Asignado</th></tr>");
			sbNoAssig.Append("<tr><th>Libreta</th><th>Nombre</th></tr>");

			foreach (Student student in students)
			{
				if (student.Assigned)
				{
					sb.Append("<tr><td>").Append(student.Id).Append("</td><td>")
						.Append(student.Name).Append("</td><td>")
						.Append(student.AssignedCourse).Append("</td></tr>");
				}
				else
				{
					hayNoAssig = true;
					sbNoAssig.Append("<tr><td>").Append(student.Id).Append("</td><td>")
						.Append(student.Name).Append("</td></tr>");
				}
			}
			sb.Append("</table><br />");
			if (hayNoAssig)
			{
				sb.Append(sbNoAssig.Append("</table><br />"));
			}
			using (StreamWriter sw =
				new StreamWriter(outPath + fileNamePrefix + "report_alumnos.html", false, Encoding.Default))
			{
				sw.Write(sb);
			}
		}

		public void CourseOrder()
		{
			StudentComparer studentComparer =
				new StudentComparer(StudentComparer.ComparisonType.AssignedCourseAndName);
			students.Sort(studentComparer);

			StringBuilder sb = new StringBuilder();

			int last = -1;
			foreach (Student student in students)
			{
				if (student.AssignedCourse != last)
				{
					if (sb.Length > 0)
					{
						sb.Append("</table><br />");
					}
					string asignado = GetAsignation(student);
					sb.Append("<table border='1'>");
					sb.Append("<tr><th colspan='2'>").Append(asignado).Append("</th></tr>");
					sb.Append("<tr><th>Libreta</th><th>Nombre</th></tr>");
					last = student.AssignedCourse;
				}

				sb.Append("<tr><td>").Append(student.Id).Append("</td><td>")
					.Append(student.Name).Append("</td></tr>");

			}
			sb.Append("</table><br />");

			using (StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "report_comisiones.html", false, Encoding.Default))
			{
				sw.Write(sb);
			}
		}

		private static string GetAsignation(Student student)
		{
			if (student.Assigned)
			{
				return "Comisi�n " + student.AssignedCourse.ToString();
			}
			else
			{
				return "No Asignado";
			}
		}

		public void PlacesLeft()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<table border='1'><tr><th>Comisi�n</th><th>Instituci�n</th><th>Cupos</th></tr>");
			bool placesLeft = false;
			foreach (Course course in courses)
			{
				if (course.PlacesLeft > 0)
				{
					placesLeft = true;
					sb.Append("<tr><td>").Append(course.Id).Append("</td><td>")
						.Append(course.Name).Append("</td><td>").Append(course.PlacesLeft).Append("</td></tr>");
				}
			}
			sb.Append("</table><br />");

			if (placesLeft == false)
			{
				sb = new StringBuilder("No hay vacantes disponibles.");
			}
			using (StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "report_vacantes.html", false, Encoding.Default))
			{
				sw.Write(sb);
			}

		}


		public void Summary(IIndividual best, int bestGeneration, int populationSize, string time)
		{
			StringBuilder sb = new StringBuilder();

			int disponibles = courses.TotalPlaces - best.Assigned;

			string generaciones = GetPluralGeneraciones(bestGeneration);
			string individuos = GetPluralIndividuos(populationSize);
			sb.Append("Se encontr� la mejor distribuci�n en ")
				.Append(bestGeneration).Append(generaciones).Append(" de ")
				.Append(populationSize).Append(individuos).Append(" cada una en ").Append(time)
				.Append(" segundos.").AppendLine();

			sb.AppendLine();

			sb.Append("* Total de alumnos a inscribir: ").Append(best.Students.Count).AppendLine();
			sb.Append("* Total de vacantes: ").Append(courses.TotalPlaces).AppendLine();
			double prcnt = best.NotAssigned / (double)best.Students.Count * 100;
			prcnt = Math.Round(prcnt, 2);

			sb.Append("* Alumnos no asignados: ").Append(best.NotAssigned).Append(" (").Append(prcnt).Append("%)").AppendLine();

			sb.AppendLine();

			sb.Append("* Vacantes disponibles: ").Append(disponibles).AppendLine();

			sb.AppendLine();

			for (int i = 0; i < best.Options.Length; i++)
			{
				if (best.Options[i] > 0)
				{
					prcnt = best.Options[i] / (double)best.Students.Count * 100;
					prcnt = Math.Round(prcnt, 2);
					sb.Append("* Alumnos asignados a la ").Append(ordinales[i + 1].ToLower()).Append(" opci�n: ").Append(best.Options[i]).Append(" (").Append(prcnt).Append("%)").AppendLine();
				}
			}

			using (StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "summary.txt", false, Encoding.Default))
			{
				sw.Write(sb);
			}

		}

		private static string GetPluralIndividuos(int populationSize)
		{
			if (populationSize == 1)
			{
				return " individuo";
			}
			else
			{
				return " individuos";
			}
		}

		private static string GetPluralGeneraciones(int bestGeneration)
		{
			if (bestGeneration == 1)
			{
				return " generaci�n";
			}
			else
			{
				return " generaciones";
			}
		}

		public void Log(IIndividual best, StringBuilder sb)
		{
			StringBuilder header = new StringBuilder();
			header.Append("\"Generaci�n\";\"Fitness\";\"No asignados\";");
			for (int i = 0; i < best.Options.Length; i++)
			{
				header.Append("\"").Append(ordinales[i + 1]).Append("\";");
			}
			header.AppendLine();
			header.Append(sb);
			using (StreamWriter outputLog = new StreamWriter(outPath + fileNamePrefix + "log.csv", false, Encoding.Default))
			{
				outputLog.Write(header);
			}
		}

		public void Result(StringBuilder sb)
		{
			StringBuilder header = new StringBuilder();
			header.AppendLine("libreta;nombre;comisi�n;opci�n");
			header.Append(sb);
			using (StreamWriter result = new StreamWriter(outPath + fileNamePrefix + "result.csv", false, Encoding.Default))
			{
				result.Write(header);
			}
		}
	}
}
