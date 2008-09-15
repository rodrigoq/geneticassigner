using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DataFactory {
	public class CourseFactory: Factory {

		public static CourseCollection CreateFromFile(string file) {
			return CreateFromArray(GetLineArray(file));
		}

		private static CourseCollection CreateFromArray(string[] lines) {
			CourseCollection cc = new CourseCollection();

			int totalPlaces = 0;
			for(int i = 0;i < lines.Length;i++) {
				string[] tokens = lines[i].Split(';');
				if(tokens.Length != 3)
					throw new Exception("Malformed line " + i);

				int places = int.Parse(tokens[2]);
				cc.Add(new Course(int.Parse(tokens[0]), tokens[1], places));

				totalPlaces += places;

			}
			cc.TotalPlaces = totalPlaces;
			return cc;
		}

	}
}
