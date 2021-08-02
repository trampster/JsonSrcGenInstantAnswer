using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JsonSrcGenInstantAnswer
{
   public class AsyncCommand : ICommand
   {
      readonly Func<Task> _func;
      public AsyncCommand(Func<Task> func)
      {
         _func = func;
      }

      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter) => true;

      public async void Execute(object parameter)
      {
         await _func();
      }
   }
}
