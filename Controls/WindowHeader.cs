using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AlarmClock.Images;
using Rug.UI.Skin;

namespace AlarmClock.Controls
{
    public partial class WindowHeader : UserControl
    {
        //Rectangle m_Rect; 
        public string NextAlarmText
        {
            get
            {
                return this.label1.Text;

            }

            set
            {
                this.label1.Text = value;
            }
        }

        public bool ShowClock 
        {
            get
            {
                return this.clockDisplay1.Visible;

            }

            set
            {
                this.label1.Visible = value; 
                this.clockDisplay1.Visible = value;
                Invalidate(); 
            }
        }

        public AlarmColours RegularColours
        {
            get
            {
                return this.clockDisplay1.RegularColours;
            }
            set
            {
                this.clockDisplay1.RegularColours = value;
            }
        }

        public AlarmColours FlashingColour
        {
            get
            {
                return this.clockDisplay1.FlashingColour;
            }
            set
            {
                this.clockDisplay1.FlashingColour = value;
            }
        }

        public WindowHeader()
        {
            // only needed so the editor picks up the correct embeded folder
            //IconGen.AddPath();

            InitializeComponent();

            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
        }

        public void AddControlsToSkin()
        {
            SkinningMaster.RegisterSkinable(this.clockDisplay1);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

/*            
            if (m_Rect == null) 
            {
                m_Rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1); 
            }

            e.Graphics.FillRectangle(Brushes.White, m_Rect);
 */
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            /*
            if (m_Rect != null)
            {
                m_Rect.Width = this.Width - 1;
                m_Rect.Height = this.Height - 1; 
            }
             */
        }

        internal void ClockTick()
        {
            clockDisplay1.Invalidate(); 
        }
    }
}
