using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApplicationCore.DTOs
{
    public class FlightTimeDTO : ValueObject
    {
        private string toStringFormat = "{0}h {1}m";
        private string toValueFormat = "\\D+";

        public int Hour { get; set; }
        public int Minute { get; set; }

        public FlightTimeDTO() { }

        public FlightTimeDTO(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
        }
        public FlightTimeDTO(int time)
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
            return this.Hour * 60 + this.Minute;
        }
        private void toHours(int time)
        {
            this.Hour = time / 60;
            this.Minute = time % 60;
        }
        public static FlightTimeDTO operator +(FlightTimeDTO x, FlightTimeDTO y) => new FlightTimeDTO(x.toMinutes() + y.toMinutes());
    }

}