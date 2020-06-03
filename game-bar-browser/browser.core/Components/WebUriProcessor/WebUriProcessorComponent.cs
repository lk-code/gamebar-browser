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

using System;
using System.Text.RegularExpressions;

namespace browser.core.Components.WebUriProcessor
{
    public class WebUriProcessorComponent
    {
        #region # events #

        #endregion

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        private const string DEFAULT_SCHEME = "https";

        #endregion

        #region # public properties #

        #endregion

        public WebUriProcessorComponent()
        {

        }

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriValue"></param>
        /// <returns></returns>
        public bool IsValidUri(string uriValue)
        {
            Uri uriResult = null;
            bool isValidUri = Uri.TryCreate(uriValue, UriKind.Absolute, out uriResult);

            return isValidUri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriValue"></param>
        /// <returns></returns>
        public bool IsValidAdress(string uriValue)
        {
            string regexPattern = "(?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\\.)+[a-z0-9][a-z0-9-]{0,61}[a-z0-9]";
            bool isValidAdress = Regex.IsMatch(uriValue, regexPattern);

            return isValidAdress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriValue"></param>
        /// <returns></returns>
        public Uri CreateUriFromDomain(string uriValue)
        {
            UriBuilder uriBuilder = new UriBuilder(uriValue);
            uriBuilder.Scheme = DEFAULT_SCHEME;
            uriBuilder.Port = -1;

            Uri uri = uriBuilder.Uri;

            return uri;
        }

        #endregion

        #region # private logic #

        #endregion
    }
}
