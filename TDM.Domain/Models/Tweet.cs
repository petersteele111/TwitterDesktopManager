using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TDM.Domain.Models
{
    public class Tweet : DomainObject
    {
        private long _id;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new long Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private DateTimeOffset _created;
        public DateTimeOffset Created
        {
            get
            {
                return _created;
            }
            set
            {
                _created = value;
                OnPropertyChanged(nameof(Created));
            }
        }

        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
                OnPropertyChanged(nameof(Body));
            }
        }

        private string _hashtags;
        public string Hashtags
        {
            get
            {
                return _hashtags;
            }
            set
            {
                _hashtags = value;
                OnPropertyChanged(nameof(Hashtags));
            }
        }

        private string _symbols;
        public string Symbols
        {
            get
            {
                return _symbols;
            }
            set
            {
                _symbols = value;
                OnPropertyChanged(nameof(Symbols));
            }
        }

        private string _userMentions;
        public string UserMentions
        {
            get
            {
                return _userMentions;
            }
            set
            {
                _userMentions = value;
                OnPropertyChanged(nameof(UserMentions));
            }
        }

        private string _urls;
        public string URLS
        {
            get
            {
                return _urls;
            }
            set
            {
                _urls = value;
                OnPropertyChanged(nameof(URLS));
            }
        }

        private User _user;
        public User User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public int UserId { get; set; }
    }
}
