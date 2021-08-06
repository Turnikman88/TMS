using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManagmentSystem.Models
{
    public static class Model
    {
        public static string GenerateLogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            string logo = @"

▄▄▄█████▓ ███▄ ▄███▓  ██████ 
▓  ██▒ ▓▒▓██▒▀█▀ ██▒▒██    ▒ 
▒ ▓██░ ▒░▓██    ▓██░░ ▓██▄   
░ ▓██▓ ░ ▒██    ▒██   ▒   ██▒
  ▒██▒ ░ ▒██▒   ░██▒▒██████▒▒
  ▒ ░░   ░ ▒░   ░  ░▒ ▒▓▒ ▒ ░
    ░    ░  ░      ░░ ░▒  ░ ░
  ░      ░      ░   ░  ░  ░  
                ░         ░  
Type 'help' to see list of all commands...
";
            
            return logo;
        }
    }
}
