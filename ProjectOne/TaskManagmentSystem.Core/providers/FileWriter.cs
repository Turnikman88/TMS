using System;
using System.IO;
using TaskManagmentSystem.Core.providers.Contracts;

namespace TaskManagmentSystem.Core.providers
{
    public class FileWriter : IWriter
    {
        ///private StreamWriter writer = new StreamWriter(Constants.PATH_TO_DATABASE + "CommandHistory.txt");
        private string path;
        public FileWriter(string path)
        {
            this.path = path;
        }
        public void Write(string value)
        {
            File.AppendAllText(this.path, value);
        }

        public void WriteLine(string value)
        {
            File.AppendAllText(this.path, value + Environment.NewLine);
        }
    }
}
