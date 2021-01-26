using System;
using System.Windows.Input;

namespace ViewModel
{
    public class Command : ICommand
    {
        private readonly Func<bool> m_CanExecute;

        private readonly Action m_Execute;

        public Command(Action execute) : this(execute, null)
        {
        }

        public Command(Action execute, Func<bool> canExecute)
        {
            m_Execute = execute ?? throw new ArgumentNullException(nameof(execute));
            m_CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (m_CanExecute == null)
                return true;
            if (parameter == null)
                return m_CanExecute();
            return m_CanExecute();
        }

        public void Execute(object parameter)
        {
            m_Execute();
        }
    }
}