using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Rug.UI.Util;

namespace AlarmClock
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new MainForm());
            }
            /*catch (Exception ex) 
            {
                MessageBox.Show("Unexpected excption.\n" + ex.Message);
            } */  
            finally
            {
                ResourceManager.Clear();
            }
        }
    }
}
