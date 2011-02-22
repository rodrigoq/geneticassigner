/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic 
*   assigning of students to class courses.
*   Copyright (C) 2008-2010  Rodrigo Queipo <rodrigoq@gmail.com>
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
using System.Reflection;

namespace GeneticAssigner
{
	public partial class frmMain : Form
	{
		GeneticAlgorithm<Individual> ga;
		DateTime startTime;
		Settings settings;
		StringBuilder log = new StringBuilder();

		Thread gaThread;

		string bestFitness;
		int bestGeneration;

		public frmMain(string[] args)
		{
			InitializeComponent();
			if (args.Length > 0)
			{
				settings = Settings.LoadFromArgs(args);
				if (settings != null && settings.Seed.HasValue)
				{
					chkFixedSeed.Checked = true;
					txtSeed.Text = settings.Seed.Value.ToString();
				}
			}
		}

		private void OnBest(object e)
		{
			int generation = ((GenerationEventArgs)e).Generation;
			IIndividual best = ((GenerationEventArgs)e).Individual;

			lblNotAssignedValue.Text = best.NotAssigned.ToString("d2");
			string text = "* best on " + generation.ToString("d4") + "  ";
			bestGeneration = generation;
			bestFitness = best.NotAssigned.ToString("d3") + " ";

			for (int i = 0; i < best.Options.Length; i++)
			{
				bestFitness += best.Options[i].ToString("d3") + " ";
			}
			bestFitness = bestFitness.Trim();

			text += bestFitness + "  " + best.Fitness.ToString();

			AddLogLine(text);

		}

		private void OnGeneration(object e)
		{

			int generation = ((GenerationEventArgs)e).Generation;
			IIndividual individual = ((GenerationEventArgs)e).Individual;

			graph1.DrawFitness(generation, individual.Fitness);
			graph1.DrawStudents(generation, individual);

			lblGenerationValue.Text = generation.ToString("d4");

			prgProgress.PerformStep();

			if (chkCreateFiles.Checked)
			{
				string stringSep = "\"";
				string separator = stringSep + ";";
				log.Append(stringSep).Append(generation).Append(separator);
				log.Append(stringSep).Append(individual.Fitness).Append(separator);
				log.Append(stringSep).Append(individual.NotAssigned).Append(separator);
				for (int i = 0; i < individual.Options.Length; i++)
				{
					log.Append(stringSep).Append(individual.Options[i]).Append(separator);
				}
				log.AppendLine();
			}
		}

		private void OnAddLogLine(object text)
		{
			txtLog.Text += "[" + DateTime.Now.ToLongTimeString() + "] " + text + Environment.NewLine;
			txtLog.Select(txtLog.Text.Length, 0);
			txtLog.ScrollToCaret();
		}

		void OnComplete()
		{
			try
			{
				TimeSpan totalTime = DateTime.Now - startTime;

				//Reset courses and studens
				Context.Courses.ResetPlacesLeft();
				Context.Students.UnAssign();

				string folderName = string.Empty;

				StringBuilder sb = AssignBestResults();

				if (chkCreateFiles.Checked)
				{
					folderName = CreateFiles(sb, totalTime);
				}
				VerifyResults();
				AddLogLine("The end.");

				StopTimer();

				if (settings.Exit)
				{
					Exit();
				}
				if (ShouldOpenFolder())
				{
					Process.Start(folderName);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				MessageBox.Show(this, ex.Message, this.Text);
			}
			finally
			{
				btnStart.Text = "Start";
				btnStart.Enabled = true;
				grpSettings.Enabled = true;
			}
		}

		private bool ShouldOpenFolder()
		{
			return chkCreateFiles.Checked && chkOpenFolder.Checked;
		}

		private void StopTimer()
		{
			tmrElapsed.Stop();
			tmrElapsed.Enabled = false;
		}

		private void VerifyResults()
		{
			Verifier verifier = new Verifier(Context.Courses, Context.Students);
			AddLogLine("Verifying...");
			verifier.Verify();
			AddLogLine("Verification OK.");
		}

		private string CreateFiles(StringBuilder sb, TimeSpan totalTime)
		{
			string folderName = string.Empty;
			string fileNamePrefix = string.Empty;
			folderName = txtOutputFolder.Text
				+ "GA_" + bestFitness.Replace(" ", "_")
				+ "_e" + ga.Seed
				+ "_g" + ga.GenerationLength
				+ "_i" + ga.PopulationCount
				+ "_m" + (ga.MutationRate * 100)
				+ Path.DirectorySeparatorChar;

			if (Directory.Exists(folderName) == false)
			{
				Directory.CreateDirectory(folderName);
			}
			Reports reports = new Reports(Context.Courses, Context.Students, folderName, fileNamePrefix);
			AddLogLine("Saving results...");

			string totalSeconds = Math.Round(totalTime.TotalSeconds, 2).ToString();
			reports.Summary(ga.Best, bestGeneration, ga.PopulationCount, totalSeconds);
			reports.AlphabeticOrder();
			reports.CourseOrder();
			reports.PlacesLeft();
			reports.Log(ga.Best, log);
			reports.Result(sb);
			return folderName;
		}

		private StringBuilder AssignBestResults()
		{
			//asign students with best result
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < ga.Best.Students.Count; i++)
			{
				int id = ga.Best.Students[i];
				for (int j = 0; j < Context.Students[id].Options.Length; j++)
				{
					Course actual = Context.Courses[Context.Students[id].Options[j]];
					if (actual.PlacesLeft > 0)
					{
						Context.Students[id].AssignOption(j);
						actual.AssignPlace();
						break;
					}
				}

				if (chkCreateFiles.Checked)
				{
					sb.Append(Context.Students[id].Id).Append(";").Append(Context.Students[id].Name).Append(";").Append(Context.Students[id].AssignedCourse).Append(";").Append(Context.Students[id].AssignedOption);
					sb.AppendLine();
				}
			}
			return sb;
		}

