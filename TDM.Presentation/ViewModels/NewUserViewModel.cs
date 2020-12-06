using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TDM.Domain.Models;
using TDM.Domain.Services;
using TDM.Presentation.Commands;
using TDM.Presentation.State.Navigators;
using Tweetinvi;
using Tweetinvi.Models;

namespace TDM.Presentation.ViewModels
{
    public class NewUserViewModel : ViewModelBase
    {

        #region Fields

        public TwitterClient appClient = null;
        public IAuthenticationRequest authenticationRequest = null;

        IDataService<User> _dataService;
        readonly string _consumerKey;
        readonly string _consumerSecret;
        IRenavigator _renavigator;
        string _pinCode;

        public bool success;

        #endregion

        #region Properties

        public string PinCode
        {
            get => _pinCode;
            set
            {
                _pinCode = value;
                OnPropertyChanged(nameof(PinCode));
            }
        }

        public User User { get; set; }

        #endregion

        #region Commands

        public ICommand PinVerificationCommand => new PinVerificationCommand(this, _renavigator, _dataService);

        #endregion

        #region Constructor

        public NewUserViewModel(string consumerKey, string consumerSecret, IRenavigator renavigator, IDataService<User> dataService)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _renavigator = renavigator;
            _dataService = dataService;
            AuthorizeAppNewUser();
        }

        #endregion

        #region Methods

        public async void AuthorizeAppNewUser()
        {
            try
            {
                appClient = new TwitterClient(_consumerKey, _consumerSecret);
                authenticationRequest = await appClient.Auth.RequestAuthenticationUrlAsync();

                Process.Start(new ProcessStartInfo(authenticationRequest.AuthorizationURL)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, something went wrong" + ex.Message);
                throw;
            }
         
        }

        #endregion

    }
}
