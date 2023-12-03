// Based on Digital Clock control by Sriram Chitturi (c) Copyright 2004
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Rug.UI.Skin;
using Rug.UI;

namespace AlarmClock.Display
{
    public enum UnitEdit { Hour, Min } 
    public delegate void TimeChangedEvent(object sender, Time time); 

    [ToolboxBitmap("Digital.bmp")]
    public partial class ClockEdit : SkinnableBase
    {
        public event TimeChangedEvent TimeChanged; 

        #region Members

        private UnitEdit m_CurrentlyEditing = UnitEdit.Hour;
        private int m_DigitEditing = 0; 

        private ClockFormat m_ClockDisplayFormat = ClockFormat.TwentyFourHourFormat;        
        private ColonType m_ColonType = ColonType.Rectangular;
        private ColourType m_ColourType = ColourType.Regular;

        private string m_LastRenderedStr = null;
        private bool m_RedrawBackground = true;
        private bool m_Blink = false; // toggle for blinking effect of colons

        private int[] m_DigitPositions = null;
        private int[] m_ColonPositions = null;

        private DigitalDisplay m_DigitDisplay = null; // panels on which digits are displayed
        private DigitalDisplay m_ColonDisplay = null; // panels for displaying colons

        private AlarmColours m_RegularColours = new AlarmColours();
        private AlarmColours m_FlashingColour = new AlarmColours();

        private int m_Hour, m_Min;
        private char m_AmPm;

        private Bitmap m_Bitmap = null;
        private Graphics m_Graphics = null; 

        private Time m_Time = new Time(0,0);

        private float m_LineWidth = 2f; 
        #endregion 
        
        #region Properties

        public float LineWidth
        {
            get { return m_LineWidth; }
            set 
            {
                m_LineWidth = value;
                if (m_DigitDisplay != null) 
                    m_DigitDisplay.LineWidth = m_LineWidth;
            } 
            
        }

        public AlarmColours RegularColours
        {
            get
            {
                return m_RegularColours;
            }
            set
            {
                m_RegularColours = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.Colours = m_RegularColours;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.Colours = m_RegularColours;

                m_RedrawBackground = true;
            }
        }

        public AlarmColours FlashingColour
        {
            get
            {
                return m_FlashingColour;
            }
            set
            {
                m_FlashingColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.AltColours = m_FlashingColour;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.AltColours = m_FlashingColour;

                m_RedrawBackground = true;
            }
        }

        public ColourType CurrentColourType
        {
            get
            {
                return m_ColourType;
            }
            set
            {
                if (m_ColourType != value)
                    m_RedrawBackground = true;

                m_ColourType = value;
            }
        }

        public ColonType ColonType
        {
            get
            {
                return m_ColonType;
            }
            set
            {
                m_ColonType = value;
            }
        }

        public ClockFormat ClockDisplayFormat
        {
            get
            {
                return m_ClockDisplayFormat;
            }
            set
            {
                if (m_ClockDisplayFormat != value)
                    m_RedrawBackground = true;

                m_ClockDisplayFormat = value;
            }
        }
        
        public UnitEdit CurrentlyEditing
        {
            get
            {
                return m_CurrentlyEditing;
            }
            set
            {
                if (m_CurrentlyEditing != value)
                    m_RedrawBackground = true;

                m_CurrentlyEditing = value;
                m_DigitEditing = 0; 

                Invalidate(); 
            }
        }

        public Time Time 
        {
            get { return m_Time; }
            set
            {
                m_Time = value; 

            }
        }

        public Color AlarmForeColor
        {
            set
            {
                RegularColours.DigitColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.Colours = m_RegularColours;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.Colours = m_RegularColours;

                m_RedrawBackground = true;
                Invalidate(); 
            }
        }

        public Color AlarmBackColor
        {
            set
            {
                RegularColours.BackColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.Colours = m_RegularColours;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.Colours = m_RegularColours;

                m_RedrawBackground = true;
                Invalidate(); 
            }
        }


