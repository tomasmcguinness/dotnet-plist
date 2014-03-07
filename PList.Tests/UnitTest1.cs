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
            PList plist = new PList();
            plist.RootDictionary.AddString("value", "this is a string");
            XDocument doc = XDocument.Parse(Encoding.UTF8.GetString(plist.GetXml()));

            Assert.AreEqual("plist", doc.Root.Name);
        }

        [TestMethod]
        public void TestMethod2()
        {
            PList plist = new PList();
            plist.RootDictionary.AddInteger("value", 12);
            XDocument doc = XDocument.Parse(Encoding.UTF8.GetString(plist.GetXml()));

            Assert.AreEqual("plist", doc.Root.Name);
        }

        [TestMethod]
        public void TestMethod3()
        {
            PList plist = new PList();
            PListDictionary dict = plist.RootDictionary;

            PListDictionary consentDict = dict.AddDictionary("ConsentText");
            consentDict.AddString("default", "Adds somethign something something");

            PListArray payloadArray = dict.AddArray("PayloadContent");

            dict = payloadArray.AddDictionary();
            dict.AddString("PayloadCertificateFileName", "Cold Bear Ltd");
            dict.AddData("PayloadContent", (object)"MIIEPDCCAySgAwIBAgIJAMNF4tOwxe3kMA0GCSqGSIb3DQEBBQUA			MHExCzAJBgNVBAYTAkdCMQ8wDQYDVQQIEwZMb25kb24xDzANBgNV			BAcTBkxvbmRvbjESMBAGA1UEChMJQ29sZCBCZWFyMRQwEgYDVQQL			EwtEZXZlbG9wbWVudDEWMBQGA1UEAxMNQ29sZCBCZWFyIEx0ZDAe");

            dict = payloadArray.AddDictionary();
            dict.AddString("Password", "Bjaxebh2");

            XDocument doc = XDocument.Parse(Encoding.UTF8.GetString(plist.GetXml()));
            Assert.AreEqual("plist", doc.Root.Name);
        }
    }
}
