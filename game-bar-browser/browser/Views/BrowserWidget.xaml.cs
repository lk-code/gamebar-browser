using browser.ViewModels;
using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace browser.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class BrowserWidget : Page
    {
        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        BrowserWidgetViewModel viewModel;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public BrowserWidget()
        {
            this.InitializeComponent();

            this.DataContext = viewModel = new BrowserWidgetViewModel();
        }

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.viewModel.XboxGameBarWidgetInstance = (e.Parameter as XboxGameBarWidget);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        private void GoToPage(string uriValue)
        {
            Uri uri = new Uri(uriValue);
            this.GoToPage(uri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        private void GoToPage(Uri uri)
        {
            this.BrowserWidget_MainContent_WebView.Source = uri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnElementClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // Do custom logic
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_HeaderUriAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                List<string> suggestions = new List<string>()
                {
                    sender.Text,
                    sender.Text + "1",
                    sender.Text + "2",
                    sender.Text + "3",
                    sender.Text + "4",
                    sender.Text + "5",
                    sender.Text + "6",
                    sender.Text + "7",
                    sender.Text + "8",
                    sender.Text + "9",
                    sender.Text + "10",
                    sender.Text + "11"
                };
                this.BrowserWidget_HeaderUriAutoSuggestBox.ItemsSource = suggestions;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void BrowserWidget_HeaderUriAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            this.GoToPage(args.SelectedItem.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_HeaderUriAutoSuggestBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            switch (e.Key)
            {
                case Windows.System.VirtualKey.Enter:
                case Windows.System.VirtualKey.GamepadA:
                    {
                        // enter the new url
                        this.GoToPage(this.BrowserWidget_HeaderUriAutoSuggestBox.Text);
                    }
                    break;
            }
        }
    }
}
