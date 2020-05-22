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
    }
}
