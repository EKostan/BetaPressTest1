namespace ConsoleUI
{
    internal interface IConsoleCommand
    {
        string Key { get; }

        string Description { get; }

        ConsoleView ConsoleView { get; set; }

        void Run();
    }
}