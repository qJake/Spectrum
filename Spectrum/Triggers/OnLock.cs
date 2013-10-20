using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Spectrum
{
    [Export(typeof(ITriggerSource))]
    public class OnLock : ITriggerSource
    {
        public string WhenDescription
        {
            get { return "This computer locks"; }
        }

        public string IconPath
        {
            get { return "Resources/Icons/Lock.png"; }
        }

        public UserControl Configuration
        {
            get { return null; }
        }

        public event Action<object> Triggered;

        public OnLock()
        {
            SystemEvents.SessionSwitch += (_, e) =>
            {
                if (e.Reason == SessionSwitchReason.SessionLock)
                {
                    if (Triggered != null)
                    {
                        Triggered(this);
                    }
                }
            };
        }
    }
}
