using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TDM.Presentation.ViewModels;

namespace TDM.Presentation.Commands
{
    public class ClearConstraintsCommand : ICommand
    {

        readonly HomeViewModel _homeViewModel;

        public ClearConstraintsCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _homeViewModel.ClearConstraints();
        }
    }
}
