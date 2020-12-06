using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TDM.Presentation.ViewModels;

namespace TDM.Presentation.Commands
{
    public class PublishTweetCommand : ICommand
    {
        readonly HomeViewModel _homeViewModel;

        public PublishTweetCommand(HomeViewModel homeViewModel)
        {
            _homeViewModel = homeViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_homeViewModel.TweetBody.Length <= 280)
            {
                return true;
            } 
            else
            {
                MessageBox.Show($"Sorry, but you can only post 280 characters! Your tweet is {_homeViewModel.TweetBody.Length} characters long");
                return false;
            }
        }

        public void Execute(object parameter)
        {   
            _homeViewModel.PublishTweet();   
        }
    }
}
