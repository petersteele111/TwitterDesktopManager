using System;
using System.Collections.Generic;
using System.Text;

namespace TDM.Domain.Models
{
    public class User : DomainObject
    {
        #region Fields

        private string _name;
        private string _screenName;
        private string _description;
        private string _profileImageURL;
        private DateTimeOffset _joined;
        private int _followers;
        private int _following;
        private string _accessToken;
        private string _accessTokenSecret;

        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string ScreenName
        {
            get
            {
                return _screenName;
            }
            set
            {
                _screenName = value;
                OnPropertyChanged(nameof(ScreenName));
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string ProfileImageURL
        {
            get
            {
                return _profileImageURL;
            }
            set
            {
                _profileImageURL = value;
                OnPropertyChanged(nameof(ProfileImageURL));
            }
        }

        public DateTimeOffset Joined
        {
            get
            {
                return _joined;
            }
            set
            {
                _joined = value;
                OnPropertyChanged(nameof(Joined));
            }
        }

        public int Followers
        {
            get
            {
                return _followers;
            }
            set
            {
                _followers = value;
                OnPropertyChanged(nameof(Followers));
            }
        }

        public int Following
        {
            get
            {
                return _following;
            }
            set
            {
                _following = value;
                OnPropertyChanged(nameof(Following));
            }
        }

                
        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                _accessToken = value;
                OnPropertyChanged(nameof(AccessToken));
            }
        }

        public string AccessTokenSecret
        {
            get
            {
                return _accessTokenSecret;
            }
            set
            {
                _accessTokenSecret = value;
                OnPropertyChanged(nameof(AccessTokenSecret));
            }
        }

        #endregion

    }
}
