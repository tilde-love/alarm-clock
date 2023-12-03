using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace AlarmClock
{
    public class Time
    {
        [Flags]
        public enum DaysOfTheWeek
        {
            Disabled = 0, Mon = 1, Tue = 2, Wed = 4, Thur = 8, Fri = 16, Sat = 32, Sun = 64, 
            EveryDay = Mon | Tue | Wed | Thur | Fri | Sat | Sun,
            Weekends = Sat | Sun,
            WeekDays = Mon | Tue | Wed | Thur | Fri
        }

        public int Hour;
        public int Minute;
        public int Seconds;
        public DaysOfTheWeek Days = DaysOfTheWeek.Disabled;
        public bool Enabled = true; 

        public readonly int c_Seconds = 0;
        
        public Time()
        {

        }

        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1} / {2}",                
                Hour.ToString().PadLeft(2, '0'),
                Minute.ToString().PadLeft(2, '0'), 
                Days.ToString());
                
        }

        public static Time Parse(string str)
        {
            // split the string using the ':' as the seperator 
            string[] parts = str.Split(':', '/');

            Time time = new Time();

            // parse the time 
            // TODO: error checking 
            //time.Enabled = enabled; 
            time.Hour = int.Parse(parts[0].Trim());
            time.Minute = int.Parse(parts[1].Trim());

            if (parts.Length > 2)
                time.Days = (DaysOfTheWeek)Enum.Parse(typeof(DaysOfTheWeek), parts[2].Trim());
            else
                time.Days = DaysOfTheWeek.Disabled; 

            return time;
        }

        public DateTime ToDateTime()
        {
            string time = string.Format("{0}:{1}", Hour.ToString().PadLeft(2, '0'), Minute.ToString().PadLeft(2, '0'));

            return DateTime.ParseExact(time, "HH:mm", CultureInfo.InvariantCulture); 
        }
    }
}
