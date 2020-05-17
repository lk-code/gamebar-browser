using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Storage;

namespace browser.Components.History
{
    public class HistoryManager
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        #endregion

        #region # private properties #

        private const string HISTORY_INDEX_DATE_SCHEME = "yyyy-MM-dd";
        private const string HISTORY_STORAGE_KEY = "HistorySettings";
        private const string HISTORY_STORAGE_ITEMS_COMPOSITE_KEY = "HISTORY_ITEMS";
        private const string HISTORY_STORAGE_INDEX_COMPOSITE_KEY = "HISTORY_INDEX";

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        public void ClearHistory()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<HistoryItem>> GetHistory()
        {
            List<string> historyIndex = this.GetHistoryIndex();
            Dictionary<string, List<HistoryItem>> history = new Dictionary<string, List<HistoryItem>>();

            foreach (string historyIndexEntry in historyIndex)
            {
                List<HistoryItem> historyItems = this.GetHistoryItemsForDay(historyIndexEntry);

                history.Add(historyIndexEntry, historyItems);
            }

            return history;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="uri"></param>
        /// <param name="visited"></param>
        public void Add(string title, Uri uri, DateTime visited)
        {
            HistoryItem historyItem = new HistoryItem
            {
                Title = title,
                Uri = uri,
                Visited = visited
            };

            this.SaveHistoryItem(historyItem);
        }

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        private List<HistoryItem> GetHistoryItemsForDay(string dateValue)
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values[HISTORY_STORAGE_KEY];

            if (composite == null)
            {
                return new List<HistoryItem>();
            }

            string dateIndexKey = dateValue.Replace('-', '_');

            string requestedKeyName = this.GetRequestedKeyName(HISTORY_STORAGE_ITEMS_COMPOSITE_KEY + "_" + dateIndexKey);
            object compositeValue = composite[requestedKeyName];

            if (compositeValue == null)
            {
                return new List<HistoryItem>();
            }

            string historyJsonContent = (compositeValue as string);

            List<HistoryItem> history = JsonConvert.DeserializeObject<List<HistoryItem>>(historyJsonContent);

            return history;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historyItem"></param>
        private void SaveHistoryItem(HistoryItem historyItem)
        {
            this.EnsureIndexForDate(historyItem.Visited);

            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values[HISTORY_STORAGE_KEY];

            if (composite == null)
            {
                composite = new ApplicationDataCompositeValue();
            }

            if (composite != null)
            {
                string todayIndexKey = historyItem.Visited.ToString(HISTORY_INDEX_DATE_SCHEME);
                string requestedKeyName = this.GetRequestedKeyName(HISTORY_STORAGE_ITEMS_COMPOSITE_KEY + "_" + todayIndexKey);
                object compositeValue = composite[requestedKeyName];

                string historyJsonContent = JsonConvert.SerializeObject(new List<HistoryItem>());

                if(compositeValue != null)
                {
                    historyJsonContent = (compositeValue as string);
                }

                string historyItemJsonContent = JsonConvert.SerializeObject(historyItem);

                if(historyJsonContent.Trim().Length > 2)
                {
                    historyJsonContent = historyJsonContent.Insert(historyJsonContent.Length - 1, ",");
                }

                historyJsonContent = historyJsonContent.Insert(historyJsonContent.Length - 1, historyItemJsonContent);

                composite[requestedKeyName] = historyJsonContent;

                roamingSettings.Values[HISTORY_STORAGE_KEY] = composite;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historyItem"></param>
        private void EnsureIndexForDate(DateTime historyItemDate)
        {
            List<string> historyIndex = this.GetHistoryIndex();
            string todayIndexKey = historyItemDate.ToString(HISTORY_INDEX_DATE_SCHEME);

            if (!historyIndex.Contains(todayIndexKey))
            {
                historyIndex.Add(todayIndexKey);
            }

            this.SaveHistoryIndex(historyIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historyIndex"></param>
        private void SaveHistoryIndex(List<string> historyIndex)
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values[HISTORY_STORAGE_KEY];

            if (composite == null)
            {
                composite = new ApplicationDataCompositeValue();
            }

            string historyIndexJsonContent = JsonConvert.SerializeObject(historyIndex);

            string requestedKeyName = this.GetRequestedKeyName(HISTORY_STORAGE_INDEX_COMPOSITE_KEY);
            composite[requestedKeyName] = historyIndexJsonContent;

            roamingSettings.Values[HISTORY_STORAGE_KEY] = composite;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<string> GetHistoryIndex()
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values[HISTORY_STORAGE_KEY];

            List<string> historyIndex = new List<string>();

            if (composite != null)
            {
                string requestedKeyName = this.GetRequestedKeyName(HISTORY_STORAGE_INDEX_COMPOSITE_KEY);
                object compositeValue = composite[requestedKeyName];

                if(compositeValue != null)
                {
                    string historyIndexJsonContent = (compositeValue as string);
                    historyIndex = JsonConvert.DeserializeObject<List<string>>(historyIndexJsonContent);
                }
            }

            historyIndex = historyIndex.OrderByDescending(x => x).ToList();

            return historyIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedKey"></param>
        /// <returns></returns>
        private string GetRequestedKeyName(string requestedKey)
        {
            string requestedKeyName = requestedKey.ToUpper();
            requestedKeyName = requestedKeyName.Replace('-', '_');

            return requestedKeyName;
        }

        #endregion
    }
}