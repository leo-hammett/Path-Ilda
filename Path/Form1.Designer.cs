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
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.OptionsLoadPathFile = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.OptionsKPPSLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OptionsFPSLabel = new System.Windows.Forms.Label();
            this.TimeLinePanel = new System.Windows.Forms.Panel();
            this.TimeLineFramesInput = new System.Windows.Forms.TextBox();
            this.TimeLineSecondsInput = new System.Windows.Forms.TextBox();
            this.TimeLineFramesLabel = new System.Windows.Forms.Label();
            this.TimeLineSecondsLabel = new System.Windows.Forms.Label();
            this.TimeLinePlay = new System.Windows.Forms.CheckBox();
            this.TimeLineNextFrame = new System.Windows.Forms.Button();
            this.TimeLineBackFrame = new System.Windows.Forms.Button();
            this.TimeLineTitleLabel = new System.Windows.Forms.Label();
            this.OptionsSavePathFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.LinePropertiesPanel = new System.Windows.Forms.Panel();
            this.OptionsToggleProject = new System.Windows.Forms.CheckBox();
            this.OptionsEmergencyLabel = new System.Windows.Forms.Label();
            this.OptionsSelectModeButton = new System.Windows.Forms.CheckBox();
            this.OptionsDrawLineMode = new System.Windows.Forms.CheckBox();
            this.LinePropertiesTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.OptionsPanel.SuspendLayout();
            this.TimeLinePanel.SuspendLayout();
            this.LinePropertiesPanel.SuspendLayout();
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
            // OptionsPanel
            // 
            this.OptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OptionsPanel.Controls.Add(this.OptionsDrawLineMode);
            this.OptionsPanel.Controls.Add(this.OptionsSelectModeButton);
            this.OptionsPanel.Controls.Add(this.OptionsEmergencyLabel);
            this.OptionsPanel.Controls.Add(this.OptionsToggleProject);
            this.OptionsPanel.Controls.Add(this.OptionsSavePathFile);
            this.OptionsPanel.Controls.Add(this.OptionsLoadPathFile);
            this.OptionsPanel.Controls.Add(this.textBox4);
            this.OptionsPanel.Controls.Add(this.textBox3);
            this.OptionsPanel.Controls.Add(this.OptionsKPPSLabel);
            this.OptionsPanel.Controls.Add(this.label4);
            this.OptionsPanel.Controls.Add(this.OptionsFPSLabel);
            this.OptionsPanel.Location = new System.Drawing.Point(530, 438);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(394, 206);
            this.OptionsPanel.TabIndex = 1;
            // 
            // OptionsLoadPathFile
            // 
            this.OptionsLoadPathFile.Location = new System.Drawing.Point(3, 99);
            this.OptionsLoadPathFile.Name = "OptionsLoadPathFile";
            this.OptionsLoadPathFile.Size = new System.Drawing.Size(116, 23);
            this.OptionsLoadPathFile.TabIndex = 12;
            this.OptionsLoadPathFile.Text = "Load Path File";
            this.OptionsLoadPathFile.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(149, 71);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(145, 23);
            this.textBox4.TabIndex = 11;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(149, 42);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(145, 23);
            this.textBox3.TabIndex = 9;
            // 
            // OptionsKPPSLabel
            // 
            this.OptionsKPPSLabel.AutoSize = true;
            this.OptionsKPPSLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.OptionsKPPSLabel.Location = new System.Drawing.Point(3, 74);
            this.OptionsKPPSLabel.Name = "OptionsKPPSLabel";
            this.OptionsKPPSLabel.Size = new System.Drawing.Size(143, 15);
            this.OptionsKPPSLabel.TabIndex = 10;
            this.OptionsKPPSLabel.Text = "KPPS (Points Per Second):";
            // 
            // label4
            // 
            this.label4.AccessibleName = "Time Manager";
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label4.Location = new System.Drawing.Point(-2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 30);
            this.label4.TabIndex = 9;
            this.label4.Text = "Options";
            // 
            // OptionsFPSLabel
            // 
            this.OptionsFPSLabel.AutoSize = true;
            this.OptionsFPSLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.OptionsFPSLabel.Location = new System.Drawing.Point(3, 45);
            this.OptionsFPSLabel.Name = "OptionsFPSLabel";
            this.OptionsFPSLabel.Size = new System.Drawing.Size(140, 15);
            this.OptionsFPSLabel.TabIndex = 9;
            this.OptionsFPSLabel.Text = "FPS (Frames Per Second):";
            // 
            // TimeLinePanel
            // 
            this.TimeLinePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TimeLinePanel.Controls.Add(this.TimeLineFramesInput);
            this.TimeLinePanel.Controls.Add(this.TimeLineSecondsInput);
            this.TimeLinePanel.Controls.Add(this.TimeLineFramesLabel);
            this.TimeLinePanel.Controls.Add(this.TimeLineSecondsLabel);
            this.TimeLinePanel.Controls.Add(this.TimeLinePlay);
            this.TimeLinePanel.Controls.Add(this.TimeLineNextFrame);
            this.TimeLinePanel.Controls.Add(this.TimeLineBackFrame);
            this.TimeLinePanel.Controls.Add(this.TimeLineTitleLabel);
            this.TimeLinePanel.Location = new System.Drawing.Point(12, 530);
            this.TimeLinePanel.Name = "TimeLinePanel";
            this.TimeLinePanel.Size = new System.Drawing.Size(512, 114);
            this.TimeLinePanel.TabIndex = 2;
            // 
            // TimeLineFramesInput
            // 
            this.TimeLineFramesInput.Location = new System.Drawing.Point(305, 79);
            this.TimeLineFramesInput.Name = "TimeLineFramesInput";
            this.TimeLineFramesInput.Size = new System.Drawing.Size(145, 23);
            this.TimeLineFramesInput.TabIndex = 8;
            // 
            // TimeLineSecondsInput
            // 
            this.TimeLineSecondsInput.Location = new System.Drawing.Point(305, 48);
            this.TimeLineSecondsInput.Name = "TimeLineSecondsInput";
            this.TimeLineSecondsInput.Size = new System.Drawing.Size(145, 23);
            this.TimeLineSecondsInput.TabIndex = 7;
            // 
            // TimeLineFramesLabel
            // 
            this.TimeLineFramesLabel.AutoSize = true;
            this.TimeLineFramesLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeLineFramesLabel.Location = new System.Drawing.Point(211, 82);
            this.TimeLineFramesLabel.Name = "TimeLineFramesLabel";
            this.TimeLineFramesLabel.Size = new System.Drawing.Size(82, 15);
            this.TimeLineFramesLabel.TabIndex = 6;
            this.TimeLineFramesLabel.Text = "Time (Frames)";
            // 
            // TimeLineSecondsLabel
            // 
            this.TimeLineSecondsLabel.AutoSize = true;
            this.TimeLineSecondsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeLineSecondsLabel.Location = new System.Drawing.Point(211, 51);
            this.TimeLineSecondsLabel.Name = "TimeLineSecondsLabel";
            this.TimeLineSecondsLabel.Size = new System.Drawing.Size(88, 15);
            this.TimeLineSecondsLabel.TabIndex = 5;
            this.TimeLineSecondsLabel.Text = "Time (Seconds)";
            // 
            // TimeLinePlay
            // 
            this.TimeLinePlay.Appearance = System.Windows.Forms.Appearance.Button;
            this.TimeLinePlay.AutoSize = true;
            this.TimeLinePlay.Location = new System.Drawing.Point(305, 4);
            this.TimeLinePlay.Name = "TimeLinePlay";
            this.TimeLinePlay.Size = new System.Drawing.Size(39, 25);
            this.TimeLinePlay.TabIndex = 4;
            this.TimeLinePlay.Text = "Play";
            this.TimeLinePlay.UseVisualStyleBackColor = true;
            // 
            // TimeLineNextFrame
            // 
            this.TimeLineNextFrame.Location = new System.Drawing.Point(350, 3);
            this.TimeLineNextFrame.Name = "TimeLineNextFrame";
            this.TimeLineNextFrame.Size = new System.Drawing.Size(77, 26);
            this.TimeLineNextFrame.TabIndex = 3;
            this.TimeLineNextFrame.Text = "Next";
            this.TimeLineNextFrame.UseVisualStyleBackColor = true;
            // 
            // TimeLineBackFrame
            // 
            this.TimeLineBackFrame.Location = new System.Drawing.Point(222, 3);
            this.TimeLineBackFrame.Name = "TimeLineBackFrame";
            this.TimeLineBackFrame.Size = new System.Drawing.Size(77, 27);
            this.TimeLineBackFrame.TabIndex = 2;
            this.TimeLineBackFrame.Text = "Back";
            this.TimeLineBackFrame.UseVisualStyleBackColor = true;
            // 
            // TimeLineTitleLabel
            // 
            this.TimeLineTitleLabel.AccessibleName = "Time Manager";
            this.TimeLineTitleLabel.AutoSize = true;
            this.TimeLineTitleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TimeLineTitleLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.TimeLineTitleLabel.Location = new System.Drawing.Point(3, 0);
            this.TimeLineTitleLabel.Name = "TimeLineTitleLabel";
            this.TimeLineTitleLabel.Size = new System.Drawing.Size(147, 30);
            this.TimeLineTitleLabel.TabIndex = 0;
            this.TimeLineTitleLabel.Text = "Time Operator";
            // 
            // OptionsSavePathFile
            // 
            this.OptionsSavePathFile.Location = new System.Drawing.Point(125, 99);
            this.OptionsSavePathFile.Name = "OptionsSavePathFile";
            this.OptionsSavePathFile.Size = new System.Drawing.Size(116, 23);
            this.OptionsSavePathFile.TabIndex = 13;
            this.OptionsSavePathFile.Text = "Save Path File";
            this.OptionsSavePathFile.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LinePropertiesPanel
            // 
            this.LinePropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesTitle);
            this.LinePropertiesPanel.Location = new System.Drawing.Point(530, 12);
            this.LinePropertiesPanel.Name = "LinePropertiesPanel";
            this.LinePropertiesPanel.Size = new System.Drawing.Size(394, 420);
            this.LinePropertiesPanel.TabIndex = 3;
            // 
            // OptionsToggleProject
            // 
            this.OptionsToggleProject.Appearance = System.Windows.Forms.Appearance.Button;
            this.OptionsToggleProject.AutoSize = true;
            this.OptionsToggleProject.Location = new System.Drawing.Point(3, 128);
            this.OptionsToggleProject.Name = "OptionsToggleProject";
            this.OptionsToggleProject.Size = new System.Drawing.Size(68, 25);
            this.OptionsToggleProject.TabIndex = 14;
            this.OptionsToggleProject.Text = "Laser On?";
            this.OptionsToggleProject.UseVisualStyleBackColor = true;
            // 
            // OptionsEmergencyLabel
            // 
            this.OptionsEmergencyLabel.AutoSize = true;
            this.OptionsEmergencyLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.OptionsEmergencyLabel.Location = new System.Drawing.Point(77, 133);
            this.OptionsEmergencyLabel.Name = "OptionsEmergencyLabel";
            this.OptionsEmergencyLabel.Size = new System.Drawing.Size(162, 15);
            this.OptionsEmergencyLabel.TabIndex = 15;
            this.OptionsEmergencyLabel.Text = "PRESS SPACE IN EMERGENCY";
            // 
            // OptionsSelectModeButton
            // 
            this.OptionsSelectModeButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.OptionsSelectModeButton.AutoSize = true;
            this.OptionsSelectModeButton.Location = new System.Drawing.Point(3, 171);
            this.OptionsSelectModeButton.Name = "OptionsSelectModeButton";
            this.OptionsSelectModeButton.Size = new System.Drawing.Size(73, 25);
            this.OptionsSelectModeButton.TabIndex = 16;
            this.OptionsSelectModeButton.Text = "Select Tool";
            this.OptionsSelectModeButton.UseVisualStyleBackColor = true;
            // 
            // OptionsDrawLineMode
            // 
            this.OptionsDrawLineMode.Appearance = System.Windows.Forms.Appearance.Button;
            this.OptionsDrawLineMode.AutoSize = true;
            this.OptionsDrawLineMode.Location = new System.Drawing.Point(82, 171);
            this.OptionsDrawLineMode.Name = "OptionsDrawLineMode";
            this.OptionsDrawLineMode.Size = new System.Drawing.Size(69, 25);
            this.OptionsDrawLineMode.TabIndex = 17;
            this.OptionsDrawLineMode.Text = "Draw Line";
            this.OptionsDrawLineMode.UseVisualStyleBackColor = true;
            // 
            // LinePropertiesTitle
            // 
            this.LinePropertiesTitle.AccessibleName = "Time Manager";
            this.LinePropertiesTitle.AutoSize = true;
            this.LinePropertiesTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LinePropertiesTitle.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.LinePropertiesTitle.Location = new System.Drawing.Point(3, 0);
            this.LinePropertiesTitle.Name = "LinePropertiesTitle";
            this.LinePropertiesTitle.Size = new System.Drawing.Size(150, 30);
            this.LinePropertiesTitle.TabIndex = 18;
            this.LinePropertiesTitle.Text = "Line Properties";
            // 
            // Form1
            // 
            this.AccessibleName = "Path Window";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(936, 656);
            this.Controls.Add(this.LinePropertiesPanel);
            this.Controls.Add(this.TimeLinePanel);
            this.Controls.Add(this.OptionsPanel);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Path";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.TimeLinePanel.ResumeLayout(false);
            this.TimeLinePanel.PerformLayout();
            this.LinePropertiesPanel.ResumeLayout(false);
            this.LinePropertiesPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private Panel OptionsPanel;
        private Panel TimeLinePanel;
        private Button TimeLineNextFrame;
        private Button TimeLineBackFrame;
        private Label TimeLineTitleLabel;
        private CheckBox TimeLinePlay;
        private TextBox textBox4;
        private TextBox textBox3;
        private Label OptionsKPPSLabel;
        private Label label4;
        private Label OptionsFPSLabel;
        private TextBox TimeLineFramesInput;
        private TextBox TimeLineSecondsInput;
        private Label TimeLineFramesLabel;
        private Label TimeLineSecondsLabel;
        private Button OptionsLoadPathFile;
        private Button OptionsSavePathFile;
        private CheckBox OptionsDrawLineMode;
        private CheckBox OptionsSelectModeButton;
        private Label OptionsEmergencyLabel;
        private CheckBox OptionsToggleProject;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private Panel LinePropertiesPanel;
        private Label LinePropertiesTitle;
    }
}