using browser.Controls;
using browser.Core;
using browser.Models;
using Microsoft.Gaming.XboxGameBar;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace browser.ViewModels
{
    public class BrowserWidgetViewModel : BaseViewModel
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        private ICommand _tabViewAddTabClickCommand;
        /// <summary>
        /// 
        /// </summary>()
        public ICommand TabViewAddTabClickCommand => _tabViewAddTabClickCommand ?? (_tabViewAddTabClickCommand = new RelayCommand((eventArgs) => { OnTabViewAddTabClick(eventArgs as RoutedEventArgs); }));

        private ICommand _tabViewTabCloseRequestedCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand TabViewTabCloseRequestedCommand => _tabViewTabCloseRequestedCommand ?? (_tabViewTabCloseRequestedCommand = new RelayCommand((eventArgs) => { OnTabViewTabCloseRequested(eventArgs as TabViewTabCloseRequestedEventArgs); }));

        #endregion

        #region # private properties #

        #endregion

        #region # public properties #

        XboxGameBarWidget _xboxGameBarWidgetInstance = null;
        /// <summary>
        /// 
        /// </summary>
        public XboxGameBarWidget XboxGameBarWidgetInstance
        {
            get
            {
                return _xboxGameBarWidgetInstance;
            }
            set
            {
                _xboxGameBarWidgetInstance = value;

                this.XboxGameBarWidgetControlInstance = new XboxGameBarWidgetControl(this._xboxGameBarWidgetInstance);

                _xboxGameBarWidgetInstance.SettingsClicked += XboxGameBarWidgetInstance_SettingsClicked;
            }
        }

        XboxGameBarWidgetControl _xboxGameBarWidgetControlInstance = null;
        /// <summary>
        /// 
        /// </summary>
        public XboxGameBarWidgetControl XboxGameBarWidgetControlInstance
        {
            get
            {
                return _xboxGameBarWidgetControlInstance;
            }
            set
            {
                _xboxGameBarWidgetControlInstance = value;
            }
        }

        ObservableCollection<TabUiItem> _currentTabUiItems = new ObservableCollection<TabUiItem>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<TabUiItem> CurrentTabUiItems
        {
            get
            {
                return _currentTabUiItems;
            }
            set
            {
                _currentTabUiItems = value;
            }
        }

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public BrowserWidgetViewModel() : base(Window.Current)
        {
            this.CurrentTabUiItems = new ObservableCollection<TabUiItem>();
        }

        #endregion

        #region # public methods #

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnTabViewAddTabClick(RoutedEventArgs eventArgs)
        {
            this.CurrentTabUiItems.Add(new TabUiItem
            {
                DocumentTitle = " ",
                Content = new Browser()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnTabViewTabCloseRequested(TabViewTabCloseRequestedEventArgs eventArgs)
        {
            TabUiItem tabUiItem = (eventArgs.Item as TabUiItem);
            TabViewItem tabViewItem = (eventArgs.Tab as TabViewItem);

            this.CurrentTabUiItems.Remove(tabUiItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void XboxGameBarWidgetInstance_SettingsClicked(XboxGameBarWidget sender, object args)
        {
            this.OpenXboxGameBarWidgetSettings();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void OpenXboxGameBarWidgetSettings()
        {
            await this.XboxGameBarWidgetInstance.ActivateSettingsAsync();
        }

        #endregion
    }
}
