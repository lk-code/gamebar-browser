using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace browser.Views
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class BrowserWidget : Page
    {
        /// <summary>
        /// 
        /// </summary>
        private XboxGameBarWidget _widget = null;
        /// <summary>
        /// 
        /// </summary>
        private XboxGameBarWidgetControl _widgetControl = null;

        /// <summary>
        /// 
        /// </summary>
        public BrowserWidget()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _widget = e.Parameter as XboxGameBarWidget;
            _widgetControl = new XboxGameBarWidgetControl(_widget);

            // events
            _widget.SettingsClicked += Widget_SettingsClicked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void Widget_SettingsClicked(XboxGameBarWidget sender, object args)
        {
            await _widget.ActivateSettingsAsync();
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_AppMenuCommandBarFlyout_ShareButton_Clicked(object sender, RoutedEventArgs e)
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            DataTransferManager.ShowShareUI();
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string pageTitle = "Google";
            string pageUri = "https://www.google.de";

            DataRequest request = args.Request;
            request.Data.Properties.Title = pageTitle;
            request.Data.Properties.Description = pageUri;
            request.Data.SetText($"{pageTitle} {pageUri}");
        }
    }
}
