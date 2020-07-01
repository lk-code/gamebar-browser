using browser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace browser.Core
{
    public interface IBrowserContentElement
    {
        event EventHandler OnOpenTabRequested;
        void OpenTab(OpenTabEventArgs eventArgs);
    }
}
