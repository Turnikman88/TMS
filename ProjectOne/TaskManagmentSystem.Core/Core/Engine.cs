using ProjectOne.Commands.Contracts;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TaskManagmentSystem.Core.Contracts;
using TaskManagmentSystem.Core.providers;
using TaskManagmentSystem.Core.providers.Contracts;
using TaskManagmentSystem.Models;
using TaskManagmentSystem.Models.Common;

namespace TaskManagmentSystem.Core
{
    public class Engine : IEngine
    {
        private readonly ICommandFactory commandFactory;
        private readonly IWriter writer;
        private IWriter commandWriter;


        public Engine(ICommandFactory commandFactory, IWriter writer)
        {
            this.commandFactory = commandFactory;
            this.writer = writer;
            this.commandWriter = new FileWriter(Constants.PATH_TO_DATABASE + "CommandHistory.txt");
        }
        public void Start()
        {
            try // I prefer to have another try catch than putting this in the loop inside if ; It is one useless operation at every iteration 
            {
                ApplicationPrepearing();
            }
            catch (Exception)
            {
                writer.WriteLine("Something went wrong... Data may be lost!");
                Console.WriteLine(Model.GenerateLogo());
                Console.ForegroundColor = ConsoleColor.Cyan;
            }
            while (true)
            {
                try
                {
                    string inputLine = Console.ReadLine().Trim();

                    if (inputLine.ToLower() == Constants.TerminationCommand)
                    {
                        break;
                    }

                    ProcessCommand(inputLine);
                    SaveSuccsessfullCommands(inputLine);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        writer.WriteLine(ex.InnerException.Message.ToString());
                    }
                    else
                    {
                        writer.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void ProcessCommand(string commandLine)
        {
            Validator.ValidateObjectIsNotNULL(commandLine.Trim(), Constants.EmptyCommandError);

            ICommand command = this.commandFactory.Create(commandLine);
            string result = command.Execute();
            writer.WriteLine(result.Trim());

        }
        private void ApplicationPrepearing()
        {
            var commands = File.ReadAllText(Constants.PATH_TO_DATABASE + "CommandHistory.txt")
                .Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Substring(0, x.Length - 1)).ToList();
            if (commands.Count > 0)
            {
                commands.ForEach(x => ProcessCommand(x));
            }
            Console.Clear();
            Console.WriteLine(Model.GenerateLogo());
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        private void SaveSuccsessfullCommands(string command)
        {
            string commandName = command.Split()[0];
            var regex = new Regex(@"(show)|(help)|(exit)|(erasehistory)|(exporthistory)");
            if (!regex.IsMatch(commandName))
            {
                commandWriter.WriteLine(command);
            }
        }

    }
}
