using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using LongIdentifierLibrary;
using NUnit.Framework;

namespace LongIdentifierTester
{
    [TestFixture]
    public class IdControllerTester
    {
        private const string _alphaNumericIdentifier = "345FAS310E575896325SA";

        [Test]
        [TestCase("345FAS310E575896325SA", "", "123", "345FAS310E575896325SA123")]
        [TestCase("345FAS310E575896325SA", "123", "234", "345FAS310E575896325SA357")]
        [TestCase("345FAS310E575896325SA", "1223372036854775807", "922337203685477", "345FAS310E575896325SA1224294374058461284")]
        public void Test_Inrement(string id, string baseValue, string incrementValue, string resultId)
        {
            var sw = new Stopwatch();
            sw.Start();

            IdController.InitAlphaNumericIdentifier(_alphaNumericIdentifier);
            IdController.Increment(baseValue);
            IdController.Increment(incrementValue);

            sw.Stop();

            Assert.AreEqual(resultId, IdController.Identifier);

            Console.WriteLine($"Numbers sum was done, Time: {sw.Elapsed}");
        }

        [Test]
        public void Test_LoadAsyncTest()
        {
            var mre = new ManualResetEvent(false);

            Console.WriteLine("Test_LoadTest BEGIN");
            var sw = new Stopwatch();
            sw.Start();

            var incrementValue = "922337203685477";
            IdController.InitAlphaNumericIdentifier(_alphaNumericIdentifier);

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            
            var tf = new TaskFactory(cancelTokenSource.Token);
            tf.StartNew(() =>
            {
                try
                {
                    Console.WriteLine($"Begin load stringNumber, Time: {sw.Elapsed}");
                    sw.Restart();
                    //string stringA = TestHelper.LoadStringNumber("stringA.txt");
                    //string stringB = TestHelper.LoadStringNumber("stringB.txt");

                    string stringA;
                    string stringB;
                    TestHelper.GetRandomStringNumber((long)(1024 * 1024 * 100), out stringA, out stringB);

                    Console.WriteLine($"StringNumber loaded, Time: {sw.Elapsed}");
                    sw.Restart();
                    IdController.Increment(stringA);
                    Console.WriteLine($"Increment valueA was done, Time: {sw.Elapsed}");
                    sw.Restart();
                    IdController.Increment(stringB);
                    Console.WriteLine($"Increment valueB was done, Time: {sw.Elapsed}");
                    sw.Restart();
                    mre.Set();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    mre.Set();
                }
            }, cancelTokenSource.Token);

            tf.StartNew(() =>
            {
                IncrementAction("Increment1", incrementValue, cancelTokenSource.Token);
            }, cancelTokenSource.Token);

            tf.StartNew(() =>
            {
                IncrementAction("Increment2", incrementValue, cancelTokenSource.Token);
            }, cancelTokenSource.Token);

            mre.WaitOne();
            cancelTokenSource.Cancel();

            sw.Stop();
            Console.WriteLine($"Numbers sum was done, Time: {sw.Elapsed}");

            Console.WriteLine("Test_LoadTest END");
        }

        [Test]
        public void Test_CancelTest()
        {
            Console.WriteLine("Test_LoadTest BEGIN");
            var sw = new Stopwatch();
            sw.Start();

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(30000);
                cancelTokenSource.Cancel();
            }, cancelTokenSource.Token);

            Assert.Throws<OperationCanceledException>(() =>
            {
                Increment(cancelTokenSource.Token);
            });

            sw.Stop();
            Console.WriteLine($"Test_LoadTest END, Time: {sw.Elapsed}");
        }

        private void Increment(CancellationToken token)
        {
            //var stringA = TestHelper.LoadStringNumber("stringA.txt");
            //var stringB = TestHelper.LoadStringNumber("stringB.txt");

            string stringA;
            string stringB;

            TestHelper.GetRandomStringNumber((long)(1024 * 1024 * 1024), out stringA, out stringB);

            IdController.InitAlphaNumericIdentifier(_alphaNumericIdentifier);
            IdController.Increment(stringA, () => token.IsCancellationRequested);
            IdController.Increment(stringB, () => token.IsCancellationRequested);
        }

        private void IncrementAction(string name, string incrementValue, CancellationToken token)
        {
            for (int i = 0; i < 1000; i++)
            {
                try
                {
                    var sw = new Stopwatch();
                    sw.Start();
                    IdController.Increment(incrementValue, () => token.IsCancellationRequested);

                    token.ThrowIfCancellationRequested();

                    Console.WriteLine($"{name} #{i} was done, Time: {sw.Elapsed}");
                    sw.Stop();
                    Thread.Sleep(100);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Ошибка выполнения {name}");
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
