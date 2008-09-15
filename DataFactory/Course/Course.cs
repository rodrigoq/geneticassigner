using System;
using System.Collections.Generic;
using System.Text;

namespace DataFactory {
	public class Course: Identifiable {

		internal Course(int id, string name, int totalPlaces) {
			if(totalPlaces < 0)
				throw new Exception("totalPlaces has to be greater than cero.");

			this.id = id;
			this.name = name;
			this.totalPlaces = totalPlaces;
			this.placesLeft = totalPlaces;
		}

		int id;
		string name;
		int totalPlaces;
		int placesLeft;

		public int Id {
			get { return id; }
		}
		public string Name {
			get { return name; }
		}
		public int TotalPlaces {
			get { return totalPlaces; }
		}
		public int PlacesLeft {
			get { return placesLeft; }
		}

		internal void ResetPlacesLeft() {
			placesLeft = totalPlaces;
		}

		public void AssignPlace() {
			if(placesLeft == 0)
				throw new Exception("No more places available.");

			placesLeft--;
		}

		public override string ToString() {
			return Id + " | " + Name + " | total: " + TotalPlaces + " | left: " + PlacesLeft;
		}


		internal Course Clone() {
			Course course = new Course(this.id, this.name, this.totalPlaces);
			course.placesLeft = this.placesLeft;
			return course;
		}

	}
}
