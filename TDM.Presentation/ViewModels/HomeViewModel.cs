using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TDM.Domain.Models;
using TDM.Domain.Services;
using TDM.Domain.Services.MongoDB;
using TDM.Presentation.Commands;
using Tweetinvi;
using Tweetinvi.Models;

namespace TDM.Presentation.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {

        #region Enums

        public enum DBType
        {
            MySQL,
            MongoDB
        }

        #endregion

        #region Fields

        readonly ITweetService _tweetService;
        readonly IMongoDbRepo _mongoTweetService;
        readonly private string _consumerKey;
        readonly private string _consumerSecret;
        ObservableCollection<Tweet> _userTweetsCollection;
        ObservableCollection<Tweet> _timelineTweets;
        Tweet _selectedTweet;
        string _searchString;
        DateTime? _oldDate;
        DateTime? _newDate;
        string _tweetBody;
        DBType _dbType;

        #endregion

        #region Properties

        public string SearchString
        {
            get => _searchString;
            set
            {
                _searchString = value;
                OnPropertyChanged(nameof(SearchString));
            }
        }

        public Tweet SelectedTweet
        {
            get => _selectedTweet;
            set
            {
                _selectedTweet = value;
                OnPropertyChanged(nameof(SelectedTweet));
            }
        }

        public ObservableCollection<Tweet> TimelineTweets
        {
            get => _timelineTweets;
            set
            {
                _timelineTweets = value;
                OnPropertyChanged(nameof(TimelineTweets));
            }
        }

        public ObservableCollection<Tweet> UserTweetsCollection
        {
            get => _userTweetsCollection;
            set
            {
                _userTweetsCollection = value;
                OnPropertyChanged(nameof(UserTweetsCollection));
            }
        }

        public DateTime? OldDate
        {
            get => _oldDate;
            set
            {
                _oldDate = value;
                OnPropertyChanged(nameof(OldDate));
            }
        }

        public DateTime? NewDate
        {
            get => _newDate;
            set
            {
                _newDate = value;
                OnPropertyChanged(nameof(NewDate));
            }
        }

        public string TweetBody
        {
            get => _tweetBody;
            set
            {
                _tweetBody = value;
                OnPropertyChanged(nameof(TweetBody));
            }
        }

        #endregion

        #region Commands

        public ICommand QuitApp => new QuitAppCommand();
        public ICommand SearchTweetsCommand => new SearchTweetsCommand(this);
        public ICommand FilterTweetsCommand => new FilterTweetsCommand(this);
        public ICommand ClearConstraintsCommand => new ClearConstraintsCommand(this);
        public ICommand SortByOldestTweetsFirstCommand => new SortByOldestTweetsCommand(this);
        public ICommand DeleteTweetCommand => new DeleteTweetCommand(this);
        public ICommand PublishTweetCommand => new PublishTweetCommand(this);
        public ICommand RefreshFeedCommand => new RefreshFeedCommand(this);
        public ICommand SelectDbProviderCommand => new SelectDBProviderCommand(this);

        #endregion

        #region Constructor

        public HomeViewModel(string consumerKey, string consumerSecret, ITweetService tweetService, IMongoDbRepo mongoTweetService)
        {
            _tweetService = tweetService;
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _mongoTweetService = mongoTweetService;
            UserTweetsCollection = new ObservableCollection<Tweet>();
            _dbType = DBType.MongoDB;
            GetTweets();

            if (UserTweetsCollection.Any()) SelectedTweet = UserTweetsCollection[0];
            TweetBody = "";
        }

        #endregion

        #region Methods

        private void GetTweets()
        {
            GetLiveUserTweets(_consumerKey, _consumerSecret, User.AccessToken, User.AccessTokenSecret);
        }

        public void SearchTweets()
        {
            if (SearchString != null)
            {
                try
                {
                    UserTweetsCollection = new ObservableCollection<Tweet>(_userTweetsCollection.Where(m =>
                        m.Body.ToLower().Contains(SearchString.ToLower())));
                }
                catch (Exception ex)
                {
                    string message = "Looks like something went wrong " + ex;
                    string title = "ERROR";
                    MessageBox.Show(message, title);
                }
            }
            else
            {
                string message = "You must enter some text first!";
                string title = "ERROR";
                MessageBox.Show(message, title);
            }
        }

