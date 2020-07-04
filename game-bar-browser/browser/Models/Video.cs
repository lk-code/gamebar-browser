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

using browser.ViewModels;
using System;

namespace browser.Models
{
    public class Video : BaseViewModel
    {
        string _videoTitle = " ";
        /// <summary>
        /// 
        /// </summary>
        public string VideoTitle
        {
            get
            {
                return _videoTitle;
            }
            set
            {
                _videoTitle = value;
            }
        }

        Uri _videoUri = null;
        /// <summary>
        /// 
        /// </summary>
        public Uri VideoUri
        {
            get
            {
                return _videoUri;
            }
            set
            {
                _videoUri = value;
            }
        }

        long _viewsCount = 0;
        /// <summary>
        /// 
        /// </summary>
        public long ViewsCount
        {
            get
            {
                return _viewsCount;
            }
            set
            {
                _viewsCount = value;
            }
        }

        Uri _previewImageUri = null;
        /// <summary>
        /// 
        /// </summary>
        public Uri PreviewImageUri
        {
            get
            {
                return _previewImageUri;
            }
            set
            {
                _previewImageUri = value;
            }
        }

        string _channelTitle = " ";
        /// <summary>
        /// 
        /// </summary>
        public string ChannelTitle
        {
            get
            {
                return _channelTitle;
            }
            set
            {
                _channelTitle = value;
            }
        }

        Uri _channelLogoUri = null;
        /// <summary>
        /// 
        /// </summary>
        public Uri ChannelLogoUri
        {
            get
            {
                return _channelLogoUri;
            }
            set
            {
                _channelLogoUri = value;
            }
        }

        Uri _channelUri = null;
        /// <summary>
        /// 
        /// </summary>
        public Uri ChannelUri
        {
            get
            {
                return _channelUri;
            }
            set
            {
                _channelUri = value;
            }
        }
    }
}
