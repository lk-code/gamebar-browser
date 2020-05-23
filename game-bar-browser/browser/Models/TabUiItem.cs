using browser.ViewModels;

namespace browser.Models
{
    public class TabUiItem : BaseViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string DocumentTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.UI.Xaml.Controls.IconSource DocumentIcon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Content { get; set; }
    }
}
