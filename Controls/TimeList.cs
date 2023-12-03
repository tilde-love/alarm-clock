using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Rug.UI;

namespace AlarmClock.Controls
{
    public partial class TimeList : CheckboxList<Time>
    {
        public TimeList()
            : base()
        {
            InitializeComponent();
        }
    }
}
