using System;

namespace ConsoleUI
{
    internal class ReadIncrementValueCommand : IConsoleCommand
    {
        public const string ReadIncrementValueKey = "c";


        public ReadIncrementValueCommand()
        {
            Key = ReadIncrementValueKey;
            Description = $"Для ввода целочисленный параметра введите \"c\"";
        }

        public string Key { get; }

        public string Description { get; }

        public ConsoleView ConsoleView { get; set; }

        public void Run()
        {
            //Console.WriteLine();
            Console.WriteLine($"Введите целочисленный параметр:");
            ConsoleView.IncrementValue = Console.ReadLine();
            ConsoleView.OnIncrementIdentifier();
        }
    }
}