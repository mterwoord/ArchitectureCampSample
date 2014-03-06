using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Contracts
{
    public class DelegateCommand : ICommand
    {
        private Predicate<object> _canExecuteHandler;
        private Action<object> _executeHandler;
        private bool _canExecuteCache;

        public DelegateCommand(Action<object> executeHandler)
        {
            _executeHandler = executeHandler;
        }

        public DelegateCommand(Predicate<object> canExecuteHandler, Action<object> executeHandler)
        {
            _canExecuteHandler = canExecuteHandler;
            _executeHandler = executeHandler;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteHandler == null) return true;
            var canExecute = _canExecuteHandler(parameter);
            if (canExecute != _canExecuteCache)
            {
                _canExecuteCache = canExecute;
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, EventArgs.Empty);
            }
            return canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _executeHandler(parameter);
        }
    }
}
