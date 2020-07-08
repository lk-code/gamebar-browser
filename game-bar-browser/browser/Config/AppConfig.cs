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

using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;

namespace browser.Config
{
    public class AppConfig
    {
        /// <summary>
        /// 
        /// </summary>
        private static AppConfig _instance;

        /// <summary>
        /// 
        /// </summary>
        private JObject _secrets;

        /// <summary>
        /// 
        /// </summary>
        private const string RESSOURCE_NAMESPACE = "browser";

        /// <summary>
        /// 
        /// </summary>
        private const string RESSOURCE_CONFIG_FILENAME = "appsettings.json";

        /// <summary>
        /// 
        /// </summary>
        private static bool _loaded = false;

        /// <summary>
        /// 
        /// </summary>
        public AppConfig()
        {
            try
            {
                Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(AppConfig)).Assembly;
                string appSettingFileResourceName = $"{RESSOURCE_NAMESPACE}.{RESSOURCE_CONFIG_FILENAME}";
                Stream stream = assembly.GetManifestResourceStream(appSettingFileResourceName);
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();

                    Analytics.TrackEvent($"AppConfig() - Load AppSettings JSON: {json}");

                    _secrets = JObject.Parse(json);
                }
            }
            catch (Exception exception)
            {
                Analytics.TrackEvent($"AppConfig() - Exception: '{exception.Message}' - {exception.StackTrace}");
                Crashes.TrackError(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static AppConfig Settings
        {
            get
            {
                if (_loaded == false)
                {
                    _instance = new AppConfig();
                    _loaded = true;
                }

                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                try
                {
                    string[] path = name.Split(':');

                    JToken node = _secrets[path[0]];
                    for (int index = 1; index < path.Length; index++)
                    {
                        node = node[path[index]];
                    }

                    return node.ToString();
                }
                catch (Exception exception)
                {
                    Analytics.TrackEvent($"AppConfig::this[string name] - Exception: '{exception.Message}' - {exception.StackTrace}");
                    Crashes.TrackError(exception);

                    return string.Empty;
                }
            }
        }
    }
}
