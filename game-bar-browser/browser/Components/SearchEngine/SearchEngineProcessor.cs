using browser.Components.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace browser.Components.SearchEngine
{
    public class SearchEngineProcessor
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        #endregion

        #region # private properties #

        private readonly List<SearchEngine> _availableSearchEngines = new List<SearchEngine>
        {
            new SearchEngine
            {
                Key = "bing",
                Title = "Bing",
                RequestScheme = "https://www.bing.com/search?q={0}"
            },
            new SearchEngine
            {
                Key = "google",
                Title = "Google",
                RequestScheme = "https://www.google.de/search?q={0}"
            },
        };

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        public SearchEngineProcessor()
        {

        }

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SearchEngine> GetAvailableSearchEngines()
        {
            List<SearchEngine> searchEngines = this._availableSearchEngines;

            return searchEngines;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedSearchEngineKey"></param>
        /// <returns></returns>
        public SearchEngine GetSearchEngineByKey(string selectedSearchEngineKey)
        {
            var searchEngines = this.GetAvailableSearchEngines().Where(x => x.Key.ToLower() == selectedSearchEngineKey.ToLower());

            if (searchEngines.Count() > 0)
            {
                SearchEngine selectedSearchEngine = searchEngines.First();

                return selectedSearchEngine;
            }

            // if there is no entry for the given search-engine => take the default one
            return this.GetSearchEngineByKey(StorageDefaults.SEARCH_ENGINE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedSearchEngineKey"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public Uri RenderSearchUri(string selectedSearchEngineKey, string searchValue)
        {
            SearchEngine searchEngine = this.GetSearchEngineByKey(selectedSearchEngineKey);

            string searchUriScheme = searchEngine.RequestScheme;
            searchUriScheme = string.Format(searchUriScheme, searchValue);
            Uri searchUri = new Uri(searchUriScheme);

            return searchUri;
        }

        #endregion

        #region # private logic #

        #endregion
    }
}
