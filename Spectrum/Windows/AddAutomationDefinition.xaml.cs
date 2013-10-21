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

        private List<AutomationAction> Actions { get; set; }

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
    }
}