        public Color AlarmDisabledColor
        {
            set
            {
                RegularColours.DimDigitColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.Colours = m_RegularColours;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.Colours = m_RegularColours;

                m_RedrawBackground = true;
                Invalidate(); 
            }
        }


        public Color AlarmAltForeColor
        {
            set
            {
                FlashingColour.DigitColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.AltColours = FlashingColour;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.AltColours = FlashingColour;

                m_RedrawBackground = true;
                Invalidate(); 
            }
        }


        public Color AlarmAltBackColor
        {
            set
            {
                FlashingColour.BackColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.AltColours = FlashingColour;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.AltColours = FlashingColour;

                m_RedrawBackground = true;
                Invalidate(); 
            }
        }


        public Color AlarmAltDisabledColor
        {
            set
            {
                FlashingColour.DimDigitColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.AltColours = FlashingColour;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.AltColours = FlashingColour;

                m_RedrawBackground = true;
                Invalidate(); 
            }
        }

        #endregion

        public ClockEdit()
        {
            InitializeComponent();
            //DoubleBuffered = true;
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }

        #region Render Methods

        private void RenderTimer(Graphics g)
        {
            bool redrawBg = m_RedrawBackground;

            #region Bitmap Setup

            if (m_Bitmap != null && (m_Bitmap.Width != this.Width || m_Bitmap.Height != this.Height)) 
            {
                m_Graphics.Dispose();
                m_Graphics = null; 

                m_Bitmap.Dispose(); 
                m_Bitmap = null;
            }

            if (m_Bitmap == null)
            {
                m_Bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
                m_Graphics = Graphics.FromImage(m_Bitmap);
                redrawBg = true;

                //m_Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //m_Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            }

            #endregion 
                     

            string timeStr = m_Time.ToString(); 

            if (!redrawBg && m_LastRenderedStr != null && timeStr.Equals(m_LastRenderedStr))
            {
                g.DrawImage(m_Bitmap, new Point(0,0));                 
                return;
            }
            
            m_LastRenderedStr = timeStr;

            #region Set the elements

            m_Hour = m_Time.Hour;
            m_Min = m_Time.Minute;
            m_AmPm = ' ';

            if (m_ClockDisplayFormat == ClockFormat.TwelveHourFormat)
            {
                m_Hour = m_Time.Hour % 12;
                if (m_Hour == 0)
                    m_Hour = 12;
            }

            switch (m_ClockDisplayFormat)
            {
                case ClockFormat.TwentyFourHourFormat:
                    break;
                case ClockFormat.TwelveHourFormat:
                    m_AmPm = (m_Time.Hour / 12 > 0) ? 'P' : 'A';
                    break;
            }            

            #endregion 

            if (m_Graphics != null) 
            {

                #region Render clock elements
                
                m_Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

                //if (m_RedrawBackground)
                {
                    m_RedrawBackground = false;

                    if (RegularColours != null && FlashingColour != null)
                    {
                        Brush backBrush = new SolidBrush(CurrentColourType == ColourType.Regular ? RegularColours.BackColour : FlashingColour.BackColour);

                        m_Graphics.FillRectangle(backBrush, new Rectangle(0,0, Width, Height));

                        backBrush.Dispose(); 
                        // m_Graphics.Clear(CurrentColourType == ColourType.Regular ? RegularColours.BackColour : FlashingColour.BackColour);
                    }
                }

                DisplayTime(m_Graphics);

                DrawColons(m_Graphics);

                #endregion

                g.DrawImage(m_Bitmap, new Point(0, 0)); 
            }
        }

        private void DisplayTime(Graphics g)
        {
            m_DigitDisplay.Draw(g, m_Hour / 10, m_DigitPositions[0], (this.Focused && m_CurrentlyEditing == UnitEdit.Hour ? ColourType.Alternate : CurrentColourType));
            m_DigitDisplay.Draw(g, m_Hour % 10, m_DigitPositions[1], (this.Focused && m_CurrentlyEditing == UnitEdit.Hour ? ColourType.Alternate : CurrentColourType));
            m_DigitDisplay.Draw(g, m_Min / 10, m_DigitPositions[2], (this.Focused && m_CurrentlyEditing == UnitEdit.Min ? ColourType.Alternate : CurrentColourType));
            m_DigitDisplay.Draw(g, m_Min % 10, m_DigitPositions[3], (this.Focused && m_CurrentlyEditing == UnitEdit.Min ? ColourType.Alternate : CurrentColourType));
            
            //MicroSecDisplay.Draw(ms / 100, g);
            //if (am_pm == ' ')
            //   AmPmDisplay.Draw(g);
            //else
            //    AmPmDisplay.Draw(am_pm, g);
        }

        private void DrawColons(Graphics g)
        {
            m_ColonDisplay.DrawColon(g, ColonType, m_Blink, m_ColonPositions[0], CurrentColourType);
            //m_ColonDisplay.DrawColon(g, ColonType, m_Blink, m_ColonPositions[1], CurrentColourType);

            //if (m_ClockType == ClockType.Freeze)
                m_Blink = true;
            //else
            //  m_Blink = !m_Blink;
        }

        #endregion

        #region Setup Display

        // function to prepare the digital clock panels by dividing the rectangle
        // It is assumed that the height of each digit is double that of it's width
        // Spacing betweent the digits is 10% of the width
        // The colon characters occupy 50% of width of the digits
        private void PreparePanels()
        {
            // from the above assumptions for height and width
            // the height should be 2.4 units and width 8.8 units :-)
            // check height and width whichever is dominant and adjust the other
            // and set up margins
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            // widths, spacings and margins
            // height of colon display is same as a digit
            int DigitWidth, DigitHeight, ColonWidth, Spacing;
            float HMargin = 0, // left and right margin
                    VMargin = 0; // top and bottom margin

            // Calculate a digit width (which is our unit) from the above metrics
            // and settle for the least value
            int WidthUnit = (int)(rect.Width / 4.2F);
            int HeightUnit = (int)(rect.Height / 2.2F);

            DigitWidth = (WidthUnit < HeightUnit) ? WidthUnit : HeightUnit;

            DigitHeight = 2 * DigitWidth;  // height is twice of width
            ColonWidth = DigitWidth / 2;  // colon width is half of a digit
            Spacing = DigitWidth / 10;

            if (Spacing < 1)
                Spacing = 1; // atleast a spacing of 1 is required

            HMargin = 0;// (rect.Width - (8.8F * DigitWidth)) / 2;
            VMargin = ((rect.Height - DigitHeight) / 2) + 1;

            // This is the basic rectangle, offset it as required
            Rectangle basicRect = new Rectangle(0, (int)VMargin, (int)DigitWidth, (int)DigitHeight);
            int YOffset = (int)(VMargin);
            
            if (m_DigitPositions == null)
                m_DigitPositions = new int[4];

            if (m_DigitDisplay == null)
                m_DigitDisplay = new DigitalDisplay(basicRect);
            else
                m_DigitDisplay.CalculateAllParameters(basicRect);

            m_DigitDisplay.LineWidth = m_LineWidth;

            for (int i = 0; i < 4; i++)
            {
                m_DigitPositions[i] = (int)(HMargin + (Spacing * (i + 2 + (i / 2))) + (i * DigitWidth) + ((i / 2) * ColonWidth));
            }

            if (m_ColonPositions == null)
                m_ColonPositions = new int[1];

            basicRect.Width = (int)ColonWidth;

            if (m_ColonDisplay == null)
                m_ColonDisplay = new DigitalDisplay(basicRect);
            else
                m_ColonDisplay.CalculateAllParameters(basicRect);

            m_ColonPositions[0] = (int)(HMargin + 3 * Spacing + 2 * DigitWidth);

            if (m_RegularColours != null)
            {
                m_DigitDisplay.Colours = m_RegularColours;
                m_ColonDisplay.Colours = m_RegularColours;
            }

            if (m_FlashingColour != null)
            {
                m_DigitDisplay.AltColours = m_FlashingColour;
                m_ColonDisplay.AltColours = m_FlashingColour;
            }
        }

        #endregion 

        #region Internal Events

        private void OnResize(object sender, EventArgs e)
        {
            PreparePanels();
            Invalidate(); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            RenderTimer(e.Graphics);

            if (m_Bitmap != null)
            {
                e.Graphics.DrawImage(m_Bitmap, new Point(0, 0));
            }
            else
                base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            /*
            RenderTimer(e.Graphics);

            if (m_Bitmap != null)
            {
                e.Graphics.DrawImage(m_Bitmap, new Point(0, 0));
            }
            else
                base.OnPaintBackground(e);
             * */
        }

        #endregion 

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Location.X < Width / 2)
                CurrentlyEditing = UnitEdit.Hour;
            else
                CurrentlyEditing = UnitEdit.Min;

