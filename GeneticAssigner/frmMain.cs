using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DataFactory;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.IO;
using GA;

namespace GeneticAssigner {
	public partial class frmMain: Form {
		GeneticAlgorithm<Individual> ga;
		DateTime startTime;
		Settings settings = new Settings();
		StringBuilder log = new StringBuilder();

		string bestFitness;
		int bestGeneration;
		int opts;
		CourseCollection courses;
		StudentCollection students;

		public frmMain(string[] args) {
			InitializeComponent();
		}

		void ga_onBest(int generation, IIndividual best) {
			lblNotAssignedValue.Text = best.NotAssigned.ToString("d2");
			string text = "* best on " + generation.ToString("d4") + "  ";
			bestGeneration = generation;
			bestFitness = best.NotAssigned.ToString("d3") + " ";

			for(int i = 0;i < best.Options.Length;i++)
				bestFitness += best.Options[i].ToString("d3") + " ";

			bestFitness = bestFitness.Trim();

			text += bestFitness + "  " + best.Fitness.ToString();

			AddLogLine(text);

			Application.DoEvents();
		}

		void ga_onGeneration(int generation, IIndividual individuo) {
			Application.DoEvents();
			TimeSpan ts = DateTime.Now - startTime;
			lblGenerationValue.Text = generation.ToString("d4");
			lblTimeValue.Text = ts.Minutes.ToString("d2") + ":" + ts.Seconds.ToString("d2") + "." + ts.Milliseconds.ToString("d3");

			prgProgress.PerformStep();

			if(chkCreateFiles.Checked) {
				string stringSep = "\"";
				string separator = stringSep + ";";
				log.Append(stringSep).Append(generation).Append(separator);
				log.Append(stringSep).Append(individuo.Fitness).Append(separator);
				log.Append(stringSep).Append(individuo.NotAssigned).Append(separator);
				for(int i = 0;i < individuo.Options.Length;i++)
					log.Append(stringSep).Append(individuo.Options[i]).Append(separator);
				log.AppendLine();
			}
		}

		private void AddLogLine(string text) {
			txtLog.Text += "[" + DateTime.Now.ToLongTimeString() + "] " + text + Environment.NewLine;
			txtLog.Select(txtLog.Text.Length, 0);
			txtLog.ScrollToCaret();
		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e) {
			Environment.Exit(0);
		}

		private void frmMain_Load(object sender, EventArgs e) {
			settings.LoadFromFile();
			SetSettingsToControls();
			courses = CourseFactory.CreateFromFile(settings.CoursesPath);
			students = StudentFactory.CreateFromFile(settings.StudentsPath);
		}

		private void SetSettingsToControls() {
			txtOutputFolder.Text = settings.OutputPath;
			txtMutationRate.Text = settings.MutationRate.ToString();
			txtGenerations.Text = settings.Generations.ToString();
			txtPopulation.Text = settings.Individuals.ToString();
			chkElitism.Checked = settings.KeepBest;
			chkCreateFiles.Checked = settings.SaveFiles;
			txtOptions.Text = settings.CantOpt.ToString();
			chkOpenFolder.Checked = settings.OpenFolder;
		}

