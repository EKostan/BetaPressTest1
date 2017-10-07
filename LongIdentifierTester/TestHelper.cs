using System;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace LongIdentifierTester
{
    internal class TestHelper
    {
        public static string DataDirectory => Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\Data"));
        private const char ZeroChar = '0';
        private static Random _random = new Random();

        public static void GetRandomStringNumber(long length, out string stringNumberA, out string stringNumberB)
        {
            var stringBuilderA = new StringBuilder();
            var stringBuilderB = new StringBuilder();


            for (int i = 0; i < length; i++)
            {
                stringBuilderA.Append(GetRandomDigit());
                stringBuilderB.Append(GetRandomDigit());
            }

            stringNumberA = stringBuilderA.ToString();
            stringNumberB = stringBuilderB.ToString();
        }

        public static char GetRandomDigit()
        {
            char z = ZeroChar;
            var r = _random.Next(0, 9);
            z += (char)r;
            return z;
        }

        public static string LoadStringNumber(string fileName)
        {
            return File.ReadAllText(Path.Combine(DataDirectory, fileName));
        }
    }
}