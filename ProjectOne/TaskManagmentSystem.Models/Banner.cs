using System;
using System.IO;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Models
{
    public static class Banner
    {
        public static string GenerateLogo()
        {
            Console.SetWindowSize(175, 35);
            Console.ForegroundColor = ConsoleColor.Magenta;

            string end = "Type 'help' to see list of all commands...";
            var logo = File.ReadAllText(Constants.PATH_TO_DATABASE + "logos.txt").Split(',');
            var r = new Random();
            int num = r.Next(0, logo.Length);

            return $"{logo[num]}\n{end}";
        }
    }
}
