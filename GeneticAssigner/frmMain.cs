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
		int cantOps;
		CourseCollection courses;
		StudentCollection students;

		public frmMain(string[] args) {
			InitializeComponent();
		}

		void ga_onBest(int generation, IIndividual best) {
			lblNoAsignados.Text = best.NotAssigned.ToString();
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
			lblGeneracion.Text = generation.ToString("d4");
			lblTiempo.Text = ts.Minutes.ToString("d2") + ":" + ts.Seconds.ToString("d2") + "." + ts.Milliseconds.ToString("d3");

			prgProgress.PerformStep();

			if(chkSaveFiles.Checked) {
				string separator = "\"";
				string closes = separator + ";";
				log.Append(separator).Append(generation).Append(closes);
				log.Append(separator).Append(individuo.Fitness).Append(closes);
				log.Append(separator).Append(individuo.NotAssigned).Append(closes);
				for(int i = 0;i < individuo.Options.Length;i++)
					log.Append(separator).Append(individuo.Options[i]).Append(closes);
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
			txtPath.Text = settings.OutputPath;
			txtMutationRate.Text = settings.MutationRate.ToString();
			txtGeneraciones.Text = settings.Generations.ToString();
			txtIndividuos.Text = settings.Individuals.ToString();
			chkElitism.Checked = settings.KeepBest;
			chkSaveFiles.Checked = settings.SaveFiles;
			txtOptions.Text = settings.CantOpt.ToString();
			chkFolder.Checked = settings.OpenFolder;
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

				Verifier v = new Verifier(courses, students);

				AddLogLine("Verifying coherence...");
				v.StudentsRepeatedOptions();
				v.StudentsInCourses();
				AddLogLine("Verification OK.");


				//if(!string.IsNullOrEmpty(txtSeed.Text))
				//	seed = int.Parse(txtSeed.Text);
				int seed = Math.Abs((int)DateTime.Now.Ticks);
				//seed = 568027582;
				txtSeed.Text = seed.ToString();

				cantOps = int.Parse(txtOptions.Text);
				if(cantOps > 5)
					throw new Exception("The maximum number of possible options is 5.");


				ga = new GeneticAlgorithm<Individual>(FitnessFunction, FirstGeneration(students, int.Parse(txtIndividuos.Text), seed), seed);
				ga.onBest += new BestDelegate(ga_onBest);
				ga.onGeneration += new GenerationDelegate(ga_onGeneration);
				ga.MutationRate = int.Parse(txtMutationRate.Text) / 100.0;
				ga.GenerationLength = int.Parse(txtGeneraciones.Text);
				ga.PopulationCount = int.Parse(txtIndividuos.Text);

				prgProgress.Maximum = ga.GenerationLength;
				prgProgress.Value = 0;
				ga.Elitism = chkElitism.Checked;

				startTime = DateTime.Now;

				//Thread thread = new Thread(new ThreadStart(ga.Start));
				//thread.Start();

				ga.Start();

				StringBuilder sb = new StringBuilder();

				courses.ResetPlacesLeft();
				students.UnAssign();

				//asigna a los alumnos con la mejor opción
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

					if(chkSaveFiles.Checked) {
						sb.Append(students[id].Id).Append(";").Append(students[id].Name).Append(";").Append(students[id].AssignedCourse).Append(";").Append(students[id].AssignedOption);
						sb.AppendLine();
					}
				}

				string folderName = "";
				if(chkSaveFiles.Checked) {
					string fileNamePrefix = "";
					folderName = txtPath.Text + "GA_" + bestFitness.Replace(" ", "_") + "_" + seed + "_" + ga.GenerationLength + "_" + ga.PopulationCount + "\\";

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

				v = new Verifier(courses, students);
				AddLogLine("Verifying...");

				v.Verify();

				AddLogLine("Verification OK.");
				AddLogLine("The end.");

				if(chkSaveFiles.Checked && chkFolder.Checked)
					Process.Start(folderName);

			} catch(Exception ex) {
				Debug.WriteLine(ex.ToString());
				MessageBox.Show(ex.Message);
			} finally {
				btnStart.Enabled = true;
				grpSettings.Enabled = true;
			}
		}

		private double FitnessFunction(IIndividual individuo) {
			int noAsignados = individuo.Students.Count;
			individuo.Options = new int[cantOps];

			courses.ResetPlacesLeft();

			for(int i = 0;i < individuo.Students.Count;i++) {
				int id = individuo.Students[i];
				int cant = Math.Min(cantOps, students[id].Options.Length);
				for(int j = 0;j < cant;j++) {
					Course actual = courses[students[id].Options[j]];
					if(actual.PlacesLeft > 0) {
						individuo.Options[j]++;
						noAsignados--;
						actual.AssignPlace();
						break;
					}
				}
			}
			individuo.Assigned = individuo.Students.Count - noAsignados;
			individuo.NotAssigned = noAsignados;

			long value = individuo.Assigned * (long)Math.Pow(10, individuo.Options.Length * 3);
			for(int i = 0;i < individuo.Options.Length;i++)
				value += individuo.Options[i] * (long)Math.Pow(10, (individuo.Options.Length - 1 - i) * 3);

			return Math.Log(value);
		}

		private List<IIndividual> FirstGeneration(StudentCollection alumnos, int cant, int seed) {
			Random random = new Random(seed + 1);
			Individual ind0 = new Individual();
			foreach(Student alumno in alumnos)
				ind0.Students.Add(alumno.Id);

			List<IIndividual> thisGeneration = new List<IIndividual>();
			for(int i = 0;i < cant;i++) {
				Individual ind = new Individual();
				ind.Students = Randomize(random, ind0);
				thisGeneration.Add(ind);
			}
			return thisGeneration;
		}

		private List<int> Randomize(Random random, IIndividual individuo) {
			List<int> indices = new List<int>();
			List<int> rnd = new List<int>();

			while(indices.Count != individuo.Students.Count) {
				int num = random.Next(0, individuo.Students.Count);
				if(!indices.Contains(num)) {
					indices.Add(num);
					rnd.Add(individuo.Students[num]);
				}
			}
			return rnd;
		}

	}
}