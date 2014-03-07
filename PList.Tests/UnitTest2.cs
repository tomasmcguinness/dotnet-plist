using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace PListFormatter.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var stream = File.Open("MDM.mobileconfig", FileMode.Open))
            {
                var plist = new PList(stream);
            }
        }
    }
}
