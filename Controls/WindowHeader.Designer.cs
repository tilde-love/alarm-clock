namespace AlarmClock.Controls
{
    partial class WindowHeader
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
            this.label1 = new System.Windows.Forms.Label();
            this.clockDisplay1 = new AlarmClock.Display.ClockDisplay();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Next: None";
            // 
            // clockDisplay1
            // 
            this.clockDisplay1.ClockDisplayFormat = AlarmClock.Display.ClockFormat.TwentyFourHourFormat;
            this.clockDisplay1.ClockType = AlarmClock.Display.ClockType.DigitalClock;
            this.clockDisplay1.ColonType = AlarmClock.Display.ColonType.Rectangular;
            this.clockDisplay1.CurrentColourType = AlarmClock.Display.ColourType.Regular;
            this.clockDisplay1.FlashingColour = alarmColours1;
            this.clockDisplay1.HorizontalPadding = 0;
            this.clockDisplay1.LineWidth = 1F;
            this.clockDisplay1.Location = new System.Drawing.Point(3, 2);
            this.clockDisplay1.Name = "clockDisplay1";
            this.clockDisplay1.RegularColours = alarmColours2;
            this.clockDisplay1.Size = new System.Drawing.Size(53, 15);
            this.clockDisplay1.TabIndex = 0;
            this.clockDisplay1.VerticalPadding = 1;
            this.clockDisplay1.GroupName = "Clock Display";
            // 
            // WindowHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.clockDisplay1);
            this.Controls.Add(this.label1);
            this.Name = "WindowHeader";
            this.Size = new System.Drawing.Size(250, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AlarmClock.Display.ClockDisplay clockDisplay1;
        private System.Windows.Forms.Label label1;
    }
}
