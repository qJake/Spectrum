using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpHue;

namespace Spectrum
{
    public class AutomationAction
    {
        public List<Light> Lights { get; set; }
        public LightStateBuilder State { get; set; }

        public AutomationAction()
        {
            Lights = new List<Light>();
            State = null;
        }
    }
}
