using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace GeneticAssigner {
	public class Settings {
		string coursesPath;
		string studentsPath;
		bool saveFiles;
		bool openFolder;
		bool keepBest;
		string outputPath;
		double mutationRate;
		int generations;
		int individuals;
		int cantOpt;

		public bool OpenFolder {
			get { return openFolder; }
			set { openFolder = value; }
		}
		public int CantOpt {
			get { return cantOpt; }
			set { cantOpt = value; }
		}
		public string CoursesPath {
			get { return coursesPath; }
			set { coursesPath = value; }
		}
		public string StudentsPath {
			get { return studentsPath; }
			set { studentsPath = value; }
		}
		public bool SaveFiles {
			get { return saveFiles; }
			set { saveFiles = value; }
		}
		public bool KeepBest {
			get { return keepBest; }
			set { keepBest = value; }
		}
		public string OutputPath {
			get { return outputPath; }
			set { outputPath = value; }
		}
		public double MutationRate {
			get { return mutationRate; }
			set { mutationRate = value; }
		}
		public int Generations {
			get { return generations; }
			set { generations = value; }
		}
		public int Individuals {
			get { return individuals; }
			set { individuals = value; }
		}


		public Settings() {
			Defaults();
		}

		/// <summary>
		/// Default start settings for the GA.
		/// </summary>
		public void Defaults() {
			saveFiles = false;
			keepBest = true;
			openFolder = true;
			mutationRate = 80;
			generations = 2000;
			individuals = 100;
			cantOpt = 3;
		}

		public void LoadFromArgs(string args) {
			throw new NotImplementedException();
		}

		public void LoadFromFile() {
			coursesPath = ConfigurationManager.AppSettings["courses"];

			studentsPath = ConfigurationManager.AppSettings["students"];

			openFolder = bool.Parse(ConfigurationManager.AppSettings["open_folder"]);

			saveFiles = bool.Parse(ConfigurationManager.AppSettings["save_files"]);

			outputPath = ConfigurationManager.AppSettings["output_path"];

			mutationRate = double.Parse(ConfigurationManager.AppSettings["mutation_rate"]);

			generations = int.Parse(ConfigurationManager.AppSettings["generations"]);

			individuals = int.Parse(ConfigurationManager.AppSettings["individuals"]);

			cantOpt = int.Parse(ConfigurationManager.AppSettings["cant_opt"]);

			keepBest = bool.Parse(ConfigurationManager.AppSettings["keep_best"]);
		}


	}
}
