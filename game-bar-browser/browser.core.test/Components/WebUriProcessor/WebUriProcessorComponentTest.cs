using browser.core.Components.WebUriProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace browser.core.test.Components.WebUriProcessor
{
    [TestClass]
    public class WebUriProcessorComponentTest
    {
        [TestMethod]
        public void TestIsValidUri()
        {
            WebUriProcessorComponent webUriProcessorComponent = new WebUriProcessorComponent();

            Dictionary<string, bool> testValues = new Dictionary<string, bool>
            {
                { "https://www.google.de", true },
                { "http://www.google.de", true },
                { "www.google.de", false },
                { "google.de", false },
                { "google", false }
            };

            foreach (KeyValuePair<string, bool> testValue in testValues)
            {
                bool result = webUriProcessorComponent.IsValidUri(testValue.Key);

                if (result == testValue.Value)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }
            }
        }

        [TestMethod]
        public void TestIsValidAdress()
        {
            WebUriProcessorComponent webUriProcessorComponent = new WebUriProcessorComponent();

            Dictionary<string, bool> testValues = new Dictionary<string, bool>
            {
                { "https://www.google.de", true },
                { "http://www.google.de", true },
                { "www.google.de", true },
                { "google.de", true },
                { "google", false }
            };

            foreach (KeyValuePair<string, bool> testValue in testValues)
            {
                bool result = webUriProcessorComponent.IsValidAdress(testValue.Key);

                if (result == testValue.Value)
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.IsTrue(false);
                }
            }
        }

        [TestMethod]
        public void TestCreateUriFromDomain()
        {
            WebUriProcessorComponent webUriProcessorComponent = new WebUriProcessorComponent();

            Dictionary<string, string> testValues = new Dictionary<string, string>
            {
                { "https://www.google.de", "https://www.google.de/" },
                { "www.google.de", "https://www.google.de/" },
                { "google.de", "https://google.de/" }
            };

            foreach (KeyValuePair<string, string> testValue in testValues)
            {
                Uri uri = webUriProcessorComponent.CreateUriFromDomain(testValue.Key);

                Assert.AreEqual<string>(testValue.Value, uri.ToString());
            }
        }
    }
}
