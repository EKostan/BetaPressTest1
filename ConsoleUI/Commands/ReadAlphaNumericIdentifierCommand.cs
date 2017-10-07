using System;

namespace ConsoleUI
{
    internal class ReadAlphaNumericIdentifierCommand : IConsoleCommand
    {
        public const string ReadAlphaNumericIdentifierKey = "id";

        public ReadAlphaNumericIdentifierCommand()
        {
            Key = ReadAlphaNumericIdentifierKey;
            Description = $"Для ввода буквенно-числового идентификатора введите \"id\"";
        }

        public string Key { get; }

        public string Description { get; }

        public ConsoleView ConsoleView { get; set; }

        public void Run()
        {
            Console.WriteLine($"Введите буквенно-числовой идентификатор");
            ConsoleView.AlphaNumericIdentifier = Console.ReadLine();
            ConsoleView.OnInitAlphaNumericIdentifier();
        }
    }
}