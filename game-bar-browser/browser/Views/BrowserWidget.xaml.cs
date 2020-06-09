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

using browser.Models;
using browser.ViewModels;
using Microsoft.Gaming.XboxGameBar;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace browser.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class BrowserWidget : Page
    {
        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        private readonly BrowserWidgetViewModel _viewModel;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public BrowserWidget()
        {
            this.InitializeComponent();

            this.DataContext = _viewModel = new BrowserWidgetViewModel();

            this.RegisterEvents();
        }

        #region # private logic #

        private void RegisterEvents()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this._viewModel.XboxGameBarWidgetInstance = (e.Parameter as XboxGameBarWidget);
        }

        #endregion

        #region #  event to viewmodel commands #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_MainTabView_AddTabButtonClick(Microsoft.UI.Xaml.Controls.TabView sender, object args)
        {
            this._viewModel.TabViewAddTabClickCommand.Execute(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_MainTabView_TabCloseRequested(Microsoft.UI.Xaml.Controls.TabView sender, Microsoft.UI.Xaml.Controls.TabViewTabCloseRequestedEventArgs args)
        {
            this._viewModel.TabViewTabCloseRequestedCommand.Execute(args);
        }

        #endregion
    }
}
