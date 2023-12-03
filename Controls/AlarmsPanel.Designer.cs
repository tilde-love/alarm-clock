namespace AlarmClock.Controls
{
    partial class AlarmsPanel
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
            AlarmClock.AlarmColours alarmColours1 = new AlarmClock.AlarmColours();
            AlarmClock.AlarmColours alarmColours2 = new AlarmClock.AlarmColours();
            AlarmClock.Time time1 = new AlarmClock.Time();
            this.m_AlarmsList = new AlarmClock.Controls.TimeList();
            this.m_TimeEditor = new AlarmClock.Display.ClockEdit();
            this.m_SaturdayBox = new Rug.UI.ImageCheckbox();
            this.m_MondayBox = new Rug.UI.ImageCheckbox();
            this.backImage1 = new Rug.UI.BackImage();
            this.m_TuesdayBox = new Rug.UI.ImageCheckbox();
            this.m_SundayBox = new Rug.UI.ImageCheckbox();
            this.m_UpdateTimeButton = new Rug.UI.ImageButton();
            this.backImage2 = new Rug.UI.BackImage();
            this.m_WednesdayBox = new Rug.UI.ImageCheckbox();
            this.m_FridayBox = new Rug.UI.ImageCheckbox();
            this.m_CreateAlarm = new Rug.UI.ImageButton();
            this.m_RemoveTimeButton = new Rug.UI.ImageButton();
            this.m_ThursdayBox = new Rug.UI.ImageCheckbox();
            this.init1 = new AlarmClock.Images.Init();
            this.SuspendLayout();
            // 
            // m_AlarmsList
            // 
            this.m_AlarmsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            //this.m_AlarmsList.BorderColor = System.Drawing.Color.Black;
            this.m_AlarmsList.GroupName = "Alarms List";
            this.m_AlarmsList.Location = new System.Drawing.Point(3, 30);
            this.m_AlarmsList.Margin = new System.Windows.Forms.Padding(0);
            this.m_AlarmsList.MouseWheelSpeed = 1;
            this.m_AlarmsList.Name = "Alarms list";
            //this.m_AlarmsList.ScrollerHighlightColor = System.Drawing.Color.White;
            this.m_AlarmsList.ScrollX = 0;
            this.m_AlarmsList.ScrollY = 0;
            this.m_AlarmsList.SelectedChecked = false;
            //this.m_AlarmsList.SelectedColor = System.Drawing.Color.White;
            this.m_AlarmsList.SelectedItem = null;
            this.m_AlarmsList.SelectedObject = null;
            //this.m_AlarmsList.SelectedTextColor = System.Drawing.Color.Black;
            this.m_AlarmsList.Size = new System.Drawing.Size(390, 70);
            this.m_AlarmsList.TabIndex = 11;
            this.m_AlarmsList.SelectionChanged += new Rug.UI.CheckboxList<AlarmClock.Time>.ItemEvent(this.OnSelectionChanged);
            this.m_AlarmsList.CheckedChanged += new Rug.UI.CheckboxList<AlarmClock.Time>.ItemCheckedEvent(this.OnCheckedChanged);
            // 
            // m_TimeEditor
            // 
            this.m_TimeEditor.ClockDisplayFormat = AlarmClock.Display.ClockFormat.TwentyFourHourFormat;
            this.m_TimeEditor.ColonType = AlarmClock.Display.ColonType.Rectangular;
            this.m_TimeEditor.CurrentColourType = AlarmClock.Display.ColourType.Regular;
            this.m_TimeEditor.CurrentlyEditing = AlarmClock.Display.UnitEdit.Hour;
            this.m_TimeEditor.FlashingColour = alarmColours1;
            this.m_TimeEditor.LineWidth = 2F;
            this.m_TimeEditor.Location = new System.Drawing.Point(3, 3);
            this.m_TimeEditor.Name = "Time editor";
            this.m_TimeEditor.RegularColours = alarmColours2;
            this.m_TimeEditor.Size = new System.Drawing.Size(52, 24);
            this.m_TimeEditor.TabIndex = 0;
            this.m_TimeEditor.Time = time1;
            this.m_TimeEditor.TimeChanged += new AlarmClock.Display.TimeChangedEvent(this.OnTimeChanged);
            this.m_TimeEditor.GroupName = "Clock Display";
            // 
            // m_SaturdayBox
            // 
            this.m_SaturdayBox.Checked = true;
            //this.m_SaturdayBox.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_SaturdayBox.GroupName = "Panel Buttons";
            //this.m_SaturdayBox.HighlightColor = System.Drawing.Color.White;
            this.m_SaturdayBox.HighlightDisabled = false;
            this.m_SaturdayBox.HighlightOnFocus = true;
            this.m_SaturdayBox.ImageFilePath = "~/alarm/saturday.bmp";
            this.m_SaturdayBox.Location = new System.Drawing.Point(149, 10);
            this.m_SaturdayBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_SaturdayBox.Name = "Saturday toggle box";
            this.m_SaturdayBox.Size = new System.Drawing.Size(16, 16);
            this.m_SaturdayBox.TabIndex = 6;
            this.m_SaturdayBox.Text = "imageCheckbox1";
            this.m_SaturdayBox.UncheckedImageFilePath = "~/strike.bmp";
            this.m_SaturdayBox.UseVisualStyleBackColor = true;
            this.m_SaturdayBox.Click += new System.EventHandler(this.OnDayClick);
            // 
            // m_MondayBox
            // 
            this.m_MondayBox.Checked = true;
            //this.m_MondayBox.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_MondayBox.GroupName = "Panel Buttons";
            //this.m_MondayBox.HighlightColor = System.Drawing.Color.White;
            this.m_MondayBox.HighlightDisabled = false;
            this.m_MondayBox.HighlightOnFocus = true;
            this.m_MondayBox.ImageFilePath = "~/alarm/monday.bmp";
            this.m_MondayBox.Location = new System.Drawing.Point(59, 10);
            this.m_MondayBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_MondayBox.Name = "Monday toggle box";
            this.m_MondayBox.Size = new System.Drawing.Size(16, 16);
            this.m_MondayBox.TabIndex = 1;
            this.m_MondayBox.Text = "imageButton3";
            this.m_MondayBox.UncheckedImageFilePath = "~/strike.bmp";
            this.m_MondayBox.UseVisualStyleBackColor = true;
            this.m_MondayBox.Click += new System.EventHandler(this.OnDayClick);
            // 
            // backImage1
            // 
            this.backImage1.GroupName = "";
            this.backImage1.ImageFilePath = "~/alarm/days-background.bmp";
            this.backImage1.Location = new System.Drawing.Point(58, 3);
            this.backImage1.Name = "Weekdays background";
            this.backImage1.Size = new System.Drawing.Size(128, 6);
            this.backImage1.TabIndex = 32;
            this.backImage1.TabStop = false;
            // 
            // m_TuesdayBox
            // 
            this.m_TuesdayBox.Checked = true;
            //this.m_TuesdayBox.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_TuesdayBox.GroupName = "Panel Buttons";
            //this.m_TuesdayBox.HighlightColor = System.Drawing.Color.White;
            this.m_TuesdayBox.HighlightDisabled = false;
            this.m_TuesdayBox.HighlightOnFocus = true;
            this.m_TuesdayBox.ImageFilePath = "~/alarm/tuesday.bmp";
            this.m_TuesdayBox.Location = new System.Drawing.Point(77, 10);
            this.m_TuesdayBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_TuesdayBox.Name = "Tuesday toggle box";
            this.m_TuesdayBox.Size = new System.Drawing.Size(16, 16);
            this.m_TuesdayBox.TabIndex = 2;
            this.m_TuesdayBox.Text = "imageButton4";
            this.m_TuesdayBox.UncheckedImageFilePath = "~/strike.bmp";
            this.m_TuesdayBox.UseVisualStyleBackColor = true;
            this.m_TuesdayBox.Click += new System.EventHandler(this.OnDayClick);
            // 
            // m_SundayBox
            // 
            this.m_SundayBox.Checked = true;
            //this.m_SundayBox.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_SundayBox.GroupName = "Panel Buttons";
            //this.m_SundayBox.HighlightColor = System.Drawing.Color.White;
            this.m_SundayBox.HighlightDisabled = false;
            this.m_SundayBox.HighlightOnFocus = true;
            this.m_SundayBox.ImageFilePath = "~/alarm/sunday.bmp";
            this.m_SundayBox.Location = new System.Drawing.Point(167, 10);
            this.m_SundayBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_SundayBox.Name = "Sunday toggle box";
            this.m_SundayBox.Size = new System.Drawing.Size(16, 16);
            this.m_SundayBox.TabIndex = 7;
            this.m_SundayBox.Text = "imageButton9";
            this.m_SundayBox.UncheckedImageFilePath = "~/strike.bmp";
            this.m_SundayBox.UseVisualStyleBackColor = true;
            this.m_SundayBox.Click += new System.EventHandler(this.OnDayClick);
            // 
            // m_UpdateTimeButton
            // 
            this.m_UpdateTimeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_UpdateTimeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            //this.m_UpdateTimeButton.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_UpdateTimeButton.GroupName = "Panel Buttons";
            //this.m_UpdateTimeButton.HighlightColor = System.Drawing.Color.White;
            this.m_UpdateTimeButton.HighlightDisabled = false;
            this.m_UpdateTimeButton.HighlightOnFocus = true;
            this.m_UpdateTimeButton.ImageFilePath = "~/alarm/update-image.bmp";
            this.m_UpdateTimeButton.Location = new System.Drawing.Point(358, 4);
            this.m_UpdateTimeButton.Margin = new System.Windows.Forms.Padding(0);
            this.m_UpdateTimeButton.Name = "Update alarm button";
            this.m_UpdateTimeButton.Size = new System.Drawing.Size(16, 16);
            this.m_UpdateTimeButton.TabIndex = 9;
            this.m_UpdateTimeButton.Text = "imageButton3";
            this.m_UpdateTimeButton.UseVisualStyleBackColor = true;
            this.m_UpdateTimeButton.Click += new System.EventHandler(this.OnUpdateClick);
            // 
            // backImage2
            // 
            this.backImage2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.backImage2.GroupName = "";
            this.backImage2.ImageFilePath = "~/alarm/control-background.bmp";
            this.backImage2.Location = new System.Drawing.Point(338, 21);
            this.backImage2.Name = "Alarm managment buttons background";
            this.backImage2.Size = new System.Drawing.Size(55, 6);
            this.backImage2.TabIndex = 33;
            this.backImage2.TabStop = false;
            // 
            // m_WednesdayBox
            // 
            this.m_WednesdayBox.Checked = true;
            //this.m_WednesdayBox.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_WednesdayBox.GroupName = "Panel Buttons";
            //this.m_WednesdayBox.HighlightColor = System.Drawing.Color.White;
            this.m_WednesdayBox.HighlightDisabled = false;
            this.m_WednesdayBox.HighlightOnFocus = true;
            this.m_WednesdayBox.ImageFilePath = "~/alarm/wednesday.bmp";
            this.m_WednesdayBox.Location = new System.Drawing.Point(95, 10);
            this.m_WednesdayBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_WednesdayBox.Name = "Wednesday toggle box";
            this.m_WednesdayBox.Size = new System.Drawing.Size(16, 16);
            this.m_WednesdayBox.TabIndex = 3;
            this.m_WednesdayBox.Text = "imageButton5";
            this.m_WednesdayBox.UncheckedImageFilePath = "~/strike.bmp";
            this.m_WednesdayBox.UseVisualStyleBackColor = true;
            this.m_WednesdayBox.Click += new System.EventHandler(this.OnDayClick);
            // 
            // m_FridayBox
            // 
            this.m_FridayBox.Checked = true;
            //this.m_FridayBox.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_FridayBox.GroupName = "Panel Buttons";
            //this.m_FridayBox.HighlightColor = System.Drawing.Color.White;
            this.m_FridayBox.HighlightDisabled = false;
            this.m_FridayBox.HighlightOnFocus = true;
            this.m_FridayBox.ImageFilePath = "~/alarm/friday.bmp";
            this.m_FridayBox.Location = new System.Drawing.Point(131, 10);
            this.m_FridayBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_FridayBox.Name = "Friday toggle box";
            this.m_FridayBox.Size = new System.Drawing.Size(16, 16);
            this.m_FridayBox.TabIndex = 5;
            this.m_FridayBox.Text = "imageButton7";
            this.m_FridayBox.UncheckedImageFilePath = "~/strike.bmp";
            this.m_FridayBox.UseVisualStyleBackColor = true;
            this.m_FridayBox.Click += new System.EventHandler(this.OnDayClick);
            // 
            // m_CreateAlarm
            // 
            this.m_CreateAlarm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_CreateAlarm.Cursor = System.Windows.Forms.Cursors.Hand;
            //this.m_CreateAlarm.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_CreateAlarm.GroupName = "Panel Buttons";
            //this.m_CreateAlarm.HighlightColor = System.Drawing.Color.White;
            this.m_CreateAlarm.HighlightDisabled = false;
            this.m_CreateAlarm.HighlightOnFocus = true;
            this.m_CreateAlarm.ImageFilePath = "~/alarm/add-image.bmp";
            this.m_CreateAlarm.Location = new System.Drawing.Point(340, 4);
            this.m_CreateAlarm.Margin = new System.Windows.Forms.Padding(0);
            this.m_CreateAlarm.Name = "Create alarm button";
            this.m_CreateAlarm.Size = new System.Drawing.Size(16, 16);
            this.m_CreateAlarm.TabIndex = 8;
            this.m_CreateAlarm.Text = "imageButton2";
            this.m_CreateAlarm.UseVisualStyleBackColor = true;
            this.m_CreateAlarm.Click += new System.EventHandler(this.OnAddClick);
            // 
            // m_RemoveTimeButton
            // 
            this.m_RemoveTimeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_RemoveTimeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            //this.m_RemoveTimeButton.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_RemoveTimeButton.GroupName = "Panel Buttons";
            //this.m_RemoveTimeButton.HighlightColor = System.Drawing.Color.White;
            this.m_RemoveTimeButton.HighlightDisabled = false;
            this.m_RemoveTimeButton.HighlightOnFocus = true;
            this.m_RemoveTimeButton.ImageFilePath = "~/alarm/cross-image.bmp";
            this.m_RemoveTimeButton.Location = new System.Drawing.Point(376, 4);
            this.m_RemoveTimeButton.Margin = new System.Windows.Forms.Padding(0);
            this.m_RemoveTimeButton.Name = "Remove alarm button";
            this.m_RemoveTimeButton.Size = new System.Drawing.Size(16, 16);
            this.m_RemoveTimeButton.TabIndex = 10;
            this.m_RemoveTimeButton.Click += new System.EventHandler(this.OnRemoveClick);
            // 
            // m_ThursdayBox
            // 
            this.m_ThursdayBox.Checked = true;
            //this.m_ThursdayBox.DisabledColor = System.Drawing.Color.DarkGray;
            this.m_ThursdayBox.GroupName = "Panel Buttons";
            //this.m_ThursdayBox.HighlightColor = System.Drawing.Color.White;
            this.m_ThursdayBox.HighlightDisabled = false;
            this.m_ThursdayBox.HighlightOnFocus = true;
            this.m_ThursdayBox.ImageFilePath = "~/alarm/thursday.bmp";
            this.m_ThursdayBox.Location = new System.Drawing.Point(113, 10);
            this.m_ThursdayBox.Margin = new System.Windows.Forms.Padding(0);
            this.m_ThursdayBox.Name = "Thursday toggle box";
            this.m_ThursdayBox.Size = new System.Drawing.Size(16, 16);
            this.m_ThursdayBox.TabIndex = 4;
            this.m_ThursdayBox.Text = "imageButton6";
            this.m_ThursdayBox.UncheckedImageFilePath = "~/strike.bmp";
            this.m_ThursdayBox.UseVisualStyleBackColor = true;
            this.m_ThursdayBox.Click += new System.EventHandler(this.OnDayClick);
            // 
            // init1
            // 
            this.init1.Path = null;
            // 
            // AlarmsPanel
            // 
            //this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            //this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.backImage1);
            this.Controls.Add(this.backImage2);
            this.Controls.Add(this.m_AlarmsList);
            this.Controls.Add(this.m_TimeEditor);
            this.Controls.Add(this.m_SaturdayBox);
            this.Controls.Add(this.m_MondayBox);
            this.Controls.Add(this.m_TuesdayBox);
            this.Controls.Add(this.m_SundayBox);
            this.Controls.Add(this.m_UpdateTimeButton);
            this.Controls.Add(this.m_WednesdayBox);
            this.Controls.Add(this.m_FridayBox);
            this.Controls.Add(this.m_CreateAlarm);
            this.Controls.Add(this.m_RemoveTimeButton);
            this.Controls.Add(this.m_ThursdayBox);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MinimumSize = new System.Drawing.Size(244, 67);
            this.Name = "Alarms Panel";
            this.Size = new System.Drawing.Size(396, 103);
            this.ResumeLayout(false);

        }

        #endregion

        private AlarmClock.Images.Init init1;
        private Rug.UI.ImageCheckbox m_SaturdayBox;
        private Rug.UI.ImageCheckbox m_SundayBox;
        private Rug.UI.ImageCheckbox m_FridayBox;
        private Rug.UI.ImageCheckbox m_ThursdayBox;
        private Rug.UI.ImageCheckbox m_WednesdayBox;
        private Rug.UI.ImageCheckbox m_TuesdayBox;
        private Rug.UI.ImageCheckbox m_MondayBox;
        private Rug.UI.ImageButton m_UpdateTimeButton;
        private Rug.UI.ImageButton m_CreateAlarm;
        private Rug.UI.ImageButton m_RemoveTimeButton;
        private AlarmClock.Display.ClockEdit m_TimeEditor;
        private Rug.UI.BackImage backImage1;
        private Rug.UI.BackImage backImage2;
        private TimeList m_AlarmsList;
    }
}
