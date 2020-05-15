﻿using browser.core.Components.WebUriProcessor;
using browser.Core;
using Microsoft.Gaming.XboxGameBar;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace browser.ViewModels
{
    public class BrowserWidgetViewModel : BaseViewModel
    {
        #region # events #

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenSettingsCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand OpenShareMenuCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AdressBarKeyUpCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand WebViewSourceChangingCommand { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand AdressBarSuggestionChosenCommand { get; set; }

        #endregion

        #region # private properties #

        #endregion

        #region # public properties #

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

        string _webViewAdressSource = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        public string WebViewAdressSource
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

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public BrowserWidgetViewModel()
        {
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
            this.OpenSettingsCommand = new RelayCommand((eventArgs) =>
            {
                this.OpenXboxGameBarWidgetSettings();
            });

            this.OpenShareMenuCommand = new RelayCommand((eventArgs) =>
            {
                this.OpenShareMenu();
            });

            this.AdressBarKeyUpCommand = new RelayCommand((eventArgs) =>
            {
                this.ProcessAdressVarKeyUp(eventArgs as KeyRoutedEventArgs);
            });

            this.WebViewSourceChangingCommand = new RelayCommand((eventArgs) =>
            {
                this.ProcessWebViewSourceChanging(eventArgs as WebViewNavigationStartingEventArgs);
            });

            this.AdressBarSuggestionChosenCommand = new RelayCommand((eventArgs) =>
            {
                this.ProcessAdressBarSuggestionChosen((eventArgs as AutoSuggestBoxSuggestionChosenEventArgs));
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webViewSource"></param>
        private void ProcessChangingWebViewSource(string webViewSource)
        {
            if(webViewSource != this._adressBarDisplayText)
            {
                this.AdressBarDisplayText = webViewSource;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestedUriValue"></param>
        private void ProcessAdressBarValue(string requestedUriValue)
        {
            string sourceUriValue = requestedUriValue;
            Uri targetUri = null;
            WebUriProcessorComponent webUriProcessorComponent = new WebUriProcessorComponent();

            // is valid url
            if (webUriProcessorComponent.IsValidUri(sourceUriValue))
            {
                targetUri = new Uri(sourceUriValue);
            } else if(webUriProcessorComponent.IsValidAdress(sourceUriValue))
            {
                targetUri = webUriProcessorComponent.CreateUriFromDomain(sourceUriValue);
            } else
            {
                // create uri for search engine
            }

            this.AdressBarDisplayText = targetUri.ToString();
            this.WebViewAdressSource = this.AdressBarDisplayText;
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
        private void ProcessWebViewSourceChanging(WebViewNavigationStartingEventArgs eventArgs)
        {
            string newUriValue = eventArgs.Uri.ToString();

            this.ProcessChangingWebViewSource(newUriValue);
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
        private void ProcessAutoSuggestValues()
        {
            string currentAdressBarValue = this.AdressBarDisplayText;

            this.AdressBarSuggestValues.Clear();

            this.AdressBarSuggestValues.Add($"{currentAdressBarValue} #0");
            this.AdressBarSuggestValues.Add($"{currentAdressBarValue} #1");
            this.AdressBarSuggestValues.Add($"{currentAdressBarValue} #2");
            this.AdressBarSuggestValues.Add($"{currentAdressBarValue}");
            this.AdressBarSuggestValues.Add($"{currentAdressBarValue} #3");
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
            string pageTitle = "Google";
            string pageUri = "https://www.google.de";

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