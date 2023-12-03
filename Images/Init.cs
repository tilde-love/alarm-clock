using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rug.UI.Util;

namespace AlarmClock.Images
{
    class Init : Initiliser
    {
        public Init() : base()
        {

        }

        public override void InitiliseImageFolders()
        {
            base.InitiliseImageFolders();

            if (!m_PathAdded)
            {
                PackedImageSource m_Embeded = new PackedImageSource(typeof(Init), "images.rpa");
                Rug.UI.Util.ImageHelper.RegisterEmbededFolder("alarm", m_Embeded); 

                m_PathAdded = true;
                //Rug.UI.Util.ImageHelper.RegisterEmbededFolder("alarm", typeof(Init));                
            }
        }

        private static bool m_PathAdded = false;
    }
}
