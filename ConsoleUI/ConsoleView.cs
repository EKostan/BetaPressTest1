using System;
using Test1ApplicationCore;

namespace ConsoleUI
{
    class ConsoleView : ILongIdentifierView
    {
        public string AlphaNumericIdentifier { get; set; }
        public string IncrementValue { get; set; }
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public event EventHandler InitAlphaNumericIdentifier;
        public event EventHandler IncrementIdentifier;

        public virtual void OnInitAlphaNumericIdentifier()
        {
            InitAlphaNumericIdentifier?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnIncrementIdentifier()
        {
            IncrementIdentifier?.Invoke(this, EventArgs.Empty);
        }
       
    }
}