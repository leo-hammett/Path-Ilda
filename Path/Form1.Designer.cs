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
            this.PreviewGraphics = new System.Windows.Forms.PictureBox();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.OptionsSnapToPoint = new System.Windows.Forms.CheckBox();
            this.OptionsColorSelecterOpener = new System.Windows.Forms.Button();
            this.OptionsDrawLineMode = new System.Windows.Forms.CheckBox();
            this.OptionsSelectModeButton = new System.Windows.Forms.CheckBox();
            this.OptionsEmergencyLabel = new System.Windows.Forms.Label();
            this.OptionsToggleProject = new System.Windows.Forms.CheckBox();
            this.OptionsSavePathFile = new System.Windows.Forms.Button();
            this.OptionsLoadPathFile = new System.Windows.Forms.Button();
            this.OptionsKPPSTextBox = new System.Windows.Forms.TextBox();
            this.OptionsFPSTextBox = new System.Windows.Forms.TextBox();
            this.OptionsKPPSLabel = new System.Windows.Forms.Label();
            this.OptionsTitleLabel = new System.Windows.Forms.Label();
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.LinePropertiesPanel = new System.Windows.Forms.Panel();
            this.PathLinePointsListBox = new System.Windows.Forms.ListBox();
            this.LinePropertiesChangeColor = new System.Windows.Forms.Button();
            this.LinePropertiesPathIndexData = new System.Windows.Forms.Label();
            this.LinePropertiesPathIndexLabel = new System.Windows.Forms.Label();
            this.LinePropertiesKeyFramesTextBox = new System.Windows.Forms.ListBox();
            this.LinePropertiesTitle = new System.Windows.Forms.Label();
            this.InformationPanel = new System.Windows.Forms.Panel();
            this.InformationPreviewModeData = new System.Windows.Forms.Label();
            this.InformationPreviewModeLabel = new System.Windows.Forms.Label();
            this.InformationClosestPointData = new System.Windows.Forms.Label();
            this.InformationClosestPointLabel = new System.Windows.Forms.Label();
            this.InformationFrameListCountInfo = new System.Windows.Forms.Label();
            this.InformationDynamicListCountInfo = new System.Windows.Forms.Label();
            this.InformationFrameListCountLabel = new System.Windows.Forms.Label();
            this.InformationDynamicListCountLabel = new System.Windows.Forms.Label();
            this.InformationPoint2Info = new System.Windows.Forms.Label();
            this.InformationPoint1Info = new System.Windows.Forms.Label();
            this.InformationPoint2TitleLabel = new System.Windows.Forms.Label();
            this.InformationPoint1TitleLablel = new System.Windows.Forms.Label();
            this.InformationTitleLabel = new System.Windows.Forms.Label();
            this.DrawerColorDialog = new System.Windows.Forms.ColorDialog();
            this.LinePropertiesColorDialog = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewGraphics)).BeginInit();
            this.OptionsPanel.SuspendLayout();
            this.TimeLinePanel.SuspendLayout();
            this.LinePropertiesPanel.SuspendLayout();
            this.InformationPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PreviewGraphics
            // 
            this.PreviewGraphics.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PreviewGraphics.Location = new System.Drawing.Point(15, 15);
            this.PreviewGraphics.Name = "PreviewGraphics";
            this.PreviewGraphics.Size = new System.Drawing.Size(509, 509);
            this.PreviewGraphics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PreviewGraphics.TabIndex = 0;
            this.PreviewGraphics.TabStop = false;
            this.PreviewGraphics.Paint += new System.Windows.Forms.PaintEventHandler(this.PreviewGraphics_Paint);
            this.PreviewGraphics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PreviewGraphics_MouseDown);
            this.PreviewGraphics.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PreviewGraphics_MouseMove);
            this.PreviewGraphics.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PreviewGraphics_MouseUp);
            this.PreviewGraphics.Resize += new System.EventHandler(this.PreviewGraphics_Resize);
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OptionsPanel.Controls.Add(this.OptionsSnapToPoint);
            this.OptionsPanel.Controls.Add(this.OptionsColorSelecterOpener);
            this.OptionsPanel.Controls.Add(this.OptionsDrawLineMode);
            this.OptionsPanel.Controls.Add(this.OptionsSelectModeButton);
            this.OptionsPanel.Controls.Add(this.OptionsEmergencyLabel);
            this.OptionsPanel.Controls.Add(this.OptionsToggleProject);
            this.OptionsPanel.Controls.Add(this.OptionsSavePathFile);
            this.OptionsPanel.Controls.Add(this.OptionsLoadPathFile);
            this.OptionsPanel.Controls.Add(this.OptionsKPPSTextBox);
            this.OptionsPanel.Controls.Add(this.OptionsFPSTextBox);
            this.OptionsPanel.Controls.Add(this.OptionsKPPSLabel);
            this.OptionsPanel.Controls.Add(this.OptionsTitleLabel);
            this.OptionsPanel.Controls.Add(this.OptionsFPSLabel);
            this.OptionsPanel.Location = new System.Drawing.Point(530, 438);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(394, 206);
            this.OptionsPanel.TabIndex = 1;
            // 
            // OptionsSnapToPoint
            // 
            this.OptionsSnapToPoint.Appearance = System.Windows.Forms.Appearance.Button;
            this.OptionsSnapToPoint.AutoSize = true;
            this.OptionsSnapToPoint.Location = new System.Drawing.Point(217, 175);
            this.OptionsSnapToPoint.Name = "OptionsSnapToPoint";
            this.OptionsSnapToPoint.Size = new System.Drawing.Size(89, 25);
            this.OptionsSnapToPoint.TabIndex = 19;
            this.OptionsSnapToPoint.Text = "Snap To Point";
            this.OptionsSnapToPoint.UseVisualStyleBackColor = true;
            this.OptionsSnapToPoint.CheckedChanged += new System.EventHandler(this.OptionsSnapToPoint_CheckedChanged);
            // 
            // OptionsColorSelecterOpener
            // 
            this.OptionsColorSelecterOpener.Location = new System.Drawing.Point(312, 176);
            this.OptionsColorSelecterOpener.Name = "OptionsColorSelecterOpener";
            this.OptionsColorSelecterOpener.Size = new System.Drawing.Size(75, 23);
            this.OptionsColorSelecterOpener.TabIndex = 18;
            this.OptionsColorSelecterOpener.Text = "Color";
            this.OptionsColorSelecterOpener.UseVisualStyleBackColor = true;
            this.OptionsColorSelecterOpener.Click += new System.EventHandler(this.OptionsColorSelecterOpener_Click);
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
            this.OptionsDrawLineMode.CheckedChanged += new System.EventHandler(this.OptionsDrawLineMode_CheckedChanged);
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
            this.OptionsSelectModeButton.CheckedChanged += new System.EventHandler(this.OptionsSelectModeButton_CheckedChanged);
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
            // OptionsSavePathFile
            // 
            this.OptionsSavePathFile.Location = new System.Drawing.Point(125, 99);
            this.OptionsSavePathFile.Name = "OptionsSavePathFile";
            this.OptionsSavePathFile.Size = new System.Drawing.Size(116, 23);
            this.OptionsSavePathFile.TabIndex = 13;
            this.OptionsSavePathFile.Text = "Save Path File";
            this.OptionsSavePathFile.UseVisualStyleBackColor = true;
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
            // OptionsKPPSTextBox
            // 
            this.OptionsKPPSTextBox.Location = new System.Drawing.Point(149, 71);
            this.OptionsKPPSTextBox.Name = "OptionsKPPSTextBox";
            this.OptionsKPPSTextBox.Size = new System.Drawing.Size(145, 23);
            this.OptionsKPPSTextBox.TabIndex = 11;
            this.OptionsKPPSTextBox.Text = "40000";
            // 
            // OptionsFPSTextBox
            // 
            this.OptionsFPSTextBox.Location = new System.Drawing.Point(149, 42);
            this.OptionsFPSTextBox.Name = "OptionsFPSTextBox";
            this.OptionsFPSTextBox.Size = new System.Drawing.Size(145, 23);
            this.OptionsFPSTextBox.TabIndex = 9;
            this.OptionsFPSTextBox.Text = "30";
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
            // OptionsTitleLabel
            // 
            this.OptionsTitleLabel.AccessibleName = "Time Manager";
            this.OptionsTitleLabel.AutoSize = true;
            this.OptionsTitleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OptionsTitleLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.OptionsTitleLabel.Location = new System.Drawing.Point(-2, 0);
            this.OptionsTitleLabel.Name = "OptionsTitleLabel";
            this.OptionsTitleLabel.Size = new System.Drawing.Size(86, 30);
            this.OptionsTitleLabel.TabIndex = 9;
            this.OptionsTitleLabel.Text = "Options";
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
            this.TimeLinePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
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
            this.TimeLineFramesInput.Location = new System.Drawing.Point(97, 77);
            this.TimeLineFramesInput.Name = "TimeLineFramesInput";
            this.TimeLineFramesInput.Size = new System.Drawing.Size(145, 23);
            this.TimeLineFramesInput.TabIndex = 8;
            this.TimeLineFramesInput.Text = "0";
            this.TimeLineFramesInput.TextChanged += new System.EventHandler(this.TimeLineFramesInput_TextChanged);
            // 
            // TimeLineSecondsInput
            // 
            this.TimeLineSecondsInput.Location = new System.Drawing.Point(97, 48);
            this.TimeLineSecondsInput.Name = "TimeLineSecondsInput";
            this.TimeLineSecondsInput.Size = new System.Drawing.Size(145, 23);
            this.TimeLineSecondsInput.TabIndex = 7;
            this.TimeLineSecondsInput.Text = "0";
            this.TimeLineSecondsInput.TextChanged += new System.EventHandler(this.TimeLineSecondsInput_TextChanged);
            // 
            // TimeLineFramesLabel
            // 
            this.TimeLineFramesLabel.AutoSize = true;
            this.TimeLineFramesLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeLineFramesLabel.Location = new System.Drawing.Point(9, 82);
            this.TimeLineFramesLabel.Name = "TimeLineFramesLabel";
            this.TimeLineFramesLabel.Size = new System.Drawing.Size(82, 15);
            this.TimeLineFramesLabel.TabIndex = 6;
            this.TimeLineFramesLabel.Text = "Time (Frames)";
            // 
            // TimeLineSecondsLabel
            // 
            this.TimeLineSecondsLabel.AutoSize = true;
            this.TimeLineSecondsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeLineSecondsLabel.Location = new System.Drawing.Point(3, 51);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // LinePropertiesPanel
            // 
            this.LinePropertiesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinePropertiesPanel.AutoScroll = true;
            this.LinePropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LinePropertiesPanel.Controls.Add(this.PathLinePointsListBox);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesChangeColor);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesPathIndexData);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesPathIndexLabel);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesKeyFramesTextBox);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesTitle);
            this.LinePropertiesPanel.Location = new System.Drawing.Point(530, 12);
            this.LinePropertiesPanel.Name = "LinePropertiesPanel";
            this.LinePropertiesPanel.Size = new System.Drawing.Size(394, 296);
            this.LinePropertiesPanel.TabIndex = 3;
            // 
            // PathLinePointsListBox
            // 
            this.PathLinePointsListBox.FormattingEnabled = true;
            this.PathLinePointsListBox.ItemHeight = 15;
            this.PathLinePointsListBox.Location = new System.Drawing.Point(3, 177);
            this.PathLinePointsListBox.Name = "PathLinePointsListBox";
            this.PathLinePointsListBox.Size = new System.Drawing.Size(384, 49);
            this.PathLinePointsListBox.TabIndex = 31;
            // 
            // LinePropertiesChangeColor
            // 
            this.LinePropertiesChangeColor.Location = new System.Drawing.Point(3, 148);
            this.LinePropertiesChangeColor.Name = "LinePropertiesChangeColor";
            this.LinePropertiesChangeColor.Size = new System.Drawing.Size(384, 23);
            this.LinePropertiesChangeColor.TabIndex = 30;
            this.LinePropertiesChangeColor.Text = "Color";
            this.LinePropertiesChangeColor.UseVisualStyleBackColor = true;
            // 
            // LinePropertiesPathIndexData
            // 
            this.LinePropertiesPathIndexData.AutoSize = true;
            this.LinePropertiesPathIndexData.ForeColor = System.Drawing.SystemColors.Control;
            this.LinePropertiesPathIndexData.Location = new System.Drawing.Point(74, 30);
            this.LinePropertiesPathIndexData.Name = "LinePropertiesPathIndexData";
            this.LinePropertiesPathIndexData.Size = new System.Drawing.Size(114, 15);
            this.LinePropertiesPathIndexData.TabIndex = 28;
            this.LinePropertiesPathIndexData.Text = "No Line Selected Yet";
            // 
            // LinePropertiesPathIndexLabel
            // 
            this.LinePropertiesPathIndexLabel.AutoSize = true;
            this.LinePropertiesPathIndexLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.LinePropertiesPathIndexLabel.Location = new System.Drawing.Point(7, 30);
            this.LinePropertiesPathIndexLabel.Name = "LinePropertiesPathIndexLabel";
            this.LinePropertiesPathIndexLabel.Size = new System.Drawing.Size(69, 15);
            this.LinePropertiesPathIndexLabel.TabIndex = 27;
            this.LinePropertiesPathIndexLabel.Text = "Path Index: ";
            // 
            // LinePropertiesKeyFramesTextBox
            // 
            this.LinePropertiesKeyFramesTextBox.FormattingEnabled = true;
            this.LinePropertiesKeyFramesTextBox.ItemHeight = 15;
            this.LinePropertiesKeyFramesTextBox.Location = new System.Drawing.Point(3, 48);
            this.LinePropertiesKeyFramesTextBox.Name = "LinePropertiesKeyFramesTextBox";
            this.LinePropertiesKeyFramesTextBox.Size = new System.Drawing.Size(384, 94);
            this.LinePropertiesKeyFramesTextBox.TabIndex = 19;
            this.LinePropertiesKeyFramesTextBox.SelectedIndexChanged += new System.EventHandler(this.LinePropertiesKeyFramesTextBox_SelectedIndexChanged);
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
            // InformationPanel
            // 
            this.InformationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InformationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InformationPanel.Controls.Add(this.InformationPreviewModeData);
            this.InformationPanel.Controls.Add(this.InformationPreviewModeLabel);
            this.InformationPanel.Controls.Add(this.InformationClosestPointData);
            this.InformationPanel.Controls.Add(this.InformationClosestPointLabel);
            this.InformationPanel.Controls.Add(this.InformationFrameListCountInfo);
            this.InformationPanel.Controls.Add(this.InformationDynamicListCountInfo);
            this.InformationPanel.Controls.Add(this.InformationFrameListCountLabel);
            this.InformationPanel.Controls.Add(this.InformationDynamicListCountLabel);
            this.InformationPanel.Controls.Add(this.InformationPoint2Info);
            this.InformationPanel.Controls.Add(this.InformationPoint1Info);
            this.InformationPanel.Controls.Add(this.InformationPoint2TitleLabel);
            this.InformationPanel.Controls.Add(this.InformationPoint1TitleLablel);
            this.InformationPanel.Controls.Add(this.InformationTitleLabel);
            this.InformationPanel.Location = new System.Drawing.Point(530, 314);
            this.InformationPanel.Name = "InformationPanel";
            this.InformationPanel.Size = new System.Drawing.Size(394, 118);
            this.InformationPanel.TabIndex = 19;
            // 
            // InformationPreviewModeData
            // 
            this.InformationPreviewModeData.AutoSize = true;
            this.InformationPreviewModeData.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationPreviewModeData.Location = new System.Drawing.Point(288, 56);
            this.InformationPreviewModeData.Name = "InformationPreviewModeData";
            this.InformationPreviewModeData.Size = new System.Drawing.Size(27, 15);
            this.InformationPreviewModeData.TabIndex = 30;
            this.InformationPreviewModeData.Text = "null";
            // 
            // InformationPreviewModeLabel
            // 
            this.InformationPreviewModeLabel.AutoSize = true;
            this.InformationPreviewModeLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationPreviewModeLabel.Location = new System.Drawing.Point(197, 56);
            this.InformationPreviewModeLabel.Name = "InformationPreviewModeLabel";
            this.InformationPreviewModeLabel.Size = new System.Drawing.Size(85, 15);
            this.InformationPreviewModeLabel.TabIndex = 29;
            this.InformationPreviewModeLabel.Text = "Preview mode:";
            // 
            // InformationClosestPointData
            // 
            this.InformationClosestPointData.AutoSize = true;
            this.InformationClosestPointData.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationClosestPointData.Location = new System.Drawing.Point(279, 41);
            this.InformationClosestPointData.Name = "InformationClosestPointData";
            this.InformationClosestPointData.Size = new System.Drawing.Size(27, 15);
            this.InformationClosestPointData.TabIndex = 28;
            this.InformationClosestPointData.Text = "null";
            // 
            // InformationClosestPointLabel
            // 
            this.InformationClosestPointLabel.AutoSize = true;
            this.InformationClosestPointLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationClosestPointLabel.Location = new System.Drawing.Point(197, 41);
            this.InformationClosestPointLabel.Name = "InformationClosestPointLabel";
            this.InformationClosestPointLabel.Size = new System.Drawing.Size(76, 15);
            this.InformationClosestPointLabel.TabIndex = 27;
            this.InformationClosestPointLabel.Text = "ClosestPoint:";
            // 
            // InformationFrameListCountInfo
            // 
            this.InformationFrameListCountInfo.AutoSize = true;
            this.InformationFrameListCountInfo.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationFrameListCountInfo.Location = new System.Drawing.Point(134, 86);
            this.InformationFrameListCountInfo.Name = "InformationFrameListCountInfo";
            this.InformationFrameListCountInfo.Size = new System.Drawing.Size(13, 15);
            this.InformationFrameListCountInfo.TabIndex = 26;
            this.InformationFrameListCountInfo.Text = "0";
            // 
            // InformationDynamicListCountInfo
            // 
            this.InformationDynamicListCountInfo.AutoSize = true;
            this.InformationDynamicListCountInfo.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationDynamicListCountInfo.Location = new System.Drawing.Point(134, 71);
            this.InformationDynamicListCountInfo.Name = "InformationDynamicListCountInfo";
            this.InformationDynamicListCountInfo.Size = new System.Drawing.Size(13, 15);
            this.InformationDynamicListCountInfo.TabIndex = 25;
            this.InformationDynamicListCountInfo.Text = "0";
            // 
            // InformationFrameListCountLabel
            // 
            this.InformationFrameListCountLabel.AutoSize = true;
            this.InformationFrameListCountLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationFrameListCountLabel.Location = new System.Drawing.Point(14, 86);
            this.InformationFrameListCountLabel.Name = "InformationFrameListCountLabel";
            this.InformationFrameListCountLabel.Size = new System.Drawing.Size(100, 15);
            this.InformationFrameListCountLabel.TabIndex = 24;
            this.InformationFrameListCountLabel.Text = "Frame List Count:";
            // 
            // InformationDynamicListCountLabel
            // 
            this.InformationDynamicListCountLabel.AutoSize = true;
            this.InformationDynamicListCountLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationDynamicListCountLabel.Location = new System.Drawing.Point(14, 71);
            this.InformationDynamicListCountLabel.Name = "InformationDynamicListCountLabel";
            this.InformationDynamicListCountLabel.Size = new System.Drawing.Size(114, 15);
            this.InformationDynamicListCountLabel.TabIndex = 23;
            this.InformationDynamicListCountLabel.Text = "Dynamic List Count:";
            // 
            // InformationPoint2Info
            // 
            this.InformationPoint2Info.AutoSize = true;
            this.InformationPoint2Info.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationPoint2Info.Location = new System.Drawing.Point(57, 56);
            this.InformationPoint2Info.Name = "InformationPoint2Info";
            this.InformationPoint2Info.Size = new System.Drawing.Size(27, 15);
            this.InformationPoint2Info.TabIndex = 22;
            this.InformationPoint2Info.Text = "null";
            // 
            // InformationPoint1Info
            // 
            this.InformationPoint1Info.AutoSize = true;
            this.InformationPoint1Info.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationPoint1Info.Location = new System.Drawing.Point(57, 41);
            this.InformationPoint1Info.Name = "InformationPoint1Info";
            this.InformationPoint1Info.Size = new System.Drawing.Size(27, 15);
            this.InformationPoint1Info.TabIndex = 21;
            this.InformationPoint1Info.Text = "null";
            // 
            // InformationPoint2TitleLabel
            // 
            this.InformationPoint2TitleLabel.AutoSize = true;
            this.InformationPoint2TitleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationPoint2TitleLabel.Location = new System.Drawing.Point(14, 56);
            this.InformationPoint2TitleLabel.Name = "InformationPoint2TitleLabel";
            this.InformationPoint2TitleLabel.Size = new System.Drawing.Size(47, 15);
            this.InformationPoint2TitleLabel.TabIndex = 20;
            this.InformationPoint2TitleLabel.Text = "Point2: ";
            // 
            // InformationPoint1TitleLablel
            // 
            this.InformationPoint1TitleLablel.AutoSize = true;
            this.InformationPoint1TitleLablel.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationPoint1TitleLablel.Location = new System.Drawing.Point(14, 41);
            this.InformationPoint1TitleLablel.Name = "InformationPoint1TitleLablel";
            this.InformationPoint1TitleLablel.Size = new System.Drawing.Size(47, 15);
            this.InformationPoint1TitleLablel.TabIndex = 19;
            this.InformationPoint1TitleLablel.Text = "Point1: ";
            // 
            // InformationTitleLabel
            // 
            this.InformationTitleLabel.AccessibleName = "Time Manager";
            this.InformationTitleLabel.AutoSize = true;
            this.InformationTitleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.InformationTitleLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.InformationTitleLabel.Location = new System.Drawing.Point(3, 0);
            this.InformationTitleLabel.Name = "InformationTitleLabel";
            this.InformationTitleLabel.Size = new System.Drawing.Size(122, 30);
            this.InformationTitleLabel.TabIndex = 18;
            this.InformationTitleLabel.Text = "Information";
            // 
            // Form1
            // 
            this.AccessibleName = "Path Window";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(936, 656);
            this.Controls.Add(this.PreviewGraphics);
            this.Controls.Add(this.InformationPanel);
            this.Controls.Add(this.LinePropertiesPanel);
            this.Controls.Add(this.TimeLinePanel);
            this.Controls.Add(this.OptionsPanel);
            this.Name = "Form1";
            this.Text = "Path";
            ((System.ComponentModel.ISupportInitialize)(this.PreviewGraphics)).EndInit();
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.TimeLinePanel.ResumeLayout(false);
            this.TimeLinePanel.PerformLayout();
            this.LinePropertiesPanel.ResumeLayout(false);
            this.LinePropertiesPanel.PerformLayout();
            this.InformationPanel.ResumeLayout(false);
            this.InformationPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox PreviewGraphics;
        private Panel OptionsPanel;
        private Panel TimeLinePanel;
        private Button TimeLineNextFrame;
        private Button TimeLineBackFrame;
        private Label TimeLineTitleLabel;
        private CheckBox TimeLinePlay;
        private TextBox OptionsKPPSTextBox;
        private TextBox OptionsFPSTextBox;
        private Label OptionsKPPSLabel;
        private Label OptionsTitleLabel;
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
        private Panel InformationPanel;
        private Label InformationTitleLabel;
        private Label InformationPoint2Info;
        private Label InformationPoint1Info;
        private Label InformationPoint2TitleLabel;
        private Label InformationPoint1TitleLablel;
        private Button OptionsColorSelecterOpener;
        private ColorDialog DrawerColorDialog;
        private Label InformationFrameListCountInfo;
        private Label InformationDynamicListCountInfo;
        private Label InformationFrameListCountLabel;
        private Label InformationDynamicListCountLabel;
        private ListBox LinePropertiesKeyFramesTextBox;
        private Label LinePropertiesPathIndexData;
        private Label LinePropertiesPathIndexLabel;
        private Button LinePropertiesChangeColor;
        private ColorDialog LinePropertiesColorDialog;
        private CheckBox OptionsSnapToPoint;
        private Label InformationClosestPointLabel;
        private Label InformationClosestPointData;
        private Label InformationPreviewModeData;
        private Label InformationPreviewModeLabel;
        private ListBox PathLinePointsListBox;
    }
}