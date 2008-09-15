using System;
using System.Collections.Generic;
using System.Text;

namespace DataFactory {
	public class StudentCollection: Collection<Student> {
		int maxOptions;

		public StudentCollection() : base() { }
		public StudentCollection(StudentCollection collection)  {
			foreach(Student student in collection) 
				list.Add(student.Clone());

			this.MaxOptions = collection.MaxOptions;
		}

		public void UnAssign() {
			for(int i = 0;i < list.Count;i++)
				list[i].UnAssign();
		}

		public int MaxOptions {
			get { return maxOptions; }
			set { maxOptions = value; }
		}
	}
}
