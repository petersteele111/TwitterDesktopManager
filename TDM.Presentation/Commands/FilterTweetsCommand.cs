using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TDM.Presentation.ViewModels;

namespace TDM.Presentation.Commands
{
    public class FilterTweetsCommand : ICommand
    {
        HomeViewModel _homeViewModel;

        public FilterTweetsCommand(HomeViewModel homeViewModel)
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
            _homeViewModel.FilterTweetsByDateRange();
        }
    }
}
