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
using System.Collections.Generic;
using System.Linq;

namespace browser.Components.TempHistory
{
    public class TempHistoryManager
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        #endregion

        #region # private properties #

        /// <summary>
        /// contains the temporary history for the webview (for the forward and backward actions)
        /// </summary>
        private List<TempHistoryItem> _temporaryHistory = null;

        /// <summary>
        /// 
        /// </summary>
        private int _currentIndex = 0;

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        public TempHistoryManager()
        {
            this.InitializeTempHistoryList();
        }

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="uri"></param>
        public void Add(string title, Uri uri)
        {
            int newIndex = ++this._currentIndex;

            this._temporaryHistory.Add(new TempHistoryItem
            {
                Index = newIndex,
                Title = title,
                Uri = uri
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanGoBack()
        {
            if(this._currentIndex > 1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanGoForward()
        {
            if (this._currentIndex < this._temporaryHistory.Count())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Uri GoBack()
        {
            if(this.CanGoBack() == true)
            {
                this._currentIndex--;

                Uri requestedHistoryUri = this._temporaryHistory.Where(x => x.Index == this._currentIndex).First().Uri;

                return requestedHistoryUri;
            }

            throw new TempHistoryInvalidNavigationException("you can not go back");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Uri GoForward()
        {
            if (this.CanGoForward() == true)
            {
                this._currentIndex++;

                Uri requestedHistoryUri = this._temporaryHistory.Where(x => x.Index == this._currentIndex).First().Uri;

                return requestedHistoryUri;
            }

            throw new TempHistoryInvalidNavigationException("you can not go forward");
        }

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        private void InitializeTempHistoryList()
        {
            this._temporaryHistory = new List<TempHistoryItem>();
            this._currentIndex = 0;
        }

        #endregion
    }
}