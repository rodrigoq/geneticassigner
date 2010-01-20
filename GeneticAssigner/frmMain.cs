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
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DataFactory;
using System.Diagnostics;
using System.Threading;
using System.IO;
using GeneticAlgorithm;

namespace GeneticAssigner {
	public partial class frmMain: Form {
		GeneticAlgorithm<Individual> ga;
		DateTime startTime;
		Settings settings;
		StringBuilder log = new StringBuilder();

		string bestFitness;
		int bestGeneration;
		int opts;
		CourseCollection courses;
		StudentCollection students;

		public frmMain(string[] args) {
			InitializeComponent();
			if(args.Length > 0) {
				settings = Settings.LoadFromArgs(args);
				if(settings != null && settings.Seed.HasValue) {
					chkFixedSeed.Checked = true;
					txtSeed.Text = settings.Seed.Value.ToString();
				}
			}
		}

		private void OnBest(object e) {
			int generation = ((GenerationEventArgs)e).Generation;
			IIndividual best = ((GenerationEventArgs)e).Individual;

			lblNotAssignedValue.Text = best.NotAssigned.ToString("d2");
			string text = "* best on " + generation.ToString("d4") + "  ";
			bestGeneration = generation;
			bestFitness = best.NotAssigned.ToString("d3") + " ";

			for(int i = 0;i < best.Options.Length;i++)
				bestFitness += best.Options[i].ToString("d3") + " ";

			bestFitness = bestFitness.Trim();

			text += bestFitness + "  " + best.Fitness.ToString();

			AddLogLine(text);

		}

