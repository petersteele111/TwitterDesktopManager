using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using TDM.Domain.Models;
using TDM.Presentation.Commands;
using TDM.Presentation.State.Navigators;
using TDM.Presentation.ViewModels.ViewModelFactories;

namespace TDM.Presentation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        #region Fields

        readonly IViewModelFactory _viewModelFactory;

        #endregion

        #region Properties

        public INavigator Navigator { get; set; }
        public ICommand UpdateCurrentViewModelCommand { get; }

        #endregion

        #region Constructor

        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            Navigator = navigator;
            _viewModelFactory = viewModelFactory;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.MainLogin);
        }

        #endregion

    }
}
