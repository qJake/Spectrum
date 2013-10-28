using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;

namespace Spectrum.Windows
{
    /// <summary>
    /// Interaction logic for AddTrigger.xaml
    /// </summary>
    public partial class AddAutomationDefinition : Window
    {
        [ImportMany(typeof(ITriggerSource))]
        private List<ITriggerSource> Sources = new List<ITriggerSource>();

        public ITriggerSource SelectedSource
        {
            get
            {
                if (SourceList.SelectedIndex == -1 || SourceList.SelectedIndex >= Sources.Count)
                {
                    return null;
                }
                return Sources[SourceList.SelectedIndex];
            }
        }

        public List<AutomationAction> Actions { get; private set; }

        public AddAutomationDefinition()
        {
            InitializeComponent();

            Actions = new List<AutomationAction>();

            RefreshSources();

            SourceList.ItemsSource = Sources;
        }

        public void RefreshSources()
        {
            var catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var container = new CompositionContainer(catalog, null);
            container.ComposeParts(this);
        }

        private void AddAction_Click(object sender, RoutedEventArgs e)
        {
            var addLights = new AddLightAction();

            var result = addLights.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Actions.Add(new AutomationAction() { State = addLights.LightState, Lights = addLights.SelectedLights });
                ActionList.ItemsSource = null;
                ActionList.ItemsSource = Actions;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
