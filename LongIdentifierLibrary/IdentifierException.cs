using System;

namespace LongIdentifierLibrary
{
    public class IdentifierException : Exception
    {
        public readonly string AlphaNumericIdentifier;

        public IdentifierException(string alphaNumericIdentifier)
            : base($"������������� {alphaNumericIdentifier} ������ ��������� ������ ����� � �����")
        {
            AlphaNumericIdentifier = alphaNumericIdentifier;
        }
    }
}