using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlarmClock.Display;
using Rug.UI;
using Rug.UI.Util;
using AlarmClock.Images;
using Rug.UI.Skin;

namespace AlarmClock.Controls
{
    public partial class AlarmsPanel : PanelWithBorder //, ISkinable
    {
        private bool m_SettingDays = false;

        //Rectangle m_Rect;

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;

                backImage1.ForeColor = value;
                backImage2.ForeColor = value; 
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
                base.BackColor = value;

                backImage1.BackColor = value;
                backImage2.BackColor = value; 
            }
        }

        public AlarmColours RegularColours 
        {
            get { return m_TimeEditor.RegularColours; }
            set 
            { 
                m_TimeEditor.RegularColours = value;
                m_TimeEditor.Invalidate();
            } 
        }

        public AlarmColours FlashingColour
        {
            get { return m_TimeEditor.FlashingColour; }
            set 
            { 
                m_TimeEditor.FlashingColour = value;
                m_TimeEditor.Invalidate();
            }
        }

        /*private Color m_BorderColor = Color.Black;

        public Color BorderColor
        {
            get { return m_BorderColor; }
            set
            {
                m_BorderColor = value;
                Invalidate();
            }
        }*/ 

        public IEnumerable<Time> Times
        {
            get { return m_AlarmsList.Objects; } 
        }

        public List<CheckboxList<Time>.CheckboxItem> Items
        {
            get { return m_AlarmsList.Items; }
        }

        public AlarmsPanel()
        {
            InitializeComponent();

        }

        public void Init()
        {
            UpdateDaysControlsFromTime();
            UpdateButtons(0, 0);
        }

        #region Internal Events

        private void OnCheckedChanged(object sender, Time item, bool newValue)
        {
            Time time = (Time)m_AlarmsList.SelectedObject; 

            if (time == null)
                return;

            time.Enabled = newValue;
        }

        private void OnSelectionChanged(object sender, Time item)
        {
            DefoucsClock();

            Time time = (Time)m_AlarmsList.SelectedObject;

            if (time == null)
            {
                m_UpdateTimeButton.Enabled = false;
                return;
            }
            else
                m_UpdateTimeButton.Enabled = true;

            UpdateDaysControlsFromTime();
        }

        private void OnDayClick(object sender, EventArgs e)
        {
            if (!m_SettingDays && m_AlarmsList.SelectedObject != null)
            {
                // if we are m_SettingDays there is no selected item exit the method 
                UpdateTimeFromDaysControls();                 
            }
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            Time AlarmTime = new Time();
            AlarmTime.Hour = this.m_TimeEditor.Time.Hour; 
            AlarmTime.Minute = this.m_TimeEditor.Time.Minute;
            AlarmTime.Days = GetActiveDaysOfTheWeek();
            
            CheckboxList<Time>.CheckboxItem checkBox = new CheckboxList<Time>.CheckboxItem(AlarmTime, true);

            m_AlarmsList.Items.Add(checkBox);
            m_AlarmsList.SelectedItem = checkBox;
            m_AlarmsList.Items.Sort();
            m_AlarmsList.ScrollSelectedIntoView(); 
            m_AlarmsList.Invalidate();

            DefoucsClock();
            UpdateDaysControlsFromTime();                                   
        }

        private void OnUpdateClick(object sender, EventArgs e)
        {
            int hour = this.m_TimeEditor.Time.Hour;
            int min = this.m_TimeEditor.Time.Minute;

            Time time = (Time)m_AlarmsList.SelectedObject;

            if (time == null)
                return;

            time.Hour = hour;
            time.Minute = min;
            
            m_AlarmsList.RenderSingleItem(time);
            m_AlarmsList.ScrollSelectedIntoView();            

            m_CreateAlarm.Enabled = false; 
            DefoucsClock();
        }

        private void OnRemoveClick(object sender, EventArgs e)
        {
            Time time = m_AlarmsList.SelectedObject as Time;
                       
            if (time != null)
            {
                if (!AlarmConfig.Config.ConfirmDelete || MessageBox.Show("Delete alarm '" + time.ToString() + "'?", "Delete", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    m_AlarmsList.Items.Remove(m_AlarmsList.SelectedItem);                                        
                    m_AlarmsList.SelectedItem = null; 
                    m_AlarmsList.Items.Sort();

                    UpdateDaysControlsFromTime();
                    UpdateButtons(0, 0);

                    AdjustScroll(); 
                    m_AlarmsList.Invalidate();
                }
            }

            DefoucsClock();
        }

        private void OnTimeChanged(object sender, Time time)
        {
            int hour = time.Hour;
            int min = time.Minute;

            UpdateButtons(hour, min);  
        }

        #endregion 

        #region Clock Control Methods 

        public void DefoucsClock()
        {
            //m_TimeEditor.CurrentlyEditing = UnitEdit.None;
        }

        public void ResetClock()
        {
            m_TimeEditor.Time.Hour = 0;
            m_TimeEditor.Time.Minute = 0; 
            m_TimeEditor.CurrentlyEditing = UnitEdit.Hour;
        }

        #endregion 

        #region Days Controls Methods

        private void UpdateTimeFromDaysControls()
        {
            Time time = (Time)m_AlarmsList.SelectedObject;

            if (time == null)
                return;

            time.Enabled = m_AlarmsList.SelectedChecked;

            time.Days = GetActiveDaysOfTheWeek();
            m_AlarmsList.RenderSingleItem(time);
            UpdateButtons(time.Hour, time.Minute);

            DefoucsClock();
        }

        private void UpdateDaysControlsFromTime()
        {
            m_SettingDays = true;

            if (m_AlarmsList.SelectedObject == null)
            {
                m_MondayBox.Enabled = false;
                m_MondayBox.Checked = true;

                m_TuesdayBox.Enabled = false;
                m_TuesdayBox.Checked = true;

                m_WednesdayBox.Enabled = false;
                m_WednesdayBox.Checked = true;

                m_ThursdayBox.Enabled = false;
                m_ThursdayBox.Checked = true;

                m_FridayBox.Enabled = false;
                m_FridayBox.Checked = true;

                m_SaturdayBox.Enabled = false;
                m_SaturdayBox.Checked = true;

                m_SundayBox.Enabled = false;
                m_SundayBox.Checked = true;
            }
            else
            {
                Time time = (Time)m_AlarmsList.SelectedObject;

                m_MondayBox.Enabled = true;
                m_MondayBox.Checked = (time.Days & Time.DaysOfTheWeek.Mon) == Time.DaysOfTheWeek.Mon;

                m_TuesdayBox.Enabled = true;
                m_TuesdayBox.Checked = (time.Days & Time.DaysOfTheWeek.Tue) == Time.DaysOfTheWeek.Tue;

                m_WednesdayBox.Enabled = true;
                m_WednesdayBox.Checked = (time.Days & Time.DaysOfTheWeek.Wed) == Time.DaysOfTheWeek.Wed;

                m_ThursdayBox.Enabled = true;
                m_ThursdayBox.Checked = (time.Days & Time.DaysOfTheWeek.Thur) == Time.DaysOfTheWeek.Thur;

                m_FridayBox.Enabled = true;
                m_FridayBox.Checked = (time.Days & Time.DaysOfTheWeek.Fri) == Time.DaysOfTheWeek.Fri;

                m_SaturdayBox.Enabled = true;
                m_SaturdayBox.Checked = (time.Days & Time.DaysOfTheWeek.Sat) == Time.DaysOfTheWeek.Sat;

                m_SundayBox.Enabled = true;
                m_SundayBox.Checked = (time.Days & Time.DaysOfTheWeek.Sun) == Time.DaysOfTheWeek.Sun;

                //this.dateTimePicker1.Value = time.ToDateTime();
                UpdateButtons(time.Hour, time.Minute);
                this.m_TimeEditor.Invalidate();
            }

            m_SettingDays = false;
        }

        #region Additional

        private Time.DaysOfTheWeek GetActiveDaysOfTheWeek()
        {
            Time.DaysOfTheWeek days = Time.DaysOfTheWeek.Disabled;

            days = AddDayOfTheWeek(m_MondayBox, days, Time.DaysOfTheWeek.Mon);
            days = AddDayOfTheWeek(m_TuesdayBox, days, Time.DaysOfTheWeek.Tue);
            days = AddDayOfTheWeek(m_WednesdayBox, days, Time.DaysOfTheWeek.Wed);
            days = AddDayOfTheWeek(m_ThursdayBox, days, Time.DaysOfTheWeek.Thur);
            days = AddDayOfTheWeek(m_FridayBox, days, Time.DaysOfTheWeek.Fri);
            days = AddDayOfTheWeek(m_SaturdayBox, days, Time.DaysOfTheWeek.Sat);
            days = AddDayOfTheWeek(m_SundayBox, days, Time.DaysOfTheWeek.Sun);

            return days;
        }

        private Time.DaysOfTheWeek AddDayOfTheWeek(ImageCheckbox box, Time.DaysOfTheWeek days, Time.DaysOfTheWeek current)
        {
            if (box.Checked)
            {
                if (days == Time.DaysOfTheWeek.Disabled)
                    days = current;
                else
                    days = days | current;
            }

            return days;
        }

        private bool IsSetToDay(Time.DaysOfTheWeek days, Time.DaysOfTheWeek current)
        {
            return (days & current) == current;
        }

        #endregion
        
        #endregion

        #region Alarm List Controls Methods

        public void AdjustScroll()
        {
            this.m_AlarmsList.AdjustScroll(); 
        }

        public void UpdateButtons(int hour, int min)
        {
            this.m_TimeEditor.Time.Hour = hour;
            this.m_TimeEditor.Time.Minute = min;

            Time time = (Time)m_AlarmsList.SelectedObject;

            m_CreateAlarm.Enabled = true;

            if (time != null)
            {
                m_UpdateTimeButton.Enabled = true;
                m_RemoveTimeButton.Enabled = true;
            }
            else
            {
                m_UpdateTimeButton.Enabled = false;
                m_RemoveTimeButton.Enabled = false;
            }

            foreach (CheckboxList<Time>.CheckboxItem item in m_AlarmsList.Items)
            {
                Time t = (Time)item.Object;

                if (t.Hour == hour && t.Minute == min)
                {
                    m_CreateAlarm.Enabled = false;

                    if (time != null && t != time)
                        m_UpdateTimeButton.Enabled = false;
                }
            }
        }

        #endregion 

        #region Styling Methods

        public void AddControlsToSkin()
        {
            SkinningMaster.RegisterSkinable(this);
            SkinningMaster.RegisterSkinable(m_AlarmsList);
            SkinningMaster.RegisterSkinable(m_RemoveTimeButton);
            SkinningMaster.RegisterSkinable(m_UpdateTimeButton);
            SkinningMaster.RegisterSkinable(m_CreateAlarm);
            SkinningMaster.RegisterSkinable(m_MondayBox);
            SkinningMaster.RegisterSkinable(m_TuesdayBox);
            SkinningMaster.RegisterSkinable(m_WednesdayBox);
            SkinningMaster.RegisterSkinable(m_ThursdayBox);
            SkinningMaster.RegisterSkinable(m_FridayBox);
            SkinningMaster.RegisterSkinable(m_SaturdayBox);
            SkinningMaster.RegisterSkinable(m_SundayBox);
            SkinningMaster.RegisterSkinable(m_TimeEditor); 
        }

        public void UpdateColours()
        {
            /*
            this.SuspendLayout(); 
            AlarmColours reg = AlarmConfig.Config.Regular;
            Color panText = AlarmConfig.Config.PanelTextColour;
            Color panBack = AlarmConfig.Config.PanelBackColour;
            this.BorderColor = AlarmConfig.Config.PanelBorderColour;

            m_TimeEditor.RegularColours = reg;
            m_TimeEditor.FlashingColour = AlarmConfig.Config.Flashing;

            //this.m_ShowPanelButton.ForeColor = reg.BackColour;
            //this.m_ShowPanelButton.BackColor = reg.DimDigitColour;
            //this.m_ShowPanelButton.HighlightColor = reg.DigitColour;
            this.BackColor = panBack;

            m_AlarmsList.ForeColor = AlarmConfig.Config.AlarmsTextColour;
            m_AlarmsList.BackColor = AlarmConfig.Config.AlarmsBackColour;
            m_AlarmsList.SelectedColor = AlarmConfig.Config.AlarmsSelectedColour;
            m_AlarmsList.SelectedTextColor = AlarmConfig.Config.AlarmsSelectedTextColour;
            m_AlarmsList.BorderColor = AlarmConfig.Config.AlarmsBorderColour;

            this.m_RemoveTimeButton.DisabledColor = reg.DimDigitColour;
            this.m_RemoveTimeButton.HighlightColor = reg.DigitColour;
            this.m_RemoveTimeButton.ForeColor = panText;

            this.m_UpdateTimeButton.DisabledColor = reg.DimDigitColour;
            this.m_UpdateTimeButton.HighlightColor = reg.DigitColour;
            this.m_UpdateTimeButton.ForeColor = panText;

            this.m_CreateAlarm.DisabledColor = reg.DimDigitColour;
            this.m_CreateAlarm.HighlightColor = reg.DigitColour;
            this.m_CreateAlarm.ForeColor = panText;

            this.m_MondayBox.DisabledColor = reg.DimDigitColour;
            this.m_MondayBox.HighlightColor = reg.DigitColour;
            this.m_MondayBox.ForeColor = panText;            

            this.m_TuesdayBox.DisabledColor = reg.DimDigitColour;
            this.m_TuesdayBox.HighlightColor = reg.DigitColour;
            this.m_TuesdayBox.ForeColor = panText;

            this.m_WednesdayBox.DisabledColor = reg.DimDigitColour;
            this.m_WednesdayBox.HighlightColor = reg.DigitColour;
            this.m_WednesdayBox.ForeColor = panText;

            this.m_ThursdayBox.DisabledColor = reg.DimDigitColour;
            this.m_ThursdayBox.HighlightColor = reg.DigitColour;
            this.m_ThursdayBox.ForeColor = panText;

            this.m_FridayBox.DisabledColor = reg.DimDigitColour;
            this.m_FridayBox.HighlightColor = reg.DigitColour;
            this.m_FridayBox.ForeColor = panText;

            this.m_SaturdayBox.DisabledColor = reg.DimDigitColour;
            this.m_SaturdayBox.HighlightColor = reg.DigitColour;
            this.m_SaturdayBox.ForeColor = panText;

            this.m_SundayBox.DisabledColor = reg.DimDigitColour;
            this.m_SundayBox.HighlightColor = reg.DigitColour;
            this.m_SundayBox.ForeColor = panText;

            this.backImage1.BackColor = panBack;
            this.backImage1.ForeColor = panText;

            this.backImage2.BackColor = panBack;
            this.backImage2.ForeColor = panText;

            m_AlarmsList.Invalidate();
            m_TimeEditor.Invalidate(); 

            this.ResumeLayout(); 
             * */
        }

        #endregion

        #region Overrides 

        /*protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (m_Rect == null)
            {
                m_Rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            }

            e.Graphics.DrawRectangle(ResourceManager.Pens[m_BorderColor, 1f], m_Rect);

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (m_Rect != null)
            {
                m_Rect.Width = this.Width - 1;
                m_Rect.Height = this.Height - 1;
            }
        }*/ 

        #endregion

/*        #region ISkinable Members

        public override string GroupName
        {
            get
            {
                return "Panel";
            }
            set
            {
                
            }
        }

        public override IEnumerable<string> GetSkinProperties()
        {
            return new string[] { "Fore Colour", "Back Colour" };  
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
                default:
                    break;
            }
        }

        #endregion */
    }
}
