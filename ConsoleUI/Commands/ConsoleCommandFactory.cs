using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    internal class ConsoleCommandFactory
    {

        public static ConsoleView ConsoleView { get; set; }

        private static Dictionary<string, IConsoleCommand> _commands = new Dictionary<string, IConsoleCommand>();

        static ConsoleCommandFactory()
        {
            _commands[ReadAlphaNumericIdentifierCommand.ReadAlphaNumericIdentifierKey] = new ReadAlphaNumericIdentifierCommand();
            _commands[ReadIncrementValueCommand.ReadIncrementValueKey] = new ReadIncrementValueCommand();
            _commands[ExitCommand.ExitKey] = new ExitCommand();
        }

        public static void RunCommand(string key)
        {
            if (_commands.ContainsKey(key))
            {
                _commands[key].ConsoleView = ConsoleView;
                _commands[key].Run();
            }
        }

        public static bool IsCommand(string key)
        {
            return _commands.ContainsKey(key);
        }

        public static List<string> GetCommandDescriptions()
        {
            return _commands.Select(x => x.Value.Description).ToList();
        }
    }
}