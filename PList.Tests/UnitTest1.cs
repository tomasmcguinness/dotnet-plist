using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;
using System.Text;

namespace PListFormatter.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            PList plist = PList.New();
            plist.RootElement.AddString("value", "this is a string");
            XDocument doc = XDocument.Parse(Encoding.UTF8.GetString(plist.GetXml()));

            Assert.AreEqual("plist", doc.Root.Name);
        }
    }
}
