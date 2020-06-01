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
using Microsoft.UI.Xaml.Controls;

namespace browser.Models
{
    public class TabUiItem : BaseViewModel
    {
        string _documentTitle = " ";
        /// <summary>
        /// 
        /// </summary>
        public string DocumentTitle
        {
            get
            {
                return _documentTitle;
            }
            set
            {
                SetProperty(ref _documentTitle, value);
            }
        }

        Microsoft.UI.Xaml.Controls.IconSource _documentIcon = null;
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.UI.Xaml.Controls.IconSource DocumentIcon
        {
            get
            {
                return _documentIcon;
            }
            set
            {
                SetProperty(ref _documentIcon, value);
            }
        }

        object _content = null;
        /// <summary>
        /// 
        /// </summary>
        public object Content
        {
            get
            {
                return _content;
            }
            set
            {
                SetProperty(ref _content, value);
            }
        }

        #region # methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        public void Update(string title, IconSource icon)
        {
            this._documentTitle = title;
            this._documentIcon = icon;

            OnPropertyChanged("DocumentIcon");
            OnPropertyChanged("DocumentTitle");
        }

        #endregion
    }
}
