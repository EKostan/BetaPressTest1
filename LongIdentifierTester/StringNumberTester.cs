using System;
using System.Diagnostics;
using System.IO;
using LongIdentifierLibrary;
using NUnit.Framework;

namespace LongIdentifierTester
{
    [TestFixture]
    public class StringNumberTester
    {
        public string DataDirectory => Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\Data"));
        private Random _random = new Random();

        [Test]
        [TestCase("123")]
        [TestCase("9223372036854775807")]
        [TestCase("92233720368547758079223372036854775807922337203685477580792233720368547758079223372036854775807")]
        [TestCase("92233720368547758000000000000000000000000000000000000000000000000000000000000000000000004775807")]
        public void Test_Parse(string valueString)
        {
            var bigNUmber = StringNumber.Parse(valueString);
            var bigNumberString = bigNUmber.ToString();
            Assert.AreEqual(valueString, bigNumberString);
        }

        [Test]
        [TestCase("123", "234", "357")]
        [TestCase("9223372036854775807", "9223372036854775807", "18446744073709551614")]
        [TestCase("1223372036854775807", "922337203685477", "1224294374058461284")]
        public void Test_Add(string valueStringA, string valueStringB, string resultString)
        {
            var sw = new Stopwatch();
            sw.Start();

            var bigNUmberA = StringNumber.Parse(valueStringA);
            var bigNUmberB = StringNumber.Parse(valueStringB);
            Console.WriteLine($"Strings was parsed, Time: {sw.Elapsed}");
            sw.Restart();

            var bigNumberSum = bigNUmberA.Increment(bigNUmberB);
            sw.Stop();

            Assert.AreEqual(resultString, bigNumberSum.Value);

            Console.WriteLine($"Numbers sum was done, Time: {sw.Elapsed}");
        }

        [Test]
        public void Test_LoadTest()
        {
            Console.WriteLine("Test_LoadTest BEGIN");
            var sw = new Stopwatch();
            sw.Start();
            
            //string stringA = TestHelper.LoadStringNumber("stringA.txt");
            //string stringB = TestHelper.LoadStringNumber("stringB.txt");

            string stringA;
            string stringB;

            TestHelper.GetRandomStringNumber((long)(1024 * 1024* 1024 / 2), out stringA, out stringB);

            Console.WriteLine($"Strings was generated, Time: {sw.Elapsed}");
            sw.Restart();


            var stringValueA = StringNumber.Parse(stringA);
            var stringValueB = StringNumber.Parse(stringB);

            //File.WriteAllText(Path.Combine(DataDirectory, "stringA.txt"), stringA);
            //File.WriteAllText(Path.Combine(DataDirectory, "stringB.txt"), stringB);

            Console.WriteLine($"Strings was parsed, Time: {sw.Elapsed}");
            sw.Restart();

            var stringValueSum = stringValueA.Increment(stringValueB);
            sw.Stop();
            Console.WriteLine($"Numbers sum was done, Time: {sw.Elapsed}");

            // хотел сравнить с BigInteger из фреймворка 4.0, 
            // но я так и не дождался первого BigInteger.Parse
            //var bigIntegerA = BigInteger.Parse(stringA);
            //var bigIntegerB = BigInteger.Parse(stringB);
            //var swBigInt = new Stopwatch();
            //swBigInt.Start();
            //var bigIntegerSum = bigIntegerA + bigIntegerB;
            //swBigInt.Stop();

            Console.WriteLine("Test_LoadTest END");
        }
    }
}
