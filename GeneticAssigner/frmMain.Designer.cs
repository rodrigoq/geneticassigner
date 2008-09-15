namespace GeneticAssigner {
	partial class frmMain {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.grpStatus = new System.Windows.Forms.GroupBox();
			this.lblGeneracion = new System.Windows.Forms.Label();
			this.lbl5 = new System.Windows.Forms.Label();
			this.lbl4 = new System.Windows.Forms.Label();
			this.lblTiempo = new System.Windows.Forms.Label();
			this.lblNoAsignados = new System.Windows.Forms.Label();
			this.lbl0 = new System.Windows.Forms.Label();
			this.btnStart = new System.Windows.Forms.Button();
			this.grpSettings = new System.Windows.Forms.GroupBox();
			this.chkFolder = new System.Windows.Forms.CheckBox();
			this.lblOptions = new System.Windows.Forms.Label();
			this.txtOptions = new System.Windows.Forms.TextBox();
			this.lblSeed = new System.Windows.Forms.Label();
			this.lblGenerations = new System.Windows.Forms.Label();
			this.lblIndividuos = new System.Windows.Forms.Label();
			this.lblMutationRate = new System.Windows.Forms.Label();
			this.lblPath = new System.Windows.Forms.Label();
			this.txtSeed = new System.Windows.Forms.TextBox();
			this.chkElitism = new System.Windows.Forms.CheckBox();
			this.txtIndividuos = new System.Windows.Forms.TextBox();
			this.txtGeneraciones = new System.Windows.Forms.TextBox();
			this.txtMutationRate = new System.Windows.Forms.TextBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.chkSaveFiles = new System.Windows.Forms.CheckBox();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.prgProgress = new System.Windows.Forms.ProgressBar();
			this.grpStatus.SuspendLayout();
			this.grpSettings.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpStatus
			// 
			this.grpStatus.Controls.Add(this.lblGeneracion);
			this.grpStatus.Controls.Add(this.lbl5);
			this.grpStatus.Controls.Add(this.lbl4);
			this.grpStatus.Controls.Add(this.lblTiempo);
			this.grpStatus.Controls.Add(this.lblNoAsignados);
			this.grpStatus.Controls.Add(this.lbl0);
			this.grpStatus.Location = new System.Drawing.Point(12, 12);
			this.grpStatus.Name = "grpStatus";
			this.grpStatus.Size = new System.Drawing.Size(211, 92);
			this.grpStatus.TabIndex = 11;
			this.grpStatus.TabStop = false;
			this.grpStatus.Text = "Estado";
			// 
			// lblGeneracion
			// 
			this.lblGeneracion.AutoSize = true;
			this.lblGeneracion.Location = new System.Drawing.Point(133, 45);
			this.lblGeneracion.Name = "lblGeneracion";
			this.lblGeneracion.Size = new System.Drawing.Size(31, 13);
			this.lblGeneracion.TabIndex = 23;
			this.lblGeneracion.Text = "0000";
			this.lblGeneracion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl5
			// 
			this.lbl5.AutoSize = true;
			this.lbl5.Location = new System.Drawing.Point(43, 69);
			this.lbl5.Name = "lbl5";
			this.lbl5.Size = new System.Drawing.Size(48, 13);
			this.lbl5.TabIndex = 22;
			this.lbl5.Text = "Tiempo: ";
			// 
			// lbl4
			// 
			this.lbl4.AutoSize = true;
			this.lbl4.Location = new System.Drawing.Point(43, 45);
			this.lbl4.Name = "lbl4";
			this.lbl4.Size = new System.Drawing.Size(65, 13);
			this.lbl4.TabIndex = 21;
			this.lbl4.Text = "Generación:";
			// 
			// lblTiempo
			// 
			this.lblTiempo.AutoSize = true;
			this.lblTiempo.Location = new System.Drawing.Point(109, 69);
			this.lblTiempo.Name = "lblTiempo";
			this.lblTiempo.Size = new System.Drawing.Size(55, 13);
			this.lblTiempo.TabIndex = 20;
			this.lblTiempo.Text = "00:00.000";
			this.lblTiempo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblNoAsignados
			// 
			this.lblNoAsignados.AutoSize = true;
			this.lblNoAsignados.Location = new System.Drawing.Point(145, 23);
			this.lblNoAsignados.Name = "lblNoAsignados";
			this.lblNoAsignados.Size = new System.Drawing.Size(19, 13);
			this.lblNoAsignados.TabIndex = 16;
			this.lblNoAsignados.Text = "00";
			this.lblNoAsignados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lbl0
			// 
			this.lbl0.AutoSize = true;
			this.lbl0.Location = new System.Drawing.Point(43, 23);
			this.lbl0.Name = "lbl0";
			this.lbl0.Size = new System.Drawing.Size(73, 13);
			this.lbl0.TabIndex = 12;
			this.lbl0.Text = "No Asignados";
			// 
			// btnStart
			// 
			this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnStart.Location = new System.Drawing.Point(12, 113);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(211, 50);
			this.btnStart.TabIndex = 11;
			this.btnStart.Text = "Iniciar";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// grpSettings
			// 
			this.grpSettings.Controls.Add(this.chkFolder);
			this.grpSettings.Controls.Add(this.lblOptions);
			this.grpSettings.Controls.Add(this.txtOptions);
			this.grpSettings.Controls.Add(this.lblSeed);
			this.grpSettings.Controls.Add(this.lblGenerations);
			this.grpSettings.Controls.Add(this.lblIndividuos);
			this.grpSettings.Controls.Add(this.lblMutationRate);
			this.grpSettings.Controls.Add(this.lblPath);
			this.grpSettings.Controls.Add(this.txtSeed);
			this.grpSettings.Controls.Add(this.chkElitism);
			this.grpSettings.Controls.Add(this.txtIndividuos);
			this.grpSettings.Controls.Add(this.txtGeneraciones);
			this.grpSettings.Controls.Add(this.txtMutationRate);
			this.grpSettings.Controls.Add(this.txtPath);
			this.grpSettings.Controls.Add(this.chkSaveFiles);
			this.grpSettings.Location = new System.Drawing.Point(240, 12);
			this.grpSettings.Name = "grpSettings";
			this.grpSettings.Size = new System.Drawing.Size(381, 151);
			this.grpSettings.TabIndex = 12;
			this.grpSettings.TabStop = false;
			this.grpSettings.Text = "Configuración";
			// 
			// chkFolder
			// 
			this.chkFolder.AutoSize = true;
			this.chkFolder.Checked = true;
			this.chkFolder.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFolder.Location = new System.Drawing.Point(7, 68);
			this.chkFolder.Name = "chkFolder";
			this.chkFolder.Size = new System.Drawing.Size(84, 17);
			this.chkFolder.TabIndex = 24;
			this.chkFolder.Text = "Open Folder";
			this.chkFolder.UseVisualStyleBackColor = true;
			// 
			// lblOptions
			// 
			this.lblOptions.AutoSize = true;
			this.lblOptions.Location = new System.Drawing.Point(158, 119);
			this.lblOptions.Name = "lblOptions";
			this.lblOptions.Size = new System.Drawing.Size(43, 13);
			this.lblOptions.TabIndex = 23;
			this.lblOptions.Text = "Options";
			// 
			// txtOptions
			// 
			this.txtOptions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtOptions.Location = new System.Drawing.Point(204, 116);
			this.txtOptions.Name = "txtOptions";
			this.txtOptions.Size = new System.Drawing.Size(38, 20);
			this.txtOptions.TabIndex = 22;
			this.txtOptions.Text = "3";
			this.txtOptions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// lblSeed
			// 
			this.lblSeed.AutoSize = true;
			this.lblSeed.Location = new System.Drawing.Point(265, 120);
			this.lblSeed.Name = "lblSeed";
			this.lblSeed.Size = new System.Drawing.Size(32, 13);
			this.lblSeed.TabIndex = 21;
			this.lblSeed.Text = "Seed";
			// 
			// lblGenerations
			// 
			this.lblGenerations.AutoSize = true;
			this.lblGenerations.Location = new System.Drawing.Point(227, 67);
			this.lblGenerations.Name = "lblGenerations";
			this.lblGenerations.Size = new System.Drawing.Size(73, 13);
			this.lblGenerations.TabIndex = 20;
			this.lblGenerations.Text = "Generaciones";
			// 
			// lblIndividuos
			// 
			this.lblIndividuos.AutoSize = true;
			this.lblIndividuos.Location = new System.Drawing.Point(245, 90);
			this.lblIndividuos.Name = "lblIndividuos";
			this.lblIndividuos.Size = new System.Drawing.Size(55, 13);
			this.lblIndividuos.TabIndex = 19;
			this.lblIndividuos.Text = "Individuos";
			// 
			// lblMutationRate
			// 
			this.lblMutationRate.AutoSize = true;
			this.lblMutationRate.Location = new System.Drawing.Point(238, 42);
			this.lblMutationRate.Name = "lblMutationRate";
			this.lblMutationRate.Size = new System.Drawing.Size(62, 13);
			this.lblMutationRate.TabIndex = 18;
			this.lblMutationRate.Text = "Mutación %";
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new System.Drawing.Point(215, 20);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(82, 13);
			this.lblPath.TabIndex = 17;
			this.lblPath.Text = "Directorio salida";
			// 
			// txtSeed
			// 
			this.txtSeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSeed.Location = new System.Drawing.Point(306, 117);
			this.txtSeed.Name = "txtSeed";
			this.txtSeed.Size = new System.Drawing.Size(69, 20);
			this.txtSeed.TabIndex = 16;
			this.txtSeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// chkElitism
			// 
			this.chkElitism.AutoSize = true;
			this.chkElitism.Checked = true;
			this.chkElitism.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkElitism.Location = new System.Drawing.Point(7, 45);
			this.chkElitism.Name = "chkElitism";
			this.chkElitism.Size = new System.Drawing.Size(194, 17);
			this.chkElitism.TabIndex = 15;
			this.chkElitism.Text = "Conservar mejor de cada genarción";
			this.chkElitism.UseVisualStyleBackColor = true;
			// 
			// txtIndividuos
			// 
			this.txtIndividuos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtIndividuos.Location = new System.Drawing.Point(306, 90);
			this.txtIndividuos.Name = "txtIndividuos";
			this.txtIndividuos.Size = new System.Drawing.Size(69, 20);
			this.txtIndividuos.TabIndex = 14;
			this.txtIndividuos.Text = "100";
			this.txtIndividuos.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtGeneraciones
			// 
			this.txtGeneraciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtGeneraciones.Location = new System.Drawing.Point(306, 64);
			this.txtGeneraciones.Name = "txtGeneraciones";
			this.txtGeneraciones.Size = new System.Drawing.Size(69, 20);
			this.txtGeneraciones.TabIndex = 5;
			this.txtGeneraciones.Text = "2000";
			this.txtGeneraciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtMutationRate
			// 
			this.txtMutationRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtMutationRate.Location = new System.Drawing.Point(306, 39);
			this.txtMutationRate.Name = "txtMutationRate";
			this.txtMutationRate.Size = new System.Drawing.Size(69, 20);
			this.txtMutationRate.TabIndex = 4;
			this.txtMutationRate.Text = "80";
			this.txtMutationRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtPath
			// 
			this.txtPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtPath.Location = new System.Drawing.Point(306, 15);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(70, 20);
			this.txtPath.TabIndex = 3;
			this.txtPath.Text = "C:\\";
			// 
			// chkSaveFiles
			// 
			this.chkSaveFiles.AutoSize = true;
			this.chkSaveFiles.Checked = true;
			this.chkSaveFiles.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkSaveFiles.Location = new System.Drawing.Point(7, 20);
			this.chkSaveFiles.Name = "chkSaveFiles";
			this.chkSaveFiles.Size = new System.Drawing.Size(108, 17);
			this.chkSaveFiles.TabIndex = 0;
			this.chkSaveFiles.Text = "Generar Archivos";
			this.chkSaveFiles.UseVisualStyleBackColor = true;
			// 
			// txtLog
			// 
			this.txtLog.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.txtLog.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLog.Location = new System.Drawing.Point(12, 169);
			this.txtLog.MaxLength = 0;
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ReadOnly = true;
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(609, 143);
			this.txtLog.TabIndex = 13;
			// 
			// prgProgress
			// 
			this.prgProgress.Location = new System.Drawing.Point(12, 318);
			this.prgProgress.Name = "prgProgress";
			this.prgProgress.Size = new System.Drawing.Size(610, 23);
			this.prgProgress.Step = 1;
			this.prgProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.prgProgress.TabIndex = 14;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(634, 350);
			this.Controls.Add(this.prgProgress);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.grpSettings);
			this.Controls.Add(this.grpStatus);
			this.Controls.Add(this.btnStart);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.Text = "Genetic Assignator";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.grpStatus.ResumeLayout(false);
			this.grpStatus.PerformLayout();
			this.grpSettings.ResumeLayout(false);
			this.grpSettings.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox grpStatus;
		private System.Windows.Forms.Label lblTiempo;
		private System.Windows.Forms.Label lblNoAsignados;
		private System.Windows.Forms.Label lbl0;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.GroupBox grpSettings;
		private System.Windows.Forms.TextBox txtMutationRate;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.CheckBox chkSaveFiles;
		private System.Windows.Forms.TextBox txtGeneraciones;
		private System.Windows.Forms.CheckBox chkElitism;
		private System.Windows.Forms.TextBox txtIndividuos;
		private System.Windows.Forms.TextBox txtSeed;
		private System.Windows.Forms.Label lblSeed;
		private System.Windows.Forms.Label lblGenerations;
		private System.Windows.Forms.Label lblIndividuos;
		private System.Windows.Forms.Label lblMutationRate;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Label lbl4;
		private System.Windows.Forms.Label lblGeneracion;
		private System.Windows.Forms.Label lbl5;
		private System.Windows.Forms.ProgressBar prgProgress;
		private System.Windows.Forms.Label lblOptions;
		private System.Windows.Forms.TextBox txtOptions;
		private System.Windows.Forms.CheckBox chkFolder;
	}
}

