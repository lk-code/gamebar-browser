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
using System.Globalization;
using System.Net.Http;
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

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        public TwitchService()
        {

        }

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTitle"></param>
        /// <param name="limit"></param>
        /// <param name="period">like the twitch arguments => Specifies the window of time to search. Valid values: week, month, all. Default: week</param>
        /// <returns></returns>
        public async Task<List<TwitchVideo>> GetVideosForGame(string gameTitle, int limit = 10, string period = "week")
        {
            List<TwitchVideo> videos = new List<TwitchVideo>();

            try
            {
                string twitchLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                HttpClient httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.twitchtv.v5+json");
                httpClient.DefaultRequestHeaders.Add("Client-ID", TWITCH_CLIENT_ID);

                string requestUriString = $"https://api.twitch.tv/kraken/videos/top?limit={limit}&language={twitchLanguage}&period={period}&game={gameTitle}";

                string responseJson = await httpClient.GetStringAsync(requestUriString);

                dynamic json = JValue.Parse(responseJson);

                JArray vods = json.vods;

                foreach (JToken vod in vods)
                {
                    JToken channel = vod.SelectToken("channel");
                    JToken previewImages = vod.SelectToken("preview");

                    TwitchVideo video = new TwitchVideo
                    {
                        VideoTitle = vod.Value<string>("title"),
                        VideoUri = new Uri(vod.Value<string>("url")),
                        ViewsCount = vod.Value<long>("views"),
                        PreviewImageUri = new Uri(previewImages.Value<string>("large")),
                        ChannelTitle = channel.Value<string>("name"),
                        ChannelLogoUri = new Uri(channel.Value<string>("logo")),
                        ChannelUri = new Uri(channel.Value<string>("url"))
                    };

                    videos.Add(video);
                }
            }
            catch (Exception err)
            {
                int i = 0;
            }

            return videos;
        }

        #endregion

        #region # private logic #

        #endregion
    }
}