		private void btnStart_Click(object sender, EventArgs e) {
			try {
				btnStart.Enabled = false;
				grpSettings.Enabled = false;
				txtLog.Clear();
				log = new StringBuilder();

				AddLogLine("Starting...");

				courses = CourseFactory.CreateFromFile(settings.CoursesPath);
				students = StudentFactory.CreateFromFile(settings.StudentsPath);

				Verifier verifier = new Verifier(courses, students);

				AddLogLine("Verifying coherence...");
				verifier.StudentsRepeatedOptions();
				verifier.StudentsInCourses();
				AddLogLine("Verification OK.");


				//if(!string.IsNullOrEmpty(txtSeed.Text))
				//	seed = int.Parse(txtSeed.Text);
				int seed = Math.Abs((int)DateTime.Now.Ticks);
				//seed = 568027582;
				txtSeed.Text = seed.ToString();

				opts = int.Parse(txtOptions.Text);
				if(opts > 5)
					throw new Exception("The maximum number of possible options is 5.");


				ga = new GeneticAlgorithm<Individual>(FitnessFunction, FirstGeneration(students, int.Parse(txtPopulation.Text), seed), seed);
				ga.onBest += new BestDelegate(ga_onBest);
				ga.onGeneration += new GenerationDelegate(ga_onGeneration);
				ga.MutationRate = int.Parse(txtMutationRate.Text) / 100.0;
				ga.GenerationLength = int.Parse(txtGenerations.Text);
				ga.PopulationCount = int.Parse(txtPopulation.Text);

				prgProgress.Maximum = ga.GenerationLength;
				prgProgress.Value = 0;
				ga.Elitism = chkElitism.Checked;

				startTime = DateTime.Now;

				ga.Start();

				StringBuilder sb = new StringBuilder();

				courses.ResetPlacesLeft();
				students.UnAssign();

				//asign students with best result
				for(int i = 0;i < ga.Best.Students.Count;i++) {
					int id = ga.Best.Students[i];
					for(int j = 0;j < students[id].Options.Length;j++) {
						Course actual = courses[students[id].Options[j]];
						if(actual.PlacesLeft > 0) {
							students[id].AssignOption(j);
							actual.AssignPlace();
							break;
						}
					}

					if(chkCreateFiles.Checked) {
						sb.Append(students[id].Id).Append(";").Append(students[id].Name).Append(";").Append(students[id].AssignedCourse).Append(";").Append(students[id].AssignedOption);
						sb.AppendLine();
					}
				}

				string folderName = "";
				if(chkCreateFiles.Checked) {
					string fileNamePrefix = "";
					folderName = txtOutputFolder.Text + "GA_" + bestFitness.Replace(" ", "_") + "_" + seed + "_" + ga.GenerationLength + "_" + ga.PopulationCount + "\\";

					if(!Directory.Exists(folderName))
						Directory.CreateDirectory(folderName);

					Reports r = new Reports(courses, students, folderName, fileNamePrefix);
					AddLogLine("Saving results...");
					r.Summary(ga.Best, bestGeneration, ga.PopulationCount);
					r.AlphabeticOrder();
					r.CourseOrder();
					r.PlacesLeft();
					r.Log(ga.Best, log);
					r.Result(sb);
				}

				verifier = new Verifier(courses, students);
				AddLogLine("Verifying...");

				verifier.Verify();

				AddLogLine("Verification OK.");
				AddLogLine("The end.");

				if(chkCreateFiles.Checked && chkOpenFolder.Checked)
					Process.Start(folderName);

			} catch(Exception ex) {
				Debug.WriteLine(ex.ToString());
				MessageBox.Show(ex.Message);
			} finally {
				btnStart.Enabled = true;
				grpSettings.Enabled = true;
			}
		}

		private double FitnessFunction(IIndividual individual) {
			int notAssigned = individual.Students.Count;
			individual.Options = new int[opts];

			courses.ResetPlacesLeft();

			for(int i = 0;i < individual.Students.Count;i++) {
				int id = individual.Students[i];
				int opt = Math.Min(opts, students[id].Options.Length);
				for(int j = 0;j < opt;j++) {
					Course actual = courses[students[id].Options[j]];
					if(actual.PlacesLeft > 0) {
						individual.Options[j]++;
						notAssigned--;
						actual.AssignPlace();
						break;
					}
				}
			}
			individual.Assigned = individual.Students.Count - notAssigned;
			individual.NotAssigned = notAssigned;

			long value = individual.Assigned * (long)Math.Pow(10, individual.Options.Length * 3);
			for(int i = 0;i < individual.Options.Length;i++)
				value += individual.Options[i] * (long)Math.Pow(10, (individual.Options.Length - 1 - i) * 3);

			return Math.Log(value);
		}

		private List<IIndividual> FirstGeneration(StudentCollection students, int size, int seed) {
			Random random = new Random(seed + 1);
			Individual ind0 = new Individual();
			foreach(Student student in students)
				ind0.Students.Add(student.Id);

			List<IIndividual> thisGeneration = new List<IIndividual>();
			for(int i = 0;i < size;i++) {
				Individual ind = new Individual();
				ind.Students = Randomize(random, ind0);
				thisGeneration.Add(ind);
			}
			return thisGeneration;
		}

		private List<int> Randomize(Random random, IIndividual individual) {
			List<int> indices = new List<int>();
			List<int> rnd = new List<int>();

			while(indices.Count != individual.Students.Count) {
				int num = random.Next(0, individual.Students.Count);
				if(!indices.Contains(num)) {
					indices.Add(num);
					rnd.Add(individual.Students[num]);
				}
			}
			return rnd;
		}

	}
}