using browser.Components.WebUriProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace browser.Test.Components.WebUriProcessor
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
                { "http://www.google.de", true }
            };

            foreach (KeyValuePair<string, bool> testValue in testValues)
            {
                bool result = webUriProcessorComponent.IsValidUri(testValue.Key);

                if (result != testValue.Value)
                {
                    Assert.IsTrue(false);
                }
            }
        }
    }
}
