using System;

namespace ConsoleUI
{
    internal class ReadIncrementValueCommand : IConsoleCommand
    {
        public const string ReadIncrementValueKey = "c";


        public ReadIncrementValueCommand()
        {
            Key = ReadIncrementValueKey;
            Description = $"��� ����� ������������� ��������� ������� \"c\"";
        }

        public string Key { get; }

        public string Description { get; }

        public ConsoleView ConsoleView { get; set; }

        public void Run()
        {
            //Console.WriteLine();
            Console.WriteLine($"������� ������������� ��������:");
            ConsoleView.IncrementValue = Console.ReadLine();
            ConsoleView.OnIncrementIdentifier();
        }
    }
}