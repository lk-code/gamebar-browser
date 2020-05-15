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

        /// <summary>
        /// 
        /// </summary>
        public StorageManager()
        {

        }

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
            ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();

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

            return requestedKeyName;
        }

        #endregion
    }
}