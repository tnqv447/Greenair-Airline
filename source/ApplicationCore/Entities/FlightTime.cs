using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using ApplicationCore;
namespace ApplicationCore.Entities
{
    [Owned]
    public class FlightTime : ValueObject
    {
        private string toStringFormat = "{0}h {1}m";
        private string toValueFormat = "\\D+";

        public int Hour { get; private set; }
        public int Minute { get; private set; }

        public FlightTime() { }

        public FlightTime(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
        public FlightTime(int time)
        {
            this.toHours(time);
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Hour;
            yield return Minute;
        }
        public string toString() => string.Format(toStringFormat, this.Hour, this.Minute);
        public void toValue(string time)
        {
            IList<string> list = StringExec.RegexSplit(time, toValueFormat);
            this.Hour = int.Parse(list[0]);
            this.Minute = int.Parse(list[1]);
        }
        public int toMinutes()
        {
            return Hour * 60 + Minute;
        }
        private void toHours(int time)
        {
            this.Hour = time / 60;
            this.Minute = time % 60;
        }
        public static FlightTime operator +(FlightTime x, FlightTime y) => new FlightTime(x.toMinutes() + y.toMinutes());
    }

}