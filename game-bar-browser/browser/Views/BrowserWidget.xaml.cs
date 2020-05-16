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
        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        BrowserWidgetViewModel ViewModel;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public BrowserWidget()
        {
            this.InitializeComponent();

            this.DataContext = ViewModel = new BrowserWidgetViewModel();

            this.LoadWebViewEnv();
        }

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ViewModel.XboxGameBarWidgetInstance = (e.Parameter as XboxGameBarWidget);
        }

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
