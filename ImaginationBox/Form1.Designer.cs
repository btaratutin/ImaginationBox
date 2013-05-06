namespace ImaginationBox
{
    partial class Form1
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
            this.startButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.wordsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numFlashesChooser = new System.Windows.Forms.NumericUpDown();
            this.goRandomButton = new System.Windows.Forms.Button();
            this.topPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.randomWordButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numFlashesChooser)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(539, 61);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start Flash";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Location = new System.Drawing.Point(112, 8);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(209, 23);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // wordsTextBox
            // 
            this.wordsTextBox.Location = new System.Drawing.Point(145, 64);
            this.wordsTextBox.Name = "wordsTextBox";
            this.wordsTextBox.Size = new System.Drawing.Size(290, 20);
            this.wordsTextBox.TabIndex = 6;
            this.wordsTextBox.Text = "university";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Words";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Separated by commas";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(630, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "# Flashes:";
            // 
            // numFlashesChooser
            // 
            this.numFlashesChooser.Location = new System.Drawing.Point(692, 64);
            this.numFlashesChooser.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numFlashesChooser.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFlashesChooser.Name = "numFlashesChooser";
            this.numFlashesChooser.Size = new System.Drawing.Size(47, 20);
            this.numFlashesChooser.TabIndex = 9;
            this.numFlashesChooser.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numFlashesChooser.ValueChanged += new System.EventHandler(this.numFlashesChooser_ValueChanged);
            // 
            // goRandomButton
            // 
            this.goRandomButton.Location = new System.Drawing.Point(978, 62);
            this.goRandomButton.Name = "goRandomButton";
            this.goRandomButton.Size = new System.Drawing.Size(94, 23);
            this.goRandomButton.TabIndex = 11;
            this.goRandomButton.Text = "Random Flash";
            this.goRandomButton.UseVisualStyleBackColor = true;
            this.goRandomButton.Click += new System.EventHandler(this.goRandomButton_Click);
            // 
            // topPanel
            // 
            this.topPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1084, 462);
            this.topPanel.TabIndex = 12;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.randomWordButton);
            this.panel2.Controls.Add(this.startButton);
            this.panel2.Controls.Add(this.goRandomButton);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.wordsTextBox);
            this.panel2.Controls.Add(this.numFlashesChooser);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 462);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1084, 100);
            this.panel2.TabIndex = 13;
            // 
            // randomWordButton
            // 
            this.randomWordButton.Location = new System.Drawing.Point(441, 61);
            this.randomWordButton.Name = "randomWordButton";
            this.randomWordButton.Size = new System.Drawing.Size(92, 23);
            this.randomWordButton.TabIndex = 12;
            this.randomWordButton.Text = "Random Words";
            this.randomWordButton.UseVisualStyleBackColor = true;
            this.randomWordButton.Click += new System.EventHandler(this.randomWordButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 562);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.statusLabel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.numFlashesChooser)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label statusLabel;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox wordsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numFlashesChooser;
        private System.Windows.Forms.Button goRandomButton;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button randomWordButton;
    }
}

