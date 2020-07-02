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

using browser.ViewModels.AppViews;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace browser.AppViews
{
    public sealed partial class SettingsView : UserControl
    {
        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        private SettingsViewModel _viewModel;

        /// <summary>
        /// 
        /// </summary>
        public SettingsViewModel ViewModel
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

        public SettingsView()
        {
            this.InitializeComponent();

            this.DataContext = _viewModel = new SettingsViewModel();
        }

        #region #  event to viewmodel commands #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserSettings_SearchEngineComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this._viewModel.SearchEngineSelectionChangedCommand.Execute(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserSettings_HomepageUriTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this._viewModel.HomepageUriLostFocusCommand.Execute(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserSettings_ShowHomepageButtonToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            this._viewModel.ShowHomepageButtonToggledCommand.Execute(e);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserSettings_DeleteStorageNoButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.BrowserSettings_DeleteStorageButton.Flyout is Flyout flayout)
            {
                flayout.Hide();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserSettings_DeleteStorageYesButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.BrowserSettings_DeleteStorageButton.Flyout is Flyout flayout)
            {
                flayout.Hide();
            }
        }
    }
}
