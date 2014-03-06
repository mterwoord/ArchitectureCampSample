using Contracts;
using Shell.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace Shell.ViewModels
{
    public class MainViewModel : ModelBase
    {
        private List<IModuleMetadata> _modules;
        private ICollectionView _modulesDataView;
        private IModuleDiscoveryService _moduleService;
        private IView _activeView;
        private IModule _activeModule;
        private static object _lockObject = new object();

        public MainViewModel()
        {
            if (App.Current != null && App.Current.MainWindow != null &&
                !DesignerProperties.GetIsInDesignMode(App.Current.MainWindow))
            {
                _moduleService = ServicePool.Current.GetService<IModuleDiscoveryService>();
                if (_moduleService != null)
                {
                    this.Modules = _moduleService.GetModulesMetadata();
                    this.ModulesDataView = new ListCollectionView(this.Modules);
                    this.ModulesDataView.CurrentChanged += this.OnActiveModuleChanged;
                }
            }
        }

        private void OnActiveModuleChanged(object sender, System.EventArgs e)
        {
            // vorheriges Modul schließen
            if (this.ActiveModule != null)
            {
                this.ActiveModule.Close();
            }

            // neues Modul erstellen und öffnen
            var moduleMetadata = this.ModulesDataView.CurrentItem as IModuleMetadata;
            if (_moduleService != null)
            {
                this.ActiveModule = _moduleService.ActivateModule(moduleMetadata);
                if (this.ActiveModule != null)
                {
                    this.ActiveView = this.ActiveModule.GetView();
                }
            }
        }

        public List<IModuleMetadata> Modules
        {
            get { return _modules; }
            set { _modules = value; this.OnPropertyChanged(); }
        }

        public ICollectionView ModulesDataView
        {
            get { return _modulesDataView; }
            set { _modulesDataView = value; this.OnPropertyChanged(); }
        }

        public IView ActiveView
        {
            get { return _activeView; }
            set { _activeView = value; this.OnPropertyChanged(); }
        }

        public IModule ActiveModule
        {
            get { return _activeModule; }
            set { _activeModule = value; this.OnPropertyChanged(); }
        }
    }
}
