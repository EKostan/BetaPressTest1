using System;

namespace Test1ApplicationCore
{
    public interface ILongIdentifierView
    {
        string AlphaNumericIdentifier { get; set; }

        string IncrementValue { get; set; }

        void ShowMessage(string message);

        event EventHandler InitAlphaNumericIdentifier;

        event EventHandler IncrementIdentifier;
    }
}