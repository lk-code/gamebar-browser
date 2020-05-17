using System;

namespace browser.Components.History
{
    public class HistoryItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public DateTime Visited { get; set; } = DateTime.Now;

        /// <summary>
        /// 
        /// </summary>
        public Uri Uri { get; set; } = null;
    }
}
