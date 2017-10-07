using System;

namespace ConsoleUI
{
    internal class ExitCommand : IConsoleCommand
    {
        public const string ExitKey = "exit";

        public ExitCommand()
        {
            Key = ExitKey;
            Description = $"Для выхода введите \"exit\"";
        }

        public string Key { get; }

        public string Description { get; }

        public ConsoleView ConsoleView { get; set; }

        public void Run()
        {
            Console.WriteLine($"Программа завершила свою работу.");
            Console.ReadLine();
            throw new ExitCommandException();
        }
    }
}