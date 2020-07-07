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

using browser.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace browser.Components.Twitch
{
    public class TwitchService
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        private readonly string TWITCH_CLIENT_ID = AppConfig.Settings["twitch:client_id"];

        /// <summary>
        /// 
        /// </summary>
        private readonly string TWITCH_CLIENT_SECRET = AppConfig.Settings["twitch:client_secret"];

        /// <summary>
        /// 
        /// </summary>
        private HttpClient _httpClient = null;

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        public TwitchService()
        {
            this.InitializeHttpClient();
        }

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="gameTitle"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public async Task<List<TwitchVideo>> GetStreamsForGameAsync(string language, string gameTitle = "", int limit = 20)
        {
            List<TwitchVideo> videos = new List<TwitchVideo>();

            string responseJson = string.Empty;

            try
            {
                StringBuilder uriStringBuilder = new StringBuilder();
                uriStringBuilder.Append("https://api.twitch.tv");
                uriStringBuilder.Append("/kraken");
                uriStringBuilder.Append("/streams");
                uriStringBuilder.Append($"?limit={limit}");
                uriStringBuilder.Append($"&language={language}");

                if(!string.IsNullOrEmpty(gameTitle.Trim())
                    && !string.IsNullOrWhiteSpace(gameTitle.Trim()))
                {
                    uriStringBuilder.Append($"&game={gameTitle}");
                }

                string requestUriString = uriStringBuilder.ToString();
                responseJson = await this._httpClient.GetStringAsync(requestUriString);
            }
            catch (ArgumentNullException err)
            {
                // exception
                int i = 0;
                return videos;
            }
            catch (HttpRequestException err)
            {
                // exception
                int i = 0;
                return videos;
            }
            catch (Exception err)
            {
                // exception
                int i = 0;
                return videos;
            }

            if (string.IsNullOrEmpty(responseJson.Trim())
                    || string.IsNullOrWhiteSpace(responseJson.Trim()))
            {
                // exception
                int i = 0;
                return videos;
            }

            JArray streams = null;

            try
            {
                dynamic json = JValue.Parse(responseJson);
                streams = json.streams;
            }
            catch (Exception err)
            {
                // exception
                int i = 0;
                return videos;
            }

            if (streams == null
                || (streams != null && streams.Count <= 0))
            {
                // exception
                int i = 0;
                return videos;
            }

            foreach (JToken stream in streams)
            {
                try
                {
                    JToken channel = stream.SelectToken("channel");
                    JToken previewImages = stream.SelectToken("preview");

                    TwitchVideo video = new TwitchVideo
                    {
                        VideoTitle = channel.Value<string>("status"),
                        VideoUri = new Uri(channel.Value<string>("url")),
                        ViewsCount = stream.Value<long>("viewers"),
                        PreviewImageUri = new Uri(previewImages.Value<string>("large")),
                        ChannelTitle = channel.Value<string>("name"),
                        ChannelLogoUri = new Uri(channel.Value<string>("logo")),
                        ChannelUri = new Uri(channel.Value<string>("url"))
                    };

                    videos.Add(video);
                }
                catch (Exception err)
                {
                    // exception
                    int i = 0;
                }
            }

            return videos;
        }

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        private void InitializeHttpClient()
        {
            this._httpClient = null;
            this._httpClient = new HttpClient();

            this._httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
            this._httpClient.DefaultRequestHeaders.Add("Client-ID", TWITCH_CLIENT_ID);
        }

        #endregion
    }
}