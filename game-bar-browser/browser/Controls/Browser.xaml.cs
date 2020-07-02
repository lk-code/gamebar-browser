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
using browser.ViewModels.Controls;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace browser.Controls
{
    public sealed partial class Browser : UserControl
    {
        #region # public properties #

        private BrowserControlViewModel _viewModel = null;

        /// <summary>
        /// 
        /// </summary>
        public BrowserControlViewModel ViewModel
        {
            get
            {
                return _viewModel;
            }
            set
            {
                _viewModel = value;
            }
        }

        #endregion

        public Browser()
        {
            this.InitializeComponent();

            this.DataContext = ViewModel = new BrowserControlViewModel();
        }

        #region # public methods #

        #endregion

        #region # readout document title #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_MainContent_WebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            this.ViewModel.WebViewNavigationCompletedCommand.Execute(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_MainContent_WebView_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            LoadDocumentTitleToViewModel();
        }

        #endregion

        #region # event to viewmodel commands #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_HeaderUriAutoSuggestBox_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            this.ViewModel.AdressBarKeyUpCommand.Execute(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_HeaderUriAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            this.ViewModel.AdressBarSuggestionChosenCommand.Execute(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_MainContent_WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            this.ViewModel.WebViewNavigationStartingCommand.Execute(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_HeaderHomeActionButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.ViewModel.ActionButtonHomeClickCommand.Execute(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_HeaderRefreshActionButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.BrowserWidget_MainContent_WebView.Refresh();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_HeaderForwardActionButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.ViewModel.ActionButtonForwardClickCommand.Execute(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_HeaderBackActionButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.ViewModel.ActionButtonBackClickCommand.Execute(e);
        }

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task LoadDocumentTitleToViewModel()
        {
            string html = await this.BrowserWidget_MainContent_WebView.InvokeScriptAsync("eval", new string[] { "document.documentElement.outerHTML;" });
            this.ViewModel.WebViewDOMContentLoadedCommand.Execute(html);
        }

        #endregion
    }
}