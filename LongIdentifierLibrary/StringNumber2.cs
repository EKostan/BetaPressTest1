using System;
using System.Text;

namespace LongIdentifierLibrary
{
    public class StringNumber2
    {
        private const char ZeroChar = '0';
        private readonly byte[] _bytes;

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"> Возникает если строка содержит не только цисла</exception>
        /// <param name="bytes"></param>
        protected StringNumber2(byte[] bytes)
        {
            _bytes = bytes;
        }

        public static StringNumber2 Parse(string stringValue)
        {
            if (!stringValue.IsDigit())
            {
                throw new ArgumentException("Строка должна содержать только цифры");
            }

            byte[] bytes = GetBytes(stringValue);

            return new StringNumber2(bytes);
        }

        private static byte[] GetBytes(string stringValue)
        {
            var bytes = new byte[stringValue.Length];
            for (int i = 0; i < stringValue.Length; i++)
            {
                bytes[i] = (byte)(stringValue[i] - ZeroChar);
            }
            return bytes;
        }

        public override string ToString()
        {
            return new string(GetChars(_bytes));
        }

        

        private char[] GetChars(byte[] bytes)
        {
            var newChars = new char[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
            {
                newChars[i] = ByteToSymbol(bytes[i]);
            }

            return newChars;
        }

        private char ByteToSymbol(byte intValue)
        {
            char z = ZeroChar;
            z += (char)(intValue);
            return z;
        }

        public static StringNumber2 operator +(StringNumber2 leftStringNumber, StringNumber2 rightStringNumber)
        {
            if (IsLessThan(leftStringNumber._bytes, rightStringNumber._bytes))
                return AddLessLeftNumber(leftStringNumber._bytes, rightStringNumber._bytes);

            return AddLessLeftNumber(rightStringNumber._bytes, leftStringNumber._bytes);
        }

        private static bool IsLessThan(byte[] leftStringValue, byte[] rightStringValue)
        {
            return leftStringValue.Length > rightStringValue.Length;
        }

        private static StringNumber2 AddLessLeftNumber(byte[] leftBytes, byte[] rightBytes)
        {
            var newBytes = new byte[leftBytes.Length];
            byte carry = 0;
            var leftIndex = leftBytes.Length - 1;

            for (int rightIndex = rightBytes.Length - 1; leftIndex >= 0; leftIndex--, rightIndex--)
            {
                var leftByte = leftBytes[leftIndex];
                byte rightByte = 0;

                if (rightIndex >= 0)
                {
                    rightByte = rightBytes[rightIndex];
                }

                newBytes[leftIndex] = Sum(leftByte, rightByte, ref carry);
            }

            if (carry > 0)
            {
                var newBytesEx = new byte[newBytes.Length+1];
                newBytesEx[0] = carry;
                Array.Copy(newBytes, 0, newBytesEx, 1, newBytes.Length);
                return new StringNumber2(newBytesEx);
            }

            return new StringNumber2(newBytes);
        }

        private static byte Sum(byte leftChar, byte rightChar, ref byte carry)
        {
            var sum = leftChar + rightChar + carry;

            if (sum > 9)
            {
                carry = 1;
                return (byte)(sum - 10);
            }

            carry = 0;
            return (byte)sum;
        }
    }
}