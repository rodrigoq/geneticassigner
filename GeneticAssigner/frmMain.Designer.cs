/*
*   GeneticAssigner - Genetic Algorithm implementation for automatic
*   assigning of students to class courses.
*   Copyright (C) 2008-2020  Rodrigo Queipo <rodrigoq@gmail.com>
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
namespace GeneticAssigner
{
	partial class FrmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.grpStatus = new System.Windows.Forms.GroupBox();
			this.lblGenerationValue = new System.Windows.Forms.Label();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblGeneration = new System.Windows.Forms.Label();
			this.lblTimeValue = new System.Windows.Forms.Label();
			this.lblNotAssignedValue = new System.Windows.Forms.Label();
			this.lblNotAssigned = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.grpSettings = new System.Windows.Forms.GroupBox();
			this.nudPopulation = new System.Windows.Forms.NumericUpDown();
			this.nudMutationRate = new System.Windows.Forms.NumericUpDown();
			this.nudGenerations = new System.Windows.Forms.NumericUpDown();
			this.nudOverflow = new System.Windows.Forms.NumericUpDown();
			this.nudOptions = new System.Windows.Forms.NumericUpDown();
			this.chkOverflow = new System.Windows.Forms.CheckBox();
			this.chkFixedSeed = new System.Windows.Forms.CheckBox();
			this.chkOpenFolder = new System.Windows.Forms.CheckBox();
			this.lblOptions = new System.Windows.Forms.Label();
			this.lblSeed = new System.Windows.Forms.Label();
			this.lblGenerations = new System.Windows.Forms.Label();
			this.lblPopulation = new System.Windows.Forms.Label();
			this.lblMutationRate = new System.Windows.Forms.Label();
			this.lblOutputFolder = new System.Windows.Forms.Label();
			this.txtSeed = new System.Windows.Forms.TextBox();
			this.chkElitism = new System.Windows.Forms.CheckBox();
			this.txtOutputFolder = new System.Windows.Forms.TextBox();
			this.chkCreateFiles = new System.Windows.Forms.CheckBox();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.prgProgress = new System.Windows.Forms.ProgressBar();
			this.tmrElapsed = new System.Windows.Forms.Timer(this.components);
			this.graph1 = new GeneticAssigner.Graph();
			this.grpStatus.SuspendLayout();
			this.grpSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPopulation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMutationRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudGenerations)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudOverflow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudOptions)).BeginInit();
			this.SuspendLayout();
			// 
			// grpStatus
			// 
			this.grpStatus.Controls.Add(this.lblGenerationValue);
			this.grpStatus.Controls.Add(this.lblTime);
			this.grpStatus.Controls.Add(this.lblGeneration);
			this.grpStatus.Controls.Add(this.lblTimeValue);
			this.grpStatus.Controls.Add(this.lblNotAssignedValue);
			this.grpStatus.Controls.Add(this.lblNotAssigned);
			this.grpStatus.Location = new System.Drawing.Point(12, 12);
			this.grpStatus.Name = "grpStatus";
			this.grpStatus.Size = new System.Drawing.Size(211, 92);
			this.grpStatus.TabIndex = 0;
			this.grpStatus.TabStop = false;
			this.grpStatus.Text = "Status";
			// 
			// lblGenerationValue
			// 
			this.lblGenerationValue.Location = new System.Drawing.Point(108, 45);
			this.lblGenerationValue.Name = "lblGenerationValue";
			this.lblGenerationValue.Size = new System.Drawing.Size(56, 13);
			this.lblGenerationValue.TabIndex = 3;
			this.lblGenerationValue.Text = "0000";
			this.lblGenerationValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblTime
			// 
			this.lblTime.AutoSize = true;
			this.lblTime.Location = new System.Drawing.Point(43, 67);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(30, 13);
			this.lblTime.TabIndex = 4;
			this.lblTime.Text = "Time";
			// 
			// lblGeneration
			// 
			this.lblGeneration.AutoSize = true;
			this.lblGeneration.Location = new System.Drawing.Point(43, 45);
			this.lblGeneration.Name = "lblGeneration";
			this.lblGeneration.Size = new System.Drawing.Size(59, 13);
			this.lblGeneration.TabIndex = 2;
			this.lblGeneration.Text = "Generation";
			// 
			// lblTimeValue
			// 
			this.lblTimeValue.AutoSize = true;
			this.lblTimeValue.Location = new System.Drawing.Point(109, 67);
			this.lblTimeValue.Name = "lblTimeValue";
			this.lblTimeValue.Size = new System.Drawing.Size(55, 13);
			this.lblTimeValue.TabIndex = 5;
			this.lblTimeValue.Text = "00:00.000";
			this.lblTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblNotAssignedValue
			// 
			this.lblNotAssignedValue.AutoSize = true;
			this.lblNotAssignedValue.Location = new System.Drawing.Point(145, 23);
			this.lblNotAssignedValue.Name = "lblNotAssignedValue";
			this.lblNotAssignedValue.Size = new System.Drawing.Size(19, 13);
			this.lblNotAssignedValue.TabIndex = 1;
			this.lblNotAssignedValue.Text = "00";
			this.lblNotAssignedValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblNotAssigned
			// 
			this.lblNotAssigned.AutoSize = true;
			this.lblNotAssigned.Location = new System.Drawing.Point(43, 23);
			this.lblNotAssigned.Name = "lblNotAssigned";
			this.lblNotAssigned.Size = new System.Drawing.Size(70, 13);
			this.lblNotAssigned.TabIndex = 0;
			this.lblNotAssigned.Text = "Not Assigned";
			// 
			// btnStart
			// 
			this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStart.Location = new System.Drawing.Point(12, 113);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(211, 50);
			this.btnStart.TabIndex = 1;
			this.btnStart.Text = "Start";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// grpSettings
			// 
			this.grpSettings.Controls.Add(this.nudPopulation);
			this.grpSettings.Controls.Add(this.nudMutationRate);
			this.grpSettings.Controls.Add(this.nudGenerations);
			this.grpSettings.Controls.Add(this.nudOverflow);
			this.grpSettings.Controls.Add(this.nudOptions);
			this.grpSettings.Controls.Add(this.chkOverflow);
			this.grpSettings.Controls.Add(this.chkFixedSeed);
			this.grpSettings.Controls.Add(this.chkOpenFolder);
			this.grpSettings.Controls.Add(this.lblOptions);
			this.grpSettings.Controls.Add(this.lblSeed);
			this.grpSettings.Controls.Add(this.lblGenerations);
			this.grpSettings.Controls.Add(this.lblPopulation);
			this.grpSettings.Controls.Add(this.lblMutationRate);
			this.grpSettings.Controls.Add(this.lblOutputFolder);
			this.grpSettings.Controls.Add(this.txtSeed);
			this.grpSettings.Controls.Add(this.chkElitism);
			this.grpSettings.Controls.Add(this.txtOutputFolder);
			this.grpSettings.Controls.Add(this.chkCreateFiles);
			this.grpSettings.Location = new System.Drawing.Point(240, 12);
			this.grpSettings.Name = "grpSettings";
			this.grpSettings.Size = new System.Drawing.Size(381, 151);
			this.grpSettings.TabIndex = 2;
			this.grpSettings.TabStop = false;
			this.grpSettings.Text = "Configuration";
			// 
			// nudPopulation
			// 
			this.nudPopulation.Increment = new decimal(new int[] {
				5,
				0,
				0,
				0});
			this.nudPopulation.Location = new System.Drawing.Point(306, 90);
			this.nudPopulation.Maximum = new decimal(new int[] {
				10000,
				0,
				0,
				0});
			this.nudPopulation.Minimum = new decimal(new int[] {
				1,
				0,
				0,
				0});
			this.nudPopulation.Name = "nudPopulation";
			this.nudPopulation.Size = new System.Drawing.Size(69, 20);
			this.nudPopulation.TabIndex = 20;
			this.nudPopulation.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudPopulation.Value = new decimal(new int[] {
				100,
				0,
				0,
				0});
			// 
			// nudMutationRate
			// 
			this.nudMutationRate.Location = new System.Drawing.Point(306, 40);
			this.nudMutationRate.Minimum = new decimal(new int[] {
				1,
				0,
				0,
				0});
			this.nudMutationRate.Name = "nudMutationRate";
			this.nudMutationRate.Size = new System.Drawing.Size(69, 20);
			this.nudMutationRate.TabIndex = 20;
			this.nudMutationRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudMutationRate.Value = new decimal(new int[] {
				80,
				0,
				0,
				0});
			// 
			// nudGenerations
			// 
			this.nudGenerations.Increment = new decimal(new int[] {
				100,
				0,
				0,
				0});
			this.nudGenerations.Location = new System.Drawing.Point(306, 65);
			this.nudGenerations.Maximum = new decimal(new int[] {
				9999999,
				0,
				0,
				0});
			this.nudGenerations.Minimum = new decimal(new int[] {
				1,
				0,
				0,
				0});
			this.nudGenerations.Name = "nudGenerations";
			this.nudGenerations.Size = new System.Drawing.Size(69, 20);
			this.nudGenerations.TabIndex = 19;
			this.nudGenerations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudGenerations.Value = new decimal(new int[] {
				2000,
				0,
				0,
				0});
			// 
			// nudOverflow
			// 
			this.nudOverflow.Location = new System.Drawing.Point(90, 113);
			this.nudOverflow.Maximum = new decimal(new int[] {
				5,
				0,
				0,
				0});
			this.nudOverflow.Minimum = new decimal(new int[] {
				1,
				0,
				0,
				0});
			this.nudOverflow.Name = "nudOverflow";
			this.nudOverflow.Size = new System.Drawing.Size(38, 20);
			this.nudOverflow.TabIndex = 18;
			this.nudOverflow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudOverflow.Value = new decimal(new int[] {
				3,
				0,
				0,
				0});
			// 
			// nudOptions
			// 
			this.nudOptions.Location = new System.Drawing.Point(90, 90);
			this.nudOptions.Maximum = new decimal(new int[] {
				5,
				0,
				0,
				0});
			this.nudOptions.Minimum = new decimal(new int[] {
				1,
				0,
				0,
				0});
			this.nudOptions.Name = "nudOptions";
			this.nudOptions.Size = new System.Drawing.Size(38, 20);
			this.nudOptions.TabIndex = 18;
			this.nudOptions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.nudOptions.Value = new decimal(new int[] {
				3,
				0,
				0,
				0});
			this.nudOptions.ValueChanged += new System.EventHandler(this.nudOptions_ValueChanged);
			// 
			// chkOverflow
			// 
			this.chkOverflow.AutoSize = true;
			this.chkOverflow.Location = new System.Drawing.Point(16, 116);
			this.chkOverflow.Name = "chkOverflow";
			this.chkOverflow.Size = new System.Drawing.Size(68, 17);
			this.chkOverflow.TabIndex = 8;
			this.chkOverflow.Text = "Overflow";
			this.chkOverflow.UseVisualStyleBackColor = true;
			this.chkOverflow.CheckedChanged += new System.EventHandler(this.chkOverflow_CheckedChanged);
			// 
			// chkFixedSeed
			// 
			this.chkFixedSeed.AutoSize = true;
			this.chkFixedSeed.Location = new System.Drawing.Point(149, 116);
			this.chkFixedSeed.Name = "chkFixedSeed";
			this.chkFixedSeed.Size = new System.Drawing.Size(101, 17);
			this.chkFixedSeed.TabIndex = 0;
			this.chkFixedSeed.Text = "Use Fixed Seed";
			this.chkFixedSeed.UseVisualStyleBackColor = true;
			// 
			// chkOpenFolder
			// 
			this.chkOpenFolder.AutoSize = true;
			this.chkOpenFolder.Checked = true;
			this.chkOpenFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkOpenFolder.Location = new System.Drawing.Point(16, 68);
			this.chkOpenFolder.Name = "chkOpenFolder";
			this.chkOpenFolder.Size = new System.Drawing.Size(84, 17);
			this.chkOpenFolder.TabIndex = 5;
			this.chkOpenFolder.Text = "Open Folder";
			this.chkOpenFolder.UseVisualStyleBackColor = true;
			// 
			// lblOptions
			// 
			this.lblOptions.AutoSize = true;
			this.lblOptions.Location = new System.Drawing.Point(32, 92);
			this.lblOptions.Name = "lblOptions";
			this.lblOptions.Size = new System.Drawing.Size(43, 13);
			this.lblOptions.TabIndex = 6;
			this.lblOptions.Text = "Options";
			// 
			// lblSeed
			// 
			this.lblSeed.AutoSize = true;
			this.lblSeed.Location = new System.Drawing.Point(256, 117);
			this.lblSeed.Name = "lblSeed";
			this.lblSeed.Size = new System.Drawing.Size(32, 13);
			this.lblSeed.TabIndex = 1;
			this.lblSeed.Text = "Seed";
			// 
			// lblGenerations
			// 
			this.lblGenerations.AutoSize = true;
			this.lblGenerations.Location = new System.Drawing.Point(224, 67);
			this.lblGenerations.Name = "lblGenerations";
			this.lblGenerations.Size = new System.Drawing.Size(64, 13);
			this.lblGenerations.TabIndex = 14;
			this.lblGenerations.Text = "Generations";
			// 
			// lblPopulation
			// 
			this.lblPopulation.AutoSize = true;
			this.lblPopulation.Location = new System.Drawing.Point(231, 92);
			this.lblPopulation.Name = "lblPopulation";
			this.lblPopulation.Size = new System.Drawing.Size(57, 13);
			this.lblPopulation.TabIndex = 16;
			this.lblPopulation.Text = "Population";
			// 
			// lblMutationRate
			// 
			this.lblMutationRate.AutoSize = true;
			this.lblMutationRate.Location = new System.Drawing.Point(229, 42);
			this.lblMutationRate.Name = "lblMutationRate";
			this.lblMutationRate.Size = new System.Drawing.Size(59, 13);
			this.lblMutationRate.TabIndex = 12;
			this.lblMutationRate.Text = "Mutation %";
			// 
			// lblOutputFolder
			// 
			this.lblOutputFolder.AutoSize = true;
			this.lblOutputFolder.Location = new System.Drawing.Point(217, 17);
			this.lblOutputFolder.Name = "lblOutputFolder";
			this.lblOutputFolder.Size = new System.Drawing.Size(71, 13);
			this.lblOutputFolder.TabIndex = 10;
			this.lblOutputFolder.Text = "Output Folder";
			// 
			// txtSeed
			// 
			this.txtSeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSeed.Location = new System.Drawing.Point(306, 115);
			this.txtSeed.MaxLength = 12;
			this.txtSeed.Name = "txtSeed";
			this.txtSeed.Size = new System.Drawing.Size(69, 20);
			this.txtSeed.TabIndex = 2;
			this.txtSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// chkElitism
			// 
			this.chkElitism.AutoSize = true;
			this.chkElitism.Checked = true;
			this.chkElitism.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkElitism.Location = new System.Drawing.Point(16, 22);
			this.chkElitism.Name = "chkElitism";
			this.chkElitism.Size = new System.Drawing.Size(74, 17);
			this.chkElitism.TabIndex = 3;
			this.chkElitism.Text = "Keep best";
			this.chkElitism.UseVisualStyleBackColor = true;
			// 
			// txtOutputFolder
			// 
			this.txtOutputFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOutputFolder.Location = new System.Drawing.Point(306, 15);
			this.txtOutputFolder.MaxLength = 2000;
			this.txtOutputFolder.Name = "txtOutputFolder";
			this.txtOutputFolder.Size = new System.Drawing.Size(70, 20);
			this.txtOutputFolder.TabIndex = 11;
			// 
			// chkCreateFiles
			// 
			this.chkCreateFiles.AutoSize = true;
			this.chkCreateFiles.Checked = true;
			this.chkCreateFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCreateFiles.Location = new System.Drawing.Point(16, 45);
			this.chkCreateFiles.Name = "chkCreateFiles";
			this.chkCreateFiles.Size = new System.Drawing.Size(81, 17);
			this.chkCreateFiles.TabIndex = 4;
			this.chkCreateFiles.Text = "Create Files";
			this.chkCreateFiles.UseVisualStyleBackColor = true;
			this.chkCreateFiles.CheckedChanged += new System.EventHandler(this.chkCreateFiles_CheckedChanged);
			// 
			// txtLog
			// 
			this.txtLog.BackColor = System.Drawing.Color.White;
			this.txtLog.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLog.Location = new System.Drawing.Point(12, 169);
			this.txtLog.MaxLength = 0;
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(609, 143);
			this.txtLog.TabIndex = 3;
			// 
			// prgProgress
			// 
			this.prgProgress.Location = new System.Drawing.Point(12, 318);
			this.prgProgress.Name = "prgProgress";
			this.prgProgress.Size = new System.Drawing.Size(610, 23);
			this.prgProgress.Step = 1;
			this.prgProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.prgProgress.TabIndex = 4;
			this.prgProgress.Click += new System.EventHandler(this.prgProgress_Click);
			// 
			// tmrElapsed
			// 
			this.tmrElapsed.Tick += new System.EventHandler(this.tmrElapsed_Tick);
			// 
			// graph1
			// 
			this.graph1.BackColor = System.Drawing.Color.White;
			this.graph1.Location = new System.Drawing.Point(12, 347);
			this.graph1.Name = "graph1";
			this.graph1.Size = new System.Drawing.Size(610, 215);
			this.graph1.TabIndex = 5;
			this.graph1.Text = "graph1";
			this.graph1.Visible = false;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634, 349);
			this.Controls.Add(this.graph1);
			this.Controls.Add(this.prgProgress);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.grpSettings);
			this.Controls.Add(this.grpStatus);
			this.Controls.Add(this.btnStart);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.Text = "Genetic Assigner";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.grpStatus.ResumeLayout(false);
			this.grpStatus.PerformLayout();
			this.grpSettings.ResumeLayout(false);
			this.grpSettings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudPopulation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMutationRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudGenerations)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudOverflow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudOptions)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox grpStatus;
		private System.Windows.Forms.Label lblTimeValue;
		private System.Windows.Forms.Label lblNotAssignedValue;
		private System.Windows.Forms.Label lblNotAssigned;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.GroupBox grpSettings;
		private System.Windows.Forms.TextBox txtOutputFolder;
		private System.Windows.Forms.CheckBox chkCreateFiles;
		private System.Windows.Forms.CheckBox chkElitism;
		private System.Windows.Forms.TextBox txtSeed;
		private System.Windows.Forms.Label lblSeed;
		private System.Windows.Forms.Label lblGenerations;
		private System.Windows.Forms.Label lblPopulation;
		private System.Windows.Forms.Label lblMutationRate;
		private System.Windows.Forms.Label lblOutputFolder;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Label lblGeneration;
		private System.Windows.Forms.Label lblGenerationValue;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.ProgressBar prgProgress;
		private System.Windows.Forms.Label lblOptions;
		private System.Windows.Forms.CheckBox chkOpenFolder;
		private System.Windows.Forms.Timer tmrElapsed;
		private System.Windows.Forms.CheckBox chkFixedSeed;
		private Graph graph1;
		private System.Windows.Forms.CheckBox chkOverflow;
		private System.Windows.Forms.NumericUpDown nudOptions;
		private System.Windows.Forms.NumericUpDown nudGenerations;
		private System.Windows.Forms.NumericUpDown nudMutationRate;
		private System.Windows.Forms.NumericUpDown nudPopulation;
		private System.Windows.Forms.NumericUpDown nudOverflow;
	}
}

