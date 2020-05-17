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

        /// <summary>
        /// 
        /// </summary>
        private const string HISTORY_INDEX_DATE_SCHEME = "yyyy-MM-dd";

        /// <summary>
        /// 
        /// </summary>
        private const string HISTORY_STORAGE_FILE_INDEX_NAME = "history_index.dat";

        /// <summary>
        /// 
        /// </summary>
        private const string HISTORY_STORAGE_FILE_ITEMS_NAME = "history_items_{0}.dat";

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
        public async Task<Dictionary<string, List<HistoryItem>>> GetHistoryAsync()
        {
            List<string> historyIndex = await this.GetHistoryIndexAsync();
            Dictionary<string, List<HistoryItem>> history = new Dictionary<string, List<HistoryItem>>();

            foreach (string historyIndexEntry in historyIndex)
            {
                List<HistoryItem> historyItems = await this.GetHistoryItemsForDayAsync(historyIndexEntry);

                if(historyItems != null
                    && historyItems.Count() > 0)
                {
                    history.Add(historyIndexEntry, historyItems);
                }
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
        /// <param name="historyDateTime"></param>
        /// <returns></returns>
        private string GetFileNameForHistoryItems(DateTime historyDateTime)
        {
            string todayIndexKey = historyDateTime.ToString(HISTORY_INDEX_DATE_SCHEME);

            string historyFileName = this.GetFileNameForHistoryItems(todayIndexKey);

            return historyFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historyDateValue"></param>
        /// <returns></returns>
        private string GetFileNameForHistoryItems(string historyDateValue)
        {
            string todayIndexKey = historyDateValue.Replace('-', '_');
            string historyFileName = string.Format(HISTORY_STORAGE_FILE_ITEMS_NAME, todayIndexKey);

            return historyFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateValue"></param>
        /// <returns></returns>
        private async Task<List<HistoryItem>> GetHistoryItemsForDayAsync(string dateValue)
        {
            string historyFileName = this.GetFileNameForHistoryItems(dateValue);
            StorageFolder roamingStorageFolder = ApplicationData.Current.RoamingFolder;
            StorageFile historyItemsStorageFile = await roamingStorageFolder.CreateFileAsync(historyFileName, CreationCollisionOption.OpenIfExists);

            string historyJsonContentValue = await FileIO.ReadTextAsync(historyItemsStorageFile);
            if (historyJsonContentValue.Trim().Length <= 2)
            {
                return new List<HistoryItem>();
            }

            string historyJsonContent = historyJsonContentValue;

            List<HistoryItem> history = JsonConvert.DeserializeObject<List<HistoryItem>>(historyJsonContent);

            return history;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historyItem"></param>
        private async void SaveHistoryItem(HistoryItem historyItem)
        {
            this.EnsureIndexForDate(historyItem.Visited);

            string historyFileName = this.GetFileNameForHistoryItems(historyItem.Visited);
            StorageFolder roamingStorageFolder = ApplicationData.Current.RoamingFolder;
            StorageFile historyItemsStorageFile = await roamingStorageFolder.CreateFileAsync(historyFileName, CreationCollisionOption.OpenIfExists);

            string historyJsonContent = JsonConvert.SerializeObject(new List<HistoryItem>());
            string historyJsonContentValue = await FileIO.ReadTextAsync(historyItemsStorageFile);

            if (historyJsonContentValue.Trim().Length > historyJsonContent.Length)
            {
                historyJsonContent = historyJsonContentValue;
            }

            string historyItemJsonContent = JsonConvert.SerializeObject(historyItem);

            if(historyJsonContent.Trim().Length > 2)
            {
                historyJsonContent = historyJsonContent.Insert(historyJsonContent.Length - 1, ",");
            }

            historyJsonContent = historyJsonContent.Insert(historyJsonContent.Length - 1, historyItemJsonContent);

            await FileIO.WriteTextAsync(historyItemsStorageFile, historyJsonContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historyItem"></param>
        private async void EnsureIndexForDate(DateTime historyItemDate)
        {
            List<string> historyIndex = await this.GetHistoryIndexAsync();
            string todayIndexKey = historyItemDate.ToString(HISTORY_INDEX_DATE_SCHEME);

            if (!historyIndex.Contains(todayIndexKey))
            {
                historyIndex.Add(todayIndexKey);
            }

            historyIndex = historyIndex.Distinct().ToList();

            this.SaveHistoryIndexAsync(historyIndex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="historyIndex"></param>
        private async void SaveHistoryIndexAsync(List<string> historyIndex)
        {
            StorageFolder roamingStorageFolder = ApplicationData.Current.RoamingFolder;
            StorageFile historyIndexStorageFile = await roamingStorageFolder.CreateFileAsync(HISTORY_STORAGE_FILE_INDEX_NAME, CreationCollisionOption.OpenIfExists);

            string historyIndexJsonContent = JsonConvert.SerializeObject(historyIndex);

            await FileIO.WriteTextAsync(historyIndexStorageFile, historyIndexJsonContent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<List<string>> GetHistoryIndexAsync()
        {
            StorageFolder roamingStorageFolder = ApplicationData.Current.RoamingFolder;
            StorageFile historyIndexStorageFile = await roamingStorageFolder.CreateFileAsync(HISTORY_STORAGE_FILE_INDEX_NAME, CreationCollisionOption.OpenIfExists);

            List<string> historyIndex = new List<string>();

            string historyIndexJsonContent = await FileIO.ReadTextAsync(historyIndexStorageFile);

            if(historyIndexJsonContent.Trim().Length >= 2)
            {
                historyIndex = JsonConvert.DeserializeObject<List<string>>(historyIndexJsonContent);
                historyIndex = historyIndex.OrderByDescending(x => x).ToList();
            }

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