using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TDM.Domain.Models;
using TDM.Domain.Services.AuthServices;
using TDM.Presentation.State.Navigators;
using TDM.Presentation.ViewModels;

namespace TDM.Presentation.Commands
{
    public class ReturningUserCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly ExistingUserViewModel _existingUserViewModel;
        private readonly IRenavigator _renavigator;
        private readonly IAuthenticationService _authenticationService;

        public ReturningUserCommand(ExistingUserViewModel existingUserViewModel, IRenavigator renavigator, IAuthenticationService authenticationService)
        {
            _existingUserViewModel = existingUserViewModel;
            _renavigator = renavigator;
            _authenticationService = authenticationService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                User user = await _authenticationService.Login(_existingUserViewModel.ScreenName);
                _existingUserViewModel.User = user;
                if (user != null)
                {
                    ViewModelBase.User = user;
                    _renavigator.Renavigate();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
