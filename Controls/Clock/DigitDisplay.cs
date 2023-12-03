// Digital Clock Display by Sriram Chitturi (c) Copyright 2004
// A class to display digital digits and characters on a rectangular area

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using AlarmClock.Display;
using Rug.UI.Util;

namespace AlarmClock
{
	// A digit class which can draw a number on a graphics surface
	internal sealed class DigitalDisplay
	{
        #region Fields

        private Pen m_LitPen = new Pen(Color.FromArgb(255,0,0));
        private Pen m_DimPen = new Pen(Color.FromArgb(60, 0, 0));

        private Pen m_LitAltPen = new Pen(Color.FromArgb(255, 255, 255));
        private Pen m_DimAltPen = new Pen(Color.FromArgb(60, 0, 0));

        private Brush m_LitBrush = null; // new Brush(Color.FromArgb(255, 0, 0));
        private Brush m_DimBrush = null; // new Brush(Color.FromArgb(60, 0, 0));

        private Brush m_LitAltBrush = null; // new Brush(Color.FromArgb(255, 255, 255));
        private Brush m_DimAltBrush = null; // new Brush(Color.FromArgb(60, 0, 0));

        private LineCap m_Cap = LineCap.Triangle;

        // pens used to draw the digits
        private float m_Linewidth = 20.0F;
        private Point[] m_Points; // end point coordinates of lines in the digital display

        // for each digit the display bits are set into an int
        // 'A' and 'P' are included for AM and PM display in the clock
        private int[] m_DisplayNum = new int[12]{63,12,118,94,77,91,123,14,127,95,
												  111, // to display 'A'
												  103}; // to display 'P'

        // Rectangles in which colons are displayed
        private Rectangle m_ColonRect1, m_ColonRect2;

        private int m_Width = 5;
        private int m_Padding = 2;

        #endregion

        #region Properties

        public LineCap Caps
        {
            get { return m_Cap; }
            set
            {

                m_Cap = value;

                if (m_Linewidth < 2f)
                    m_Cap = LineCap.Square;

                m_LitPen.StartCap = m_Cap;
                m_LitPen.EndCap = m_Cap;

                m_DimPen.StartCap = m_Cap;
                m_DimPen.EndCap = m_Cap;

                m_LitAltPen.StartCap = m_Cap;
                m_LitAltPen.EndCap = m_Cap;

                m_DimAltPen.StartCap = m_Cap;
                m_DimAltPen.EndCap = m_Cap;
            }
        }


        public AlarmColours Colours
        {
            set
            {
                m_LitPen.Color = value.DigitColour;
                m_LitPen.Width = m_Linewidth;

                m_LitPen.StartCap = m_Cap;
                m_LitPen.EndCap = m_Cap;

                m_DimPen.Color = value.DimDigitColour;
                m_DimPen.Width = m_Linewidth;

                m_DimPen.StartCap = m_Cap;
                m_DimPen.EndCap = m_Cap;

                m_LitBrush = ResourceManager.Brushes[value.DigitColour];
                m_DimBrush = ResourceManager.Brushes[value.DimDigitColour];
            }
        }
        
        public AlarmColours AltColours
        {
            set
            {
                m_LitAltPen.Color = value.DigitColour;
                m_LitAltPen.Width = m_Linewidth;

                m_LitAltPen.StartCap = m_Cap;
                m_LitAltPen.EndCap = m_Cap;

                m_DimAltPen.Color = value.DimDigitColour;
                m_DimAltPen.Width = m_Linewidth;

                m_DimAltPen.StartCap = m_Cap;
                m_DimAltPen.EndCap = m_Cap;

                m_LitAltBrush = ResourceManager.Brushes[value.DigitColour];
                m_DimAltBrush = ResourceManager.Brushes[value.DimDigitColour];
            }
        }

        public float LineWidth
        {
            get { return m_Linewidth; }
            set
            {
                m_Linewidth = value;

                m_LitPen.Width = m_Linewidth;
                m_DimPen.Width = m_Linewidth;
                m_LitAltPen.Width = m_Linewidth;
                m_DimAltPen.Width = m_Linewidth;

                Caps = Caps; 
            }
        }

        public int Width
        {
            get
            {
                return m_Width; 
            }
        }

        public int Padding
        {
            get
            {
                return m_Padding;
            }

            set
            {
                m_Padding = value;
            }
        }

        public int Stride
        {
            get
            {
                return m_Width + m_Padding;
            }
        }

