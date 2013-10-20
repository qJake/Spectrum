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
    public partial class AddTrigger : Window
    {
        [ImportMany(typeof(ITriggerSource))]
        private List<ITriggerSource> Sources = new List<ITriggerSource>();

        public AddTrigger()
        {
            InitializeComponent();

            RefreshSources();

            SourceList.ItemsSource = Sources;
        }

        public void RefreshSources()
        {
            var catalog = new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
            var container = new CompositionContainer(catalog, null);
            container.ComposeParts(this);
        }
    }
}
