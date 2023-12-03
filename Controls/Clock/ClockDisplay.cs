// Based on Digital Clock control by Sriram Chitturi (c) Copyright 2004
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Rug.UI;
using Rug.UI.Skin;

namespace AlarmClock.Display
{
    public enum ColonType { Circular, Rectangular };
    public enum ClockType { DigitalClock, StopWatch, CountDown, Freeze };
    public enum DigitalColor { RedColor, BlueColor, GreenColor };
    // Clock format. For 12 hour format display 'A' (AM) or 'P' (PM)
    public enum ClockFormat { TwentyFourHourFormat, TwelveHourFormat };
    public enum ColourType { Regular, Alternate }

    [ToolboxBitmap("Digital.bmp")]
    public partial class ClockDisplay : SkinnableBase
    {
        #region Members

        private ClockFormat m_ClockDisplayFormat = ClockFormat.TwentyFourHourFormat;
        private ClockType m_ClockType = ClockType.DigitalClock;        
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

        private int m_Hour, m_Min, m_Sec, m_Ms;
        private char m_AmPm;

        // date time used to display stopwatch, count begins from this variable
        private DateTime m_StopwatchBegin = DateTime.Now;
        // whenever count down starts this time is set to Now + countDownMilliSeconds
        private DateTime m_CountDownTo;

        private Bitmap m_Bitmap = null;
        private Graphics m_Graphics = null;

        private int m_HorizontalPadding = 0;
        private int m_VerticalPadding = 2;

        private float m_LineWidth = 5f; 
        #endregion 
        
        #region Propertys

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

        public int HorizontalPadding
        {
            get { return m_HorizontalPadding; }
            set 
            { 
                m_HorizontalPadding = value;
                PreparePanels();
            } 
        }

        public int VerticalPadding
        {
            get { return m_VerticalPadding; }
            set
            {
                m_VerticalPadding = value;
                PreparePanels();
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

        public ClockType ClockType
        {
            get
            {
                return m_ClockType;
            }
            set
            {
                if (m_ClockType != value)
                    m_RedrawBackground = true;

                m_ClockType = value;
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
            }
        }


        public Color AlarmAltForeColor
        {
            set
            {
                FlashingColour.DigitColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.AltColours = m_FlashingColour;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.AltColours = m_FlashingColour;

                m_RedrawBackground = true;
            }
        }


        public Color AlarmAltBackColor
        {
            set
            {
                FlashingColour.BackColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.AltColours = m_FlashingColour;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.AltColours = m_FlashingColour;

                m_RedrawBackground = true;
            }
        }


        public Color AlarmAltDisabledColor
        {
            set
            {
                FlashingColour.DimDigitColour = value;

                if (m_DigitDisplay != null)
                    m_DigitDisplay.AltColours = m_FlashingColour;
                if (m_ColonDisplay != null)
                    m_ColonDisplay.AltColours = m_FlashingColour;

                m_RedrawBackground = true;
            }
        }

        #endregion

        public ClockDisplay()
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
                //m_Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                redrawBg = true;
            }

            #endregion 

            DateTime dt = DateTime.Now;                  

            string timeStr = dt.ToString("HHMMss");

            if (!redrawBg && m_LastRenderedStr != null && timeStr.Equals(m_LastRenderedStr))
            {
                g.DrawImage(m_Bitmap, new Point(0,0));                 
                return;
            }
            
            m_LastRenderedStr = timeStr;

            if (m_ClockType == ClockType.Freeze)
                m_Blink = false;
            else
                m_Blink = !m_Blink;

            #region Set the elements

            if (m_ClockType != ClockType.Freeze)
            {
                m_Hour = dt.Hour;
                m_Min = dt.Minute;
                m_Sec = dt.Second;
                m_Ms = dt.Millisecond;
                m_AmPm = ' ';
            }     

            TimeSpan ts = TimeSpan.Zero;

            switch (m_ClockType)
            {
                case ClockType.DigitalClock:
                    if (m_ClockDisplayFormat == ClockFormat.TwelveHourFormat)
                    {
                        m_Hour = dt.Hour % 12;
                        
                        if (m_Hour == 0)
                            m_Hour = 12;
                    }
                    switch (m_ClockDisplayFormat)
                    {
                        case ClockFormat.TwentyFourHourFormat:
                            break;
                        case ClockFormat.TwelveHourFormat:
                            m_AmPm = (dt.Hour / 12 > 0) ? 'P' : 'A';
                            break;
                    }
                    break;
                case ClockType.CountDown:
                    ts = m_CountDownTo.Subtract(dt);
                    if (ts < TimeSpan.Zero)
                    {
                        m_ClockType = ClockType.DigitalClock;
                        ts = TimeSpan.Zero;
                        //if (CountDownDone != null)
                        //    CountDownDone();
                    }
                    break;
                case ClockType.StopWatch:
                    ts = dt.Subtract(this.m_StopwatchBegin);
                    break;
                case ClockType.Freeze:
                    break;
            }

            if (m_ClockType != ClockType.DigitalClock &&
                m_ClockType != ClockType.Freeze) // ts used for stopwatch or countdown
            {
                m_Hour = ts.Hours;
                m_Min = ts.Minutes;
                m_Sec = ts.Seconds;
                m_Ms = ts.Milliseconds;
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
                        //m_Graphics.Clear(CurrentColourType == ColourType.Regular ? RegularColours.BackColour : FlashingColour.BackColour);
                        Brush backBrush = new SolidBrush(CurrentColourType == ColourType.Regular ? RegularColours.BackColour : FlashingColour.BackColour);

                        m_Graphics.FillRectangle(backBrush, new Rectangle(0, 0, Width, Height));

                        backBrush.Dispose();
                    }
                }

