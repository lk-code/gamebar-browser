using System;

namespace browser.Components.TempHistory
{
    [Serializable]
    public class TempHistoryInvalidNavigationException : Exception
    {
        public TempHistoryInvalidNavigationException()
        { }

        public TempHistoryInvalidNavigationException(string message)
            : base(message)
        { }

        public TempHistoryInvalidNavigationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
