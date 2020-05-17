using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Printing;

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
        public string GoBack()
        {
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GoForward()
        {
            return string.Empty;
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