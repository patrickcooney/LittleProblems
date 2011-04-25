using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Threading;

namespace SerialTestLibrary1
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void LongTest()  
        {
            Thread.Sleep(20000);
            Assert.IsTrue(true);
        }

    }
}
