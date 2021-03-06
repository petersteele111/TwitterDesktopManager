﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TDM.Presentation.ViewModels;

namespace TDM.Presentation.Commands
{
    public class SearchTweetsCommand : ICommand
    {

        HomeViewModel _viewModel;

        public SearchTweetsCommand(HomeViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SearchTweets();
        }
    }
}
