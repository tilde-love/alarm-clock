using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using AlarmClock.Display;
using AlarmClock.Controls;
using System.Diagnostics;
using Rug.UI;
using AlarmClock.Images;
using Rug.UI.Util;
using Rug.UI.Skin;
using Rug.UI.Dialogs;
using System.Drawing.Imaging;


namespace AlarmClock
{
    public partial class MainForm : Form, ISkinnable, ISkinnableApplication
    {
        #region Members

        AlarmConfig m_Config = AlarmConfig.Config;

        private bool m_Registered = false;

        public bool Registered
        {
            get
            {
                return m_Registered;
            }
            set
            {
                m_Registered = value;
            }
        }

        private ColorPickerDialog m_ColorDialog = null;

        private ColorPickerDialog ColorDialog
        {
            get
            {
                if (m_ColorDialog == null)
                {
                    m_ColorDialog = new ColorPickerDialog();
                    m_ColorDialog.Location = m_Config.ColourDialogLocation;
                    m_ColorDialog.PickerMode = m_Config.ColourDialogMode;
                }

                return m_ColorDialog;
            }
        }

        private Time NewTime = new Time();
        private Time LastTime = new Time();
        private bool RingAlarm = false;
        private Player m_Player = new Player();
        private bool m_Initiated = false;
        private bool m_Loaded = false;
        private bool m_Beeping = false;
        private bool m_Close = false;
        private bool m_FirstTime = true;
        private int m_DigitalColor = 0;
        private IconGen m_IconGen = new IconGen();

        private bool m_Moveing = false;
        private Point m_LastPosition = Point.Empty;

        private int m_StylesChanged = 5;
        private bool m_DoNotLayout = false;

        #endregion

        #region Properties

        public ClenseProfile ClenseProfile
        {
            get { return m_Config.ClenseProfile; }
            set 
            {
                m_Config.ClenseProfile = value;

                this.minimalToolStripMenuItem.Checked = value == ClenseProfile.Minimal;
                this.aggressiveToolStripMenuItem.Checked = value == ClenseProfile.Aggressive;
                this.standardToolStripMenuItem.Checked = value == ClenseProfile.Standard;
            }
        }

        public bool AlwaysOnTop
        {
            get
            {
                return m_Config.AlwaysOnTop;
            }

            set
            {
                this.TopMost = value;
                m_Config.AlwaysOnTop = value;
                alwaysOnTopToolStripMenuItem.Checked = value;
            }
        }

        public bool WindowShrunk
        {
            get
            {
                return m_Config.WindowShrunk;
            }

            set
            {
                if (!m_DoNotLayout)
                    this.SuspendLayout(); 

                m_Config.WindowShrunk = value;
                m_ShrinkButton.Checked = !value;

                SetWindowShrink(value);

                if (!m_DoNotLayout)
                    this.ResumeLayout(); 
            }
        }

        private void SetWindowShrink(bool value)
        {
            if (!m_DoNotLayout)
                this.SuspendLayout();
           
            if (!value)
                ResourceManager.PreLoad(); 

            this.windowHeader1.ShowClock = value;

            this.menuStrip1.Visible = !value;
            digitalClockCtrl1.Visible = !value;
            m_NextAlarmPanel.Visible = !value;
            m_ShowPanelButton.Visible = !value;

            if (value)
            {
                alarmsPanel1.Visible = false;
                panel2.Visible = false;
            }
            else
            {
                alarmsPanel1.Visible = AlarmPanelExpanded;
                panel2.Visible = AlarmPanelExpanded;
            }

            m_StylesChanged += 10;

            //PreRender();

            if (!m_DoNotLayout)
                this.ResumeLayout(true); 

        }

        public bool AlarmPanelExpanded
        {
            get
            {
                return m_Config.PanelExpanded;
            }

            set
            {
                if (!m_DoNotLayout)
                    this.SuspendLayout();

                if (value)
                    ResourceManager.PreLoad(); 

                m_Config.PanelExpanded = value;
                panel2.Visible = value;
                alarmsPanel1.Visible = value;
                alarmPanelToolStripMenuItem.Checked = value;

                m_ShowPanelButton.Checked = value;

                m_StylesChanged += 10;

                //PreRender(); 

                if (!m_DoNotLayout)
                    this.ResumeLayout(true); 
            }
        }

        public bool Armed
        {
            get
            {
                return m_Config.Armed;
            }

            set
            {
                m_Config.Armed = value;

                this.armedToolStripMenuItem.Checked = value;
                this.m_ShowPanelButton.Checked = value;

                if (!m_Config.Armed)
                {
                    if (m_Loaded)
                        StopFile();

                    if (timer1.Enabled)
                        timer1.Enabled = false;
                }
                else
                {
                    if (!timer1.Enabled)
                        timer1.Enabled = true;
                }
            }
        }

        public Color HeaderBackColor 
        { 
            set
            {
                //this.m_TopBackFill.BackColor = value;
                this.menuStrip1.BackColor = value;
                this.windowHeader1.BackColor = value;
                contextMenuStrip1.BackColor = value;

                SkinHelper.ApplyColorToMenuItems(menuStrip1, true, false, MenuSkinColors.BackColor, value);
                SkinHelper.ApplyColorToMenuItems(contextMenuStrip1, true, false, MenuSkinColors.BackColor, value); 

            }
        }

