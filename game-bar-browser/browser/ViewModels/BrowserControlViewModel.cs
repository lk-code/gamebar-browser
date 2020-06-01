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

using browser.Components.History;
using browser.Components.SearchEngine;
using browser.Components.Storage;
using browser.Components.TempHistory;
using browser.core.Components.WebUriProcessor;
using browser.Core;
using browser.Models;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace browser.ViewModels
{
    public class BrowserControlViewModel : WindowViewModel
    {
        #region # events #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public delegate void OnDocumentTitleChangedEventHandler(object source, DocumentTitleChangedEventArgs e);
        /// <summary>
        /// 
        /// </summary>
        public event OnDocumentTitleChangedEventHandler OnDocumentTitleChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public delegate void OnDocumentIconChangedEventHandler(object source, DocumentIconChangedEventArgs e);
        /// <summary>
        /// 
        /// </summary>
        public event OnDocumentIconChangedEventHandler OnDocumentIconChanged;

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        private ICommand _openSettingsCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenSettingsCommand => _openSettingsCommand ?? (_openSettingsCommand = new RelayCommand((eventArgs) => { this.OpenXboxGameBarWidgetSettings(); }));

        private ICommand _openShareMenuCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenShareMenuCommand => _openShareMenuCommand ?? (_openShareMenuCommand = new RelayCommand((eventArgs) => { this.OpenShareMenu(); }));

        private ICommand _adressBarKeyUpCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand AdressBarKeyUpCommand => _adressBarKeyUpCommand ?? (_adressBarKeyUpCommand = new RelayCommand((eventArgs) => { this.ProcessAdressVarKeyUp(eventArgs as KeyRoutedEventArgs); }));

        private ICommand _webViewNavigationStartingCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand WebViewNavigationStartingCommand => _webViewNavigationStartingCommand ?? (_webViewNavigationStartingCommand = new RelayCommand((eventArgs) => { this.ProcessWebViewNavigationStarting(eventArgs as WebViewNavigationStartingEventArgs); }));

        private ICommand _adressBarSuggestionChosenCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand AdressBarSuggestionChosenCommand => _adressBarSuggestionChosenCommand ?? (_adressBarSuggestionChosenCommand = new RelayCommand((eventArgs) => { this.ProcessAdressBarSuggestionChosen((eventArgs as AutoSuggestBoxSuggestionChosenEventArgs)); }));

        private ICommand _webViewDocumentTitleChangedCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand WebViewDocumentTitleChangedCommand => _webViewDocumentTitleChangedCommand ?? (_webViewDocumentTitleChangedCommand = new RelayCommand((eventArgs) => { this.ProcessWebViewDocumentTitleChanged((eventArgs as string)); }));

        private ICommand _webViewNavigationCompletedCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand WebViewNavigationCompletedCommand => _webViewNavigationCompletedCommand ?? (_webViewNavigationCompletedCommand = new RelayCommand((eventArgs) => { this.ProcessWebViewNavigationCompleted((eventArgs as WebViewNavigationCompletedEventArgs)); }));

        private ICommand _actionButtonBackClickCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand ActionButtonBackClickCommand => _actionButtonBackClickCommand ?? (_actionButtonBackClickCommand = new RelayCommand((eventArgs) => { this.ProcessActionButtonBackClick((eventArgs as RoutedEventArgs)); }));

        private ICommand _actionButtonForwardClickCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand ActionButtonForwardClickCommand => _actionButtonForwardClickCommand ?? (_actionButtonForwardClickCommand = new RelayCommand((eventArgs) => { this.ProcessActionButtonForwardClick((eventArgs as RoutedEventArgs)); }));

        private ICommand _actionButtonRefreshClickCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand ActionButtonRefreshClickCommand => _actionButtonRefreshClickCommand ?? (_actionButtonRefreshClickCommand = new RelayCommand((eventArgs) => { this.ProcessActionButtonRefreshClick((eventArgs as RoutedEventArgs)); }));

        private ICommand _actionButtonHomeClickCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand ActionButtonHomeClickCommand => _actionButtonHomeClickCommand ?? (_actionButtonHomeClickCommand = new RelayCommand((eventArgs) => { this.ProcessActionButtonHomeClick((eventArgs as RoutedEventArgs)); }));

        private ICommand _openFeedbackCommand;
        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenFeedbackCommand => _openFeedbackCommand ?? (_openFeedbackCommand = new RelayCommand((eventArgs) => { this.LaunchFeedbackHub(); }));

        #endregion

        #region # private properties #

        /// <summary>
        /// 
        /// </summary>
        private StorageManager _storageManager { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        private SearchEngineProcessor _searchEngineProcessor { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        private HistoryManager _historyManager { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        private TempHistoryManager _tempHistoryManager { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        private bool _navigateInTempHistory { get; set; } = false;

        #endregion

        #region # public properties #

        /// <summary>
        /// 
        /// </summary>
        public readonly Guid Id = Guid.NewGuid();

        string _adressBarDisplayText = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string AdressBarDisplayText
        {
            get
            {
                return _adressBarDisplayText;
            }
            set
            {
                SetProperty(ref _adressBarDisplayText, value);
            }
        }

        Uri _webViewAdressSource = null;
        /// <summary>
        /// 
        /// </summary>
        public Uri WebViewAdressSource
        {
            get
            {
                return _webViewAdressSource;
            }
            set
            {
                SetProperty(ref _webViewAdressSource, value);

                this.ProcessChangingWebViewSource(this._webViewAdressSource);
            }
        }

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

        ObservableCollection<string> _adressBarSuggestValues = new ObservableCollection<string>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<string> AdressBarSuggestValues
        {
            get
            {
                return _adressBarSuggestValues;
            }
            set
            {
                _adressBarSuggestValues = value;
            }
        }

        bool _showHomepageButton = false;
        /// <summary>
        /// 
        /// </summary>
        public bool ShowHomepageButton
        {
            get
            {
                return _showHomepageButton;
            }
            set
            {
                SetProperty(ref _showHomepageButton, value);
            }
        }

        string _webViewCurrentTitle = " ";
        /// <summary>
        /// 
        /// </summary>
        public string WebViewCurrentTitle
        {
            get
            {
                return _webViewCurrentTitle;
            }
            set
            {
                SetProperty(ref _webViewCurrentTitle, value);

                OnDocumentTitleChanged(this, new DocumentTitleChangedEventArgs(this.Id, _webViewCurrentTitle));
            }
        }

        bool _isActionButtonBackEnabled = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsActionButtonBackEnabled
        {
            get
            {
                return _isActionButtonBackEnabled;
            }
            set
            {
                SetProperty(ref _isActionButtonBackEnabled, value);
            }
        }

        bool _isActionButtonForwardEnabled = false;
        /// <summary>
        /// 
        /// </summary>
        public bool IsActionButtonForwardEnabled
        {
            get
            {
                return _isActionButtonForwardEnabled;
            }
            set
            {
                SetProperty(ref _isActionButtonForwardEnabled, value);
            }
        }

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public BrowserControlViewModel() : base(Window.Current)
        {
            this.RegisterEvents();
            this.InitializeStorageManager();
            this.InitializeHistoryManager();
            this.InitializeSearchEngineProcessor();
            this.InitializeTemporaryHistory();
            this.InitializeAdressBarButtons();
            this.LoadHomepageToWebView();
        }

        #endregion

        #region # public methods #

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        private async void LaunchFeedbackHub()
        {
            // This launcher is part of the Store Services SDK https://docs.microsoft.com/windows/uwp/monetize/microsoft-store-services-sdk
            var launcher = Microsoft.Services.Store.Engagement.StoreServicesFeedbackLauncher.GetDefault();
            await launcher.LaunchAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RegisterEvents()
        {
            Messenger.Default.Register<MessagingEventTypes>(MessagingEventTypes.SETTING_SHOW_HOMEPAGE_BUTTON, (MessagingEventTypes type) =>
            {
                this.ProcessSettingShowHomepageButtonChanged(type);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="MessagingEventTypes"></typeparam>
        /// <param name="notificationMessageAction"></param>
        private void ProcessSettingShowHomepageButtonChanged<MessagingEventTypes>(MessagingEventTypes type)
        {
            this.LoadActionBarHomepageButton();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeTemporaryHistory()
        {
            this._tempHistoryManager = new TempHistoryManager();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessActionButtonBackClick(RoutedEventArgs eventArgs)
        {
            if(this._tempHistoryManager.CanGoBack() == true)
            {
                this._navigateInTempHistory = true;

                Uri backUriValue = this._tempHistoryManager.GoBack();

                this.GoToUri(backUriValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessActionButtonForwardClick(RoutedEventArgs eventArgs)
        {
            if (this._tempHistoryManager.CanGoForward() == true)
            {
                this._navigateInTempHistory = true;

                Uri forwardUriValue = this._tempHistoryManager.GoForward();

                this.GoToUri(forwardUriValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessTempHistoryActionButtons()
        {
            this.IsActionButtonBackEnabled = false;
            this.IsActionButtonForwardEnabled = false;

            if (this._tempHistoryManager.CanGoBack())
            {
                this.IsActionButtonBackEnabled = true;
            }

            if (this._tempHistoryManager.CanGoForward())
            {
                this.IsActionButtonForwardEnabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessActionButtonRefreshClick(RoutedEventArgs eventArgs)
        {
            string currentAdressUri = this.WebViewAdressSource.ToString();

            this.WebViewAdressSource = new Uri(currentAdressUri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessActionButtonHomeClick(RoutedEventArgs eventArgs)
        {
            this.LoadHomepageToWebView();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessWebViewNavigationCompleted(WebViewNavigationCompletedEventArgs eventArgs)
        {
            this._historyManager.Add(this.WebViewCurrentTitle, new Uri(this.AdressBarDisplayText), DateTime.Now);
            this.AddPageToTempHistory(this.WebViewCurrentTitle, this.AdressBarDisplayText);

            this.ProcessTempHistoryActionButtons();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webViewCurrentTitle"></param>
        /// <param name="adressBarDisplayText"></param>
        private void AddPageToTempHistory(string webViewCurrentTitle, string adressBarDisplayText)
        {
            if(this._navigateInTempHistory == true)
            {
                this._navigateInTempHistory = false;

                return;
            }

            this._tempHistoryManager.Add(webViewCurrentTitle, new Uri(adressBarDisplayText));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessWebViewDocumentTitleChanged(string eventArgs)
        {
            this.WebViewCurrentTitle = eventArgs;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadHomepageToWebView()
        {
            string homepageUri = this._storageManager.GetValue<string>(StorageKey.HOMEPAGE_URI, StorageDefaults.HOMEPAGE_URI);
            homepageUri = homepageUri.Trim();

            if (string.IsNullOrEmpty(homepageUri)
                || string.IsNullOrWhiteSpace(homepageUri))
            {
                return;
            }

            Uri targetUri = this.GetProcessedUriFromRequestValue(homepageUri);

            this.GoToUri(targetUri);
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeAdressBarButtons()
        {
            this.LoadActionBarHomepageButton();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadActionBarHomepageButton()
        {
            bool showHomepageButton = this._storageManager.GetValue<bool>(StorageKey.SHOW_HOMEPAGE_BUTTON, StorageDefaults.SHOW_HOMEPAGE_BUTTON);

            this.ShowHomepageButton = showHomepageButton;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeSearchEngineProcessor()
        {
            this._searchEngineProcessor = new SearchEngineProcessor();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeStorageManager()
        {
            this._storageManager = new StorageManager();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeHistoryManager()
        {
            this._historyManager = new HistoryManager();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webViewSource"></param>
        private void ProcessChangingWebViewSource(Uri webViewSource)
        {
            if(webViewSource.ToString() != this._adressBarDisplayText)
            {
                this.AdressBarDisplayText = webViewSource.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedUriValue"></param>
        private void ProcessAdressBarValue(string requestedUriValue)
        {
            string sourceUriValue = requestedUriValue;
            Uri targetUri = this.GetProcessedUriFromRequestValue(requestedUriValue);

            this.GoToUri(targetUri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetUri"></param>
        private void GoToUri(Uri targetUri)
        {
            this.AdressBarDisplayText = targetUri.ToString();
            this.WebViewAdressSource = targetUri;

            this.ProcessTempHistoryActionButtons();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedUriValue"></param>
        private Uri GetProcessedUriFromRequestValue(string requestedUriValue)
        {
            string sourceUriValue = requestedUriValue;
            Uri targetUri = null;
            WebUriProcessorComponent webUriProcessorComponent = new WebUriProcessorComponent();

            // is valid url
            if (webUriProcessorComponent.IsValidUri(sourceUriValue))
            {
                targetUri = new Uri(sourceUriValue);
            }
            else if (webUriProcessorComponent.IsValidAdress(sourceUriValue))
            {
                targetUri = webUriProcessorComponent.CreateUriFromDomain(sourceUriValue);
            }
            else
            {
                string selectedSearchEngineKey = this._storageManager.GetValue<string>(StorageKey.SEARCH_ENGINE, StorageDefaults.SEARCH_ENGINE);

                targetUri = this._searchEngineProcessor.RenderSearchUri(selectedSearchEngineKey, sourceUriValue);
            }

            return targetUri;
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
        private void ProcessAdressBarSuggestionChosen(AutoSuggestBoxSuggestionChosenEventArgs eventArgs)
        {
            string chosenSuggestValue = (eventArgs.SelectedItem as string);

            this.ProcessAdressBarValue(chosenSuggestValue);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessWebViewNavigationStarting(WebViewNavigationStartingEventArgs eventArgs)
        {
            Uri newUri = eventArgs.Uri;

            this.ProcessChangingWebViewSource(newUri);
        }

        /// <summary>
        /// 
        /// </summary>
        private void ProcessAdressVarKeyUp(KeyRoutedEventArgs eventArgs)
        {
            if(eventArgs.Key == Windows.System.VirtualKey.Enter)
            {
                // process the content
                string adressValue = this.AdressBarDisplayText;

                this.ProcessAdressBarValue(adressValue);

                return;
            }

            this.ProcessAutoSuggestValues();
        }

        /// <summary>
        /// 
        /// </summary>
        private async void ProcessAutoSuggestValues()
        {
            string currentAdressBarValue = this.AdressBarDisplayText;

            this.AdressBarSuggestValues.Clear();

            Dictionary<string, List<HistoryItem>> history = await this._historyManager.GetHistoryAsync();

            var foundedItems = history.Select(
                x => x.Value.Where(
                    y => (y.Title.ToLower().Contains(currentAdressBarValue.ToLower())
                    || y.Uri.ToString().ToLower().Contains(currentAdressBarValue.ToLower()))
                )
            );

            List<string> suggestEntries = new List<string>();

            foreach(var foundItemList in foundedItems)
            {
                List<HistoryItem> items = foundItemList.ToList();

                foreach (HistoryItem historyItem in items)
                {
                    suggestEntries.Add(historyItem.Uri.ToString());
                }
            }

            suggestEntries = suggestEntries.Distinct().ToList();

            foreach(string suggestEntry in suggestEntries)
            {
                this.AdressBarSuggestValues.Add(suggestEntry);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenShareMenu()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;

            DataTransferManager.ShowShareUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            string pageTitle = this.WebViewCurrentTitle;
            string pageUri = this.WebViewAdressSource.ToString();

            DataRequest request = args.Request;
            request.Data.Properties.Title = pageTitle;
            request.Data.Properties.Description = pageUri;
            request.Data.SetText($"{pageTitle} {pageUri}");
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