        private void GetDBTweets()
        {
            if (_dbType == DBType.MySQL)
            {
                UserTweetsCollection = new ObservableCollection<Tweet>(_tweetService.GetTweets(User.Id));
            }
            else 
            {
                UserTweetsCollection = new ObservableCollection<Tweet>(_mongoTweetService.GetAllByUser(User.Id));
            }
            
            

        }

        private async void GetLiveUserTweets(string consumerKey, string consumerSecret, string accessToken, string accessSecret)
        {
            var userClient = new TwitterClient(consumerKey, consumerSecret, accessToken, accessSecret);
            var userTimeLineTweets = await userClient.Timelines.GetUserTimelineAsync(User.ScreenName);

            List<Tweet> tempList = new List<Tweet>();

            foreach (var item in userTimeLineTweets)
            {
                var tweet = new Tweet()
                {
                    Id = item.Id,
                    Body = item.FullText,
                    Created = item.CreatedAt,
                    UserId = User.Id
                };

                var mongoTweet = new Tweet()
                {
                    Id = item.Id,
                    Body = item.FullText,
                    Created = item.CreatedAt,
                    UserId = User.Id,
                    User = new User
                    {
                        Name = item.CreatedBy.Name,
                        ScreenName = item.CreatedBy.ScreenName,
                        Description = item.CreatedBy.Description,
                        Joined = item.CreatedBy.CreatedAt,
                        Followers = item.CreatedBy.FollowersCount,
                        Following = item.CreatedBy.FriendsCount,
                        AccessToken = User.AccessToken,
                        AccessTokenSecret = User.AccessTokenSecret,
                        ProfileImageURL = item.CreatedBy.ProfileImageUrlFullSize
                    }
                };
                tempList.Add(tweet);
                try
                {
                    _mongoTweetService.Create(mongoTweet);
                    await _tweetService.Create(tweet);
                }
                catch (Exception)
                {
                    
                }
            }

            TimelineTweets = new ObservableCollection<Tweet>(tempList);
            GetDBTweets();
        }

        public void FilterTweetsByDateRange()
        {
            if (OldDate != null && NewDate != null)
            {
                try
                {
                    UserTweetsCollection = new ObservableCollection<Tweet>(_userTweetsCollection.Where(t => t.Created >= OldDate && t.Created <= NewDate));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } else
            {
                MessageBox.Show("You MUST select a date range to filter by!");
            }
        }

        public void ClearConstraints()
        {
            GetTweets();
            SearchString = "";
            OldDate = null;
            NewDate = null;
        }

        public void SortByOldestTweetsFirst()
        {
            UserTweetsCollection = new ObservableCollection<Tweet>(_userTweetsCollection.OrderBy(d => d.Created));
        }

        public void DeleteTweet()
        {
            DeleteTweetFromTwitter(_consumerKey, _consumerSecret, User.AccessToken, User.AccessTokenSecret);
        }

        private void DeleteTweetFromTwitter(string consumerKey, string consumerSecret, string accessToken, string accessSecret)
        {
            

            var userClient = new TwitterClient(consumerKey, consumerSecret, accessToken, accessSecret);
            try
            {
                userClient.Tweets.DestroyTweetAsync(SelectedTweet.Id);
                _mongoTweetService.Delete(SelectedTweet.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
            MessageBox.Show("You have successfully deleted the tweet!");
            _tweetService.Delete(SelectedTweet.Id);

            GetTweets();
        }

        public void PublishTweet()
        {
            PublishTweetToTwitter(_consumerKey, _consumerSecret, User.AccessToken, User.AccessTokenSecret);
        }

        private void PublishTweetToTwitter(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            var userClient = new TwitterClient(consumerKey, consumerSecret, accessToken, accessTokenSecret);
            try
            {
                userClient.Tweets.PublishTweetAsync(TweetBody);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, Could not Publish the Tweet: {ex.Message}");
                throw;
            }
            MessageBox.Show("Congrats, your tweet was published!");
            TweetBody = "";
            GetTweets();
        }

        public void RefreshFeed()
        {
            GetTweets();
        }

        public void SelectDbProvider(object param)
        {
            
            switch (param)
            {
                case "MySQL":
                    _dbType = DBType.MySQL;
                    break;
                case "MongoDB":
                    _dbType = DBType.MongoDB;
                    break;
            }
            #endregion

        }
    }
}