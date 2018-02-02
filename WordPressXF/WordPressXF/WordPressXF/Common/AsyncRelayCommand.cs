using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WordPressXF.Common
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<Task> execute) : this(execute, null)
        {
        }

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync();
        }

        public async void Execute()
        {
            await ExecuteAsync();
        }


        public async Task ExecuteAsync()
        {
            await _execute();
        }

        public void RaiseCanExecuteChange()
        {
            var handler = CanExecuteChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
