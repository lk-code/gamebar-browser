using System;

namespace browser.Models
{
    public class DocumentTitleChangedEventArgs : EventArgs
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

        #endregion

        #region # constructor #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="browserId"></param>
        /// <param name="documentTitle"></param>
        public DocumentTitleChangedEventArgs(Guid browserId, string documentTitle)
        {
            this.BrowserId = browserId;
            this.DocumentTitle = documentTitle;
        }

        #endregion
    }
}
