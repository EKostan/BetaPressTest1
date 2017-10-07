using System;
using LongIdentifierLibrary;
using Test1ApplicationCore;

namespace ConsoleUI
{
    internal class Program
    {
        static ConsoleView _consoleView = new ConsoleView();

        private static void Main(string[] args)
        {
            var presenter = new LongIdentifiePresenter(_consoleView);
            ConsoleCommandFactory.ConsoleView = _consoleView;

            ConsoleCommandFactory.RunCommand(ReadAlphaNumericIdentifierCommand.ReadAlphaNumericIdentifierKey);
            try
            {

                while (true)
                {
                    WriteCommandInfo();

                    var readedLine = Console.ReadLine();

                    if (string.IsNullOrEmpty(readedLine))
                    {
                        continue;
                    }

                    ConsoleCommandFactory.RunCommand(readedLine.ToLower());
                }
            }
            catch (ExitCommandException)
            {

            }
            catch (Exception e)
            {
                Console.WriteLine($"Во время выполнения программы возникла ошибка: {e}.");
                Console.ReadLine();
            }
        }

        private static void WriteCommandInfo()
        {
            Console.WriteLine();
            ConsoleCommandFactory.GetCommandDescriptions().ForEach(Console.WriteLine);
        }
    }
}
