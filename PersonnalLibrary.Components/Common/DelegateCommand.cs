using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersonnalLibrary.Common
{
    public class DelegateCommand : ICommand
    {
        #region Fields

        private Func<object, bool> _canExecute;

        private Action<object> _execute;

        #endregion

        #region Constructor

        public DelegateCommand(
            Action<object> execute)
        {
            Initialize(null, execute);
        }

        public DelegateCommand(
            Func<object, bool> canExecute,
            Action<object> execute)
        {
            Initialize(canExecute, execute);
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Private methods

        private void Initialize(
            Func<object, bool> canExecute,
            Action<object> execute)
        {
            if (canExecute == null)
            {
                canExecute = (obj) => true; 
            }

            if (execute == null)
            {
                execute = (obj) => { };
            }

            _canExecute = canExecute;
            _execute = execute;
        }

        #endregion

        #region Public methods

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
