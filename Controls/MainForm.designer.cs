namespace AlarmClock
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            AlarmClock.AlarmColours alarmColours1 = new AlarmClock.AlarmColours();
            AlarmClock.AlarmColours alarmColours2 = new AlarmClock.AlarmColours();
            AlarmClock.AlarmColours alarmColours3 = new AlarmClock.AlarmColours();
            AlarmClock.AlarmColours alarmColours4 = new AlarmClock.AlarmColours();
            AlarmClock.AlarmColours alarmColours5 = new AlarmClock.AlarmColours();
            AlarmClock.AlarmColours alarmColours6 = new AlarmClock.AlarmColours();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.KillButton = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.armedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new Rug.UI.MenuSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alwaysOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alarmPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new Rug.UI.MenuSeparator();
            this.dialogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confirmDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clenseModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aggressiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coloursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadColoursFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveColoursToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skinEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.factorySetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_NextAlarmLabel = new System.Windows.Forms.Label();
            this.m_NextAlarmPanel = new System.Windows.Forms.Panel();
            this.m_ShowPanelButton = new Rug.UI.ImageCheckbox();
            this.m_CloseButton = new Rug.UI.ImageButton();
            this.m_ShrinkButton = new Rug.UI.ImageCheckbox();
            this.m_ToTaskButton = new Rug.UI.ImageButton();
            this.panel2 = new Rug.UI.PanelWithBorder();
            this.label2 = new System.Windows.Forms.Label();
            this.imageButton1 = new Rug.UI.ImageButton();
            this.alarmsPanel1 = new AlarmClock.Controls.AlarmsPanel();
            this.digitalClockCtrl1 = new AlarmClock.Display.ClockDisplay();
            this.windowHeader1 = new AlarmClock.Controls.WindowHeader();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.m_NextAlarmPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.OnClockTick);
            // 
            // KillButton
            // 
            this.KillButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KillButton.Location = new System.Drawing.Point(0, 251);
            this.KillButton.Margin = new System.Windows.Forms.Padding(0);
            this.KillButton.Name = "KillButton";
            this.KillButton.Size = new System.Drawing.Size(250, 53);
            this.KillButton.TabIndex = 0;
            this.KillButton.Text = "Kill !!!";
            this.KillButton.UseVisualStyleBackColor = false;
            this.KillButton.Visible = false;
            this.KillButton.Click += new System.EventHandler(this.KillButton_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.BalloonTipTitle = "Alarm Clock";
            this.NotifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TrayResizeForm_Resize);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(112, 48);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.Open_MenuClick);
            // 
            // exitMenuItem1
            // 
            this.exitMenuItem1.Name = "exitMenuItem1";
            this.exitMenuItem1.Size = new System.Drawing.Size(111, 22);
            this.exitMenuItem1.Text = "Exit";
            this.exitMenuItem1.Click += new System.EventHandler(this.Exit_MenuClick);
            // 
            // colorTimer
            // 
            this.colorTimer.Interval = 500;
            this.colorTimer.Tick += new System.EventHandler(this.ChangeColor);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(250, 17);
            this.menuStrip1.TabIndex = 20;
            this.menuStrip1.TabStop = true;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnDoubleClick);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.armedToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 17);
            this.fileToolStripMenuItem.Text = "Alarm";
            // 
            // armedToolStripMenuItem
            // 
            this.armedToolStripMenuItem.Name = "armedToolStripMenuItem";
            this.armedToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.armedToolStripMenuItem.Text = "Armed";
            this.armedToolStripMenuItem.Click += new System.EventHandler(this.ArmedToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.GroupName = "Menu";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Registered = false;
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit_MenuClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alwaysOnTopToolStripMenuItem,
            this.alarmPanelToolStripMenuItem,
            this.toolStripSeparator2,
            this.dialogsToolStripMenuItem,
            this.skinImagesToolStripMenuItem,
            this.coloursToolStripMenuItem,
            this.skinEditorToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 17);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // alwaysOnTopToolStripMenuItem
            // 
            this.alwaysOnTopToolStripMenuItem.Name = "alwaysOnTopToolStripMenuItem";
            this.alwaysOnTopToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.alwaysOnTopToolStripMenuItem.Text = "Always on top";
            this.alwaysOnTopToolStripMenuItem.Click += new System.EventHandler(this.alwaysOnTopToolStripMenuItem_Click);
            // 
            // alarmPanelToolStripMenuItem
            // 
            this.alarmPanelToolStripMenuItem.Checked = true;
            this.alarmPanelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.alarmPanelToolStripMenuItem.Name = "alarmPanelToolStripMenuItem";
            this.alarmPanelToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.alarmPanelToolStripMenuItem.Text = "Alarm Panel";
            this.alarmPanelToolStripMenuItem.Click += new System.EventHandler(this.Expanded_MenuClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.GroupName = "Menu";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Registered = false;
            this.toolStripSeparator2.Size = new System.Drawing.Size(150, 6);
            // 
            // dialogsToolStripMenuItem
            // 
            this.dialogsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.confirmDeleteToolStripMenuItem});
            this.dialogsToolStripMenuItem.Name = "dialogsToolStripMenuItem";
            this.dialogsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.dialogsToolStripMenuItem.Text = "Dialogs";
            // 
            // confirmDeleteToolStripMenuItem
            // 
            this.confirmDeleteToolStripMenuItem.Checked = true;
            this.confirmDeleteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.confirmDeleteToolStripMenuItem.Name = "confirmDeleteToolStripMenuItem";
            this.confirmDeleteToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.confirmDeleteToolStripMenuItem.Text = "Confirm delete";
            this.confirmDeleteToolStripMenuItem.Click += new System.EventHandler(this.confirmDeleteToolStripMenuItem_Click);
            // 
            // skinImagesToolStripMenuItem
            // 
            this.skinImagesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clenseModeToolStripMenuItem,
            this.totalToolStripMenuItem,
            this.clearToolStripMenuItem,
            this.clenseToolStripMenuItem});
            this.skinImagesToolStripMenuItem.Name = "skinImagesToolStripMenuItem";
            this.skinImagesToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.skinImagesToolStripMenuItem.Text = "Images";
            // 
            // clenseModeToolStripMenuItem
            // 
            this.clenseModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimalToolStripMenuItem,
            this.standardToolStripMenuItem,
            this.aggressiveToolStripMenuItem});
            this.clenseModeToolStripMenuItem.Name = "clenseModeToolStripMenuItem";
            this.clenseModeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.clenseModeToolStripMenuItem.Text = "Clense Mode";
            // 
            // minimalToolStripMenuItem
            // 
            this.minimalToolStripMenuItem.Name = "minimalToolStripMenuItem";
            this.minimalToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.minimalToolStripMenuItem.Text = "Minimal";
            this.minimalToolStripMenuItem.Click += new System.EventHandler(this.clenseProfileToolStripMenuItem_Click);
            // 
            // standardToolStripMenuItem
            // 
            this.standardToolStripMenuItem.Name = "standardToolStripMenuItem";
            this.standardToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.standardToolStripMenuItem.Text = "Standard";
            this.standardToolStripMenuItem.Click += new System.EventHandler(this.clenseProfileToolStripMenuItem_Click);
            // 
            // aggressiveToolStripMenuItem
            // 
            this.aggressiveToolStripMenuItem.Name = "aggressiveToolStripMenuItem";
            this.aggressiveToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.aggressiveToolStripMenuItem.Text = "Aggressive";
            this.aggressiveToolStripMenuItem.Click += new System.EventHandler(this.clenseProfileToolStripMenuItem_Click);
            // 
            // totalToolStripMenuItem
            // 
            this.totalToolStripMenuItem.Name = "totalToolStripMenuItem";
            this.totalToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.totalToolStripMenuItem.Text = "Total";
            this.totalToolStripMenuItem.Click += new System.EventHandler(this.totalToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // clenseToolStripMenuItem
            // 
            this.clenseToolStripMenuItem.Name = "clenseToolStripMenuItem";
            this.clenseToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.clenseToolStripMenuItem.Text = "Clense";
            this.clenseToolStripMenuItem.Click += new System.EventHandler(this.clenseToolStripMenuItem_Click);
            // 
            // coloursToolStripMenuItem
            // 
            this.coloursToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadColoursFromFileToolStripMenuItem,
            this.saveColoursToFileToolStripMenuItem});
            this.coloursToolStripMenuItem.Name = "coloursToolStripMenuItem";
            this.coloursToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.coloursToolStripMenuItem.Text = "Colours";
            // 
            // loadColoursFromFileToolStripMenuItem
            // 
            this.loadColoursFromFileToolStripMenuItem.Name = "loadColoursFromFileToolStripMenuItem";
            this.loadColoursFromFileToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.loadColoursFromFileToolStripMenuItem.Text = "Load Colours from file";
            this.loadColoursFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadColoursFromFileToolStripMenuItem_Click);
            // 
            // saveColoursToFileToolStripMenuItem
            // 
            this.saveColoursToFileToolStripMenuItem.Name = "saveColoursToFileToolStripMenuItem";
            this.saveColoursToFileToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.saveColoursToFileToolStripMenuItem.Text = "Save Colours to file";
            this.saveColoursToFileToolStripMenuItem.Click += new System.EventHandler(this.saveColoursToFileToolStripMenuItem_Click);
            // 
            // skinEditorToolStripMenuItem
            // 
            this.skinEditorToolStripMenuItem.Name = "skinEditorToolStripMenuItem";
            this.skinEditorToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.skinEditorToolStripMenuItem.Text = "Skin Editor";
            this.skinEditorToolStripMenuItem.Click += new System.EventHandler(this.skinEditorToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.factorySetupToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 17);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // factorySetupToolStripMenuItem
            // 
            this.factorySetupToolStripMenuItem.Name = "factorySetupToolStripMenuItem";
            this.factorySetupToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.factorySetupToolStripMenuItem.Text = "Factory Setup";
            this.factorySetupToolStripMenuItem.Click += new System.EventHandler(this.factorySetupToolStripMenuItem_Click);
            // 
            // m_NextAlarmLabel
            // 
            this.m_NextAlarmLabel.AutoSize = true;
            this.m_NextAlarmLabel.Location = new System.Drawing.Point(2, 2);
            this.m_NextAlarmLabel.Name = "m_NextAlarmLabel";
            this.m_NextAlarmLabel.Size = new System.Drawing.Size(89, 13);
            this.m_NextAlarmLabel.TabIndex = 22;
            this.m_NextAlarmLabel.Text = "Next alarm: None";
            // 
            // m_NextAlarmPanel
            // 
            this.m_NextAlarmPanel.Controls.Add(this.m_NextAlarmLabel);
            this.m_NextAlarmPanel.Location = new System.Drawing.Point(0, 90);
            this.m_NextAlarmPanel.Name = "m_NextAlarmPanel";
            this.m_NextAlarmPanel.Size = new System.Drawing.Size(236, 18);
            this.m_NextAlarmPanel.TabIndex = 23;
            this.m_NextAlarmPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseMove);
            this.m_NextAlarmPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseDown);
            this.m_NextAlarmPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseUp);
            // 
            // m_ShowPanelButton
            // 
            this.m_ShowPanelButton.BackColor = System.Drawing.Color.Transparent;
            this.m_ShowPanelButton.Checked = true;
            this.m_ShowPanelButton.DisabledColor = System.Drawing.SystemColors.ButtonShadow;
            this.m_ShowPanelButton.FlatAppearance.BorderSize = 0;
            this.m_ShowPanelButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.m_ShowPanelButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.m_ShowPanelButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.m_ShowPanelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ShowPanelButton.GroupName = "Clock Display";
            this.m_ShowPanelButton.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_ShowPanelButton.HighlightDisabled = false;
            this.m_ShowPanelButton.HighlightOnFocus = true;
            this.m_ShowPanelButton.ImageFilePath = "~/up-image.bmp";
            this.m_ShowPanelButton.Location = new System.Drawing.Point(236, 96);
            this.m_ShowPanelButton.Margin = new System.Windows.Forms.Padding(0);
            this.m_ShowPanelButton.Name = "m_ShowPanelButton";
            this.m_ShowPanelButton.Registered = false;
            this.m_ShowPanelButton.Size = new System.Drawing.Size(12, 12);
            this.m_ShowPanelButton.TabIndex = 1;
            this.m_ShowPanelButton.Text = "imageButton2";
            this.m_ShowPanelButton.UncheckedImageFilePath = "~/down-image.bmp";
            this.m_ShowPanelButton.UseVisualStyleBackColor = true;
            this.m_ShowPanelButton.Click += new System.EventHandler(this.Expanded_MenuClick);
            // 
            // m_CloseButton
            // 
            this.m_CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_CloseButton.BackColor = System.Drawing.Color.Transparent;
            this.m_CloseButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_CloseButton.DisabledColor = System.Drawing.SystemColors.ButtonShadow;
            this.m_CloseButton.FlatAppearance.BorderSize = 0;
            this.m_CloseButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.m_CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.m_CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.m_CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_CloseButton.GroupName = "Control Buttons";
            this.m_CloseButton.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_CloseButton.HighlightDisabled = false;
            this.m_CloseButton.HighlightOnFocus = true;
            this.m_CloseButton.ImageFilePath = "~/alarm/close-image.bmp";
            this.m_CloseButton.Location = new System.Drawing.Point(236, 3);
            this.m_CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.m_CloseButton.Name = "m_CloseButton";
            this.m_CloseButton.Registered = false;
            this.m_CloseButton.Size = new System.Drawing.Size(12, 12);
            this.m_CloseButton.TabIndex = 23;
            this.m_CloseButton.Text = "imageButton2";
            this.m_CloseButton.UseVisualStyleBackColor = true;
            this.m_CloseButton.Click += new System.EventHandler(this.OnCloseClicked);
            // 
            // m_ShrinkButton
            // 
            this.m_ShrinkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ShrinkButton.BackColor = System.Drawing.Color.Transparent;
            this.m_ShrinkButton.Checked = true;
            this.m_ShrinkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_ShrinkButton.DisabledColor = System.Drawing.SystemColors.ButtonShadow;
            this.m_ShrinkButton.FlatAppearance.BorderSize = 0;
            this.m_ShrinkButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.m_ShrinkButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.m_ShrinkButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.m_ShrinkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ShrinkButton.GroupName = "Control Buttons";
            this.m_ShrinkButton.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_ShrinkButton.HighlightDisabled = false;
            this.m_ShrinkButton.HighlightOnFocus = true;
            this.m_ShrinkButton.ImageFilePath = "~/alarm/minimize-image.bmp";
            this.m_ShrinkButton.Location = new System.Drawing.Point(208, 3);
            this.m_ShrinkButton.Margin = new System.Windows.Forms.Padding(0);
            this.m_ShrinkButton.Name = "m_ShrinkButton";
            this.m_ShrinkButton.Registered = false;
            this.m_ShrinkButton.Size = new System.Drawing.Size(12, 12);
            this.m_ShrinkButton.TabIndex = 21;
            this.m_ShrinkButton.Text = "imageCheckbox1";
            this.m_ShrinkButton.UncheckedImageFilePath = "~/down-image.bmp";
            this.m_ShrinkButton.UseVisualStyleBackColor = true;
            this.m_ShrinkButton.Click += new System.EventHandler(this.OnShrink);
            // 
            // m_ToTaskButton
            // 
            this.m_ToTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_ToTaskButton.BackColor = System.Drawing.Color.Transparent;
            this.m_ToTaskButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.m_ToTaskButton.DisabledColor = System.Drawing.SystemColors.ButtonShadow;
            this.m_ToTaskButton.FlatAppearance.BorderSize = 0;
            this.m_ToTaskButton.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.m_ToTaskButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.m_ToTaskButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.m_ToTaskButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.m_ToTaskButton.GroupName = "Control Buttons";
            this.m_ToTaskButton.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.m_ToTaskButton.HighlightDisabled = false;
            this.m_ToTaskButton.HighlightOnFocus = true;
            this.m_ToTaskButton.ImageFilePath = "~/alarm/task-image.bmp";
            this.m_ToTaskButton.Location = new System.Drawing.Point(222, 3);
            this.m_ToTaskButton.Margin = new System.Windows.Forms.Padding(0);
            this.m_ToTaskButton.Name = "m_ToTaskButton";
            this.m_ToTaskButton.Registered = false;
            this.m_ToTaskButton.Size = new System.Drawing.Size(12, 12);
            this.m_ToTaskButton.TabIndex = 22;
            this.m_ToTaskButton.Text = "imageButton2";
            this.m_ToTaskButton.UseVisualStyleBackColor = true;
            this.m_ToTaskButton.Click += new System.EventHandler(this.OnGotoTask);
            // 
            // panel2
            // 
            this.panel2.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.imageButton1);
            this.panel2.GroupName = "Panel";
            this.panel2.Location = new System.Drawing.Point(0, 223);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Registered = false;
            this.panel2.Size = new System.Drawing.Size(250, 22);
            this.panel2.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(22, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(225, 14);
            this.label2.TabIndex = 6;
            this.label2.Text = "No sound file loaded";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageButton1
            // 
            this.imageButton1.BackColor = System.Drawing.Color.Transparent;
            this.imageButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageButton1.DisabledColor = System.Drawing.SystemColors.ButtonShadow;
            this.imageButton1.FlatAppearance.BorderSize = 0;
            this.imageButton1.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.imageButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.imageButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.imageButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.imageButton1.GroupName = "Panel Buttons";
            this.imageButton1.HighlightColor = System.Drawing.SystemColors.ButtonHighlight;
            this.imageButton1.HighlightDisabled = false;
            this.imageButton1.HighlightOnFocus = true;
            this.imageButton1.ImageFilePath = "~/alarm/sound-image.bmp";
            this.imageButton1.Location = new System.Drawing.Point(3, 3);
            this.imageButton1.Margin = new System.Windows.Forms.Padding(0);
            this.imageButton1.Name = "imageButton1";
            this.imageButton1.Registered = false;
            this.imageButton1.Size = new System.Drawing.Size(16, 16);
            this.imageButton1.TabIndex = 0;
            this.imageButton1.Text = "imageButton1";
            this.imageButton1.UseVisualStyleBackColor = true;
            this.imageButton1.Click += new System.EventHandler(this.LoadFilebutton_Click);
            // 
            // alarmsPanel1
            // 
            this.alarmsPanel1.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.alarmsPanel1.FlashingColour = alarmColours1;
            this.alarmsPanel1.GroupName = "Panel";
            this.alarmsPanel1.Location = new System.Drawing.Point(0, 111);
            this.alarmsPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.alarmsPanel1.MinimumSize = new System.Drawing.Size(244, 67);
            this.alarmsPanel1.Name = "alarmsPanel1";
            this.alarmsPanel1.Registered = false;
            this.alarmsPanel1.RegularColours = alarmColours2;
            this.alarmsPanel1.Size = new System.Drawing.Size(250, 114);
            this.alarmsPanel1.TabIndex = 7;
            // 
            // digitalClockCtrl1
            // 
            this.digitalClockCtrl1.ClockDisplayFormat = AlarmClock.Display.ClockFormat.TwentyFourHourFormat;
            this.digitalClockCtrl1.ClockType = AlarmClock.Display.ClockType.DigitalClock;
            this.digitalClockCtrl1.ColonType = AlarmClock.Display.ColonType.Rectangular;
            this.digitalClockCtrl1.CurrentColourType = AlarmClock.Display.ColourType.Regular;
            this.digitalClockCtrl1.FlashingColour = alarmColours3;
            this.digitalClockCtrl1.GroupName = "Clock Display";
            this.digitalClockCtrl1.HorizontalPadding = 0;
            this.digitalClockCtrl1.LineWidth = 5F;
            this.digitalClockCtrl1.Location = new System.Drawing.Point(6, 21);
            this.digitalClockCtrl1.Margin = new System.Windows.Forms.Padding(0);
            this.digitalClockCtrl1.Name = "digitalClockCtrl1";
            this.digitalClockCtrl1.Registered = false;
            this.digitalClockCtrl1.RegularColours = alarmColours4;
            this.digitalClockCtrl1.Size = new System.Drawing.Size(238, 68);
            this.digitalClockCtrl1.TabIndex = 0;
            this.digitalClockCtrl1.VerticalPadding = 0;
            this.digitalClockCtrl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseMove);
            this.digitalClockCtrl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseDown);
            this.digitalClockCtrl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseUp);
            // 
            // windowHeader1
            // 
            this.windowHeader1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.windowHeader1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.windowHeader1.FlashingColour = alarmColours5;
            this.windowHeader1.Location = new System.Drawing.Point(0, 0);
            this.windowHeader1.Margin = new System.Windows.Forms.Padding(0);
            this.windowHeader1.MinimumSize = new System.Drawing.Size(250, 19);
            this.windowHeader1.Name = "windowHeader1";
            this.windowHeader1.NextAlarmText = "Next: None";
            this.windowHeader1.RegularColours = alarmColours6;
            this.windowHeader1.ShowClock = false;
            this.windowHeader1.Size = new System.Drawing.Size(250, 19);
            this.windowHeader1.TabIndex = 29;
            this.windowHeader1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseMove);
            this.windowHeader1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnDoubleClick);
            this.windowHeader1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseDown);
            this.windowHeader1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(250, 377);
            this.Controls.Add(this.m_ShowPanelButton);
            this.Controls.Add(this.m_CloseButton);
            this.Controls.Add(this.m_ShrinkButton);
            this.Controls.Add(this.m_ToTaskButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.m_NextAlarmPanel);
            this.Controls.Add(this.alarmsPanel1);
            this.Controls.Add(this.digitalClockCtrl1);
            this.Controls.Add(this.KillButton);
            this.Controls.Add(this.windowHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(600, 600);
            this.Name = "MainForm";
            this.Text = "Alarm Clock";
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseUp);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SaveSettings);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Close_FormClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnClockMouseMove);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.m_NextAlarmPanel.ResumeLayout(false);
            this.m_NextAlarmPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button KillButton;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private AlarmClock.Display.ClockDisplay digitalClockCtrl1;
        private System.Windows.Forms.Timer colorTimer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alarmPanelToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem armedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem factorySetupToolStripMenuItem;
        private System.Windows.Forms.Label m_NextAlarmLabel;
        private System.Windows.Forms.Panel m_NextAlarmPanel;
        private Rug.UI.ImageCheckbox m_ShowPanelButton;
        private AlarmClock.Controls.AlarmsPanel alarmsPanel1;
        private Rug.UI.ImageButton m_ToTaskButton;
        private Rug.UI.MenuSeparator toolStripSeparator1;
        private Rug.UI.MenuSeparator toolStripSeparator2;
        private Rug.UI.ImageButton imageButton1;
        private Rug.UI.ImageCheckbox m_ShrinkButton;
        private AlarmClock.Controls.WindowHeader windowHeader1;
        private Rug.UI.ImageButton m_CloseButton;
        private System.Windows.Forms.ToolStripMenuItem alwaysOnTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dialogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confirmDeleteToolStripMenuItem;
        private Rug.UI.PanelWithBorder panel2;
        private System.Windows.Forms.ToolStripMenuItem skinEditorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coloursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadColoursFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveColoursToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skinImagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clenseModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem standardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aggressiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clenseToolStripMenuItem;
       
    }
}
