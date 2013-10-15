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
using Spectrum.Pages;

namespace Spectrum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();

            SwitchPages<Now>();
        }

        // TODO: Add effect or fade or loading or something.
        public void SwitchPages<T>() where T : UserControl
        {
            ControlContainer.Content = null;

            UserControl ctl = Activator.CreateInstance<T>() as UserControl;

            ControlContainer.Content = ctl;
        }
    }
}