            base.OnMouseDown(e);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            //Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (CurrentlyEditing == UnitEdit.Hour)
            {
                m_DigitEditing = 0;

                Time.Hour += e.Delta / 120;

                while (Time.Hour > 23)
                    Time.Hour -= 24;

                while (Time.Hour < 0)
                    Time.Hour += 24;

                Invalidate();

                if (TimeChanged != null)
                    TimeChanged(this, Time); 
            }
            else if (CurrentlyEditing == UnitEdit.Min)
            {
                m_DigitEditing = 0;

                Time.Minute += e.Delta / 120;

                while (Time.Minute > 59)
                    Time.Minute -= 60;

                while (Time.Minute < 0)
                    Time.Minute += 60;

                Invalidate();

                if (TimeChanged != null)
                    TimeChanged(this, Time); 
            }
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {          
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                if (CurrentlyEditing == UnitEdit.Hour)
                {
                    string digits = Time.Hour.ToString().PadLeft(2, '0');

                    if (m_DigitEditing == 0) 
                    {
                        m_DigitEditing = 1;
                        digits = e.KeyChar + digits.Substring(1,1); 
                    }
                    else 
                    {
                        digits = digits.Substring(0,1) + e.KeyChar;
                        m_DigitEditing = 0;
                    }

                    Time.Hour = int.Parse(digits);

                    Time.Hour = Time.Hour < 0 ? 0 : Time.Hour > 23 ? 23 : Time.Hour;                   

                    Invalidate();

                    if (TimeChanged != null)
                        TimeChanged(this, Time); 
                }
                else if (CurrentlyEditing == UnitEdit.Min)
                {                    
                    string digits = Time.Minute.ToString().PadLeft(2, '0');

                    if (m_DigitEditing == 0)
                    {
                        m_DigitEditing = 1;
                        digits = e.KeyChar + digits.Substring(1, 1);
                    }
                    else
                    {
                        digits = digits.Substring(0, 1) + e.KeyChar;
                        m_DigitEditing = 0;
                    }

                    Time.Minute = int.Parse(digits);
                    Time.Minute = Time.Minute < 0 ? 0 : Time.Minute > 59 ? 59 : Time.Minute;

                    Invalidate();

                    if (TimeChanged != null)
                        TimeChanged(this, Time); 
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                switch (CurrentlyEditing)
	            {
		            case UnitEdit.Hour:
                        Time.Hour = Time.Hour + 1;
                        Time.Hour = Time.Hour < 0 ? Time.Hour + 24 : Time.Hour > 23 ? Time.Hour - 24 : Time.Hour;
                        Invalidate();

                        if (TimeChanged != null)
                            TimeChanged(this, Time); 
                     break;
                    case UnitEdit.Min:
                        Time.Minute = Time.Minute + 1;
                        Time.Minute = Time.Minute < 0 ? Time.Minute + 60 : Time.Minute > 59 ? Time.Minute - 60 : Time.Minute;
                        Invalidate();

                        if (TimeChanged != null)
                            TimeChanged(this, Time); 
                        break;
                    default:
                        break;
	            }
                return true;
            }
            else if (keyData == Keys.Down)
            {
                switch (CurrentlyEditing)
                {
                    case UnitEdit.Hour:
                        Time.Hour = Time.Hour - 1;
                        Time.Hour = Time.Hour < 0 ? Time.Hour + 24 : Time.Hour > 23 ? Time.Hour - 24 : Time.Hour;
                        m_RedrawBackground = true; 
                        Invalidate();

                        if (TimeChanged != null)
                            TimeChanged(this, Time);
                        break;
                    case UnitEdit.Min:
                        Time.Minute = Time.Minute - 1;
                        Time.Minute = Time.Minute < 0 ? Time.Minute + 60 : Time.Minute > 59 ? Time.Minute - 60 : Time.Minute;
                        m_RedrawBackground = true; 
                        Invalidate();

                        if (TimeChanged != null)
                            TimeChanged(this, Time);
                        break;
                    default:
                        break;

                }
                return true;
            }
            else if (keyData == Keys.Right)
            {
                if (this.CurrentlyEditing == UnitEdit.Hour)
                {
                    this.CurrentlyEditing = UnitEdit.Min;
                    m_RedrawBackground = true;
                    Invalidate();
                    return true;
                }
            }
            else if (keyData == Keys.Left) 
            {
                if (this.CurrentlyEditing == UnitEdit.Min)
                {
                    this.CurrentlyEditing = UnitEdit.Hour;
                    m_RedrawBackground = true; 
                    Invalidate();
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessTabKey(bool forward)
        {
            if (this.CurrentlyEditing == UnitEdit.Hour && forward)
            {
                this.CurrentlyEditing = UnitEdit.Min;
                m_RedrawBackground = true;
                Invalidate();
                return true;
            }
            else if (this.CurrentlyEditing == UnitEdit.Min && !forward)
            {
                this.CurrentlyEditing = UnitEdit.Hour;
                m_RedrawBackground = true;
                Invalidate();
                return true;
            }

            return base.ProcessTabKey(forward);
        }

        protected override void Select(bool directed, bool forward)
        {
            if (forward)
                this.CurrentlyEditing = UnitEdit.Hour; 
            else
                this.CurrentlyEditing = UnitEdit.Min; 

            base.Select(directed, forward);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            //this.CurrentlyEditing = UnitEdit.Hour;
            m_RedrawBackground = true; 
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            m_RedrawBackground = true;
            Invalidate();
        }


        public override IEnumerable<string> GetSkinProperties()
        {
            return new string[] { "Fore Colour", "Back Colour", "Disabled Colour", "Alt. Fore Colour", "Alt. Back Colour", "Alt. Disabled Colour" };
        }

        public override void OnSkinPropertyChanged(Rug.UI.Skin.SkinGroup group, string name, Color value)
        {
            switch (name)
            {
                case "Fore Colour":
                    AlarmForeColor = value;
                    break;
                case "Back Colour":
                    AlarmBackColor = value;
                    break;
                case "Disabled Colour":
                    AlarmDisabledColor = value;
                    break;
                case "Alt. Fore Colour":
                    AlarmAltForeColor = value;
                    break;
                case "Alt. Back Colour":
                    AlarmAltBackColor = value;
                    break;
                case "Alt. Disabled Colour":
                    AlarmAltDisabledColor = value;
                    break;
                default:
                    break;
            }
        }

        public override IEnumerable<SkinImagePointer> GetSkinImages()
        {
            return new SkinImagePointer[0];
        }

        public override void OnSkinImageChanged(string name, string path)
        {

        }
    }
}