        public Color HeaderForeColor
        {
            set 
            {
                //this.m_TopBackFill.ForeColor = value;
                this.menuStrip1.ForeColor = value;
                this.windowHeader1.ForeColor = value;
                contextMenuStrip1.ForeColor = value;

                SkinHelper.ApplyColorToMenuItems(menuStrip1, true, false, MenuSkinColors.ForeColor, value);
                SkinHelper.ApplyColorToMenuItems(contextMenuStrip1, true, false, MenuSkinColors.ForeColor, value); 
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {

                Color val = value;

                if (val == Color.Transparent)
                    val = SystemColors.ControlDark;

                base.BackColor = val;

                //this.panel2.BackColor = value;
                this.KillButton.BackColor = value;
                //this.imageButton1.BackColor = value;    
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                Color val = value;

                if (val == Color.Transparent)
                    val = SystemColors.ControlText;

                base.ForeColor = val;

                //this.panel2.ForeColor = value;
                this.KillButton.ForeColor = val;
                //this.imageButton1.ForeColor = value;       
            }
        }

        public new Color TransparencyKey
        {
            get
            {
                return base.TransparencyKey;
            }
            set
            {
                if (m_SkinEditorActive)
                    base.TransparencyKey = Color.Empty;
                else 
                    base.TransparencyKey = value;
            }
        }
        

        public Color MenuBackColor
        {
            set
            {
                SkinHelper.ApplyColorToMenuItems(menuStrip1, false, true, MenuSkinColors.BackColor, value);
                SkinHelper.ApplyColorToMenuItems(contextMenuStrip1, false, true, MenuSkinColors.BackColor, value);   
            }
        }

        public Color MenuForeColor
        {
            set
            {
                SkinHelper.ApplyColorToMenuItems(menuStrip1, false, true, MenuSkinColors.ForeColor, value);
                SkinHelper.ApplyColorToMenuItems(contextMenuStrip1, false, true, MenuSkinColors.ForeColor, value); 
            }
        }

        public Color AlarmForeColor
        {
            set
            {
                digitalClockCtrl1.RegularColours.DigitColour = value;
                alarmsPanel1.RegularColours.DigitColour = value;
                windowHeader1.RegularColours.DigitColour = value;

                digitalClockCtrl1.RegularColours = digitalClockCtrl1.RegularColours;
                alarmsPanel1.RegularColours = alarmsPanel1.RegularColours;
                windowHeader1.RegularColours = windowHeader1.RegularColours; 
            }
        }

        public Color AlarmBackColor
        {            
            set
            {
                digitalClockCtrl1.RegularColours.BackColour = value;
                alarmsPanel1.RegularColours.BackColour = value;
                windowHeader1.RegularColours.BackColour = value;

                digitalClockCtrl1.RegularColours = digitalClockCtrl1.RegularColours;
                alarmsPanel1.RegularColours = alarmsPanel1.RegularColours;
                windowHeader1.RegularColours = windowHeader1.RegularColours; 
            }
        }


        public Color AlarmDisabledColor
        {            
            set
            {
                digitalClockCtrl1.RegularColours.DimDigitColour = value;
                alarmsPanel1.RegularColours.DimDigitColour = value;
                windowHeader1.RegularColours.DimDigitColour = value;

                digitalClockCtrl1.RegularColours = digitalClockCtrl1.RegularColours;
                alarmsPanel1.RegularColours = alarmsPanel1.RegularColours;
                windowHeader1.RegularColours = windowHeader1.RegularColours; 
            }
        }


        public Color AlarmAltForeColor
        {            
            set
            {
                digitalClockCtrl1.FlashingColour.DigitColour = value;
                alarmsPanel1.FlashingColour.DigitColour = value;
                windowHeader1.FlashingColour.DigitColour = value;

                digitalClockCtrl1.FlashingColour = digitalClockCtrl1.FlashingColour;
                alarmsPanel1.FlashingColour = alarmsPanel1.FlashingColour;
                windowHeader1.FlashingColour = windowHeader1.FlashingColour; 
            }
        }


        public Color AlarmAltBackColor
        {            
            set
            {
                digitalClockCtrl1.FlashingColour.BackColour = value;
                alarmsPanel1.FlashingColour.BackColour = value;
                windowHeader1.FlashingColour.BackColour = value;

                digitalClockCtrl1.FlashingColour = digitalClockCtrl1.FlashingColour;
                alarmsPanel1.FlashingColour = alarmsPanel1.FlashingColour;
                windowHeader1.FlashingColour = windowHeader1.FlashingColour; 
            }
        }

        
        public Color AlarmAltDisabledColor
        {            
            set
            {
                digitalClockCtrl1.FlashingColour.DimDigitColour = value;
                alarmsPanel1.FlashingColour.DimDigitColour = value;
                windowHeader1.FlashingColour.DimDigitColour = value;

                digitalClockCtrl1.FlashingColour = digitalClockCtrl1.FlashingColour;
                alarmsPanel1.FlashingColour = alarmsPanel1.FlashingColour;
                windowHeader1.FlashingColour = windowHeader1.FlashingColour; 
            }
        }

        /*
        public new Size Size
        {
            get { return base.Size; }
            set 
            {
                this.MinimumSize = new Size(value.Width, 0);
                this.MaximumSize = new Size(value.Width, 0); 

                base.Size = value; 
            }
        }
         * */ 

        #endregion

        #region Constructors

        public MainForm()
        {
            // only needed so the editor picks up the correct embeded folder
            //IconGen.AddPath();            
            this.Visible = false;
            InitializeComponent();
                                    
            this.minimalToolStripMenuItem.Tag = ClenseProfile.Minimal;
            this.aggressiveToolStripMenuItem.Tag = ClenseProfile.Aggressive;
            this.standardToolStripMenuItem.Tag = ClenseProfile.Standard;


            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);

            this.DoubleBuffered = true;
            this.SuspendLayout(); 

            AddControlsToSkin();

            FactorySetup();

            SetTimes();

            this.LoadSettings(this, EventArgs.Empty);

            this.ResumeLayout(false);


            this.PerformLayout();

            int maxWidth = 0;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Right > maxWidth)
                    maxWidth = ctrl.Right;
            }

            this.windowHeader1.MinimumSize = new Size(maxWidth, 0);
            this.MinimumSize = new Size(maxWidth, 0); 


            ResourceManager.Loading += new ResourceManager.EnsureSkinElementsEvent(ResourceManager_Loading);
            ResourceManager.Cleared += new ResourceManager.ClearedEvent(ResourceManager_Cleared);
            ResourceManager.Clensing += new ResourceManager.MarkInUseEvent(ResourceManager_Clensing);

            m_Initiated = true; 
            timer1.Enabled = true;
            //NotifyIcon.Visible = false;

