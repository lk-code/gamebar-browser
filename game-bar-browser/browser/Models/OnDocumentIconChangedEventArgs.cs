using System;

namespace browser.Models
{
    public class DocumentIconChangedEventArgs : EventArgs
    {
        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        public Guid BrowserId { get; set; } = Guid.Empty;

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
        /// <param name="documentIcon"></param>
        public DocumentIconChangedEventArgs(Guid browserId, string documentIcon)
        {
            this.BrowserId = browserId;
            this.DocumentIcon = documentIcon;
        }

        #endregion
    }
}