        #endregion

        // Constructor takes a rectangle and prepares the end points
        // of the lines to be drawn for the clock
        internal DigitalDisplay(Rectangle rect)
        {
            Caps = LineCap.Triangle;

            m_Points = new Point[14]; // there are 7 lines in a display

            for (int i = 0; i < 14; i++)
                m_Points[i] = new Point(0, 0);

            CalculateAllParameters(rect);

            ResourceManager.Loading += new ResourceManager.EnsureSkinElementsEvent(ResourceManager_Loading);
            ResourceManager.Cleared += new ResourceManager.ClearedEvent(ResourceManager_Cleared);
            ResourceManager.Clensing += new ResourceManager.MarkInUseEvent(ResourceManager_Clensing);
        }

        void ResourceManager_Loading()
        {
            
        }

        void ResourceManager_Clensing(ResourceManager.ClenseMode mode, ResourceMarkers markers)
        {
            ResourceManager_Cleared(); 
        }

        void ResourceManager_Cleared()
        {
            m_LitBrush = null;
            m_DimBrush = null;
            m_LitAltBrush = null;
            m_DimAltBrush = null; 
        }

        #region Draw Methods

        // This function is called by the paint method to display the numbers
		// A set of bits in the 'displayNum' variable define which of the
		// display legs to display
		// Based on this the ones with a '1' are in bright color and the rest
		// with '0's are in a dull color giving the effect of a digital clock
		internal void Draw(Graphics g, int num, int offset, ColourType colour)
		{
            Matrix transform = g.Transform;

			int check; // used to check if a leg of digit should be bright or dull

            Pen lit = colour == ColourType.Regular ? m_LitPen : m_LitAltPen;
            Pen dim = colour == ColourType.Regular ? m_DimPen : m_DimAltPen; 

			// although pens are global linewidths are specific to each instance
            // lit.Width = dim.Width = m_Linewidth;
            g.TranslateTransform(offset, 0); 

			for (int i=0; i<7; i++)
			{
				check = (int)System.Math.Pow(2, i);
				if ((check & m_DisplayNum[num])==0)
                    g.DrawLine(dim, m_Points[i * 2], m_Points[i * 2 + 1]);
				else
                    g.DrawLine(lit, m_Points[i * 2], m_Points[i * 2 + 1]);
			}

            //g.ResetTransform();
            g.Transform = transform; 
		}

		// function that draws a colon in the middle of the rectangular panel
		// possible modes are circular or rectangular points in the colon
        internal void DrawColon(Graphics g, ColonType type, bool dim, int offset, ColourType colour)
		{
            Matrix transform = g.Transform; 

            g.TranslateTransform(offset, 0);

            if (m_LitBrush == null || m_LitAltBrush == null)
            {
                m_LitBrush = ResourceManager.Brushes[m_LitPen.Color];
                m_DimBrush = ResourceManager.Brushes[m_DimPen.Color];
                m_LitAltBrush = ResourceManager.Brushes[m_LitAltPen.Color];
                m_DimAltBrush = ResourceManager.Brushes[m_DimAltPen.Color];
            }

            Brush b = (dim) ? (colour == ColourType.Regular ? m_DimBrush : m_DimAltBrush)
                          : (colour == ColourType.Regular ? m_LitBrush : m_LitAltBrush);			

			switch(type)
			{
				case ColonType.Circular:
                    SmoothingMode mode = g.SmoothingMode;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    g.FillEllipse(b, m_ColonRect1);
                    g.FillEllipse(b, m_ColonRect2);
                    
                    g.SmoothingMode = mode; 
					break;
				case ColonType.Rectangular:
                    g.FillRectangle(b, m_ColonRect1);
                    g.FillRectangle(b, m_ColonRect2);
					break;
			}

            //g.ResetTransform(); 
            g.Transform = transform; 
		}
        
		// Draws the complete rectangle in dim shade to give the digital effect :-)
		internal void Draw(Graphics g, bool dim, int offset, ColourType colour)
		{
            Matrix transform = g.Transform; 

            g.TranslateTransform(offset, 0); 

            Pen p = (dim) ? (colour == ColourType.Regular ? m_DimPen : m_DimAltPen) 
                          : (colour == ColourType.Regular ? m_LitPen : m_LitAltPen);			

			for (int i=0; i<7; i++)
				g.DrawLine(p, m_Points[i*2], m_Points[i*2+1]);

            //g.ResetTransform(); 
            g.Transform = transform; 
		}

