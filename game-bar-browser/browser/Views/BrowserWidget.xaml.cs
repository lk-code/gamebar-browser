using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        private void TextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            switch(e.Key)
            {
                case Windows.System.VirtualKey.Enter:
                case Windows.System.VirtualKey.GamepadA:
                    {
                        // enter the new url
                        this.GoToPage(this.BrowserWidget_HeaderUri_TextBox.Text);
                    } break;
            }
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
    }
}
