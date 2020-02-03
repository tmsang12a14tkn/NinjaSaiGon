using System;
using System.Globalization;

namespace NinjaSaiGon.Admin.Helpers
{
    public static class Utilities
    {
        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }

        public static DateTime GetLastDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime lastDayInWeek = dayInWeek.Date;
            while (lastDayInWeek.DayOfWeek != firstDay)
                lastDayInWeek = lastDayInWeek.AddDays(1);

            return lastDayInWeek;
        }

        public static DateTime GetFirstDateOfWeek(int year, int weekOfYear, DayOfWeek firstDay = DayOfWeek.Monday)
        {
            DateTime jan1 = new DateTime(year, 1, 1);

            DateTime firstDayInWeek = jan1.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek.AddDays(7 * weekOfYear);
        }

        public static DateTime GetLastDateOfWeek(int year, int weekOfYear, DayOfWeek lastDay = DayOfWeek.Sunday)
        {
            DateTime jan1 = new DateTime(year, 1, 1);

            DateTime lastDayInWeek = jan1.Date;
            while (lastDayInWeek.DayOfWeek != lastDay)
                lastDayInWeek = lastDayInWeek.AddDays(1);

            return lastDayInWeek.AddDays(7 * weekOfYear);

        }

        public static string FormatDatetime(DateTime date)
        {
            var dtFI = new CultureInfo("vi-VN", false).DateTimeFormat;
            dtFI.DayNames = new[] { "Chủ nhật", "Thứ 2", "Thứ 3", "Thứ 4", "Thứ 5", "Thứ 6", "Thứ 7" };
            return date.ToString("dddd - dd/MM/yyyy", dtFI);
        }
    }
}