        // Overloaded function to display characters 'A' and 'P' for AM and PM
        // Using the same algorithm used to display numbers above
        internal void Draw(Graphics g, char ch, bool dim, int offset, ColourType colour)                        
        {
            // 10 and 11 are indices of A and P in the displayNum array
            switch (Char.ToUpper(ch))
            {
                case 'A':
                    Draw(g, 10, offset, colour);
                    break;
                case 'P':
                    Draw(g, 11, offset, colour);
                    break;
            }
        }

        #endregion 

        #region Rectange Setup

        internal void CalculateAllParameters(Rectangle rect)
		{
            m_Width = rect.Width; 

            int width = (int)(rect.Width/6);

            if (width < 2) 
                width = 2;
            if (width > 20) 
                width = 20;

            LineWidth = (float)width; 

			CalculateLineEnds(rect);
			CalculateColonRectangles(rect);
		}

		// Function calculates end points of lines to display
		// The draw function will draw lines using this data
		private void CalculateLineEnds(Rectangle rect)
		{
			// 0,1,2,9,10,11,12 points share the same left edge X coordinate
			m_Points[0].X = m_Points[1].X = m_Points[2].X = m_Points[9].X = 
				m_Points[10].X = m_Points[11].X = m_Points[12].X = rect.Left;
 
			// points 3,4,5,6,7,8,13 the right edge X coordinate
			m_Points[3].X = m_Points[4].X = m_Points[5].X = m_Points[6].X =
					m_Points[7].X = m_Points[8].X = m_Points[13].X= rect.Right-(int)m_Linewidth;

			// Points 1,2,3,4 are the top most points
			m_Points[1].Y = m_Points[2].Y = m_Points[3].Y = m_Points[4].Y = (int)(rect.Top);

			// Points 0,11,12,13,5,6 are the middle points
			m_Points[0].Y = m_Points[11].Y = m_Points[12].Y = m_Points[13].Y =
						m_Points[5].Y = m_Points[6].Y = 
							rect.Top + (int)((rect.Height-m_Linewidth)/2.0);
			// points 7,8,9,10 are on the bottom edge
			m_Points[7].Y = m_Points[8].Y = m_Points[9].Y = m_Points[10].Y 
							= rect.Top + (int)(rect.Height-m_Linewidth);

			// now adjust the coordinates that were computed, to get the digital look
			AdjustCoordinates();
		}
	
		// This function is necessary to give the lines a digital clock look
		// Push the coordinates a little away so that they look apart
		private void AdjustCoordinates()
		{
			Point swap; // required in case points have to be swapped
			for (int i=0; i<7; i++)
			{
				// Always draw from left to right and top to bottom
				// Adjust the end points accordingly
				if (m_Points[i*2].X > m_Points[(i*2)+1].X || m_Points[i*2].Y > m_Points[(i*2)+1].Y)
				{
					swap = m_Points[i*2]; m_Points[i*2]= m_Points[(i*2)+1]; m_Points[(i*2)+1]=swap;
				}

				// for horizontal lines adjust the X coord
				if (m_Points[i*2].X != m_Points[(i*2)+1].X)
				{
					m_Points[i*2].X += (int)(m_Linewidth/1.6);
					m_Points[(i*2)+1].X -= (int)(m_Linewidth/1.6);
				}
				// for vertical lines adjust the y coord
				if (m_Points[i*2].Y != m_Points[(i*2)+1].Y)
				{
					m_Points[i*2].Y += (int)(m_Linewidth/1.6);
					m_Points[(i*2)+1].Y -= (int)(m_Linewidth/1.6);
				}
			}
		}

		// function to calculate the rectangles required to drawn colon dot inside
		private void CalculateColonRectangles(Rectangle rect)
		{
            int width = rect.Width / 3;

			m_ColonRect1 = m_ColonRect2 = rect;
            m_ColonRect1.X = m_ColonRect2.X = rect.X + (int)((rect.Width - width) / 2.0);
			m_ColonRect1.Y = rect.Y + rect.Height/3;
			m_ColonRect2.Y = rect.Y + (rect.Height*2)/3;

			m_ColonRect1.Width = m_ColonRect1.Height =
                m_ColonRect2.Width = m_ColonRect2.Height = (int)width; //  m_Linewidth;
        }

        #endregion 
    }
}
