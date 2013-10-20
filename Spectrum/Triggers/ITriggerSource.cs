using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Spectrum
{
    public interface ITriggerSource
    {
        string WhenDescription { get; }
        string IconPath { get; }

        UserControl Configuration { get; }

        event Action<object> Triggered;
    }
}
