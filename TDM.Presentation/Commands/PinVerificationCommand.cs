using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TDM.Domain.Models;
using TDM.Domain.Services;
using TDM.Presentation.State.Navigators;
using TDM.Presentation.ViewModels;
using Tweetinvi;
using Tweetinvi.Models;

namespace TDM.Presentation.Commands
{
    public class PinVerificationCommand : ICommand
    {

        #region Fields

        NewUserViewModel _newUserViewModel;
        readonly IRenavigator _renavigator;
        IDataService<User> _dataService;

        #endregion

        #region Constructor

        public PinVerificationCommand(NewUserViewModel newUserViewModel, IRenavigator renavigator, IDataService<User> dataService)
        {
            _newUserViewModel = newUserViewModel;
            _renavigator = renavigator;
            _dataService = dataService;
        }

        #endregion

        #region Methods

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                var userCredentials = await _newUserViewModel.appClient.Auth.RequestCredentialsFromVerifierCodeAsync(_newUserViewModel.PinCode, _newUserViewModel.authenticationRequest);

                var userClient = new TwitterClient(userCredentials);
                var tweetinviUser = await userClient.Users.GetAuthenticatedUserAsync();

                _newUserViewModel.User = new User()
                {
                    Name = tweetinviUser.Name,
                    ScreenName = tweetinviUser.ScreenName,
                    Description = tweetinviUser.Description,
                    ProfileImageURL = tweetinviUser.ProfileImageUrlFullSize,
                    Joined = tweetinviUser.CreatedAt,
                    Followers = tweetinviUser.FollowersCount,
                    Following = tweetinviUser.FriendsCount,
                    AccessToken = tweetinviUser.Credentials.AccessToken,
                    AccessTokenSecret = tweetinviUser.Credentials.AccessTokenSecret
                };

                ObservableCollection<User> Users = new ObservableCollection<User>(await _dataService.GetAll());

                if (Users.Count == 0)
                {
                    await _dataService.Create(_newUserViewModel.User);
                    ViewModelBase.User = _newUserViewModel.User;
                    _renavigator.Renavigate();
                }
                else
                {
                    foreach (var item in Users)
                    {
                        if (item.ScreenName == _newUserViewModel.User.ScreenName)
                        {
                            MessageBox.Show("Sorry, that user already exists! Please login in as a returning user or a different account");
                            _newUserViewModel.success = false;
                            break;
                        }
                        else
                        {
                            _newUserViewModel.success = true;
                            await _dataService.Create(_newUserViewModel.User);
                            ViewModelBase.User = _newUserViewModel.User;
                            _renavigator.Renavigate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion

    }
}