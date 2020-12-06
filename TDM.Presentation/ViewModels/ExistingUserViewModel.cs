using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TDM.Domain.Models;
using TDM.Domain.Services.AuthServices;
using TDM.Presentation.Commands;
using TDM.Presentation.State.Navigators;

namespace TDM.Presentation.ViewModels
{
    public class ExistingUserViewModel : ViewModelBase
    {

        #region Fields

        string _screenName;
        readonly IRenavigator _renavigator;
        readonly IAuthenticationService _authenticationService;

        #endregion

        #region Properties

        public User User { get; set; }
        
        public string ScreenName
        {
            get => _screenName;
            set
            {
                _screenName = value;
                OnPropertyChanged(nameof(ScreenName));
            }
        }

        #endregion

        #region Commands

        public ICommand ReturningUserCommand => new ReturningUserCommand(this, _renavigator, _authenticationService);

        #endregion

        #region Constructor

        public ExistingUserViewModel(IRenavigator renavigator, IAuthenticationService authenticationService)
        {
            _renavigator = renavigator;
            _authenticationService = authenticationService;
        }

        #endregion

    }
}
