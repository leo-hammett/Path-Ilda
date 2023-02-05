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
            this.TimeLineFramesInput = new System.Windows.Forms.TextBox();
            this.OptionsSnapToPoint = new System.Windows.Forms.CheckBox();
            this.TimeLineFramesLabel = new System.Windows.Forms.Label();
            this.TimeLineSecondsInput = new System.Windows.Forms.TextBox();
            this.OptionsColorSelecterOpener = new System.Windows.Forms.Button();
            this.OptionsDrawLineMode = new System.Windows.Forms.CheckBox();
            this.TimeLineSecondsLabel = new System.Windows.Forms.Label();
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
            this.projectMaxTimeSelector = new System.Windows.Forms.NumericUpDown();
            this.timelineGUIHugger = new System.Windows.Forms.Panel();
            this.timelineGUI = new System.Windows.Forms.PictureBox();
            this.TimeLinePlay = new System.Windows.Forms.CheckBox();
            this.TimeLineNextFrame = new System.Windows.Forms.Button();
            this.TimeLineBackFrame = new System.Windows.Forms.Button();
            this.TimeLineTitleLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.LinePropertiesPanel = new System.Windows.Forms.Panel();
            this.linePropertiesStrobeSettings = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.LinePropertiesYCoordinate = new System.Windows.Forms.TextBox();
            this.PathLinePointsListBox = new System.Windows.Forms.ListBox();
            this.LinePropertiesXCoordinate = new System.Windows.Forms.TextBox();
            this.LinePropertiesChangeColor = new System.Windows.Forms.Button();
            this.LinePropertiesYCoordinateLabel = new System.Windows.Forms.Label();
            this.LinePropertiesPathIndexData = new System.Windows.Forms.Label();
            this.LinePropertiesXCoordinateLabel = new System.Windows.Forms.Label();
            this.LinePropertiesPathIndexLabel = new System.Windows.Forms.Label();
            this.LinePropertiesKeyFramesTextBox = new System.Windows.Forms.ListBox();
            this.LinePropertiesTitle = new System.Windows.Forms.Label();
            this.InformationPanel = new System.Windows.Forms.Panel();
            this.InformationPreviewModeData = new System.Windows.Forms.Label();
            this.InformationConnectedDACCount = new System.Windows.Forms.Label();
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPathProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dACSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToDACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectDACToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewGraphics)).BeginInit();
            this.OptionsPanel.SuspendLayout();
            this.TimeLinePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectMaxTimeSelector)).BeginInit();
            this.timelineGUIHugger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timelineGUI)).BeginInit();
            this.LinePropertiesPanel.SuspendLayout();
            this.linePropertiesStrobeSettings.SuspendLayout();
            this.InformationPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PreviewGraphics
            // 
            this.PreviewGraphics.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PreviewGraphics.Location = new System.Drawing.Point(5, 3);
            this.PreviewGraphics.Name = "PreviewGraphics";
            this.PreviewGraphics.Size = new System.Drawing.Size(546, 558);
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
            this.OptionsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OptionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OptionsPanel.Controls.Add(this.TimeLineFramesInput);
            this.OptionsPanel.Controls.Add(this.OptionsSnapToPoint);
            this.OptionsPanel.Controls.Add(this.TimeLineFramesLabel);
            this.OptionsPanel.Controls.Add(this.TimeLineSecondsInput);
            this.OptionsPanel.Controls.Add(this.OptionsColorSelecterOpener);
            this.OptionsPanel.Controls.Add(this.OptionsDrawLineMode);
            this.OptionsPanel.Controls.Add(this.TimeLineSecondsLabel);
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
            this.OptionsPanel.Location = new System.Drawing.Point(3, 538);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(385, 221);
            this.OptionsPanel.TabIndex = 1;
            // 
            // TimeLineFramesInput
            // 
            this.TimeLineFramesInput.Location = new System.Drawing.Point(341, 135);
            this.TimeLineFramesInput.Name = "TimeLineFramesInput";
            this.TimeLineFramesInput.Size = new System.Drawing.Size(51, 23);
            this.TimeLineFramesInput.TabIndex = 8;
            this.TimeLineFramesInput.Text = "0";
            this.TimeLineFramesInput.TextChanged += new System.EventHandler(this.TimeLineFramesInput_TextChanged);
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
            // TimeLineFramesLabel
            // 
            this.TimeLineFramesLabel.AutoSize = true;
            this.TimeLineFramesLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeLineFramesLabel.Location = new System.Drawing.Point(253, 138);
            this.TimeLineFramesLabel.Name = "TimeLineFramesLabel";
            this.TimeLineFramesLabel.Size = new System.Drawing.Size(82, 15);
            this.TimeLineFramesLabel.TabIndex = 6;
            this.TimeLineFramesLabel.Text = "Time (Frames)";
            // 
            // TimeLineSecondsInput
            // 
            this.TimeLineSecondsInput.Location = new System.Drawing.Point(341, 104);
            this.TimeLineSecondsInput.Name = "TimeLineSecondsInput";
            this.TimeLineSecondsInput.Size = new System.Drawing.Size(51, 23);
            this.TimeLineSecondsInput.TabIndex = 7;
            this.TimeLineSecondsInput.Text = "0";
            this.TimeLineSecondsInput.TextChanged += new System.EventHandler(this.TimeLineSecondsInput_TextChanged);
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
            // TimeLineSecondsLabel
            // 
            this.TimeLineSecondsLabel.AutoSize = true;
            this.TimeLineSecondsLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.TimeLineSecondsLabel.Location = new System.Drawing.Point(247, 107);
            this.TimeLineSecondsLabel.Name = "TimeLineSecondsLabel";
            this.TimeLineSecondsLabel.Size = new System.Drawing.Size(88, 15);
            this.TimeLineSecondsLabel.TabIndex = 5;
            this.TimeLineSecondsLabel.Text = "Time (Seconds)";
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
            this.TimeLinePanel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TimeLinePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TimeLinePanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TimeLinePanel.Controls.Add(this.projectMaxTimeSelector);
            this.TimeLinePanel.Controls.Add(this.timelineGUIHugger);
            this.TimeLinePanel.Controls.Add(this.TimeLinePlay);
            this.TimeLinePanel.Controls.Add(this.TimeLineNextFrame);
            this.TimeLinePanel.Controls.Add(this.TimeLineBackFrame);
            this.TimeLinePanel.Controls.Add(this.TimeLineTitleLabel);
            this.TimeLinePanel.Location = new System.Drawing.Point(5, 567);
            this.TimeLinePanel.Name = "TimeLinePanel";
            this.TimeLinePanel.Size = new System.Drawing.Size(546, 192);
            this.TimeLinePanel.TabIndex = 2;
            // 
            // projectMaxTimeSelector
            // 
            this.projectMaxTimeSelector.Location = new System.Drawing.Point(486, 4);
            this.projectMaxTimeSelector.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.projectMaxTimeSelector.Name = "projectMaxTimeSelector";
            this.projectMaxTimeSelector.Size = new System.Drawing.Size(53, 23);
            this.projectMaxTimeSelector.TabIndex = 6;
            this.projectMaxTimeSelector.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // timelineGUIHugger
            // 
            this.timelineGUIHugger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timelineGUIHugger.AutoScroll = true;
            this.timelineGUIHugger.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.timelineGUIHugger.Controls.Add(this.timelineGUI);
            this.timelineGUIHugger.Location = new System.Drawing.Point(5, 36);
            this.timelineGUIHugger.Name = "timelineGUIHugger";
            this.timelineGUIHugger.Size = new System.Drawing.Size(534, 149);
            this.timelineGUIHugger.TabIndex = 5;
            // 
            // timelineGUI
            // 
            this.timelineGUI.Location = new System.Drawing.Point(0, 0);
            this.timelineGUI.Name = "timelineGUI";
            this.timelineGUI.Size = new System.Drawing.Size(531, 149);
            this.timelineGUI.TabIndex = 5;
            this.timelineGUI.TabStop = false;
            this.timelineGUI.Paint += new System.Windows.Forms.PaintEventHandler(this.timeline_GUI_updater);
            this.timelineGUI.MouseDown += new System.Windows.Forms.MouseEventHandler(this.timelineGUI_MouseDown);
            this.timelineGUI.MouseMove += new System.Windows.Forms.MouseEventHandler(this.timelineGUI_MouseMove);
            // 
            // TimeLinePlay
            // 
            this.TimeLinePlay.Appearance = System.Windows.Forms.Appearance.Button;
            this.TimeLinePlay.AutoSize = true;
            this.TimeLinePlay.Location = new System.Drawing.Point(239, 3);
            this.TimeLinePlay.Name = "TimeLinePlay";
            this.TimeLinePlay.Size = new System.Drawing.Size(39, 25);
            this.TimeLinePlay.TabIndex = 4;
            this.TimeLinePlay.Text = "Play";
            this.TimeLinePlay.UseVisualStyleBackColor = true;
            // 
            // TimeLineNextFrame
            // 
            this.TimeLineNextFrame.Location = new System.Drawing.Point(284, 4);
            this.TimeLineNextFrame.Name = "TimeLineNextFrame";
            this.TimeLineNextFrame.Size = new System.Drawing.Size(77, 24);
            this.TimeLineNextFrame.TabIndex = 3;
            this.TimeLineNextFrame.Text = "Next";
            this.TimeLineNextFrame.UseVisualStyleBackColor = true;
            // 
            // TimeLineBackFrame
            // 
            this.TimeLineBackFrame.Location = new System.Drawing.Point(156, 3);
            this.TimeLineBackFrame.Name = "TimeLineBackFrame";
            this.TimeLineBackFrame.Size = new System.Drawing.Size(77, 25);
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
            this.openFileDialog1.DefaultExt = "dyPth";
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "dyPth";
            // 
            // LinePropertiesPanel
            // 
            this.LinePropertiesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinePropertiesPanel.AutoScroll = true;
            this.LinePropertiesPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LinePropertiesPanel.Controls.Add(this.linePropertiesStrobeSettings);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesYCoordinate);
            this.LinePropertiesPanel.Controls.Add(this.PathLinePointsListBox);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesXCoordinate);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesChangeColor);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesYCoordinateLabel);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesPathIndexData);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesXCoordinateLabel);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesPathIndexLabel);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesKeyFramesTextBox);
            this.LinePropertiesPanel.Controls.Add(this.LinePropertiesTitle);
            this.LinePropertiesPanel.Location = new System.Drawing.Point(3, 3);
            this.LinePropertiesPanel.Name = "LinePropertiesPanel";
            this.LinePropertiesPanel.Size = new System.Drawing.Size(385, 405);
            this.LinePropertiesPanel.TabIndex = 3;
            // 
            // linePropertiesStrobeSettings
            // 
            this.linePropertiesStrobeSettings.Controls.Add(this.tabPage1);
            this.linePropertiesStrobeSettings.Controls.Add(this.tabPage2);
            this.linePropertiesStrobeSettings.Location = new System.Drawing.Point(3, 246);
            this.linePropertiesStrobeSettings.Name = "linePropertiesStrobeSettings";
            this.linePropertiesStrobeSettings.SelectedIndex = 0;
            this.linePropertiesStrobeSettings.Size = new System.Drawing.Size(373, 112);
            this.linePropertiesStrobeSettings.TabIndex = 32;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(365, 84);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(365, 84);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // LinePropertiesYCoordinate
            // 
            this.LinePropertiesYCoordinate.Location = new System.Drawing.Point(120, 149);
            this.LinePropertiesYCoordinate.Name = "LinePropertiesYCoordinate";
            this.LinePropertiesYCoordinate.Size = new System.Drawing.Size(68, 23);
            this.LinePropertiesYCoordinate.TabIndex = 23;
            this.LinePropertiesYCoordinate.TextChanged += new System.EventHandler(this.LinePropertiesXCoordinate_Leave);
            // 
            // PathLinePointsListBox
            // 
            this.PathLinePointsListBox.FormattingEnabled = true;
            this.PathLinePointsListBox.ItemHeight = 15;
            this.PathLinePointsListBox.Location = new System.Drawing.Point(217, 48);
            this.PathLinePointsListBox.Name = "PathLinePointsListBox";
            this.PathLinePointsListBox.Size = new System.Drawing.Size(159, 94);
            this.PathLinePointsListBox.TabIndex = 31;
            this.PathLinePointsListBox.SelectedIndexChanged += new System.EventHandler(this.PathLinePointsListBox_SelectedIndexChanged);
            // 
            // LinePropertiesXCoordinate
            // 
            this.LinePropertiesXCoordinate.Location = new System.Drawing.Point(30, 149);
            this.LinePropertiesXCoordinate.Name = "LinePropertiesXCoordinate";
            this.LinePropertiesXCoordinate.Size = new System.Drawing.Size(54, 23);
            this.LinePropertiesXCoordinate.TabIndex = 20;
            this.LinePropertiesXCoordinate.TextChanged += new System.EventHandler(this.LinePropertiesXCoordinate_Leave);
            // 
            // LinePropertiesChangeColor
            // 
            this.LinePropertiesChangeColor.Location = new System.Drawing.Point(197, 148);
            this.LinePropertiesChangeColor.Name = "LinePropertiesChangeColor";
            this.LinePropertiesChangeColor.Size = new System.Drawing.Size(179, 23);
            this.LinePropertiesChangeColor.TabIndex = 30;
            this.LinePropertiesChangeColor.Text = "Color";
            this.LinePropertiesChangeColor.UseVisualStyleBackColor = true;
            // 
            // LinePropertiesYCoordinateLabel
            // 
            this.LinePropertiesYCoordinateLabel.AutoSize = true;
            this.LinePropertiesYCoordinateLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.LinePropertiesYCoordinateLabel.Location = new System.Drawing.Point(97, 152);
            this.LinePropertiesYCoordinateLabel.Name = "LinePropertiesYCoordinateLabel";
            this.LinePropertiesYCoordinateLabel.Size = new System.Drawing.Size(17, 15);
            this.LinePropertiesYCoordinateLabel.TabIndex = 22;
            this.LinePropertiesYCoordinateLabel.Text = "Y:";
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
            // LinePropertiesXCoordinateLabel
            // 
            this.LinePropertiesXCoordinateLabel.AutoSize = true;
            this.LinePropertiesXCoordinateLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.LinePropertiesXCoordinateLabel.Location = new System.Drawing.Point(7, 152);
            this.LinePropertiesXCoordinateLabel.Name = "LinePropertiesXCoordinateLabel";
            this.LinePropertiesXCoordinateLabel.Size = new System.Drawing.Size(17, 15);
            this.LinePropertiesXCoordinateLabel.TabIndex = 21;
            this.LinePropertiesXCoordinateLabel.Text = "X:";
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
            this.LinePropertiesKeyFramesTextBox.Size = new System.Drawing.Size(213, 94);
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
            this.InformationPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InformationPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.InformationPanel.Controls.Add(this.InformationPreviewModeData);
            this.InformationPanel.Controls.Add(this.InformationConnectedDACCount);
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
            this.InformationPanel.Location = new System.Drawing.Point(3, 414);
            this.InformationPanel.Name = "InformationPanel";
            this.InformationPanel.Size = new System.Drawing.Size(385, 118);
            this.InformationPanel.TabIndex = 19;
            // 
            // InformationPreviewModeData
            // 
            this.InformationPreviewModeData.AutoSize = true;
            this.InformationPreviewModeData.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationPreviewModeData.Location = new System.Drawing.Point(303, 56);
            this.InformationPreviewModeData.Name = "InformationPreviewModeData";
            this.InformationPreviewModeData.Size = new System.Drawing.Size(27, 15);
            this.InformationPreviewModeData.TabIndex = 30;
            this.InformationPreviewModeData.Text = "null";
            // 
            // InformationConnectedDACCount
            // 
            this.InformationConnectedDACCount.AutoSize = true;
            this.InformationConnectedDACCount.ForeColor = System.Drawing.SystemColors.Control;
            this.InformationConnectedDACCount.Location = new System.Drawing.Point(197, 56);
            this.InformationConnectedDACCount.Name = "InformationConnectedDACCount";
            this.InformationConnectedDACCount.Size = new System.Drawing.Size(100, 15);
            this.InformationConnectedDACCount.TabIndex = 29;
            this.InformationConnectedDACCount.Text = "Connected DACs:";
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
            this.InformationTitleLabel.Location = new System.Drawing.Point(3, 2);
            this.InformationTitleLabel.Name = "InformationTitleLabel";
            this.InformationTitleLabel.Size = new System.Drawing.Size(122, 30);
            this.InformationTitleLabel.TabIndex = 18;
            this.InformationTitleLabel.Text = "Information";
            // 
            // DrawerColorDialog
            // 
            this.DrawerColorDialog.Color = System.Drawing.Color.White;
            // 
            // LinePropertiesColorDialog
            // 
            this.LinePropertiesColorDialog.Color = System.Drawing.Color.White;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(973, 24);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPathProjectToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newPathProjectToolStripMenuItem
            // 
            this.newPathProjectToolStripMenuItem.Name = "newPathProjectToolStripMenuItem";
            this.newPathProjectToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.newPathProjectToolStripMenuItem.Text = "New Path Project";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.openToolStripMenuItem.Text = "Open Path File";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveToolStripMenuItem.Text = "Save Path File As";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sToolStripMenuItem,
            this.dACSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.sToolStripMenuItem.Text = "Laser Settings";
            // 
            // dACSettingsToolStripMenuItem
            // 
            this.dACSettingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToDACToolStripMenuItem,
            this.disconnectDACToolStripMenuItem});
            this.dACSettingsToolStripMenuItem.Name = "dACSettingsToolStripMenuItem";
            this.dACSettingsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.dACSettingsToolStripMenuItem.Text = "DAC Settings";
            // 
            // connectToDACToolStripMenuItem
            // 
            this.connectToDACToolStripMenuItem.Name = "connectToDACToolStripMenuItem";
            this.connectToDACToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.connectToDACToolStripMenuItem.Text = "Connect To DAC";
            this.connectToDACToolStripMenuItem.Click += new System.EventHandler(this.connectToDACToolStripMenuItem_Click);
            // 
            // disconnectDACToolStripMenuItem
            // 
            this.disconnectDACToolStripMenuItem.Name = "disconnectDACToolStripMenuItem";
            this.disconnectDACToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.disconnectDACToolStripMenuItem.Text = "Disconnect DAC";
            this.disconnectDACToolStripMenuItem.Click += new System.EventHandler(this.disconnectDACToolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.PreviewGraphics);
            this.splitContainer1.Panel1.Controls.Add(this.TimeLinePanel);
            this.splitContainer1.Panel1.Resize += new System.EventHandler(this.PreviewGraphics_Resize);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LinePropertiesPanel);
            this.splitContainer1.Panel2.Controls.Add(this.InformationPanel);
            this.splitContainer1.Panel2.Controls.Add(this.OptionsPanel);
            this.splitContainer1.Size = new System.Drawing.Size(949, 762);
            this.splitContainer1.SplitterDistance = 554;
            this.splitContainer1.TabIndex = 21;
            this.splitContainer1.Resize += new System.EventHandler(this.PreviewGraphics_Resize);
            // 
            // Form1
            // 
            this.AccessibleName = "Path Window";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(973, 801);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Path";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.PreviewGraphics)).EndInit();
            this.OptionsPanel.ResumeLayout(false);
            this.OptionsPanel.PerformLayout();
            this.TimeLinePanel.ResumeLayout(false);
            this.TimeLinePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.projectMaxTimeSelector)).EndInit();
            this.timelineGUIHugger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timelineGUI)).EndInit();
            this.LinePropertiesPanel.ResumeLayout(false);
            this.LinePropertiesPanel.PerformLayout();
            this.linePropertiesStrobeSettings.ResumeLayout(false);
            this.InformationPanel.ResumeLayout(false);
            this.InformationPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Label InformationConnectedDACCount;
        private ListBox PathLinePointsListBox;
        private TabControl linePropertiesStrobeSettings;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox LinePropertiesYCoordinate;
        private TextBox LinePropertiesXCoordinate;
        private Label LinePropertiesYCoordinateLabel;
        private Label LinePropertiesXCoordinateLabel;
        private PictureBox timelineGUI;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem newPathProjectToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem sToolStripMenuItem;
        private SplitContainer splitContainer1;
        private ToolStripMenuItem dACSettingsToolStripMenuItem;
        private ToolStripMenuItem connectToDACToolStripMenuItem;
        private ToolStripMenuItem disconnectDACToolStripMenuItem;
        private Panel timelineGUIHugger;
        private NumericUpDown projectMaxTimeSelector;
    }
}