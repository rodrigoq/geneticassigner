using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataFactory {
	public class StudentFactory: Factory {

		public static StudentCollection CreateFromFile(string file) {
			return CreateFromArray(GetLineArray(file));
		}

		public static StudentCollection CreateFromAssignedFile(string file, string assignedFile) {
			StudentCollection assignedStudents = CreateFromAssignedArray(GetLineArray(assignedFile));
			StudentCollection students = CreateFromArray(GetLineArray(file));
			int maxOptions = 0;
			
			foreach(Student student in students) {
				student.AssignOption(assignedStudents[student.Id].AssignedOption);
				if(student.Options.Length > maxOptions)
					maxOptions = student.Options.Length;
			}
			students.MaxOptions = maxOptions;
			return students;
		}

		private static StudentCollection CreateFromAssignedArray(string[] lines) {
			StudentCollection ac = new StudentCollection();

			for(int i = 0;i < lines.Length;i++) {
				string[] tokens = lines[i].Split(';');
				if(tokens.Length != 4)
					throw new Exception("Malformed line " + i);

				ac.Add(new Student(int.Parse(tokens[0]), tokens[1], int.Parse(tokens[3])));
			}
			return ac;
		}

		private static StudentCollection CreateFromArray(string[] lines) {
			StudentCollection sc = new StudentCollection();
			
			int maxOptions = 0;
			
			for(int i = 0;i < lines.Length;i++) {
				List<int> options = new List<int>();
				string[] tokens = lines[i].Split(';');
				if(tokens.Length < 3)
					throw new Exception("Malformed line " + i);

				for(int j = 2;j < tokens.Length;j++)
					options.Add(int.Parse(tokens[j]));

				sc.Add(new Student(int.Parse(tokens[0]), tokens[1], options.ToArray()));

				if(options.Count > maxOptions)
					maxOptions = options.Count;
			}

			sc.MaxOptions = maxOptions;

			return sc;
		}

		public static StudentCollection CreateRandomizedFromArray(Student[] students, Random rand) {
			StudentCollection sc = new StudentCollection();

			List<int> indices = new List<int>();
			while(indices.Count != students.Length) {
				int num = rand.Next(0, students.Length);
				if(!indices.Contains(num)) {
					indices.Add(num);
					sc.Add(students[num]);
				}
			}
			return sc;
		}

	}
}
