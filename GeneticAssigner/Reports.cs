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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using DataFactory;
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
		Settings settings;

		public Reports(CourseCollection courses, StudentCollection students, 
			Settings settings, string outPath, string fileNamePrefix)
		{
			this.courses = new CourseCollection(courses);
			this.students = new StudentCollection(students);
			this.settings = settings;
			this.fileNamePrefix = fileNamePrefix;
			this.outPath = outPath;
		}

		public void AlphabeticOrder()
		{
			var template = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.docx");
			if(File.Exists(template) == false)
			{
				AlphabeticOrderHtml();
				return;
			}

			StudentComparer studentComparer =
				new StudentComparer(StudentComparer.ComparisonType.Name);
			students.Sort(studentComparer);

			var assigned = StudentsToArray(students.Where(x => x.Assigned), true);
			var noAssigned = StudentsToArray(students.Where(x => x.Assigned == false), false);

			var headerAssigned = new string[] { "", "APELLIDO Y NOMBRES", 
				"Libreta" + Environment.NewLine + "Universitaria", 
				"CH" + Environment.NewLine + "Asignada" };

			var headerNoAssigned = new string[headerAssigned.Length - 1];
			Array.Copy(headerAssigned, headerNoAssigned, headerNoAssigned.Length);

			var dest = Path.Combine(outPath, fileNamePrefix + "report_alumnos.docx");
			if (File.Exists(dest))
				File.Delete(dest);
			File.Copy(template, dest);
			using (var wr = new WordReport(dest))
			{
				ReplaceFields(wr);
				if(noAssigned.Length > 0)
					wr.AddTable("No Asignados", headerNoAssigned, noAssigned, false);
				if(assigned.Length > 0)
					wr.AddTable("Asignados", headerAssigned, assigned, true);
			}
		}

		public void AlphabeticOrderHtml()
		{
			StudentComparer studentComparer =
				new StudentComparer(StudentComparer.ComparisonType.Name);
			students.Sort(studentComparer);

			bool hayNoAssig = false;
			StringBuilder sb = new StringBuilder();
			sb.Append("<!doctype html><html><head><meta charset='utf-8'></head><body>");
			sb.Append("<table border='1'><tr><th>Libreta</th><th>Nombre</th><th>Comisión</th></tr>");
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
			sb.Append("</table><br>");
			if (hayNoAssig)
			{
				sb.Append(sbNoAssig.Append("</table><br>"));
			}
			sb.Append("</body></html>");
			using (StreamWriter sw =
				new StreamWriter(Path.Combine(outPath, fileNamePrefix + "report_alumnos.html"), false, Encoding.Default))
			{
				sw.Write(sb);
			}
		}

		private void ReplaceFields(WordReport wr)
		{
			wr.Replace("[ANIO]", DateTime.Now.Year.ToString(), true);
			wr.Replace("[TANDA]", GetVarFromPath(), true);
		}

		private string GetVarFromPath()
		{
			var match = Regex.Match(settings.CoursesPath, 
				"PH_(\\d+)", RegexOptions.Compiled);
			if (match.Groups.Count == 2)
				return match.Groups[1].Value;
			return "";
		}

		private string[][] StudentsToArray(IEnumerable<Student> students, bool withAssignement)
		{
			var ret = new string[students.Count()][];
			var i = 0;
			foreach (var student in students)
			{
				var arr = new string[] { student.Name, student.Id.ToString(),
					student.AssignedCourse.ToString() };
				if (withAssignement == false)
					Array.Resize(ref arr, arr.Length - 1);

				ret[i++] = arr;
			}
			return ret;
		}

		public void CourseOrder()
		{
			var template = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.docx");
			if(File.Exists(template) == false)
			{
				CourseOrderHtml();
				return;
			}

			StudentComparer studentComparer =
				new StudentComparer(StudentComparer.ComparisonType.AssignedCourseAndName);
			students.Sort(studentComparer);

			StringBuilder sb = new StringBuilder();

			var groups = students.ToLookup(x => x.AssignedCourse);
			var dest = Path.Combine(outPath, fileNamePrefix + "report_comisiones.docx");
			if (File.Exists(dest))
				File.Delete(dest);
			File.Copy(template, dest);

			using (var wr = new WordReport(dest))
			{
				ReplaceFields(wr);
				var header = new string[] { "", "Nombre", "Libreta" };
				foreach (var group in groups)
				{
					var arr = StudentsToArray(group, false);
					wr.AddTable(GetAsignation(group.FirstOrDefault()), header, arr, false);
				}
			}
		}

		public void CourseOrderHtml()
		{
			StudentComparer studentComparer =
				new StudentComparer(StudentComparer.ComparisonType.AssignedCourseAndName);
			students.Sort(studentComparer);

			StringBuilder sb = new StringBuilder();
			sb.Append("<!doctype html><html><head><meta charset='utf-8'></head><body>");

			int last = -1;
			foreach (Student student in students)
			{
				if (student.AssignedCourse != last)
				{
					if (sb.Length > 0)
					{
						sb.Append("</table><br>");
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
			sb.Append("</table><br>");
			sb.Append("</body></html>");

			using (StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "report_comisiones.html", false, Encoding.Default))
			{
				sw.Write(sb);
			}
		}

		private static string GetAsignation(Student student)
		{
			if (student == null)
				return "";

			if (student.Assigned)
			{
				return "Comisión " + student.AssignedCourse.ToString();
			}
			else
			{
				return "No Asignado";
			}
		}

		public void PlacesLeft()
		{
			var template = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "template.docx");
			if(File.Exists(template) == false)
			{
				PlacesLeftHtml();
				return;
			}

			var dest = Path.Combine(outPath, fileNamePrefix + "report_vacantes.docx");

			if (File.Exists(dest))
				File.Delete(dest);
			File.Copy(template, dest);
			using (var wr = new WordReport(dest))
			{
				ReplaceFields(wr);
				var header = new string[] { "Comisión", "Institución", "Cupos" };

				var arr = CoursesToArray(courses.Where(x => x.PlacesLeft > 0));
				if (arr.Length > 0)
					wr.AddTable("Vacantes", header, arr, true, false);
				else
					wr.AddText("No hay vacantes disponilbes");
			}
		}

		private void PlacesLeftHtml()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<!doctype html><html><head><meta charset='utf-8'></head><body>");
			sb.Append("<table border='1'><tr><th>Comisión</th><th>Institución</th><th>Cupos</th></tr>");
			bool placesLeft = false;
			foreach (Course course in courses)
			{
				if (course.PlacesLeft > 0)
				{
					placesLeft = true;
					sb.Append("<tr><td>").Append(course.Id).Append("</td><td>")
						.Append(course.Name).Append("</td><td>")
						.Append(course.PlacesLeft).Append("</td></tr>");
				}
			}
			sb.Append("</table><br>");
			sb.Append("</body></html>");

			if (placesLeft == false)
			{
				sb = new StringBuilder("<!doctype html><html><head><meta charset='utf-8'></head><body>");
				sb.Append("No hay vacantes disponibles.");
				sb.Append("</body></html>");
			}
			using (StreamWriter sw = new StreamWriter(Path.Combine(outPath,
				fileNamePrefix + "report_vacantes.html"), false, Encoding.Default))
			{
				sw.Write(sb);
			}
		}

		private string[][] CoursesToArray(IEnumerable<Course> courses)
		{
			var ret = new string[courses.Count()][];
			var i = 0;
			foreach (var course in courses)
				ret[i++] = new string[] { course.Id.ToString(), course.Name, course.PlacesLeft.ToString() };

			return ret;
		}

		public void Summary(IIndividual best, int bestGeneration, int populationSize, string time)
		{
			StringBuilder sb = new StringBuilder();

			int disponibles = courses.TotalPlaces - best.Assigned;

			string generaciones = GetPluralGeneraciones(bestGeneration);
			string individuos = GetPluralIndividuos(populationSize);
			sb.Append("Se encontró la mejor distribución en ")
				.Append(bestGeneration).Append(generaciones).Append(" de ")
				.Append(populationSize).Append(individuos).Append(" cada una en ").Append(time)
				.Append(" segundos.").AppendLine();

			sb.AppendLine();

			sb.Append("* Total de alumnos a inscribir: ").Append(best.Students.Count).AppendLine();
			sb.Append("* Total de vacantes: ").Append(courses.TotalPlaces).AppendLine();
			double prcnt = best.NotAssigned / (double)best.Students.Count * 100;
			prcnt = Math.Round(prcnt, 2);

			sb.Append("* Alumnos no asignados: ").Append(best.NotAssigned)
				.Append(" (").Append(prcnt).Append("%)").AppendLine();

			sb.AppendLine();

			sb.Append("* Vacantes disponibles: ").Append(disponibles).AppendLine();

			sb.AppendLine();

			for (int i = 0; i < best.Options.Length; i++)
			{
				if (best.Options[i] > 0)
				{
					prcnt = best.Options[i] / (double)best.Students.Count * 100;
					prcnt = Math.Round(prcnt, 2);
					sb.Append("* Alumnos asignados a la ").Append(ordinales[i + 1].ToLower())
						.Append(" opción: ").Append(best.Options[i])
						.Append(" (").Append(prcnt).Append("%)").AppendLine();
				}
			}

			using (StreamWriter sw = new StreamWriter(Path.Combine(outPath, 
				fileNamePrefix + "summary.txt"), false, Encoding.Default))
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
				return " generación";
			}
			else
			{
				return " generaciones";
			}
		}

		public void Log(IIndividual best, StringBuilder sb)
		{
			StringBuilder header = new StringBuilder();
			header.Append("\"Generación\";\"Fitness\";\"No asignados\";");
			for (int i = 0; i < best.Options.Length; i++)
			{
				header.Append("\"").Append(ordinales[i + 1]).Append("\";");
			}
			header.AppendLine();
			header.Append(sb);
			using (StreamWriter outputLog = new StreamWriter(Path.Combine(outPath,
				fileNamePrefix + "log.csv"), false, Encoding.Default))
			{
				outputLog.Write(header);
			}
		}

		public void Result(StringBuilder sb)
		{
			StringBuilder header = new StringBuilder();
			header.AppendLine("libreta;nombre;comisión;opción");
			header.Append(sb);
			using (StreamWriter result = new StreamWriter(Path.Combine(outPath,
				fileNamePrefix + "result.csv"), false, Encoding.Default))
			{
				result.Write(header);
			}
		}
	}
}
