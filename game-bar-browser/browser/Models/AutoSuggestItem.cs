﻿/**
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

using browser.ViewModels;
using System;

namespace browser.Models
{
    public class AutoSuggestItem : BaseViewModel
    {
        string _websiteUri = " ";
        /// <summary>
        /// 
        /// </summary>
        public string WebsiteUri
        {
            get
            {
                return _websiteUri;
            }
            set
            {
                _websiteUri = value;
            }
        }

        string _websiteTitle = " ";
        /// <summary>
        /// 
        /// </summary>
        public string WebsiteTitle
        {
            get
            {
                return _websiteTitle;
            }
            set
            {
                _websiteTitle = value;
            }
        }

        Uri _websiteIcon = null;
        /// <summary>
        /// 
        /// </summary>
        public Uri WebsiteIcon
        {
            get
            {
                return _websiteIcon;
            }
            set
            {
                _websiteIcon = value;
            }
        }
    }
}
