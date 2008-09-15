using System;
using System.Collections.Generic;
using System.Text;

namespace DataFactory {
	public class CourseCollection: Collection<Course> {

		public CourseCollection() : base() { }
		public CourseCollection(CourseCollection collection) {
			foreach(Course centro in collection)
				this.list.Add(centro.Clone());

			this.TotalPlaces = collection.TotalPlaces;
		}

		public void ResetPlacesLeft() {
			for(int i = 0;i < list.Count;i++)
				list[i].ResetPlacesLeft();
		}

		int totalPlaces;

		public int TotalPlaces {
			get { return totalPlaces; }
			set { totalPlaces = value; }
		}

	}
}