		private void AddLogLine(string text)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new WaitCallback(OnAddLogLine), new object[] { text });
			}
			else
			{
				OnAddLogLine(text);
			}
		}

		private void SetSettingsToControls()
		{
			txtOutputFolder.Text = settings.OutputPath;
			txtMutationRate.Text = settings.MutationRate.ToString();
			txtGenerations.Text = settings.Generations.ToString();
			txtPopulation.Text = settings.Individuals.ToString();
			chkElitism.Checked = settings.KeepBest;
			chkCreateFiles.Checked = settings.SaveFiles;
			txtOptions.Text = settings.CantOpt.ToString();
			chkOpenFolder.Checked = settings.OpenFolder;
			if (settings.Seed.HasValue)
			{
				txtSeed.Text = settings.Seed.Value.ToString();
				chkFixedSeed.Checked = true;
			}

		}

		private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			Exit();
		}

		private void Exit()
		{
			Environment.Exit(0);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				SetWindowCaption();

				if (settings == null)
				{
					settings = Settings.LoadFromFile();
				}
				SetSettingsToControls();

				//Carga los datos de los archivos para validar que existan,
				//así, si no los encuentra da error desde el comienzo y no 
				//permite continuar la ejecución.
				CourseCollection courses = CourseFactory.CreateFromFile(settings.CoursesPath);
				StudentCollection students = StudentFactory.CreateFromFile(settings.StudentsPath);

				Context.InitializeContext(courses, students, settings.CantOpt);

				if (settings.Start)
				{
					Start();
				}
			}
			catch (Exception ex)
			{
				Debug.Write(ex.ToString());
				MessageBox.Show("Ocurrió un error que no permite continuar con la ejecución de la aplicación." + Environment.NewLine + Environment.NewLine + ex.Message);
				btnStart.Enabled = false;
			}
		}

		private void SetWindowCaption()
		{
			string appFile = Assembly.GetAssembly(this.GetType()).Location;
			AssemblyName ass = AssemblyName.GetAssemblyName(appFile);
			this.Text += GetVersionString(ass);
		}

		private static string GetVersionString(AssemblyName ass)
		{
			return " (v" + ass.Version.Major
				+ "." + ass.Version.Minor
				+ "." + ass.Version.Build
				+ ")";
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (btnStart.Text == "Start")
			{
				Start();
			}
			else
			{
				Stop();
			}
		}

		private void Stop()
		{
			try
			{
				if (ga.IsRunning)
				{
					btnStart.Enabled = false;
					btnStart.Text = "Stopping...";
					AddLogLine("Stopping...");

					gaThread.Abort();
					gaThread.Join();

					//Reset courses and studens
					Context.Courses.ResetPlacesLeft();
					Context.Students.UnAssign();
					AddLogLine("Interrupted by user.");
					StopTimer();
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				MessageBox.Show(this, ex.Message, this.Text);
			}
			finally
			{
				btnStart.Text = "Start";
				btnStart.Enabled = true;
				grpSettings.Enabled = true;
			}
		}

		void OnStop()
		{



		}


		private void Start()
		{
			try
			{
				btnStart.Text = "Stop";
				grpSettings.Enabled = false;
				txtLog.Clear();
				lblGenerationValue.Text = "0000";
				lblNotAssignedValue.Text = "00";
				lblTimeValue.Text = "00:00.000";
				log = new StringBuilder();

				AddLogLine("Starting...");


				Context.Courses = CourseFactory.CreateFromFile(settings.CoursesPath);
				Context.Students = StudentFactory.CreateFromFile(settings.StudentsPath);

				Verifier verifier = new Verifier(Context.Courses, Context.Students);

				AddLogLine("Verifying coherence...");
				verifier.StudentsRepeatedOptions();
				verifier.StudentsInCourses();
				AddLogLine("Verification OK.");

				int seed = GetSeed();

				Context.Places = GetOptions();

				SetupGeneticAlgorithm(seed);

				prgProgress.Maximum = ga.GenerationLength;
				prgProgress.Value = 0;

				startTime = DateTime.Now;

				tmrElapsed.Interval = 10;
				tmrElapsed.Enabled = true;
				tmrElapsed.Start();

				gaThread = new Thread(new ThreadStart(AsyncStart));
				gaThread.Start();

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				MessageBox.Show(this, ex.Message, this.Text);
				btnStart.Text = "Start";
				grpSettings.Enabled = true;
			}
		}

		private void SetupGeneticAlgorithm(int seed)
		{
			List<IIndividual> firstGeneration =
				Individual.FirstGeneration(int.Parse(txtPopulation.Text), seed);

			ga = new GeneticAlgorithm<Individual>(firstGeneration, seed);
			ga.onBest += new BestDelegate(ga_onBest);
			ga.onGeneration += new GenerationDelegate(ga_onGeneration);
			ga.onComplete += new CompletedDelegate(ga_onComplete);
			ga.MutationRate = double.Parse(txtMutationRate.Text) / 100.0;
			ga.GenerationLength = int.Parse(txtGenerations.Text);
			ga.PopulationCount = int.Parse(txtPopulation.Text);
			ga.Elitism = chkElitism.Checked;
		}

		private int GetOptions()
		{
			int options;
			if (int.TryParse(txtOptions.Text.Trim(), out options)
				&& options > 0 && options < 6)
			{
				return options;
			}
			else
			{
				throw new Exception("Options should be a number between 1 and 5.");
			}
		}

		private int GetSeed()
		{
			if (chkFixedSeed.Checked)
			{
				return GetSeedFromTextBox();
			}
			else
			{
				int seed = Math.Abs((int)DateTime.Now.Ticks);
				txtSeed.Text = seed.ToString();
				return seed;
			}
		}

		private int GetSeedFromTextBox()
		{
			string msg = "Seed must be a positive number.";
			if (string.IsNullOrEmpty(txtSeed.Text) == false)
			{
				int seed;
				if (int.TryParse(txtSeed.Text, out seed) && seed >= 0)
				{
					return seed;
				}
				else
				{
					throw new Exception(msg);
				}
			}
			else
			{
				throw new Exception(msg);
			}
		}

		void ga_onComplete(object sender, EventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new ThreadStart(OnComplete));
			}
			else
			{
				OnComplete();
			}
		}

		void ga_onGeneration(object sender, GenerationEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new WaitCallback(OnGeneration), new object[] { e });
			}
			else
			{
				OnGeneration(e);
			}
		}

		void ga_onBest(object sender, GenerationEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new WaitCallback(OnBest), new object[] { e });
			}
			else
			{
				OnBest(e);
			}
		}

		private void AsyncStart()
		{
			ga.Start();
		}

		private void tmrElapsed_Tick(object sender, EventArgs e)
		{
			TimeSpan ts = DateTime.Now - startTime;
			lblTimeValue.Text = ts.Minutes.ToString("d2") + ":"
				+ ts.Seconds.ToString("d2") + "."
				+ ts.Milliseconds.ToString("d3");
		}

		private void chkCreateFiles_CheckedChanged(object sender, EventArgs e)
		{
			chkOpenFolder.Checked = chkCreateFiles.Checked;
		}

		private void prgProgress_Click(object sender, EventArgs e)
		{
			/*if (graph1.Visible)
			{
				this.Height -= graph1.Height + 10;
				graph1.Visible = false;
			}
			else
			{
				this.Height += graph1.Height + 10;
				graph1.Visible = true;
			}*/
		}
	}
}