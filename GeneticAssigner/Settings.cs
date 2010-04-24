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
using System;

namespace GeneticAssigner
{
	public class Settings
	{
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
		public int? Seed { get; set; }
		public bool Start { get; set; }
		public bool Exit { get; set; }

		public Settings()
		{
			Defaults();
		}

		/// <summary>
		/// Default start settings for the GA.
		/// </summary>
		public void Defaults()
		{
			SaveFiles = false;
			KeepBest = true;
			OpenFolder = true;
			MutationRate = 80;
			Generations = 2000;
			Individuals = 100;
			CantOpt = 3;
			Seed = null;
			Start = false;
			Exit = false;
		}

		public static Settings LoadFromArgs(string[] args)
		{
			if(args.Length > 0 && (args[0] == "--help" || args[0] == "-h"))
			{
				ShowUsage();
				return null;
			}
			Settings settings = new Settings();


			if(args.Length > 0 && args[0] == "start")
			{
				args[0] = " ";
				settings.Start = true;
			}

			if(args.Length > 0 && args[0] == "startexit")
			{
				args[0] = " ";
				settings.Start = true;
				settings.Exit = true;
			}

			foreach(string s in args)
			{
				switch(s[0])
				{
					case 'c'://courses
						settings.CoursesPath = s.Substring(1);
						break;
					case 's'://students
						settings.StudentsPath = s.Substring(1);
						break;
					case 'v'://save files
						settings.SaveFiles = ParseBool(s.Substring(1));
						break;
					case 'o'://open folder
						settings.OpenFolder = ParseBool(s.Substring(1));
						break;
					case 'm'://mutation
						settings.MutationRate = double.Parse(s.Substring(1));
						break;
					case 'g'://generations
						settings.Generations = int.Parse(s.Substring(1));
						break;
					case 'i'://individuals
						settings.Individuals = int.Parse(s.Substring(1));
						break;
					case 'n'://cant options
						settings.CantOpt = int.Parse(s.Substring(1));
						break;
					case 'k'://keep best
						settings.KeepBest = ParseBool(s.Substring(1));
						break;
					case 'p'://output path
						settings.OutputPath = s.Substring(1);
						break;
					case 'e'://seed
						settings.Seed = int.Parse(s.Substring(1));
						if(settings.Seed == int.MaxValue)
							throw new Exception("Seed value has to be smaller than " + int.MaxValue);
						break;
					default:
						break;
				}
			}
			return settings;
		}

		private static bool ParseBool(string s)
		{
			switch(s.ToLower())
			{
				case "1":
					return true;
				case "y":
					return true;
				case "s":
					return true;
				case "true":
					return true;
				case "0":
					return false;
				case "n":
					return false;
				case "false":
					return false;
				default:
					return bool.Parse(s);
			}
		}

		private static void ShowUsage()
		{
			string usage = @"Usage: GeneticAssigner [start|startexit] <parameters> [all are mandatory]
Commands:   
   start       Start processing automatically
   startexit   Start processing automatically and exits when finished

Parameters:
   c           courses file path
   s           students file path
   v           save files (0, 1)
   o           open folder (0, 1)
   m           mutation rate (0-100)
   g           number of generations
   i           number of individuals per generation
   n           number of options to choose from
   k           keep best (0, 1)
   p           output path
   e           seed

Example:
GeneticAssigner start cC:\courses.txt sC:\students.txt v1 o1 m80 pC:\ g2000 i100 n3 k1 e75168413
";
			System.Windows.Forms.MessageBox.Show(usage);
		}

		public static Settings LoadFromFile()
		{
			Settings settings = new Settings();
			settings.CoursesPath = ConfigurationManager.AppSettings["courses"];

			settings.StudentsPath = ConfigurationManager.AppSettings["students"];

			settings.OpenFolder = bool.Parse(ConfigurationManager.AppSettings["open_folder"]);

			settings.SaveFiles = bool.Parse(ConfigurationManager.AppSettings["save_files"]);

			settings.OutputPath = ConfigurationManager.AppSettings["output_path"];

			settings.MutationRate = double.Parse(ConfigurationManager.AppSettings["mutation_rate"]);

			settings.Generations = int.Parse(ConfigurationManager.AppSettings["generations"]);

			settings.Individuals = int.Parse(ConfigurationManager.AppSettings["individuals"]);

			settings.CantOpt = int.Parse(ConfigurationManager.AppSettings["cant_opt"]);

			settings.KeepBest = bool.Parse(ConfigurationManager.AppSettings["keep_best"]);

			if(ConfigurationManager.AppSettings["seed"] == null)
			{
				settings.Seed = null;
			}
			else
			{
				settings.Seed = int.Parse(ConfigurationManager.AppSettings["seed"]);
				if(settings.Seed == int.MaxValue)
					throw new Exception("Seed value has to be smaller than " + int.MaxValue);
			}
			return settings;
		}


	}
}
