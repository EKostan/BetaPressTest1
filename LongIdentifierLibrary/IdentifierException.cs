using System;

namespace LongIdentifierLibrary
{
    public class IdentifierException : Exception
    {
        public readonly string AlphaNumericIdentifier;

        public IdentifierException(string alphaNumericIdentifier)
            : base($"Идентификатор {alphaNumericIdentifier} должен содержать только буквы и цифры")
        {
            AlphaNumericIdentifier = alphaNumericIdentifier;
        }
    }
}