using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Win32;

namespace Spectrum
{
    [Export(typeof(ITriggerSource))]
    public class OnUnlock : ITriggerSource
    {
        public string WhenDescription
        {
            get { return "This computer unlocks"; }
        }

        public string IconPath
        {
            get { return "Resources/Icons/Unlock.png"; }
        }

        public UserControl Configuration
        {
            get { return null; }
        }

        public event Action<object> Triggered;

        public OnUnlock()
        {
            SystemEvents.SessionSwitch += (_, e) =>
            {
                if (e.Reason == SessionSwitchReason.SessionUnlock)
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
