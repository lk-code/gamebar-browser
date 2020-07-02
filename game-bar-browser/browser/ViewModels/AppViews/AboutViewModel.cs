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

using browser.Core;
using browser.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.System;
using Windows.UI.Xaml;

namespace browser.ViewModels.AppViews
{
    public class AboutViewModel : WindowViewModel, IBrowserContentElement
    {
        #region # events #

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler OnOpenTabRequested;

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        private ICommand _openProjectPageCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenProjectPageCommand => _openProjectPageCommand ?? (_openProjectPageCommand = new RelayCommand((eventArgs) => { this.OnOpenProjectPageClickAsync(); }));

        private ICommand _openDeveloperHomepageCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenDeveloperHomepageCommand => _openDeveloperHomepageCommand ?? (_openDeveloperHomepageCommand = new RelayCommand((eventArgs) => { this.OnOpenDeveloperHomepageClickAsync(); }));

        #endregion

        #region # private properties #

        #endregion

        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        public string AppVersionText
        {
            get
            {
                Package package = Package.Current;
                PackageId packageId = package.Id;
                PackageVersion version = packageId.Version;

                return string.Format("v{0}.{1} (Build {2})", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public AboutViewModel() : base(Window.Current)
        {
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
        private async Task OnOpenProjectPageClickAsync()
        {
            await Launcher.LaunchUriAsync(new Uri("https://github.com/lk-code/gamebar-browser"));
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task OnOpenDeveloperHomepageClickAsync()
        {
            await Launcher.LaunchUriAsync(new Uri("https://lk-code.azurewebsites.net/"));
        }

        #endregion
    }
}
