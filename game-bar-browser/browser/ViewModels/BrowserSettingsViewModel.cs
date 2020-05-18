﻿using browser.Components.SearchEngine;
using browser.Components.Storage;
using browser.Core;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Gaming.XboxGameBar;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace browser.ViewModels
{
    public class BrowserSettingsViewModel : BaseViewModel
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        /// <summary>
        /// 
        /// </summary>
        public ICommand SearchEngineSelectionChangedCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand ShowHomepageButtonToggledCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand HomepageUriLostFocusCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand SaveSettingsButtonClickCommand { get; set; }

        #endregion

        #region # private properties #

        /// <summary>
        /// If true a save button is shown and only when clicking on it will be saved.
        /// </summary>
        private const bool SAVE_VIA_BUTTON = false;

        /// <summary>
        /// 
        /// </summary>
        private StorageManager _storageManager { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        private SearchEngineProcessor _searchEngineProcessor { get; set; } = null;

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

        bool _isSettingsFormSaveButtonVisible = SAVE_VIA_BUTTON;
        /// <summary>
        /// 
        /// </summary>
        public bool IsSettingsFormSaveButtonVisible
        {
            get
            {
                return _isSettingsFormSaveButtonVisible;
            }
            set
            {
                SetProperty(ref _isSettingsFormSaveButtonVisible, value);
            }
        }

        ObservableCollection<SearchEngine> _availableSearchEngines = new ObservableCollection<SearchEngine>();
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<SearchEngine> AvailableSearchEngines
        {
            get
            {
                return _availableSearchEngines;
            }
            set
            {
                _availableSearchEngines = value;
            }
        }

        SearchEngine _searchEngineSelectedItem = null;
        /// <summary>
        /// 
        /// </summary>
        public SearchEngine SearchEngineSelectedItem
        {
            get
            {
                return _searchEngineSelectedItem;
            }
            set
            {
                SetProperty(ref _searchEngineSelectedItem, value);
            }
        }

        string _homepageUriSettingValue = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string HomepageUriSettingValue
        {
            get
            {
                return _homepageUriSettingValue;
            }
            set
            {
                SetProperty(ref _homepageUriSettingValue, value);
            }
        }

        bool _showHomepageButtonSettingValue = false;
        /// <summary>
        /// 
        /// </summary>
        public bool ShowHomepageButtonSettingValue
        {
            get
            {
                return _showHomepageButtonSettingValue;
            }
            set
            {
                SetProperty(ref _showHomepageButtonSettingValue, value);
            }
        }

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public BrowserSettingsViewModel() : base(Window.Current)
        {
            this.InitializeStorageManager();
            this.InitializeSearchEngineProcessor();
            this.LoadSettingValues();
            this.InitializeCommands();
        }

        #endregion

        #region # public methods #

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        private void InitializeCommands()
        {
            this.SearchEngineSelectionChangedCommand = new RelayCommand((eventArgs) =>
            {
                this.ProcessSearchEngineSelectionChanged(eventArgs as SelectionChangedEventArgs);
            });

            this.ShowHomepageButtonToggledCommand = new RelayCommand((eventArgs) =>
            {
                this.ProcessShowHomepageButtonToggled(eventArgs as RoutedEventArgs);
            });

            this.HomepageUriLostFocusCommand = new RelayCommand((eventArgs) =>
            {
                this.ProcessHomepageUriLostFocus(eventArgs as RoutedEventArgs);
            });

            this.SaveSettingsButtonClickCommand = new RelayCommand((eventArgs) =>
            {
                this.ProcessSaveSettingsButtonClick(eventArgs as RoutedEventArgs);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessSaveSettingsButtonClick(RoutedEventArgs eventArgs)
        {
            if (SAVE_VIA_BUTTON == true)
            {
                this.SaveSearchEngineSetting();
                this.SaveHomepageUriSetting();
                this.SaveShowHomepageButtonSetting();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveShowHomepageButtonSetting()
        {
            bool showHomepageButton = this.ShowHomepageButtonSettingValue;

            this.SaveShowHomepageButtonSettingValue(showHomepageButton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="showHomepageButton"></param>
        private void SaveShowHomepageButtonSettingValue(bool showHomepageButton)
        {
            this._storageManager.SetValue(StorageKey.SHOW_HOMEPAGE_BUTTON, showHomepageButton);

            Task.Factory.StartNew((args) =>
            {
                Messenger.Default.Send<MessagingEventTypes>(MessagingEventTypes.SETTING_SHOW_HOMEPAGE_BUTTON);
            },
            CancellationToken.None,
            TaskCreationOptions.None);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveHomepageUriSetting()
        {
            string homepageUri = this.HomepageUriSettingValue;

            this.SaveHomepageUriSettingValue(homepageUri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="homepageUri"></param>
        private void SaveHomepageUriSettingValue(string homepageUri)
        {
            this._storageManager.SetValue(StorageKey.HOMEPAGE_URI, homepageUri);

            Messenger.Default.Send<MessagingEventTypes>(MessagingEventTypes.SETTING_HOMEPAGE_URI);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SaveSearchEngineSetting()
        {
            SearchEngine selectedSearchEngine = this.SearchEngineSelectedItem;

            this.SaveSearchEngineSettingValue(selectedSearchEngine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedSearchEngine"></param>
        private void SaveSearchEngineSettingValue(SearchEngine selectedSearchEngine)
        {
            this._storageManager.SetValue(StorageKey.SEARCH_ENGINE, selectedSearchEngine.Key);

            Messenger.Default.Send<MessagingEventTypes>(MessagingEventTypes.SETTING_SEARCH_ENGINE);
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSettingValues()
        {
            this.LoadSearchEngineSetting();
            this.LoadHomepageUriSetting();
            this.LoadShowHomepageButtonSetting();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadShowHomepageButtonSetting()
        {
            bool showHomepageButton = this._storageManager.GetValue<bool>(StorageKey.SHOW_HOMEPAGE_BUTTON, StorageDefaults.SHOW_HOMEPAGE_BUTTON);
            this.ShowHomepageButtonSettingValue = showHomepageButton;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadHomepageUriSetting()
        {
            string homepageUri = this._storageManager.GetValue<string>(StorageKey.HOMEPAGE_URI, StorageDefaults.HOMEPAGE_URI);
            this.HomepageUriSettingValue = homepageUri;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessHomepageUriLostFocus(RoutedEventArgs eventArgs)
        {
            if (SAVE_VIA_BUTTON == false)
            {
                string homepageUri = (eventArgs.OriginalSource as TextBox).Text;

                this.SaveHomepageUriSettingValue(homepageUri);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessShowHomepageButtonToggled(RoutedEventArgs eventArgs)
        {
            if (SAVE_VIA_BUTTON == false)
            {
                bool showHomepageButton = (eventArgs.OriginalSource as ToggleSwitch).IsOn;

                this.SaveShowHomepageButtonSettingValue(showHomepageButton);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessSearchEngineSelectionChanged(SelectionChangedEventArgs eventArgs)
        {
            if(SAVE_VIA_BUTTON == false)
            {
                SearchEngine selectedSearchEngine = (eventArgs.AddedItems[0] as SearchEngine);

                this.SaveSearchEngineSettingValue(selectedSearchEngine);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeSearchEngineProcessor()
        {
            this._searchEngineProcessor = new SearchEngineProcessor();
            List<SearchEngine> availableSearchEngines = this._searchEngineProcessor.GetAvailableSearchEngines();

            ObservableCollection<SearchEngine> availableSearchEnginesCollection = new ObservableCollection<SearchEngine>();
            availableSearchEngines.ForEach(x => availableSearchEnginesCollection.Add(x));

            this.AvailableSearchEngines.Clear();
            this.AvailableSearchEngines = availableSearchEnginesCollection;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadSearchEngineSetting()
        {
            string selectedSearchEngineKey = this._storageManager.GetValue<string>(StorageKey.SEARCH_ENGINE, StorageDefaults.SEARCH_ENGINE);

            SearchEngine selectedSearchEngine = this._searchEngineProcessor.GetSearchEngineByKey(selectedSearchEngineKey);
            if(selectedSearchEngine != null)
            {
                this.SearchEngineSelectedItem = selectedSearchEngine;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeStorageManager()
        {
            this._storageManager = new StorageManager();
        }

        #endregion
    }
}