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

using browser.Components.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// 
        /// </summary>
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
            new SearchEngine
            {
                Key = "youtube",
                Title = "YouTube",
                RequestScheme = "https://www.youtube.com/results?search_query={0}"
            },
            new SearchEngine
            {
                Key = "ecosia",
                Title = "Ecosia",
                RequestScheme = "https://www.ecosia.org/search?q={0}"
            },
            new SearchEngine
            {
                Key = "minecraftwiki-de",
                Title = "Minecraft-Wiki (Deutsch)",
                RequestScheme = "https://minecraft-de.gamepedia.com/index.php?search={0}"
            },
            new SearchEngine
            {
                Key = "minecraftwiki-en",
                Title = "Minecraft-Wiki (English)",
                RequestScheme = "https://minecraft.gamepedia.com/index.php?search={0}"
            },
            new SearchEngine
            {
                Key = "duckduckgo",
                Title = "DuckDuckGo",
                RequestScheme = "https://duckduckgo.com/?q={0}"
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
