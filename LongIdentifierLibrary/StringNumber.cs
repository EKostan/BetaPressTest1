using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LongIdentifierLibrary
{
    public class StringNumber
    {
        private const char ZeroChar = '0';
        private readonly string _stringValue;

        public static StringNumber ZeroStringNumber = new StringNumber(string.Empty);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"> Возникает если строка содержит не только цисла</exception>
        /// <param name="stringValue"></param>
        protected StringNumber(string stringValue)
        {
            _stringValue = stringValue;
        }

        public static StringNumber Parse(string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return ZeroStringNumber;
            }

            if (!stringValue.All(char.IsDigit))
            {
                throw new ArgumentException("Строка должна содержать только цифры");
            }

            return new StringNumber(stringValue);
        }

        public string Value => _stringValue;

        public override string ToString()
        {
            return _stringValue;
        }

        public StringNumber Increment(StringNumber stringNumber, Func<bool> isCancelled)
        {
            if (string.IsNullOrEmpty(Value) && string.IsNullOrEmpty(stringNumber.Value))
            {
                return ZeroStringNumber;
            }

            if (string.IsNullOrEmpty(Value))
            {
                return stringNumber;
            }

            if (string.IsNullOrEmpty(stringNumber.Value))
            {
                return this;
            }

            if (IsLessThan(Value, stringNumber.Value))
                return AddLessLeftNumber(Value, stringNumber.Value, isCancelled);

            return AddLessLeftNumber(stringNumber.Value, Value, isCancelled);
        }

        public StringNumber Increment(StringNumber stringNumber)
        {
            return Increment(stringNumber, () => false);
        }

        private static bool IsLessThan(string leftStringValue, string rightStringValue)
        {
            return leftStringValue.Length > rightStringValue.Length;
        }

        private static StringNumber AddLessLeftNumber(string leftStringValue, string rightStringValue, Func<bool> isCancelled)
        {
            var newChars = new char[leftStringValue.Length];
            byte carry = 0;
            int leftIndex = leftStringValue.Length - 1;

            for (int rightIndex = rightStringValue.Length - 1; leftIndex >= 0; leftIndex--, rightIndex--)
            {
                var leftChar = leftStringValue[leftIndex];

                if (rightIndex >= 0)
                {
                    var rightChar = rightStringValue[rightIndex];
                    newChars[leftIndex] = ByteToChar(Sum(CharToDigit(leftChar), CharToDigit(rightChar), ref carry));
                }
                else
                {
                    newChars[leftIndex] = carry > 0
                        ? ByteToChar(Sum(CharToDigit(leftChar), 0, ref carry))
                        : leftChar;
                }

                if (isCancelled())
                {
                    throw new OperationCanceledException("Операция была прервана пользователем");
                }
            }

            if (carry > 0)
            {
                return new StringNumber(string.Concat(ByteToChar(carry), new string(newChars)));
            }

            return new StringNumber(new string(newChars));
        }

        private static byte Sum(byte leftChar, byte rightChar, ref byte carry)
        {
            var sum = leftChar + rightChar + carry;
            carry = 0;

            if (sum > 9)
            {
                carry = 1;
                return (byte)(sum-10);
            }

            return (byte)sum;
        }

        private static byte CharToDigit(char charValue)
        {
            return (byte)(charValue - ZeroChar);
        }

        private static char ByteToChar(byte value)
        {
            char z = ZeroChar;
            z += (char)(value);
            return z;
        }
    }
}