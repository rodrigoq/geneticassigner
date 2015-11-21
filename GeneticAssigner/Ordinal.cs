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

namespace GeneticAssigner
{
	public class Ordinal
	{
		string locale = "es";

		public Ordinal() { }

		public Ordinal(string locale)
		{
			this.locale = locale;
		}

		public string this[int i]
		{
			get
			{
				if (locale.ToLower() == "es")
				{
					return ordinalEs(i);
				}
				else if (locale.ToLower() == "en")
				{
					return ordinalEn(i);
				}
				else
				{
					throw new Exception("locale '" + locale + "' Not Implemented.");
				}
			}
		}

		private static string ordinalEs(int i)
		{
			switch (i)
			{
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

		private static string ordinalEn(int i)
		{
			switch (i)
			{
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
