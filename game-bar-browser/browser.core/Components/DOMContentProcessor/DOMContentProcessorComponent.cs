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

using browser.core.Components.DOMContentProcessor.Exceptions;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace browser.core.Components.DOMContentProcessor
{
    public class DOMContentProcessorComponent
    {
        #region # events #

        #endregion

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        private bool _isLoaded
        {
            get
            {
                if(_loadedHtmlDocument == null)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private HtmlDocument _loadedHtmlDocument = null;

        #endregion

        #region # public properties #

        #endregion

        public DOMContentProcessorComponent()
        {

        }

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriValue"></param>
        /// <returns></returns>
        public void LoadDOMContent(string domContent)
        {
            this._loadedHtmlDocument = null;

            this._loadedHtmlDocument = new HtmlDocument();
            this._loadedHtmlDocument.LoadHtml(domContent);
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetDocumentTitle()
        {
            if (this._isLoaded != true)
            {
                throw new ProcessorNotLoadedException();
            }

            var root = this._loadedHtmlDocument.DocumentNode;
            var nodes = root.Descendants("title");

            if (nodes.Count() <= 0)
            {
                return null;
            }

            string title = nodes.First().InnerText;

            return title;
        }

        /// <summary>
        /// 
        /// </summary>
        public string GetFaviconPath()
        {
            if (this._isLoaded != true)
            {
                throw new ProcessorNotLoadedException();
            }

            var root = this._loadedHtmlDocument.DocumentNode;
            var nodes = root.Descendants("link");

            if (nodes.Count() <= 0)
            {
                return null;
            }

            string faviconPath = string.Empty;

            /* load from <link rel="shortcut icon" /> */
            var shortcutIconNodes = nodes.Where(x => x.Attributes["rel"].Value.ToLowerInvariant() == "shortcut icon");
            if (shortcutIconNodes.Count() > 0)
            {
                faviconPath = shortcutIconNodes.First().Attributes["href"].Value;
            }

            /* load from <link rel="icon" /> */
            var iconNodes = nodes.Where(x => x.Attributes["rel"].Value.ToLowerInvariant() == "icon");
            if (iconNodes.Count() > 0)
            {
                faviconPath = iconNodes.First().Attributes["href"].Value;

                try
                {
                    var iconNodesSizes = iconNodes.Where(x => x.Attributes.Where(y => y.Name.ToLowerInvariant() == "sizes").Any());
                    if (iconNodesSizes.Count() >= 1)
                    {
                        iconNodesSizes = iconNodesSizes.OrderByDescending(x => x.Attributes["sizes"].Value.Split('x')[0]);

                        faviconPath = iconNodesSizes.First().Attributes["href"].Value;
                    }
                } catch(Exception err)
                {

                }
            }

            /* return default value => /favicon.ico */
            if (string.IsNullOrEmpty(faviconPath)
                || string.IsNullOrWhiteSpace(faviconPath))
            {
                faviconPath = "/favicon.ico";
            }

            return faviconPath;
        }

        #endregion

        #region # private logic #

        #endregion
    }
}
