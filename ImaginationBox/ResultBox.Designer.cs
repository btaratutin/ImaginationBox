namespace ImaginationBox
{
    partial class ResultBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultBox));
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.pictureBox_label = new System.Windows.Forms.Label();
            this.btnThumbUp = new System.Windows.Forms.CheckBox();
            this.btnThumbDown = new System.Windows.Forms.CheckBox();
            this.rating_label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox.Location = new System.Drawing.Point(16, 14);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(260, 245);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // pictureBox_label
            // 
            this.pictureBox_label.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBox_label.AutoSize = true;
            this.pictureBox_label.Location = new System.Drawing.Point(110, 271);
            this.pictureBox_label.Name = "pictureBox_label";
            this.pictureBox_label.Size = new System.Drawing.Size(50, 13);
            this.pictureBox_label.TabIndex = 12;
            this.pictureBox_label.Text = "loading...";
            this.pictureBox_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnThumbUp
            // 
            this.btnThumbUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThumbUp.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnThumbUp.Image = ((System.Drawing.Image)(resources.GetObject("btnThumbUp.Image")));
            this.btnThumbUp.Location = new System.Drawing.Point(205, 264);
            this.btnThumbUp.Name = "btnThumbUp";
            this.btnThumbUp.Size = new System.Drawing.Size(32, 32);
            this.btnThumbUp.TabIndex = 13;
            this.btnThumbUp.UseVisualStyleBackColor = true;
            this.btnThumbUp.Click += new System.EventHandler(this.btnThumbUp_Click);
            // 
            // btnThumbDown
            // 
            this.btnThumbDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThumbDown.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnThumbDown.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnThumbDown.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnThumbDown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnThumbDown.Image = ((System.Drawing.Image)(resources.GetObject("btnThumbDown.Image")));
            this.btnThumbDown.Location = new System.Drawing.Point(243, 264);
            this.btnThumbDown.Name = "btnThumbDown";
            this.btnThumbDown.Size = new System.Drawing.Size(32, 32);
            this.btnThumbDown.TabIndex = 14;
            this.btnThumbDown.UseVisualStyleBackColor = true;
            this.btnThumbDown.Click += new System.EventHandler(this.btnThumbDown_Click);
            // 
            // rating_label
            // 
            this.rating_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rating_label.AutoSize = true;
            this.rating_label.Location = new System.Drawing.Point(174, 271);
            this.rating_label.Name = "rating_label";
            this.rating_label.Size = new System.Drawing.Size(25, 13);
            this.rating_label.TabIndex = 15;
            this.rating_label.Text = "(+1)";
            this.rating_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ResultBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rating_label);
            this.Controls.Add(this.btnThumbDown);
            this.Controls.Add(this.btnThumbUp);
            this.Controls.Add(this.pictureBox_label);
            this.Controls.Add(this.pictureBox);
            this.Name = "ResultBox";
            this.Size = new System.Drawing.Size(296, 298);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label pictureBox_label;
        private System.Windows.Forms.CheckBox btnThumbUp;
        private System.Windows.Forms.CheckBox btnThumbDown;
        private System.Windows.Forms.Label rating_label;
    }
}
