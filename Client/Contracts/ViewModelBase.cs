using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace Contracts
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public abstract class ViewModelBase : ModelBase
    {
        private string _currentState;
        public string CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; this.OnPropertyChanged(); }
        }

        private bool _hasErrors;
        public bool HasErrors
        {
            get { return _hasErrors; }
            set { _hasErrors = value; this.OnPropertyChanged(); }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            set { _errorText = value; this.OnPropertyChanged(); }
        }
        
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; this.OnPropertyChanged(); this.RefreshCommands(); }
        }

        protected void RefreshCommands()
        {
            var t = this.GetType();
            var props = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var cmdProps = (from p in props where typeof(ICommand).IsAssignableFrom(p.PropertyType) select p).ToList();
            cmdProps.ForEach(p =>
            {
                var command = p.GetValue(this) as ICommand;
                if (command != null)
                    command.CanExecute(null);
            });
        }
    }
}
