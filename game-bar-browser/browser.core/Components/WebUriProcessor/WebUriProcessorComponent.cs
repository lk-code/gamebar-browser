using System;
using System.Text.RegularExpressions;

namespace browser.core.Components.WebUriProcessor
{
    public class WebUriProcessorComponent
    {
        #region # events #

        #endregion

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        private const string DEFAULT_SCHEME = "https";

        #endregion

        #region # public properties #

        #endregion

        public WebUriProcessorComponent()
        {

        }

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriValue"></param>
        /// <returns></returns>
        public bool IsValidUri(string uriValue)
        {
            Uri uriResult = null;
            bool isValidUri = Uri.TryCreate(uriValue, UriKind.Absolute, out uriResult);

            return isValidUri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriValue"></param>
        /// <returns></returns>
        public bool IsValidAdress(string uriValue)
        {
            string regexPattern = "(?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\\.)+[a-z0-9][a-z0-9-]{0,61}[a-z0-9]";
            bool isValidAdress = Regex.IsMatch(uriValue, regexPattern);

            return isValidAdress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriValue"></param>
        /// <returns></returns>
        public Uri CreateUriFromDomain(string uriValue)
        {
            UriBuilder uriBuilder = new UriBuilder(uriValue);
            uriBuilder.Scheme = DEFAULT_SCHEME;
            uriBuilder.Port = -1;

            Uri uri = uriBuilder.Uri;

            return uri;
        }

        #endregion

        #region # private logic #

        #endregion
    }
}
