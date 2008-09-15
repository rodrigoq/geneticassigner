using System;
using System.Collections.Generic;
using System.Text;
using DataFactory;
using System.IO;
using GA;

namespace GeneticAssigner {
	public class Reports {

		StudentCollection students;
		CourseCollection courses;
		string fileNamePrefix;
		string outPath;
		Ordinal ordinales = new Ordinal();

		public Reports(CourseCollection courses, StudentCollection students, string outPath, string fileNamePrefix) {
			this.courses = new CourseCollection(courses);
			this.students = new StudentCollection(students);
			this.fileNamePrefix = fileNamePrefix;
			this.outPath = outPath;
		}

		public void AlphabeticOrder() {
			students.Sort(delegate(Student a1, Student a2) { return a1.Name.CompareTo(a2.Name); });
			bool hayNoAssig = false;
			StringBuilder sb = new StringBuilder();
			sb.Append("<table border='1'><tr><th>Libreta</th><th>Nombre</th><th>Comisión</th></tr>");
			StringBuilder sbNoAssig = new StringBuilder(sb.ToString());

			foreach(Student student in students) {
				if(student.Assigned)
					sb.Append("<tr><td>").Append(student.Id).Append("</td><td>").Append(student.Name).Append("</td><td>").Append(student.AssignedCourse).Append("</td></tr>");
				else {
					hayNoAssig = true;
					sbNoAssig.Append("<tr><td>").Append(student.Id).Append("</td><td>").Append(student.Name).Append("</td><td>No Asignado</td></tr>");
				}
			}
			sb.Append("</table><br />");
			if(hayNoAssig)
				sb.Append(sbNoAssig.Append("</table><br />"));

			using(StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "report_alumnos.html", false, Encoding.Default)) {
				sw.Write(sb);
			}
		}

		public void CourseOrder() {
			students.Sort(delegate(Student a1, Student a2) { return a1.Name.CompareTo(a2.Name); });
			students.Sort(delegate(Student a1, Student a2) { return a1.AssignedCourse.CompareTo(a2.AssignedCourse); });

			StringBuilder sb = new StringBuilder();

			int last = -1;
			foreach(Student student in students) {
				if(student.AssignedCourse != last) {
					if(sb.Length > 0)
						sb.Append("</table><br />");

					sb.Append("<table border='1'>");
					sb.Append("<tr><th>Libreta</th><th>Nombre</th><th>Comisión</th></tr>");
					last = student.AssignedCourse;
				}
				string asignado;
				if(student.Assigned)
					asignado = student.AssignedCourse.ToString();
				else
					asignado = "No Asignado";
				sb.Append("<tr><td>").Append(student.Id).Append("</td><td>").Append(student.Name).Append("</td><td>").Append(asignado).Append("</td></tr>");

			}
			sb.Append("</table><br />");

			using(StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "report_comisiones.html", false, Encoding.Default)) {
				sw.Write(sb);
			}
		}

		public void PlacesLeft() {
			StringBuilder sb = new StringBuilder();
			sb.Append("<table border='1'><tr><th>Comisión</th><th>Institución</th><th>Cupos</th></tr>");

			foreach(Course course in courses) {
				if(course.PlacesLeft > 0)
					sb.Append("<tr><td>").Append(course.Id).Append("</td><td>").Append(course.Name).Append("</td><td>").Append(course.PlacesLeft).Append("</td></tr>");
			}
			sb.Append("</table><br />");

			using(StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "report_vacantes.html", false, Encoding.Default)) {
				sw.Write(sb);
			}

		}


		public void Summary(IIndividual best, int bestGeneration, int populationSize) {
			StringBuilder sb = new StringBuilder();

			int disponibles = courses.TotalPlaces - best.Assigned;

			sb.Append("Se encontró la mejor distribución en ").Append(bestGeneration).Append(" generaciones de ").Append(populationSize).Append(" individuos cada una.").AppendLine();

			sb.AppendLine();

			sb.Append("* Total de alumnos a inscribir: ").Append(best.Students.Count).AppendLine();
			sb.Append("* Total de vacantes: ").Append(courses.TotalPlaces).AppendLine();
			sb.Append("* Alumnos no asignados: ").Append(best.NotAssigned).AppendLine();

			sb.AppendLine();

			sb.Append("* Vacantes disponibles: ").Append(disponibles).AppendLine();

			sb.AppendLine();

			for(int i = 0;i < best.Options.Length;i++) {
				if(best.Options[i] > 0) {
					double prcnt = best.Options[i] / (double)best.Students.Count * 100;
					prcnt = Math.Round(prcnt, 2);
					sb.Append("* Alumnos asignados a la ").Append(ordinales[i + 1].ToLower()).Append(" opción: ").Append(best.Options[i]).Append(" (").Append(prcnt).Append("%)").AppendLine();
				}
			}

			using(StreamWriter sw = new StreamWriter(outPath + fileNamePrefix + "summary.txt", false, Encoding.Default)) {
				sw.Write(sb);
			}

		}

		public void Log(IIndividual best, StringBuilder log) {
			StringBuilder header = new StringBuilder();
			header.Append("\"Generación\";\"Fitness\";\"No asignados\";");
			for(int i = 0;i < best.Options.Length;i++)
				header.Append("\"").Append(ordinales[i + 1]).Append("\";");
			header.AppendLine();
			header.Append(log);
			using(StreamWriter outputLog = new StreamWriter(outPath + fileNamePrefix + "log.csv", false, Encoding.Default)) {
				outputLog.Write(header);
			}
		}

		public void Result(StringBuilder sb) {
			StringBuilder header = new StringBuilder();
			header.AppendLine("libreta;nombre;comisión;opción");
			header.Append(sb);
			using(StreamWriter result = new StreamWriter(outPath + fileNamePrefix + "result.csv", false, Encoding.Default)) {
				result.Write(header);
			}
		}


	}
}
