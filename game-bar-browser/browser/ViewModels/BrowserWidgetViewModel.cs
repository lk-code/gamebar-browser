﻿/**
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
using Windows.UI.Xaml.Data;

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
            Browser browserControl = new Browser();

            browserControl.ViewModel.OnDocumentTitleChanged += ViewModel_OnDocumentTitleChanged;
            browserControl.ViewModel.OnDocumentIconChanged += ViewModel_OnDocumentIconChanged;

            TabUiItem tabUiItem = new TabUiItem
            {
                DocumentTitle = " ",
                Content = browserControl
            };

            this.CurrentTabUiItems.Add(tabUiItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void ViewModel_OnDocumentIconChanged(object source, DocumentIconChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void ViewModel_OnDocumentTitleChanged(object source, DocumentTitleChangedEventArgs e)
        {
            this.ProcessDocumentTitleChanged(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessDocumentTitleChanged(DocumentTitleChangedEventArgs eventArgs)
        {
            Guid browserId = eventArgs.BrowserId;
            string documentTitle = eventArgs.DocumentTitle;

            /*

            TabUiItem tabUiItem = this.CurrentTabUiItems.FirstOrDefault(x => x.Content.GetType() == typeof(Browser) && (x.Content as Browser).ViewModel.Id.Equals(browserId));
            int index = this.CurrentTabUiItems.IndexOf(tabUiItem);
            if (tabUiItem != null)
            {
                tabUiItem.DocumentTitle = documentTitle;
            }

            this.SendPropertyChanged("CurrentTabUiItems");
            /* */
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
