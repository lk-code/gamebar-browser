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

namespace browser.Models
{
    public class WebViewHeaderChangedEventArgs : EventArgs
    {
        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        public Guid BrowserId { get; set; } = Guid.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string DocumentTitle { get; set; } = " ";

        /// <summary>
        /// 
        /// </summary>
        public string DocumentIcon { get; set; } = " ";

        #endregion

        #region # constructor #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browserId"></param>
        /// <param name="documentTitle"></param>
        /// <param name="documentIcon"></param>
        public WebViewHeaderChangedEventArgs(Guid browserId, string documentTitle, string documentIcon)
        {
            this.BrowserId = browserId;
            this.DocumentTitle = documentTitle;
            this.DocumentIcon = documentIcon;
        }

        #endregion
    }
}
