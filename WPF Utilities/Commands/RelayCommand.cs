using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFUtilities.Commands
{
    /// <summary>
    /// This class provides a convienient way to support data bindning to view model functions
    /// </summary>
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// Handles hooking and unhooking the command manager when CanExecute changes
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Provided delegate that is invoked by the ICommand
        /// </summary>
        private readonly Action<object> _execute;

        /// <summary>
        /// Provided delegate that determines if the delegate can be invoked
        /// </summary>
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Basic constructor for a command that is always executable
        /// </summary>
        /// <param name="execute">Action to be invoked by the command</param>
        public RelayCommand(Action<object> execute) 
            : this(execute, null)
        {
        }

        /// <summary>
        /// Basic constructor for a command that is conditionally executable
        /// </summary>
        /// <param name="execute">Action to be invoked by the command</param>
        /// <param name="canExecute">Predicate to determine if the command can be executed</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Whether the command can be executed
        /// </summary>
        /// <returns>True if the command can be executed</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        /// <summary>
        /// Invokes the execute action provided in the constructor
        /// </summary>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
