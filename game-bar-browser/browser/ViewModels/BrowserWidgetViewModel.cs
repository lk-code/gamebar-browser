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

using browser.AppViews;
using browser.Controls;
using browser.core.Components.WebUriProcessor;
using browser.Core;
using browser.Models;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Gaming.XboxGameBar;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace browser.ViewModels
{
    public class BrowserWidgetViewModel : WindowViewModel
    {
        #region # events #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public delegate void OnTabIndexChangedEventHandler(object source, TabIndexChangedEventArgs e);
        /// <summary>
        /// 
        /// </summary>
        public event OnTabIndexChangedEventHandler OnTabIndexChanged;

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

        private ICommand _tabViewActionSettingButtonClickCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand TabViewActionSettingButtonClickCommand => _tabViewActionSettingButtonClickCommand ?? (_tabViewActionSettingButtonClickCommand = new RelayCommand((eventArgs) => { OnTabViewActionSettingButtonClick(eventArgs as RoutedEventArgs); }));

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

        ItemsChangeObservableCollection<TabUiItem> _currentTabUiItems = new ItemsChangeObservableCollection<TabUiItem>();
        /// <summary>
        /// 
        /// </summary>
        public ItemsChangeObservableCollection<TabUiItem> CurrentTabUiItems
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
            this.CurrentTabUiItems = new ItemsChangeObservableCollection<TabUiItem>();
            this.RegisterEvents();

            this.CheckTabItemsEmptyTabs();
        }

        #endregion

        #region # public methods #

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        private void RegisterEvents()
        {
            Messenger.Default.Register(MessagingEventTypes.OPEN_VIEW_ABOUT_THIS_APP, async (MessagingEventTypes type) =>
            {
                if(type != MessagingEventTypes.OPEN_VIEW_ABOUT_THIS_APP)
                {
                    return;
                }
                
                await this._currentWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    this.OpenAboutPage();
                });

            });
        }

        /// <summary>
        /// 
        /// </summary>
        private TabUiItem GenerateAboutPage()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("Resources");
            string title = resources.GetString("ViewTitleAboutThisApp");
            IconSource icon = new FontIconSource { Glyph = "\uE946", FontFamily = new FontFamily("Segoe MDL2 Assets"), FontSize = 20 };
            AboutView view = new AboutView();

            view.ViewModel.OnOpenTabRequested += ViewModel_OnOpenTabRequested;

            TabUiItem tabUiItem = this.GenerateTabUiItem(view, title, icon);

            return tabUiItem;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenAboutPage()
        {
            TabUiItem tabUiItem = this.GenerateAboutPage();

            this.AddTab(tabUiItem);
        }

        /// <summary>
        /// 
        /// </summary>
        private TabUiItem GenerateSettingsPage()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("Resources");
            string title = resources.GetString("ViewTitleSettings");
            IconSource icon = new FontIconSource { Glyph = "\uE713", FontFamily = new FontFamily("Segoe MDL2 Assets"), FontSize = 20 };
            SettingsView view = new SettingsView();

            view.ViewModel.OnOpenTabRequested += ViewModel_OnOpenTabRequested;

            TabUiItem tabUiItem = this.GenerateTabUiItem(view, title, icon);

            return tabUiItem;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenSettingsPage()
        {
            TabUiItem tabUiItem = this.GenerateSettingsPage();

            this.AddTab(tabUiItem);
        }

        /// <summary>
        /// 
        /// </summary>
        private TabUiItem GenerateGamerStartPage()
        {
            ResourceLoader resources = ResourceLoader.GetForCurrentView("Resources");
            string title = resources.GetString("ViewTitleGamerStart");
            IconSource icon = new FontIconSource { Glyph = "\uE7FC", FontFamily = new FontFamily("Segoe MDL2 Assets"), FontSize = 20 };
            GamerStartPage view = new GamerStartPage();

            view.ViewModel.OnOpenTabRequested += ViewModel_OnOpenTabRequested;

            TabUiItem tabUiItem = this.GenerateTabUiItem(view, title, icon);

            return tabUiItem;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenGamerStartPage()
        {
            TabUiItem tabUiItem = this.GenerateGamerStartPage();

            this.AddTab(tabUiItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        /// <param name="icon"></param>
        private TabUiItem GenerateTabUiItem(object content, string title, IconSource icon)
        {
            TabUiItem tabUiItem = new TabUiItem
            {
                DocumentTitle = title,
                Content = content,
                DocumentIcon = icon
            };

            return tabUiItem;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routedEventArgs"></param>
        private void OnTabViewActionSettingButtonClick(RoutedEventArgs routedEventArgs)
        {
            // this.OpenXboxGameBarWidgetSettingsAsync();
            this.OpenSettingsPage();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnTabViewAddTabClick(RoutedEventArgs eventArgs)
        {
            this.OpenNewDefaultWebViewTab();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenNewDefaultWebViewTab()
        {
            Browser browserControl = new Browser();

            browserControl.ViewModel.OnWebViewHeaderChanged += ViewModel_OnWebViewHeaderChanged;
            browserControl.ViewModel.OnOpenTabRequested += ViewModel_OnOpenTabRequested;

            TabUiItem tabUiItem = new TabUiItem
            {
                DocumentTitle = " ",
                Content = browserControl
            };

            this.AddTab(tabUiItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewModel_OnOpenTabRequested(object sender, EventArgs e)
        {
            OpenTabEventArgs eventArgs = (e as OpenTabEventArgs);

            TabUiItem requestedTab = this.GetRequestedTabFromOpenTabEventArgs(eventArgs);

            this.AddTab(requestedTab);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        private TabUiItem GetRequestedTabFromOpenTabEventArgs(OpenTabEventArgs eventArgs)
        {
            bool isValidUrl = WebUriProcessorComponent.IsValidUri(eventArgs.RequestedUrl);

            if (isValidUrl)
            {
                Uri uri = new Uri(eventArgs.RequestedUrl);

                if(uri.Scheme.ToLowerInvariant() == App.BROWSER_RESERVED_SCHEME)
                {
                    switch(uri.Host)
                    {
                        case BrowserReservedPages.ABOUT:
                            {
                                TabUiItem aboutTabUiItem = this.GenerateAboutPage();

                                return aboutTabUiItem;
                            }
                            break;
                        case BrowserReservedPages.SETTINGS:
                            {
                                TabUiItem aboutTabUiItem = this.GenerateSettingsPage();

                                return aboutTabUiItem;
                            }
                            break;
                    }
                }
            }

            return new TabUiItem();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabUiItem"></param>
        private void AddTab(TabUiItem tabUiItem)
        {
            /*
            foreach(TabUiItem tab in this.CurrentTabUiItems)
            {
                tab.IsSelected = false;
            }

            tabUiItem.IsSelected = true;
            /* */

            this.CurrentTabUiItems.Add(tabUiItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void ViewModel_OnWebViewHeaderChanged(object source, WebViewHeaderChangedEventArgs e)
        {
            this.ProcessWebViewHeaderChanged(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessWebViewHeaderChanged(WebViewHeaderChangedEventArgs eventArgs)
        {
            Guid browserId = eventArgs.BrowserId;

            TabUiItem tabUiItem = this.CurrentTabUiItems.FirstOrDefault(x => x.Content.GetType() == typeof(Browser) && (x.Content as Browser).ViewModel.GetId().Equals(browserId));

            if (tabUiItem != null)
            {
                string documentTitle = eventArgs.DocumentTitle;
                tabUiItem.DocumentTitle = documentTitle;

                string documentIcon = eventArgs.DocumentIcon;
                if(documentIcon == null)
                {
                    tabUiItem.DocumentIcon = null;
                } else
                {
                    tabUiItem.DocumentIcon = new BitmapIconSource { UriSource = new Uri(documentIcon), ShowAsMonochrome = false };
                }
            }
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

            this.CheckTabItemsEmptyTabs();
        }

        /// <summary>
        /// 
        /// </summary>
        private void CheckTabItemsEmptyTabs()
        {
            if(this.CurrentTabUiItems.Count() <= 0)
            {
                this.OpenNewDefaultWebViewTab();
                this.OpenGamerStartPage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void XboxGameBarWidgetInstance_SettingsClicked(XboxGameBarWidget sender, object args)
        {
            this.OpenXboxGameBarWidgetSettingsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task OpenXboxGameBarWidgetSettingsAsync()
        {
            await this.XboxGameBarWidgetInstance.ActivateSettingsAsync();
        }

        #endregion
    }
}
