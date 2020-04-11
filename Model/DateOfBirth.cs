using System;

namespace Model
{
    public class DateOfBirth
    {
        public int Date { get; private set; }
        public int Month { get; private set; }
        public int Year { get; private set; }

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
    }
}