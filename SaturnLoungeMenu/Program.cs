using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;

namespace SaturnLoungeMenu
{
    internal class Program
    {
        private const string DownloadUrl = @"http://www.saturn-lounge.at/wp-content/uploads/menu.pdf";

        private static void Main()
        {
            var menuFile = EnsureMenuFile();
            Process.Start(menuFile.FullName);
        }

        private static FileInfo EnsureMenuFile()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var weekNr = currentCulture.Calendar.GetWeekOfYear(
                DateTime.Today,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

            var fileName = Path.Combine(Path.GetTempPath(), $"saturnloungemenu_{weekNr}.pdf");

            if (!File.Exists(fileName))
                using (var client = new WebClient())
                {
                    client.DownloadFile(DownloadUrl, fileName);
                }

            return new FileInfo(fileName);
        }
    }
}