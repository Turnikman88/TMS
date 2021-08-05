using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Models
{
    public static class Model
    {
        public static string GenerateLogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            string end = "Type 'help' to see list of all commands...";
            var logo = File.ReadAllText(Constants.PATH_TO_DATABASE + "logos.txt").Split(new string[] { "\r\n\r\n" },
                               StringSplitOptions.RemoveEmptyEntries);
            var r = new Random();
            int num = r.Next(0, logo.Length);


            return $"{logo[num]}\n{end}";
        }
    }
}
