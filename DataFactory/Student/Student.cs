using System;
using System.Collections.Generic;
using System.Text;

namespace DataFactory {
	public class Student: Identifiable {

		internal Student(int id, string name, params int[] options) {
			this.id = id;
			this.name = name;
			this.options = options;
			this.UnAssign();
		}

		int[] options;
		string name;
		int id;
		int assignedCourse;
		int assignedOption;

		public int Id {
			get { return id; }
		}
		public int[] Options {
			get { return options; }
		}
		public string Name {
			get { return name; }
		}
		public int AssignedCourse {
			get { return assignedCourse; }
		}
		public bool Assigned {
			get { return assignedCourse != 0; }
		}
		public int AssignedOption {
			get { return assignedOption; }
		}

		public void AssignOption(int option) {
			assignedOption = option;
			assignedCourse = options[option];
		}

		internal void UnAssign() {
			assignedCourse = 0;
			assignedOption = -1;
		}

		public override string ToString() {
			string options = "";
			for(int i = 0;i < Options.Length;i++) {
				if(Assigned && AssignedOption == i)
					options += "[" + Options[i] + "] ";
				else
					options += Options[i] + " ";
			}
			return Id + " | " + Name + " | " + options.Trim();
		}

		internal Student Clone() {
			Student student = new Student(this.id, this.name, new int[this.options.Length]);
			Array.Copy(this.options, student.options, this.options.Length);
			student.assignedCourse = this.assignedCourse;
			student.assignedOption = this.assignedOption;
			
			return student;

		}

	}
}
