using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;
using System.Diagnostics;

namespace TestLibrary1
{
    [TestFixture]
    public class SomeTests
    {
        [Test]
        public void X1()
        {
            Thread.Sleep(10);
        }

        [Test]
        public void X10()
        {
            for (int i = 0; i < 5000; i++) Console.Write(" ");
            Thread.Sleep(10);
        }
        [Test]
        public void X9()
        {
            Console.WriteLine("some junk");
            Console.Out.WriteLine("some junk");
            Console.Out.Write("some junk");
            Thread.Sleep(10);
        }
        [Test]
        public void X8()
        {
            Thread.Sleep(10);
        }
        [Test]
        public void X7()
        {
            Thread.Sleep(10);
        }
        [Test]
        public void X6()
        {
            Thread.Sleep(10);
        }
        [Test]
        public void X5()
        {
            Thread.Sleep(10);
        }
        [Test]
        public void X4()
        {
            Thread.Sleep(10);
        }
        [Test]
        public void X3()
        {
            Thread.Sleep(10);
        }
        [Test]
        public void X2()
        {
            Thread.Sleep(10);
        }

        [Test]
        [Ignore("some")]
        public void Y()
        {
            Console.Write("disrupt test");
            Thread.Sleep(20);
        }
    }
}
