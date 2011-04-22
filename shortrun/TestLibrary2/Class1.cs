using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;
using System.Diagnostics;

namespace TestLibrary2
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Z()
        {
            Thread.Sleep(10);
            Assert.IsTrue(false);
        }

        [Test]
        public void Y()
        {
            throw new NotImplementedException();
        }
    }
}
