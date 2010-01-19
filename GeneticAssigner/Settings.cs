/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008  Rodrigo Queipo <rodrigoq@gmail.com>
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
using System.Configuration;

namespace GeneticAssigner {
	public class Settings {
		public bool OpenFolder { get; set; }
		public int CantOpt { get; set; }
		public string CoursesPath { get; set; }
		public string StudentsPath { get; set; }
		public bool SaveFiles { get; set; }
		public bool KeepBest { get; set; }
		public string OutputPath { get; set; }
		public double MutationRate { get; set; }
		public int Generations { get; set; }
		public int Individuals { get; set; }
		
		public Settings() {
			Defaults();
		}

		/// <summary>
		/// Default start settings for the GA.
		/// </summary>
		public void Defaults() {
			SaveFiles = false;
			KeepBest = true;
			OpenFolder = true;
			MutationRate = 80;
			Generations = 2000;
			Individuals = 100;
			CantOpt = 3;
		}

		public void LoadFromArgs(string[] args) {
			foreach(string s in args) {
				switch(s[0]) {
					case 'c'://courses
						break;
					case 's'://students
						break;
					case 'v'://save files
						break;
					case 'o'://open folder
						break;
					case 'm'://mutation
						break;
					case 'g'://generations
						break;
					case 'i'://individuals
						break;
					case 'n'://cant options
						break;
					case 'k'://keep best
						break;
					case 'p'://output path
						break;
					case 'e'://seed
						break;
					default:
						break;
				}
			}
		}

		public void LoadFromFile() {
			CoursesPath = ConfigurationManager.AppSettings["courses"];

			StudentsPath = ConfigurationManager.AppSettings["students"];

			OpenFolder = bool.Parse(ConfigurationManager.AppSettings["open_folder"]);

			SaveFiles = bool.Parse(ConfigurationManager.AppSettings["save_files"]);

			OutputPath = ConfigurationManager.AppSettings["output_path"];

			MutationRate = double.Parse(ConfigurationManager.AppSettings["mutation_rate"]);

			Generations = int.Parse(ConfigurationManager.AppSettings["generations"]);

			Individuals = int.Parse(ConfigurationManager.AppSettings["individuals"]);

			CantOpt = int.Parse(ConfigurationManager.AppSettings["cant_opt"]);

			KeepBest = bool.Parse(ConfigurationManager.AppSettings["keep_best"]);
		}


	}
}
