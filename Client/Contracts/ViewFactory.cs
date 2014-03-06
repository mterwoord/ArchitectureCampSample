using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;

namespace Contracts
{
    public static class ViewFactory
    {
        public static bool IsTesting { get; set; }
        
        public static IView CreateView(object viewModel)
        {
            var viewName = viewModel.GetType().Name.Replace("ViewModel", "View");
            IView view = null;
            if (IsTesting)
            {
                view = new TestView();
            }
            else
            {
                // View anhand des Namens ermitteln & exportieren
                var registration = new RegistrationBuilder();
                registration.ForTypesMatching<IView>(item=>item.Name.Equals(viewName))
                    .Export<IView>()
                    .SetCreationPolicy(CreationPolicy.NonShared);

                // View erstellen
                var catalog = new AssemblyCatalog(viewModel.GetType().Assembly, registration);
                var container = new CompositionContainer(catalog);
                view = container.GetExportedValue<IView>();
            }
            if (view != null)
                view.DataContext = viewModel;
            return view;
        }
    }
}
