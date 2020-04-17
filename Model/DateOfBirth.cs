using System;

namespace Model
{
    public class DateOfBirth
    {
        public int Date { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public DateOfBirth()
        {
        }

        public DateOfBirth(DateTime time)
        {
            Date = time.Day;
            Month = time.Month;
            Year = time.Year;
        }

        public DateOfBirth(int date, int month, int year)
        {
            Date = date;
            Month = month;
            Year = year;
        }

        //TODO check dates
        public DateOfBirth(string s)
        {
            var split = s.Split('.');
            Date = int.Parse(split[0]);
            Month = int.Parse(split[1]);
            Year = int.Parse(split[2]);
        }

        public override string ToString()
        {
            return $"{Date}/{Month}/{Year}";
        }
    }
}