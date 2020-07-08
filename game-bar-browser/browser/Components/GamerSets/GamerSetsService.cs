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
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace browser.Components.GamerSets
{
    public class GamerSetsService
    {
        #region # events #

        #endregion

        #region # dependencies #

        /// <summary>
        /// 
        /// </summary>
        private StorageManager _storageManager { get; set; } = null;

        #endregion

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, GamerSet> _gamerSets { get; set; } = null;

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        public GamerSetsService()
        {
            this.InitializeStorageManager();
            this.LoadGamerSets();
        }

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        public GamerSet GetCurrentSetOrDefault()
        {
            GamerSet gamerSet = new GamerSet();

            string currentGamerSetKey = this.GetCurrentGamerSetKey();

            if(string.IsNullOrEmpty(currentGamerSetKey)
                || string.IsNullOrWhiteSpace(currentGamerSetKey))
            {
                var defaultGamerSets = this._gamerSets.Where(x => x.Key.ToLower().Contains(".default."));

                if(defaultGamerSets != null
                    && defaultGamerSets.Count() > 0)
                {
                    gamerSet = defaultGamerSets.FirstOrDefault().Value;
                }
            } else
            {
                var currentGamerSets = this._gamerSets.Where(x => x.Key.ToLower() == currentGamerSetKey.ToLower());

                if (currentGamerSets != null
                    && currentGamerSets.Count() > 0)
                {
                    gamerSet = currentGamerSets.FirstOrDefault().Value;
                }
            }

            return gamerSet;
        }

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        private string GetCurrentGamerSetKey()
        {
            string selectedGamerSetKey = this._storageManager.GetValue<string>(StorageKey.SELECTED_GAMERSET_KEY, StorageDefaults.SELECTED_GAMERSET_KEY);

            return selectedGamerSetKey;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadGamerSets()
        {
            this._gamerSets = null;
            this._gamerSets = new Dictionary<string, GamerSet>();

            // load from app ressources
            Dictionary<string, GamerSet> gamerSetsAppRessources = this.LoadGamerSetsFromAppRessources();

            this._gamerSets.Concat(gamerSetsAppRessources)
                .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, GamerSet> LoadGamerSetsFromAppRessources()
        {
            Dictionary<string, GamerSet> gamerSets = new Dictionary<string, GamerSet>();

            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            List<string> gamerSetRessourcesPaths = assembly.GetManifestResourceNames().Where(x => x.Contains(".Ressources.GamerSets.")).ToList();

            foreach (string gamerSetRessourcesPath in gamerSetRessourcesPaths)
            {
                Stream stream = assembly.GetManifestResourceStream(gamerSetRessourcesPath);
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonContent = reader.ReadToEnd();
                    GamerSet gamerSet = JsonConvert.DeserializeObject<GamerSet>(jsonContent);

                    this._gamerSets.Add(gamerSetRessourcesPath, gamerSet);
                }
            }

            return gamerSets;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeStorageManager()
        {
            this._storageManager = new StorageManager();
        }

        #endregion
    }
}