            ResourceManager.PreLoad();

            //PreRender(); 
            this.Visible = true;
        }
       
        private void AddControlsToSkin()
        {
            SkinningMaster.RegisterSkinable(this);

            MenuSkinHelper menuSkinHelper = new MenuSkinHelper(this);
            SkinningMaster.RegisterSkinable(menuSkinHelper);
            
            //ClockColorsSkinHelper clockColorsSkinHelper = new ClockColorsSkinHelper(this);
            //SkinningMaster.RegisterSkinable(clockColorsSkinHelper); 

 
            
            SkinningMaster.RegisterSkinable(toolStripSeparator1);
            SkinningMaster.RegisterSkinable(toolStripSeparator2);
            SkinningMaster.RegisterSkinable(imageButton1);
            SkinningMaster.RegisterSkinable(panel2);

            SkinningMaster.RegisterSkinable(digitalClockCtrl1);
            SkinningMaster.RegisterSkinable(m_ShowPanelButton);

            SkinningMaster.RegisterSkinable(m_ToTaskButton);
            SkinningMaster.RegisterSkinable(m_ShrinkButton);
            SkinningMaster.RegisterSkinable(m_CloseButton);

            alarmsPanel1.AddControlsToSkin();
            windowHeader1.AddControlsToSkin(); 
        }

        public void FactorySetup()
        {
            if (m_Initiated) 
                this.SuspendLayout();

            SkinningMaster.ResetAllMappings(this); 

            m_Config.Armed = true;

            m_Config.ConfirmDelete = true;

            m_Config.AudioFilePath = null;
            m_Config.Location = Point.Empty;
            m_Config.PanelExpanded = true;
            m_Config.WindowShrunk = false;
            m_Config.ImageOverridePath = null;
            SkinningMaster.SetOverrideSource(m_Config.ImageOverridePath); 

            m_Config.Times = new List<Time>();

            m_Config.ColourDialogLocation = Point.Empty;
            m_Config.ColourDialogMode = ColorPickerDialog.ColorPickerDialogMode.Mix;

            m_Config.SkinEditorDialogLocation = Point.Empty;
            m_Config.SkinEditorDialogSize = Point.Empty;

            SkinningMaster.SetProperty("Window", "Fore Colour", SystemColors.ControlLightLight);
            SkinningMaster.SetProperty("Window", "Back Colour", SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Window", "Header Fore Colour", SystemColors.MenuText);
            SkinningMaster.SetProperty("Window", "Header Back Colour", SystemColors.MenuBar);
            SkinningMaster.SetProperty("Window", "Transparent Key", Color.FromArgb(255, 255, 0, 255)); // Color.Empty);

            SkinningMaster.SetProperty("Menu", "Fore Colour", SystemColors.MenuText);
            SkinningMaster.SetProperty("Menu", "Back Colour", SystemColors.Menu);
            SkinningMaster.SetProperty("Menu", "Separator Colour", SystemColors.MenuText);

            SkinningMaster.SetProperty("Clock Display", "Fore Colour", SystemColors.GradientActiveCaption);
            SkinningMaster.SetProperty("Clock Display", "Back Colour", SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Clock Display", "Disabled Colour", SystemColors.ControlDark);
            SkinningMaster.SetProperty("Clock Display", "Alt. Fore Colour", SystemColors.ControlLightLight);
            SkinningMaster.SetProperty("Clock Display", "Alt. Back Colour",  SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Clock Display", "Alt. Disabled Colour", SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Clock Display", "Highlight Colour", SystemColors.GradientActiveCaption);

            SkinningMaster.SetProperty("Control Buttons", "Fore Colour", SystemColors.MenuBar);
            SkinningMaster.SetProperty("Control Buttons", "Back Colour", SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Control Buttons", "Highlight Colour", SystemColors.ControlLightLight);
            SkinningMaster.SetProperty("Control Buttons", "Disabled Colour", SystemColors.ControlDark);

            SkinningMaster.SetProperty("Panel Buttons", "Fore Colour", SystemColors.MenuBar);
            SkinningMaster.SetProperty("Panel Buttons", "Back Colour", SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Panel Buttons", "Highlight Colour", SystemColors.ControlLightLight);
            SkinningMaster.SetProperty("Panel Buttons", "Disabled Colour", SystemColors.ControlDark);

            SkinningMaster.SetProperty("Panel", "Fore Colour", SystemColors.ControlText);
            SkinningMaster.SetProperty("Panel", "Back Colour", SystemColors.Control);
            SkinningMaster.SetProperty("Panel", "Border Colour", SystemColors.WindowFrame);

            SkinningMaster.SetProperty("Alarms List", "Fore Colour", SystemColors.ControlLightLight);
            SkinningMaster.SetProperty("Alarms List", "Back Colour", SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Alarms List", "Border Colour", SystemColors.WindowFrame);
            SkinningMaster.SetProperty("Alarms List", "Scroller Highlight Colour", SystemColors.ControlLightLight);
            SkinningMaster.SetProperty("Alarms List", "Scroller Fore Colour", SystemColors.WindowFrame);
            SkinningMaster.SetProperty("Alarms List", "Scroller Face Colour", SystemColors.ScrollBar);
            SkinningMaster.SetProperty("Alarms List", "Selected Check Colour", SystemColors.Highlight);
            SkinningMaster.SetProperty("Alarms List", "Selected Colour", SystemColors.Highlight);
            SkinningMaster.SetProperty("Alarms List", "Selected Text Colour", SystemColors.HighlightText);
            SkinningMaster.SetProperty("Alarms List", "Check Fore Colour", SystemColors.ControlDarkDark);
            SkinningMaster.SetProperty("Alarms List", "Check Back Colour", SystemColors.HighlightText);

            if (m_Initiated) 
                this.ResumeLayout(); 

            //SkinningMaster.ResetAllMappings(this);

            /*m_Config.Regular.DigitColour = Color.FromArgb(0, 255, 0);
            m_Config.Regular.DimDigitColour = Color.FromArgb(0, 60, 0);
            m_Config.Regular.BackColour = Color.FromArgb(0, 20, 0);

            m_Config.Flashing.DigitColour = Color.White;
            m_Config.Flashing.DimDigitColour = Color.FromArgb(60, 60, 60);
            m_Config.Flashing.BackColour = Color.Black;*/
            /*
                       m_Config.PanelBackColour = Color.FromArgb(255, Color.FromArgb(0xcccccc));
                       m_Config.PanelTextColour = Color.FromArgb(255, Color.FromArgb(0x000000));
                       m_Config.PanelBorderColour = Color.Black;

                       m_Config.AlarmsBackColour = Color.FromArgb(255, Color.FromArgb(0x333333));
                       m_Config.AlarmsTextColour = Color.FromArgb(255, Color.FromArgb(0xa0a0a0));
                       m_Config.AlarmsSelectedColour = Color.FromArgb(255, Color.FromArgb(0x000000));
                       m_Config.AlarmsSelectedTextColour = Color.FromArgb(255, Color.FromArgb(0xFFFFFF));

                       m_Config.WindowTop = Color.FromArgb(255, Color.FromArgb(0xcccccc));
                       m_Config.WindowTopText = Color.FromArgb(255, Color.FromArgb(0x000000));

                       m_Config.MinmizeHover = Color.FromArgb(255, Color.FromArgb(0xffffff));
                       m_Config.MinmizeFore = Color.FromArgb(255, Color.FromArgb(0x000000));
                       m_Config.MinmizeBack = Color.FromArgb(255, Color.FromArgb(0xcccccc));

                       m_Config.MenuText = Color.FromArgb(255, Color.FromArgb(0x000000));
                       m_Config.MenuBack = Color.FromArgb(255, Color.FromArgb(0xcccccc));
                       m_Config.MenuSeparator = Color.FromArgb(255, Color.FromArgb(0x000000));
             */
            //SkinningMaster.SetAllToTransparent(); 
        }
        #endregion

        #region Arming Alarm

        private void ArmedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Armed = !this.armedToolStripMenuItem.Checked;
        }

        #endregion

        #region Alarm Ring

        private void OnClockTick(object sender, EventArgs e)
        {
            if (this.Visible || m_SkinEditorActive)
            {
                if (digitalClockCtrl1.Visible)
                    digitalClockCtrl1.Invalidate();
                else
                    windowHeader1.ClockTick();
            }

            if (m_SkinEditorActive)
                if (UpdateEditor != null)
                    UpdateEditor(this, EventArgs.Empty); 

            if (!m_Config.Armed)
                return;

            SetTimes();
            CheckAlarm();

            if (RingAlarm == true)
            {
                Alarm();
            }
            else if (m_Beeping)
            {
                Console.Beep();
            }

            if (m_StylesChanged < 0)
            {
                m_StylesChanged = -1;
            }
            else if (m_StylesChanged == 0)
            {
                if (ClenseProfile == ClenseProfile.Aggressive)
                    ResourceManager.Clense(ResourceManager.ClenseMode.Full);
                else if (ClenseProfile == ClenseProfile.Standard)
                {
                    if (!this.AlarmPanelExpanded)
                        ResourceManager.Clense(ResourceManager.ClenseMode.Panels);
                    else
                        ResourceManager.Clense(ResourceManager.ClenseMode.Internal);
                }
                //else
                    //ResourceManager.Clense(ResourceManager.ClenseMode.Minimal);

                try
                {
                    Process.GetCurrentProcess().MaxWorkingSet = Process.GetCurrentProcess().MaxWorkingSet;
                    Process.GetCurrentProcess().MinWorkingSet = Process.GetCurrentProcess().MinWorkingSet; // 10000;
                }
                catch
                {
                }

                totalToolStripMenuItem.Text = "Total (" + ResourceManager.TotalImages + ")";

                m_StylesChanged = -1;
            }
            else
                m_StylesChanged--;
        }

        private void SetTimes()
        {
            LastTime.Hour = NewTime.Hour;
            LastTime.Minute = NewTime.Minute;
            LastTime.Seconds = NewTime.Seconds;

            DateTime now = DateTime.Now;

            NewTime.Hour = now.Hour;
            NewTime.Minute = now.Minute;
            NewTime.Seconds = now.Second;
        }

        private void CheckAlarm()
        {
            Time.DaysOfTheWeek realDay = Time.DaysOfTheWeek.Mon;
            Time.DaysOfTheWeek nextDay = Time.DaysOfTheWeek.Tue;

            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    realDay = Time.DaysOfTheWeek.Fri;
                    nextDay = Time.DaysOfTheWeek.Sat;
                    break;
                case DayOfWeek.Monday:
                    realDay = Time.DaysOfTheWeek.Mon;
                    nextDay = Time.DaysOfTheWeek.Tue;
                    break;
                case DayOfWeek.Saturday:
                    realDay = Time.DaysOfTheWeek.Sat;
                    nextDay = Time.DaysOfTheWeek.Sun;
                    break;
                case DayOfWeek.Sunday:
                    realDay = Time.DaysOfTheWeek.Sun;
                    nextDay = Time.DaysOfTheWeek.Mon;
                    break;
                case DayOfWeek.Thursday:
                    realDay = Time.DaysOfTheWeek.Thur;
                    nextDay = Time.DaysOfTheWeek.Fri;
                    break;
                case DayOfWeek.Tuesday:
                    realDay = Time.DaysOfTheWeek.Tue;
                    nextDay = Time.DaysOfTheWeek.Wed;
                    break;
                case DayOfWeek.Wednesday:
                    realDay = Time.DaysOfTheWeek.Wed;
                    nextDay = Time.DaysOfTheWeek.Thur;
                    break;
                default:
                    break;
            }

            string nextAlarm = "None";

            Time next = null;

            foreach (CheckboxList<Time>.CheckboxItem item in alarmsPanel1.Items)
            {
                Time time = (Time)item.Object;

                if (!time.Enabled)
                    continue;

                if (IsSetToDay(time.Days, realDay))
                {
                    if (time.Hour >= LastTime.Hour &&
                        time.Hour <= NewTime.Hour &&
                        time.Minute >= LastTime.Minute &&
                        time.Minute <= NewTime.Minute &&
                        time.c_Seconds >= LastTime.Seconds &&
                        time.c_Seconds <= NewTime.Seconds)
                    {
                        RingAlarm = true;
                    }
                    else if (next == null &&
                        ((time.Hour == NewTime.Hour &&
                        time.Minute >= NewTime.Minute) ||
                        (time.Hour > NewTime.Hour)))
                    {
                        next = time;
                    }
                }
            }

            if (next == null)
            {
                foreach (CheckboxList<Time>.CheckboxItem item in alarmsPanel1.Items)
                {
                    Time time = (Time)item.Object;

                    if (!time.Enabled)
                        continue;

                    if (IsSetToDay(time.Days, nextDay))
                    {
                        next = time;
                        break;
                    }
                }
            }

            if (next != null)
            {
                nextAlarm = next.ToString();
            }

            this.m_NextAlarmLabel.Text = "Next: " + nextAlarm;

            if (this.WindowShrunk)
            {
                int index = nextAlarm.IndexOf('/');

                if (index >= 0)
                    windowHeader1.NextAlarmText = "Next: " + nextAlarm.Substring(0, index);
                if (index < 0)
                    windowHeader1.NextAlarmText = "Next: " + nextAlarm;
            }
        }

        private bool IsSetToDay(Time.DaysOfTheWeek days, Time.DaysOfTheWeek current)
        {
            return (days & current) == current;
        }

        private void Alarm()
        {
            if (m_Loaded)
                PlayFile();
            else
            {
                m_Beeping = true;
                Console.Beep();
            }

            RingAlarm = false;
            KillButton.Location = alarmsPanel1.Location;
            //KillButton.Location = panel2.Location;
            KillButton.Size = new Size(alarmsPanel1.Size.Width - (KillButton.FlatAppearance.BorderSize * 2), panel2.Size.Height + alarmsPanel1.Size.Height - (KillButton.FlatAppearance.BorderSize * 2));

            digitalClockCtrl1.CurrentColourType = ColourType.Alternate;

            KillButton.BringToFront();
            KillButton.Visible = true;
            colorTimer.Enabled = true;
            timer1.Interval = 500;

            // If it is and the program is minimized, restore it.
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                //this.ShowInTaskbar = true;
            }

            // Make our tray icon show a little balloon message, just for fun.
            NotifyIcon.Visible = true;
            NotifyIcon.BalloonTipText = "WAKE UP!";
            NotifyIcon.ShowBalloonTip(5000);

            SetWindowShrink(false);

            // Set focus to our program so the user doesn't have to mess with
            // the mouse.
            this.TopMost = true;
            this.Focus();

            KillButton.Focus();

            this.BringToFront();
        }

        private void KillButton_Click(object sender, EventArgs e)
        {
            if (m_Loaded)
                StopFile();
            else
                m_Beeping = false; 

            colorTimer.Enabled = false;
            m_DigitalColor = 0;
            digitalClockCtrl1.CurrentColourType = ColourType.Regular;

            timer1.Interval = 1000;
            digitalClockCtrl1.Invalidate();

            KillButton.Visible = false;
            NotifyIcon.Visible = false;
            this.TopMost = m_Config.AlwaysOnTop;

            WindowShrunk = WindowShrunk;
        }

        /// <summary>
        /// flash the display 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeColor(object sender, EventArgs e)
        {
            if (m_DigitalColor == 0)
            {
                digitalClockCtrl1.CurrentColourType = ColourType.Regular;
                //digitalClockCtrl1.Invalidate();
                m_DigitalColor = 1;
            }
            else if (m_DigitalColor == 1)
            {
                digitalClockCtrl1.CurrentColourType = ColourType.Alternate;
                //digitalClockCtrl1.Invalidate();
                m_DigitalColor = 0;
            }
        }

        #endregion

        #region LoadAndPlayMp3

        private void LoadFilebutton_Click(object sender, EventArgs e)
        {

            // Set up our file open dialog box
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.DefaultExt = "mp3";
            dialog.Multiselect = false;
            dialog.CheckPathExists = true;
            dialog.CheckFileExists = true;
            dialog.AddExtension = true;
            dialog.ValidateNames = true;
            dialog.Title = "Select Audio File";

            dialog.Filter = "Audio Files|*.mp3;*.wav|MP3 Files (*.mp3)|*.mp3|Wav Files (*.wav)|*.wav|All Files (*.*)|*.*";

            // Show the user the dialog.  If the user clicks on "Cancel"
            // we'll do nothing and keep the previous file selection, if any.
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SetSoundFile(dialog.FileName);
            }

            dialog.Dispose();
        }

        private void SetSoundFile(string file)
        {
            m_Config.AudioFilePath = file;
            label2.Text = m_Config.AudioFilePath;

            LoadFile(m_Config.AudioFilePath);
        }

        private void LoadFile(string file)
        {
            if (m_Loaded)
                UnLoadFile();

            if (Helper.IsNotNullOrEmpty(file))
            {
                m_Player.Open(file);
                m_Loaded = true;
            }
        }

        private void UnLoadFile()
        {
            if (m_Loaded)
            {
                m_Player.Close();
                m_Loaded = false;
            }
        }

        private void PlayFile()
        {
            m_Player.Play(true);
        }

        private void StopFile()
        {
            m_Player.Stops();
        }

        #endregion

        #region XmlLoadAndSave

        private void SaveSettings(object sender, FormClosedEventArgs arg)
        {
            m_Config.Times = new List<Time>();

            foreach (CheckboxList<Time>.CheckboxItem item in this.alarmsPanel1.Items)
            {
                Time time = (Time)item.Object;
                m_Config.Times.Add(time);
            }

            Point location = m_Config.Location;

            if (m_ColorDialog != null)
            {
                m_Config.ColourDialogLocation = m_ColorDialog.Location;
                m_Config.ColourDialogMode = m_ColorDialog.PickerMode;
            }

            if (this.WindowState == FormWindowState.Normal)
                m_Config.Location = this.Location;
            else
                m_Config.Location = m_LastPosition;

            m_Config.Save();

            UnLoadFile();
        }

        private void LoadSettings(object sender, EventArgs e)
        {
            alarmsPanel1.Items.Clear();
            if (this.WindowState == FormWindowState.Normal)
                m_Config.Location = this.Location;

            m_Config.State = this.WindowState;

            m_Config.Load();

            m_DoNotLayout = true; 

            AlarmPanelExpanded = m_Config.PanelExpanded;
            WindowShrunk = m_Config.WindowShrunk;
            AlwaysOnTop = m_Config.AlwaysOnTop;
            ClenseProfile = m_Config.ClenseProfile; 

            SkinningMaster.SetOverrideSource(m_Config.ImageOverridePath); 

            m_DoNotLayout = false;

            if (m_Config.AudioFilePath != null)
                SetSoundFile(m_Config.AudioFilePath);

            this.armedToolStripMenuItem.Checked = m_Config.Armed;

            this.StartPosition = FormStartPosition.Manual; 

            this.Location = m_Config.Location;
            m_LastPosition = m_Config.Location;

            foreach (Time t in m_Config.Times)
            {
                alarmsPanel1.Items.Add(new CheckboxList<Time>.CheckboxItem(t, t.Enabled));

            }

            alarmsPanel1.Init();

            SkinningMaster.ApplyMappings(this);

            if (m_Config.State == FormWindowState.Minimized)
            {
                MinimiseApp(); 
                //Close();
                //MinimiseApp(); 
            }
            else
            {
                m_FirstTime = false;
            }
        }

        #endregion

        #region Form Apperance
        private void PreRender()
        {
            using (Bitmap bmp = new Bitmap(this.Size.Width, this.Size.Height, PixelFormat.Format32bppArgb))
            {
                DrawToBitmap(bmp);
            }
        }

        private void RestoreApp()
        {

            this.SuspendLayout();
            //this.Visible = false;              
            //this.Show();
            this.WindowState = FormWindowState.Normal;

            ResourceManager.PreLoad();

            this.ShowInTaskbar = true;
            NotifyIcon.Visible = false;
            
            this.Show();
            this.Location = m_Config.Location;
            m_Config.State = FormWindowState.Normal;
            this.WindowState = FormWindowState.Normal;
            this.ResumeLayout(true);
            //this.Visible = true;
            //Invalidate(); 
        }

        private void MinimiseApp()
        {
            m_Config.Location = this.Location;

            m_Config.State = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            this.Hide();

            NotifyIcon.Visible = true;

            NotifyIcon.BalloonTipTitle = "Alarm Clock";

            if (m_FirstTime == false)
            {
                NotifyIcon.BalloonTipText = "Is still running in the background.";
                NotifyIcon.ShowBalloonTip(100);
            }
            else
            {
                NotifyIcon.BalloonTipText = "Is running in the background.";
                NotifyIcon.ShowBalloonTip(500);
                m_FirstTime = false;
            }
        }

        #region Internal Events

        private void TrayResizeForm_Resize(object sender, MouseEventArgs e)
        {
            RestoreApp();
        }

        private void Exit_MenuClick(object sender, EventArgs e)
        {
            m_Close = true;
            Close();
        }

        private void Open_MenuClick(object sender, EventArgs e)
        {
            RestoreApp();
        }

        private void Close_FormClick(object sender, FormClosingEventArgs e)
        {
            if (m_Close == false &&
                (e.CloseReason == CloseReason.UserClosing ||
                e.CloseReason == CloseReason.None))
            {
                e.Cancel = true;
                MinimiseApp();
            }

        }

        private void Expanded_MenuClick(object sender, EventArgs e)
        {
            AlarmPanelExpanded = !AlarmPanelExpanded;
            alarmsPanel1.DefoucsClock();
        }

        #endregion

        #endregion

        #region Dialog Options

        private void confirmDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.confirmDeleteToolStripMenuItem.Checked = !this.confirmDeleteToolStripMenuItem.Checked;
            m_Config.ConfirmDelete = this.confirmDeleteToolStripMenuItem.Checked;
        }

        #endregion

        #region Colour Events

        private void factorySetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reset to factory defaults?", "Reset Config", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                FactorySetup();

                //digitalClockCtrl1.RegularColours = m_Config.Regular;
                //digitalClockCtrl1.FlashingColour = m_Config.Flashing;
                //digitalClockCtrl1.BackColor = m_Config.Regular.BackColour;

                if (m_ColorDialog != null)
                    m_ColorDialog.PickerMode = m_Config.ColourDialogMode;

                //alarmsPanel1.UpdateColours();

                m_StylesChanged += 1;
            }
        }

        #endregion       

        private void imageButton2_Click(object sender, EventArgs e)
        {

        }

        private void OnClockMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_LastPosition = e.Location;
                m_Moveing = true;
            }
        }

        private void OnClockMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_Moveing = false;
            }
        }

        private void OnClockMouseMove(object sender, MouseEventArgs e)
        {
            if (m_Moveing)
            {
                if (m_LastPosition != Point.Empty)
                {
                    int x, y;

                    x = this.Location.X - (m_LastPosition.X - e.Location.X);
                    y = this.Location.Y - (m_LastPosition.Y - e.Location.Y);

                    Rectangle rect = Screen.GetWorkingArea(this);

                    if (x < rect.X + 10 && x > rect.X - 10)
                        x = rect.X;

                    if (y < rect.Y + 10 && y > rect.Y - 10)
                        y = rect.Y;

                    if (x + Width < rect.X + rect.Width + 10 && x + Width > rect.X + rect.Width - 10)
                        x = rect.X + rect.Width - Width;

                    if (y + Height < rect.Y + rect.Height + 10 && y + Height > rect.Y + rect.Height - 10)
                        y = rect.Y + rect.Height - Height;

                    this.Location = new Point(x, y);

                    m_LastPosition = new Point(e.Location.X + (m_LastPosition.X - e.Location.X),
                                               e.Location.Y + (m_LastPosition.Y - e.Location.Y));
                }
                else
                    m_LastPosition = e.Location;
            }
        }

        private void OnGotoTask(object sender, EventArgs e)
        {
            Close();
            m_ShrinkButton.ResetHover();
            m_ToTaskButton.ResetHover();
        }

        private void OnShrink(object sender, EventArgs e)
        {
            WindowShrunk = !WindowShrunk;
        }

        private void OnCloseClicked(object sender, EventArgs e)
        {
            this.m_Close = true;
            Close();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();

            about.ShowDialog();
        }

        private void clenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResourceManager.Clense(ResourceManager.ClenseMode.Internal);
            totalToolStripMenuItem.Text = "Total (" + ResourceManager.TotalImages + ")";
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResourceManager.Clear();
            GetAllMenuIcons();
            totalToolStripMenuItem.Text = "Total (" + ResourceManager.TotalImages + ")";
        }

        private void GetAllMenuIcons()
        {
            
        }

        void ResourceManager_Clensing(ResourceManager.ClenseMode mode, ResourceMarkers markers)
        {
            // if its not a full clense then keep the reference
            if (mode != ResourceManager.ClenseMode.Full)
            {               
            }
            else
                ResourceManager_Cleared();

        }

        void ResourceManager_Loading()
        {

        }

        void ResourceManager_Cleared()
        {
            
        }

        private void totalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            totalToolStripMenuItem.Text = "Total (" + ResourceManager.TotalImages + ")";
        }

        private void OnDoubleClick(object sender, MouseEventArgs e)
        {
            WindowShrunk = !WindowShrunk;
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlwaysOnTop = !AlwaysOnTop;
        }

        private void loadColoursFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.DefaultExt = "xml";
            dialog.Multiselect = false;
            dialog.CheckPathExists = true;
            dialog.CheckFileExists = true;
            dialog.AddExtension = true;
            dialog.ValidateNames = true;
            dialog.Title = "Select Colours File";

            dialog.Filter = "Xml Files|*.xml|All Files (*.*)|*.*";

            // Show the user the dialog.  If the user clicks on "Cancel"
            // we'll do nothing and keep the previous file selection, if any.
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                m_Config.LoadColoursFromFile(dialog.FileName);

                if (m_ColorDialog != null)
                    m_ColorDialog.PickerMode = m_Config.ColourDialogMode;


                alarmsPanel1.UpdateColours();

                m_StylesChanged += 1;
            }

            dialog.Dispose();
        }

        private void saveColoursToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "xml";            
            dialog.CheckPathExists = true;
            dialog.CheckFileExists = false;
            dialog.AddExtension = true;
            dialog.ValidateNames = true;
            dialog.Title = "Save Colours to file";

            dialog.Filter = "Xml Files|*.xml|All Files (*.*)|*.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                m_Config.SaveColoursToFile(dialog.FileName);
            }

            dialog.Dispose();
        }

        private void skinEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkinEditorDialog dialog = null;

            try
            {                
                this.Visible = false;
                this.windowHeader1.MinimumSize = new Size(0, 0);
                this.MinimumSize = new Size(0, 0);

                dialog = new SkinEditorDialog(this);

                if (m_Config.SkinEditorDialogLocation != Point.Empty)
                {
                    dialog.Location = m_Config.SkinEditorDialogLocation;
                    dialog.StartPosition = FormStartPosition.Manual;
                }
                else
                    dialog.StartPosition = FormStartPosition.CenterParent;

                if (m_Config.ColourDialogLocation != Point.Empty)
                {
                    dialog.ColourDialogLocation = m_Config.ColourDialogLocation;
                }
                
                dialog.ColourDialogMode = m_Config.ColourDialogMode;
                dialog.SetInitialOverrideFilePath(m_Config.ImageOverridePath);

                if (m_Config.SkinEditorDialogSize != Point.Empty)
                    dialog.Size = new Size(m_Config.SkinEditorDialogSize.X, m_Config.SkinEditorDialogSize.Y);

                dialog.FormClosed += new FormClosedEventHandler(dialog_FormClosed);
                
                dialog.Show();
            }
            finally
            {
                //if (dialog != null) 
                //    dialog.Dispose();

                //this.Visible = true;
            }
        }

        void dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            SkinEditorDialog dialog = sender as SkinEditorDialog;

            m_Config.SkinEditorDialogLocation = dialog.Location;
            m_Config.SkinEditorDialogSize = new Point(dialog.Size.Width, dialog.Size.Height);
            m_Config.ImageOverridePath = dialog.OverrideFilePath;

            m_Config.ColourDialogLocation = dialog.ColourDialogLocation;
            m_Config.ColourDialogMode = dialog.ColourDialogMode; 

            if (dialog != null)
                dialog.Dispose();

            int maxWidth = 0;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Right > maxWidth)
                    maxWidth = ctrl.Right;
            }

            this.windowHeader1.MinimumSize = new Size(maxWidth, 0);
            this.MinimumSize = new Size(maxWidth, 0);

            this.Visible = true;
        }

        #region ISkinable Members

        public string GroupName
        {
            get
            {
                return "Window";
            }
            set
            {                
            }
        }

        public IEnumerable<string> GetSkinProperties()
        {
            return new string[] { "Fore Colour", "Back Colour", "Header Fore Colour", "Header Back Colour", "Transparent Key" };
        }

        public void OnSkinPropertyChanged(SkinGroup group, string name, Color value)
        {
            switch (name)
            {
                case "Fore Colour":
                    ForeColor = value;
                    break;
                case "Back Colour":
                    BackColor = value;
                    break;
                case "Header Fore Colour":
                    HeaderForeColor = value;
                    break;
                case "Header Back Colour":
                    HeaderBackColor = value;
                    break;
                case "Transparent Key":
                    TransparencyKey = value;
                    break;
                default:
                    break;
            }
        }

        #endregion

        private class MenuSkinHelper : ISkinnable
        {
            private MainForm m_Owner;
            private bool m_Registered = false;

            public bool Registered
            {
                get
                {
                    return m_Registered;
                }
                set
                {
                    m_Registered = value;
                }
            }

            public MenuSkinHelper(MainForm owner)
            {
                m_Owner = owner; 
            }

            #region ISkinnable Members

            public string GroupName
            {
                get
                {
                    return "Menu"; 
                }
                set
                {
                    
                }
            }

            public IEnumerable<string> GetSkinProperties()
            {
                return new string[] { "Fore Colour", "Back Colour" };
            }

            public void OnSkinPropertyChanged(SkinGroup group, string name, Color value)
            {
                switch (name)
                {
                    case "Fore Colour":
                        m_Owner.MenuForeColor = value;
                        break;
                    case "Back Colour":
                        m_Owner.MenuBackColor = value;
                        break;
                    default:
                        break;
                }
            }

            public IEnumerable<SkinImagePointer> GetSkinImages()
            {
                return new SkinImagePointer[0];
            }

            public void OnSkinImageChanged(string name, string path)
            {

            }

            #endregion
        }


        public IEnumerable<SkinImagePointer> GetSkinImages()
        {
            return new SkinImagePointer[0];
        }

        public void OnSkinImageChanged(string name, string path)
        {

        }

        #region ISkinableApplication Members

        private bool m_SkinEditorActive = false; 

        public bool SkinEditorActive
        {
            get
            {
                return m_SkinEditorActive;
            }
            set
            {
                m_SkinEditorActive = value;

                if (value)
                {
                    this.TransparencyKey = Color.Transparent; 
                    m_ShrinkButton.BringToFront();
                    m_ToTaskButton.BringToFront();
                    m_CloseButton.BringToFront();
                    //menuStrip1.Visible = false;
                    //windowHeader1.Visible = false;
                }
                else
                {
                    this.TransparencyKey = SkinningMaster.GetProperty(this.GroupName, "Transparent Key"); 
                    //menuStrip1.Visible = true;
                    //windowHeader1.Visible = false;
                }
            }
        }

        private string[] m_EditorViewNames = new string[] { "Open", "Mini" }; 

        public string[] EditorViewNames
        {
            get { return m_EditorViewNames; }
        }
        
        public string CurrentEditorView
        {
            get
            {
                if (this.WindowShrunk)
                    return "Mini";
                else
                    return "Open"; 
            }
            set
            {
                if (value == "Mini")
                    this.WindowShrunk = true;
                else
                    this.WindowShrunk = false;
            }
        }

        public event EventHandler UpdateEditor;

        public void DrawToBitmap(Bitmap bitmap)
        {
            base.DrawToBitmap(bitmap, new Rectangle(1, 1, Size.Width, Size.Height));

            Graphics g = Graphics.FromImage(bitmap);

            Point pt = m_ToTaskButton.Location;
            pt.Offset(1, 1); 
            m_ToTaskButton.RenderToGraphics(g, pt, m_ToTaskButton.Enabled ? (m_ToTaskButton.Hover || (m_ToTaskButton.HighlightOnFocus && m_ToTaskButton.Focused) ? RenderMode.Highlight : RenderMode.Regular) : RenderMode.Disabled);

            pt = m_ShrinkButton.Location;
            pt.Offset(1, 1);
            m_ShrinkButton.RenderToGraphics(g, pt, m_ShrinkButton.Enabled ? (m_ShrinkButton.Hover || (m_ShrinkButton.HighlightOnFocus && m_ShrinkButton.Focused) ? RenderMode.Highlight : RenderMode.Regular) : RenderMode.Disabled);

            pt = m_CloseButton.Location;
            pt.Offset(1, 1);
            m_CloseButton.RenderToGraphics(g, pt, m_CloseButton.Enabled ? (m_CloseButton.Hover || (m_CloseButton.HighlightOnFocus && m_CloseButton.Focused) ? RenderMode.Highlight : RenderMode.Regular) : RenderMode.Disabled);

            g.Dispose();

        }

        public Control FindControlForPoint(Point point)
        {
            Control ctrl = this.GetChildAtPoint(point, GetChildAtPointSkip.Invisible);

            if (ctrl != null && ctrl.Controls.Count > 0)
            {
                Control sub = ctrl;

                while (sub != null)
                {
                    point.Offset(-sub.Bounds.X, -sub.Bounds.Y);

                    if (sub.Controls.Count > 0)
                    {
                        sub = sub.GetChildAtPoint(point, GetChildAtPointSkip.Invisible);
                    }
                    else
                        sub = null;

                    if (sub != null && sub != ctrl)
                        ctrl = sub;
                    else
                        sub = null; 
                }
            }

            if (ctrl == null)
                ctrl = this; 

            return ctrl; 
        }

        public Control FindControlByName(Control root, string name) 
        {
            if (root.Controls.ContainsKey(name))
                return root.Controls[name]; 

            foreach (Control ctrl in root.Controls)
            {
                Control found = FindControlByName(ctrl, name);

                if (found != null) 
                    return found; 
            }

            return null; 
        }

        public Control FindControlByName(string name)
        {
            return FindControlByName(this, name);
        }

        public ISkinnable FindSkinnableByName(string name)        
        {
            Control ctrl = FindControlByName(this, name);

            if (ctrl != null)
                return GetSkinable(ctrl);
            else
                return null; 
        }

        ISkinnable GetSkinable(Control ctrl)
        {
            bool exit = false;

            while (!exit)
            {
                if (ctrl.Parent == null)
                    exit = true;
                else if (ctrl is ISkinnable && (ctrl as ISkinnable).Registered)
                    exit = true;
                else
                    ctrl = ctrl.Parent;
            }

            if (ctrl is ISkinnable)
                return ctrl as ISkinnable;
            else
                return null;
        }

        #endregion        

        private void clenseProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (sender as ToolStripMenuItem);

            this.ClenseProfile = (ClenseProfile)item.Tag; 
        }
    }
}


  