using browser.Components.SearchEngine;
using browser.Components.Storage;
using browser.Core;
using Microsoft.Gaming.XboxGameBar;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
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
                _searchEngineSelectedItem = value;
            }
        }

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public BrowserSettingsViewModel()
        {
            this.InitializeStorageManager();
            this.InitializeSearchEngineProcessor();
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventArgs"></param>
        private void ProcessSearchEngineSelectionChanged(SelectionChangedEventArgs eventArgs)
        {
            SearchEngine selectedSearchEngine = (eventArgs.AddedItems[0] as SearchEngine);

            this._storageManager.SetValue(StorageKey.SEARCH_ENGINE, selectedSearchEngine.Key);
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

            this.LoadSearchEngineSetting();
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
