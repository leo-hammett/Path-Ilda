namespace Path
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.timeLinePanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.TimeLineBackOneFrame = new System.Windows.Forms.Button();
            this.timeLineTitle = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.timeLinePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(530, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 632);
            this.panel1.TabIndex = 1;
            // 
            // timeLinePanel
            // 
            this.timeLinePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.timeLinePanel.Controls.Add(this.checkBox1);
            this.timeLinePanel.Controls.Add(this.button1);
            this.timeLinePanel.Controls.Add(this.TimeLineBackOneFrame);
            this.timeLinePanel.Controls.Add(this.timeLineTitle);
            this.timeLinePanel.Location = new System.Drawing.Point(12, 530);
            this.timeLinePanel.Name = "timeLinePanel";
            this.timeLinePanel.Size = new System.Drawing.Size(512, 114);
            this.timeLinePanel.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(350, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77, 39);
            this.button1.TabIndex = 3;
            this.button1.Text = "BackOneFrame";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TimeLineBackOneFrame
            // 
            this.TimeLineBackOneFrame.Location = new System.Drawing.Point(222, 3);
            this.TimeLineBackOneFrame.Name = "TimeLineBackOneFrame";
            this.TimeLineBackOneFrame.Size = new System.Drawing.Size(77, 39);
            this.TimeLineBackOneFrame.TabIndex = 2;
            this.TimeLineBackOneFrame.Text = "BackOneFrame";
            this.TimeLineBackOneFrame.UseVisualStyleBackColor = true;
            // 
            // timeLineTitle
            // 
            this.timeLineTitle.AccessibleName = "Time Manager";
            this.timeLineTitle.AutoSize = true;
            this.timeLineTitle.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.timeLineTitle.Location = new System.Drawing.Point(3, 0);
            this.timeLineTitle.Name = "timeLineTitle";
            this.timeLineTitle.Size = new System.Drawing.Size(83, 15);
            this.timeLineTitle.TabIndex = 0;
            this.timeLineTitle.Text = "Time Operator";
            // 
            // checkBox1
            // 
            this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(305, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(39, 25);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Play";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AccessibleName = "Path Window";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(936, 656);
            this.Controls.Add(this.timeLinePanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Path";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.timeLinePanel.ResumeLayout(false);
            this.timeLinePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Panel panel1;
        private Panel timeLinePanel;
        private Button button1;
        private Button TimeLineBackOneFrame;
        private Label timeLineTitle;
        private CheckBox checkBox1;
    }
}