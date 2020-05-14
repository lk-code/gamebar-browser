using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace browser.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region # events #

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region # dependencies #

        #endregion

        #region # commands #

        #endregion

        #region # private properties #

        #endregion

        #region # public properties #

        #endregion

        #region # constructors #

        /// <summary>
        /// 
        /// </summary>
        public BaseViewModel()
        {

        }

        #endregion

        #region # public methods #

        #endregion

        #region # private logic #

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingStore"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="onChanged"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T backingStore,
            T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);

            return true;
        }

        #endregion
    }
}