                DisplayTime(m_Graphics);

                DrawColons(m_Graphics);

                #endregion

                //g.DrawImage(m_Bitmap, new Point(0, 0)); 
            }
        }

        private void DisplayTime(Graphics g)
        {
            m_DigitDisplay.Draw(g, m_Hour / 10, m_DigitPositions[0], CurrentColourType);
            m_DigitDisplay.Draw(g, m_Hour % 10, m_DigitPositions[1], CurrentColourType);
            m_DigitDisplay.Draw(g, m_Min / 10, m_DigitPositions[2], CurrentColourType);
            m_DigitDisplay.Draw(g, m_Min % 10, m_DigitPositions[3], CurrentColourType);
            m_DigitDisplay.Draw(g, m_Sec / 10, m_DigitPositions[4], CurrentColourType);
            m_DigitDisplay.Draw(g, m_Sec % 10, m_DigitPositions[5], CurrentColourType);
            
            //MicroSecDisplay.Draw(ms / 100, g);
            //if (am_pm == ' ')
            //   AmPmDisplay.Draw(g);
            //else
            //    AmPmDisplay.Draw(am_pm, g);
        }

        private void DrawColons(Graphics g)
        {
            m_ColonDisplay.DrawColon(g, ColonType, m_Blink, m_ColonPositions[0], CurrentColourType);
            m_ColonDisplay.DrawColon(g, ColonType, m_Blink, m_ColonPositions[1], CurrentColourType);
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
            int WidthUnit = (int)(rect.Width / 5.4F);
            int HeightUnit = (int)(rect.Height / 2.2F);

            DigitWidth = (WidthUnit < HeightUnit) ? WidthUnit : HeightUnit;

            DigitHeight = 2 * DigitWidth;  // height is twice of width
            ColonWidth = DigitWidth / 2;  // colon width is half of a digit
            Spacing = DigitWidth / 10;

            if (Spacing < 1)
                Spacing = 1; // atleast a spacing of 1 is required

            //HMargin = 0;// (rect.Width - (8.8F * DigitWidth)) / 2;
            HMargin = m_HorizontalPadding;//  (rect.Width - (5.4F * DigitWidth)) / 32;
            VMargin = ((rect.Height - DigitHeight) / 2) + m_VerticalPadding;

            // This is the basic rectangle, offset it as required
            Rectangle basicRect = new Rectangle(0, (int)VMargin, (int)DigitWidth, (int)DigitHeight);
            int YOffset = (int)(VMargin);
            
            if (m_DigitPositions == null)
                m_DigitPositions = new int[6];

            if (m_DigitDisplay == null)
                m_DigitDisplay = new DigitalDisplay(basicRect);
            else
                m_DigitDisplay.CalculateAllParameters(basicRect);

            m_DigitDisplay.LineWidth = m_LineWidth;

            for (int i = 0; i < 6; i++)
            {
                m_DigitPositions[i] = (int)(HMargin + (Spacing * (i + 2 + (i / 2))) + (i * DigitWidth) + ((i / 2) * ColonWidth));
            }

            if (m_ColonPositions == null)
                m_ColonPositions = new int[2];

            basicRect.Width = (int)ColonWidth;

            if (m_ColonDisplay == null)
                m_ColonDisplay = new DigitalDisplay(basicRect);
            else
                m_ColonDisplay.CalculateAllParameters(basicRect);

            m_ColonPositions[0] = (int)(HMargin + 3 * Spacing + 2 * DigitWidth);
            m_ColonPositions[1] = (int)((6 * Spacing) + (4 * DigitWidth) + ColonWidth + HMargin);

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
            /*RenderTimer(e.Graphics);

            if (m_Bitmap != null) 
            {
                e.Graphics.DrawImage(m_Bitmap, new Point(0, 0)); 
            }
            else
                base.OnPaintBackground(e);*/ 
        }

        #endregion 
    
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
