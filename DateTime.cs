using System;

class DateTimeDemo
{
    static void Main()
    {
        // 1. Current Date and Time
        DateTime now = DateTime.Now;
        Console.WriteLine("Current DateTime: " + now);

        // 2. Date Only and Time Only
        Console.WriteLine("Date Only: " + now.Date);
        Console.WriteLine("Time Only: " + now.TimeOfDay);

        // 3. Specific Date Creation
        DateTime specific = new DateTime(2025, 7, 4, 12, 30, 0);
        Console.WriteLine("Specific DateTime: " + specific);

        // 4. Add and Subtract
        Console.WriteLine("Add 10 Days: " + now.AddDays(10));
        Console.WriteLine("Add 3 Months: " + now.AddMonths(3));
        Console.WriteLine("Subtract 5 Hours: " + now.AddHours(-5));

        // 5. Difference Between Dates
        DateTime future = new DateTime(2026, 1, 1);
        TimeSpan diff = future - now;
        Console.WriteLine("Days Until 2026: " + diff.Days);

        // 6. Comparing Dates
        DateTime date1 = DateTime.Now;
        DateTime date2 = DateTime.Now.AddDays(5);

        Console.WriteLine("date1 == date2: " + (date1 == date2));
        Console.WriteLine("date1 < date2: " + (date1 < date2));
        Console.WriteLine("date1.CompareTo(date2): " + date1.CompareTo(date2));

        // 7. Date Formatting
        Console.WriteLine("Long Date String: " + now.ToLongDateString());
        Console.WriteLine("Short Date String: " + now.ToShortDateString());
        Console.WriteLine("Long Time String: " + now.ToLongTimeString());
        Console.WriteLine("Short Time String: " + now.ToShortTimeString());
        Console.WriteLine("Custom Format: " + now.ToString("yyyy-MM-dd HH:mm:ss"));

        // 8. Parsing Strings to DateTime
        string dateStr = "2025-12-25 18:00";
        DateTime parsedDate = DateTime.Parse(dateStr);
        Console.WriteLine("Parsed DateTime: " + parsedDate);

        // 9. TryParse
        if (DateTime.TryParse("2025-02-30", out DateTime result))
        {
            Console.WriteLine("Valid Date: " + result);
        }
        else
        {
            Console.WriteLine("Invalid Date");
        }

        // 10. Date Properties
        Console.WriteLine("Year: " + now.Year);
        Console.WriteLine("Month: " + now.Month);
        Console.WriteLine("Day: " + now.Day);
        Console.WriteLine("Hour: " + now.Hour);
        Console.WriteLine("Minute: " + now.Minute);
        Console.WriteLine("Second: " + now.Second);
        Console.WriteLine("Day of Week: " + now.DayOfWeek);
        Console.WriteLine("Day of Year: " + now.DayOfYear);
        Console.WriteLine("Is Leap Year: " + DateTime.IsLeapYear(now.Year));

        // 11. MinValue and MaxValue
        Console.WriteLine("Min Value: " + DateTime.MinValue);
        Console.WriteLine("Max Value: " + DateTime.MaxValue);

        // 12. Ticks (1 tick = 100 nanoseconds)
        Console.WriteLine("Ticks: " + now.Ticks);

        // 13. UTC and Local Time
        Console.WriteLine("UTC Now: " + DateTime.UtcNow);
        Console.WriteLine("To Local Time: " + DateTime.UtcNow.ToLocalTime());

        // 14. DateOnly and TimeOnly (C# 10 / .NET 6+)
#if NET6_0_OR_GREATER
        DateOnly dateOnly = DateOnly.FromDateTime(DateTime.Now);
        TimeOnly timeOnly = TimeOnly.FromDateTime(DateTime.Now);
        Console.WriteLine("DateOnly: " + dateOnly);
        Console.WriteLine("TimeOnly: " + timeOnly);
#endif

        // 15. Using TimeSpan with DateTime
        TimeSpan duration = new TimeSpan(2, 30, 0); // 2.5 hours
        DateTime newTime = now.Add(duration);
        Console.WriteLine("New Time After Duration: " + newTime);

        // 16. Culture-specific Formatting
        Console.WriteLine("Culture Format (en-US): " + now.ToString("D", new System.Globalization.CultureInfo("en-US")));
        Console.WriteLine("Culture Format (fr-FR): " + now.ToString("D", new System.Globalization.CultureInfo("fr-FR")));
    }
}
