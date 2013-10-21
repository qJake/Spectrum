using System.Collections.Generic;
using System.Windows;
using SharpHue;

namespace Spectrum.Windows
{
    /// <summary>
    /// Interaction logic for AddLightAction.xaml
    /// </summary>
    public partial class AddLightAction : Window
    {
        public List<Light> SelectedLights { get; protected set; }
        public LightStateBuilder LightState { get; protected set; }

        public AddLightAction()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (LightSelector.SelectedLights.Count == 0)
            {
                MessageBox.Show("You must select at least one light for this action.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (StatePicker.SelectedState == null)
            {
                MessageBox.Show("You must select a state for this action.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SelectedLights = LightSelector.SelectedLights;
            LightState = StatePicker.SelectedState;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
