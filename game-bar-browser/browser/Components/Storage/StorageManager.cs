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

using Windows.Storage;

namespace browser.Components.Storage
{
    public class StorageManager
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        #endregion

        #region # private properties #

        private readonly string _roamingSettingsKey = "RoamingSettings";

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestedKey"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public T GetValue<T>(StorageKey requestedKey, T fallback)
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values[this._roamingSettingsKey];

            if (composite != null)
            {
                string requestedKeyName = this.GetRequestedKeyName(requestedKey);
                object value = composite[requestedKeyName];

                if(value != null)
                {
                    return (T)value;
                } else
                {
                    return fallback;
                }
            }

            return fallback;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedKey"></param>
        /// <param name="value"></param>
        public void SetValue(StorageKey requestedKey, object value)
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)roamingSettings.Values[this._roamingSettingsKey];

            if(composite == null)
            {
                composite = new ApplicationDataCompositeValue();
            }

            string requestedKeyName = this.GetRequestedKeyName(requestedKey);
            composite[requestedKeyName] = value;

            roamingSettings.Values[this._roamingSettingsKey] = composite;
        }

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedKey"></param>
        /// <returns></returns>
        private string GetRequestedKeyName(StorageKey requestedKey)
        {
            string requestedKeyName = requestedKey.ToString().ToUpper();
            requestedKeyName = requestedKeyName.Replace('-', '_');

            return requestedKeyName;
        }

        #endregion
    }
}