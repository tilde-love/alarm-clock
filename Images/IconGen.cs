using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Rug.UI.Util;

namespace AlarmClock.Images
{
    public class IconGen
    {
        public string GetImageKey(Color colour)
        {
            return ResourceManager.ColorisedImages.GetKey("~/icon-base.bmp", colour); 
        }

        public Image GetImage(Color colour)
        {
            return ResourceManager.ColorisedImages["~/icon-base.bmp", colour];
        }

        public Image GetImage(AlarmColours colours)
        {
            Bitmap bmp;

            if (colours.Icon != null)
                bmp = colours.Icon;
            else
            {
                bmp = new Bitmap(16, 15, PixelFormat.Format32bppArgb);
                colours.Icon = bmp;
            }

            Graphics g = Graphics.FromImage(bmp);                        
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            
            g.Clear(Color.Transparent);

            Bitmap lit = ResourceManager.Images["~/alarm/icon-lit.bmp"];
            Bitmap dim = ResourceManager.Images["~/alarm/icon-dim.bmp"];
            Bitmap @base = ResourceManager.ColorisedImages["~/icon-base.bmp", colours.BackColour];

            dim = ImageHelper.GetLumToAlphaMap(dim, colours.DimDigitColour);
            lit = ImageHelper.GetLumToAlphaMap(lit, colours.DigitColour);

            Point p = new Point(0,0);
            g.DrawImage(@base, p);
            g.DrawImage(lit, p);
            g.DrawImage(dim, p);

            lit.Dispose();
            dim.Dispose();

            g.Dispose();

            return bmp; 
        }
    }
}
