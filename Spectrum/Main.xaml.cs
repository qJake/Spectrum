using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpHue;
using Spectrum.Pages;

namespace Spectrum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        private Style Deselected;
        private Style Selected;

        private Type ActiveView = null;

        private Dictionary<Type, UserControl> ViewState = new Dictionary<Type, UserControl>();

        public Main()
        {
            InitializeComponent();

            Configuration.Initialize("36e02089265925772f085fcd3884ec9b");

            ViewState.Add(typeof(Automate), Activator.CreateInstance<Automate>());
            ViewState.Add(typeof(Configure), Activator.CreateInstance<Configure>());
            ViewState.Add(typeof(Now), Activator.CreateInstance<Now>());
            ViewState.Add(typeof(Schedule), Activator.CreateInstance<Schedule>());

            Deselected = FindResource("Text-Header-MainNav") as Style;
            Selected = FindResource("Text-Header-MainNav-Selected") as Style;

            SwitchPages<Now>(NavNow);
        }

        // TODO: Add effect or fade or loading or something.
        public void SwitchPages<T>(FrameworkElement selectedControl) where T : UserControl
        {
            SwitchStyle(selectedControl);

            ControlContainer.Content = ViewState[typeof(T)];

            ActiveView = typeof(T);
        }

        private void SwitchStyle(FrameworkElement selectedControl)
        {
            NavAutomate.Style = Deselected;
            NavNow.Style = Deselected;
            NavSchedule.Style = Deselected;
            NavConfigure.Style = Deselected;

            selectedControl.Style = Selected;
        }

        private void NavNow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!ActiveView.Equals(typeof(Now)))
            {
                SwitchPages<Now>(sender as FrameworkElement);
            }
        }

        private void NavSchedule_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!ActiveView.Equals(typeof(Schedule)))
            {
                SwitchPages<Schedule>(sender as FrameworkElement);
            }
        }

        private void NavAutomate_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!ActiveView.Equals(typeof(Automate)))
            {
                SwitchPages<Automate>(sender as FrameworkElement);
            }
        }

        private void NavConfigure_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!ActiveView.Equals(typeof(Configure)))
            {
                SwitchPages<Configure>(sender as FrameworkElement);
            }
        }
    }
}
