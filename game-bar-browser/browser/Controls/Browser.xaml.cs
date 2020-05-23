using browser.ViewModels;
using System;
using Windows.UI.Xaml.Controls;

// Die Elementvorlage "Benutzersteuerelement" wird unter https://go.microsoft.com/fwlink/?LinkId=234236 dokumentiert.

namespace browser.Controls
{
    public sealed partial class Browser : UserControl
    {
        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        public BrowserControlViewModel ViewModel;

        #endregion

        public Browser()
        {
            this.InitializeComponent();

            this.DataContext = ViewModel = new BrowserControlViewModel();

            this.LoadWebViewEnv();
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
            this.LoadWebViewEnv();

            this.ViewModel.WebViewDocumentTitleChangedCommand.Execute(this.BrowserWidget_MainContent_WebView.DocumentTitle);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadWebViewEnv()
        {
            string functionString = " new MutationObserver(function () { window.external.notify(document.title); }).observe(document.querySelector('title'), { childList: true })";

            this.BrowserWidget_MainContent_WebView.InvokeScriptAsync("eval", new string[] { functionString });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWidget_MainContent_WebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            string title = e.Value;

            this.ViewModel.WebViewDocumentTitleChangedCommand.Execute(title);
        }

        #endregion
    }
}
