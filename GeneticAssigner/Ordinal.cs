using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAssigner {
	public class Ordinal {

		string locale = "es";

		public Ordinal() { }

		public Ordinal(string locale) {
			this.locale = locale;
		}

		public string this[int i] {
			get {
				if(locale.ToLower() == "es")
					return ordinalEs(i);
				else if(locale.ToLower() == "en")
					return ordinalEn(i);
				else
					throw new NotImplementedException("locale '" + locale + "'");
			}
		}

		private string ordinalEs(int i) {
			switch(i) {
				case 1: return "Primera";
				case 2: return "Segunda";
				case 3: return "Tercera";
				case 4: return "Cuarta";
				case 5: return "Quinta";
				case 6: return "Sexta";
				case 7: return "Séptima";
				case 8: return "Octaba";
				case 9: return "Novena";
				case 10: return "Décima";
				default:
					throw new NotImplementedException("Sólo números positivos menores que diez.");
			}
		}

		private string ordinalEn(int i) {
			switch(i) {
				case 1: return "First";
				case 2: return "Second";
				case 3: return "Third";
				case 4: return "Fourth";
				case 5: return "Fifth";
				case 6: return "Sixth";
				case 7: return "Seventh";
				case 8: return "Eighth";
				case 9: return "Ninth";
				case 10: return "Tenth";
				default:
					throw new NotImplementedException("Only positive numbers below ten.");
			}
		}



	}
}
