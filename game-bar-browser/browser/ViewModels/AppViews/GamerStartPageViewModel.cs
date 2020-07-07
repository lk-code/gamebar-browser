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

using browser.Components.Twitch;
using browser.Core;
using browser.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace browser.ViewModels.AppViews
{
    public class GamerStartPageViewModel : WindowViewModel, IBrowserContentElement
    {
        #region # events #

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnOpenTabRequested;

        #endregion

        #region # dependencies #

        /// <summary>
        /// 
        /// </summary>
        private TwitchService _twitchService = null;

        #endregion

        #region # commands #

        private ICommand _twitchVideoSelected;
        /// <summary>
        /// 
        /// </summary>
        public ICommand TwitchVideoSelected => _twitchVideoSelected ?? (_twitchVideoSelected = new RelayCommand((eventArgs) => { this.OnTwitchVideoSelected(eventArgs); }));

        #endregion

        #region # private properties #

        #endregion

        #region # public properties #

        ItemsChangeObservableCollection<Video> _twitchVideos = new ItemsChangeObservableCollection<Video>();
        /// <summary>
        /// 
        /// </summary>
        public ItemsChangeObservableCollection<Video> TwitchVideos
        {
            get
            {
                return _twitchVideos;
            }
            set
            {
                _twitchVideos = value;
            }
        }

        bool _isLoadingTwitchContent = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsLoadingTwitchContent
        {
            get
            {
                return _isLoadingTwitchContent;
            }
            set
            {
                SetProperty(ref _isLoadingTwitchContent, value);
            }
        }

        Visibility _twitchContentVisibility = Visibility.Collapsed;
        /// <summary>
        /// 
        /// </summary>
        public Visibility TwitchContentVisibility
        {
            get
            {
                return _twitchContentVisibility;
            }
            set
            {
                SetProperty(ref _twitchContentVisibility, value);
            }
        }

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public GamerStartPageViewModel() : base(Window.Current)
        {
            this.InitializeTwitchService();

            this.LoadTwitchContent();
        }

        #endregion

        #region # public methods #

        /// <summary>
        /// 
        /// </summary>
        public void OpenTab(OpenTabEventArgs eventArgs)
        {
            if (OnOpenTabRequested != null) OnOpenTabRequested(this, eventArgs);
        }

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        private async void LoadTwitchContent()
        {
            this.IsLoadingTwitchContent = true;

            List<TwitchVideo> twitchVideos = await this._twitchService.GetStreamsForGameAsync("Minecraft");

            this.TwitchVideos.Clear();

            foreach (TwitchVideo twitchVideo in twitchVideos)
            {
                Video video = new Video
                {
                    VideoTitle = twitchVideo.VideoTitle,
                    VideoUri = twitchVideo.VideoUri,
                    ViewsCount = twitchVideo.ViewsCount,
                    PreviewImageUri = twitchVideo.PreviewImageUri,
                    ChannelTitle = twitchVideo.ChannelTitle,
                    ChannelUri = twitchVideo.ChannelUri,
                    ChannelLogoUri = twitchVideo.ChannelLogoUri
                };

                this.TwitchVideos.Add(video);
            }

            this.IsLoadingTwitchContent = false;
            this.UpdateTwitchContentVisibility();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateTwitchContentVisibility()
        {
            if (!this.IsLoadingTwitchContent
                && this.TwitchVideos != null
                && this.TwitchVideos.Count > 0)
            {
                this.TwitchContentVisibility = Visibility.Visible;
            }
            else
            {
                this.TwitchContentVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeTwitchService()
        {
            this._twitchService = new TwitchService();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnTwitchVideoSelected(object eventArgs)
        {
            ItemClickEventArgs args = (eventArgs as ItemClickEventArgs);
            Video video = (args.ClickedItem as Video);

            string twitchContentUri = video.VideoUri.ToString();

            this.OpenTab(new OpenTabEventArgs(twitchContentUri));
        }

        #endregion
    }
}