		private void OnGeneration(object e) {
			int generation = ((GenerationEventArgs)e).Generation;
			IIndividual individuo = ((GenerationEventArgs)e).Individual;

			lblGenerationValue.Text = generation.ToString("d4");

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

		private void OnAddLogLine(object text) {
			txtLog.Text += "[" + DateTime.Now.ToLongTimeString() + "] " + text + Environment.NewLine;
			txtLog.Select(txtLog.Text.Length, 0);
			txtLog.ScrollToCaret();
		}

		void OnComplete(object dummy) {
			try {
				//Reset courses and studens
				courses.ResetPlacesLeft();
				students.UnAssign();

				string folderName = string.Empty;

				StringBuilder sb = AssignBestResults();

				if(chkCreateFiles.Checked)
					folderName = CreateFiles(sb);

				VerifyResults();
				AddLogLine("The end.");

				StopTimer();

				if(ShouldOpenFolder())
					Process.Start(folderName);

			} catch(Exception ex) {
				Debug.WriteLine(ex.ToString());
				MessageBox.Show(this, ex.Message, this.Text);
			} finally {
				btnStart.Enabled = true;
				grpSettings.Enabled = true;
			}
		}

		private bool ShouldOpenFolder() {
			return chkCreateFiles.Checked && chkOpenFolder.Checked;
		}

		private void StopTimer() {
			tmrElapsed.Stop();
			tmrElapsed.Enabled = false;
		}

		private void VerifyResults() {
			Verifier verifier = new Verifier(courses, students);
			AddLogLine("Verifying...");
			verifier.Verify();
			AddLogLine("Verification OK.");
		}

		private string CreateFiles(StringBuilder sb) {
			string folderName = string.Empty;
			string fileNamePrefix = string.Empty;
			folderName = txtOutputFolder.Text + "GA_" + bestFitness.Replace(" ", "_") + "_" + ga.Seed + "_" + ga.GenerationLength + "_" + ga.PopulationCount + Path.DirectorySeparatorChar;

			if(Directory.Exists(folderName) == false)
				Directory.CreateDirectory(folderName);

			Reports r = new Reports(courses, students, folderName, fileNamePrefix);
			AddLogLine("Saving results...");
			r.Summary(ga.Best, bestGeneration, ga.PopulationCount);
			r.AlphabeticOrder();
			r.CourseOrder();
			r.PlacesLeft();
			r.Log(ga.Best, log);
			r.Result(sb);
			return folderName;
		}

		private StringBuilder AssignBestResults() {
			//asign students with best result
			StringBuilder sb = new StringBuilder();
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
			return sb;
		}

		private void AddLogLine(string text) {
			if(this.InvokeRequired)
				this.Invoke(new WaitCallback(OnAddLogLine), new object[] { text });
			else
				OnAddLogLine(text);
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

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e) {
			Environment.Exit(0);
		}

		private void frmMain_Load(object sender, EventArgs e) {
			try {
				if(settings == null)
					settings = Settings.LoadFromFile();
				SetSettingsToControls();
				courses = CourseFactory.CreateFromFile(settings.CoursesPath);
				students = StudentFactory.CreateFromFile(settings.StudentsPath);
				if(settings.Start)
					Start();
			} catch(Exception ex) {
				Debug.Write(ex.ToString());
				MessageBox.Show("Ocurrió un error que no permite continuar con la ejecución de la aplicación.\n\n" + ex.Message);
				btnStart.Enabled = false;
			}
		}

		private void btnStart_Click(object sender, EventArgs e) {
			Start();
		}

		private void Start() {
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

				int seed = GetSeed();

				opts = GetOptions();

				SetupGeneticAlgorithm(seed);

				prgProgress.Maximum = ga.GenerationLength;
				prgProgress.Value = 0;

				startTime = DateTime.Now;

				tmrElapsed.Interval = 10;
				tmrElapsed.Enabled = true;
				tmrElapsed.Start();
				ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncStart));

			} catch(Exception ex) {
				Debug.WriteLine(ex.ToString());
				MessageBox.Show(this, ex.Message, this.Text);
				btnStart.Enabled = true;
				grpSettings.Enabled = true;
			}
		}

		private void SetupGeneticAlgorithm(int seed) {
			ga = new GeneticAlgorithm<Individual>(FitnessFunction, FirstGeneration(students, int.Parse(txtPopulation.Text), seed), seed);
			ga.onBest += new BestDelegate(ga_onBest);
			ga.onGeneration += new GenerationDelegate(ga_onGeneration);
			ga.onComplete += new CompletedDelegate(ga_onComplete);
			ga.MutationRate = int.Parse(txtMutationRate.Text) / 100.0;
			ga.GenerationLength = int.Parse(txtGenerations.Text);
			ga.PopulationCount = int.Parse(txtPopulation.Text);
			ga.Elitism = chkElitism.Checked;
		}

		private int GetOptions() {
			int options;
			if(int.TryParse(txtOptions.Text.Trim(), out options)
				&& options > 0 && options < 6)
				return options;
			else
				throw new Exception("Options should be a number between 1 and 5.");
		}

		private int GetSeed() {
			if(chkFixedSeed.Checked) {
				return GetSeedFromTextBox();
			} else {
				int seed = Math.Abs((int)DateTime.Now.Ticks);
				txtSeed.Text = seed.ToString();
				return seed;
			}
		}

		private int GetSeedFromTextBox() {
			string msg = "Seed must be a positive number.";
			if(string.IsNullOrEmpty(txtSeed.Text) == false) {
				int seed;
				if(int.TryParse(txtSeed.Text, out seed) && seed >= 0)
					return seed;
				else
					throw new Exception(msg);
			} else
				throw new Exception(msg);
		}

		void ga_onComplete(object sender, EventArgs e) {
			if(this.InvokeRequired)
				this.Invoke(new WaitCallback(OnComplete), new object[] { null });
			else
				OnComplete(null);
		}

		void ga_onGeneration(object sender, GenerationEventArgs e) {
			if(this.InvokeRequired)
				this.Invoke(new WaitCallback(OnGeneration), new object[] { e });
			else
				OnGeneration(e);
		}

		void ga_onBest(object sender, GenerationEventArgs e) {
			if(this.InvokeRequired)
				this.Invoke(new WaitCallback(OnBest), new object[] { e });
			else
				OnBest(e);
		}

		private void AsyncStart(object dummy) {
			ga.Start();
		}

		private List<IIndividual> FirstGeneration(StudentCollection students, int size, int seed) {
			Random random = new Random(seed + 1);
			List<int> students0 = new List<int>();
			foreach(Student student in students)
				students0.Add(student.Id);

			List<IIndividual> thisGeneration = new List<IIndividual>();
			for(int i = 0;i < size;i++) {
				Individual ind = new Individual();
				ind.Students = Shuffle(random, students0);
				thisGeneration.Add(ind);
			}
			return thisGeneration;
		}

		public double FitnessFunction(IIndividual individual) {
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

		private List<int> Shuffle(Random random, List<int> students) {
			int n = students.Count;
			while(n > 1) {
				int k = random.Next(n);
				n--;
				Swap(students, n, k);
			}
			return new List<int>(students);
		}

		private void Swap(List<int> students, int i, int j) {
			int value = students[i];
			students[i] = students[j];
			students[j] = value;
		}

		private void tmrElapsed_Tick(object sender, EventArgs e) {
			TimeSpan ts = DateTime.Now - startTime;
			lblTimeValue.Text = ts.Minutes.ToString("d2") + ":"
				+ ts.Seconds.ToString("d2") + "."
				+ ts.Milliseconds.ToString("d3");
		}

	}
}