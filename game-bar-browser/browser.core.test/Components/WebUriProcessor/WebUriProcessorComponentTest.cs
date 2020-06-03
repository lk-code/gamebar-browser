/**
 * MIT License
 * 
 * Copyright (c) 2020 lk-code
 * see more at https://github.com/lk-code/gamebar-browser
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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
