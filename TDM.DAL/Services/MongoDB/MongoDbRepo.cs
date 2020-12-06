using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using TDM.Domain.Models;
using TDM.Domain.Services.MongoDB;

namespace TDM.DAL.Services.MongoDB
{
    public class MongoDbRepo : IMongoDbRepo
    {

        List<Tweet> _tweets;
        IMongoCollection<Tweet> _collection;

        string _serverPath;
        string _userName;
        string _dbName;
        string _dbPassword;
        string _collectionName;

        public MongoDbRepo(string serverPath, string userName, string dbName, string dbPassword, string collectionName)
        {
            _serverPath = serverPath;
            _userName = userName;
            _dbName = dbName;
            _dbPassword = dbPassword;
            _collectionName = collectionName;
            Connect();
        }

        private void Connect()
        {
            try
            {
                MongoClient client = new MongoClient($"mongodb+srv://{_userName}:{_dbPassword}@{_serverPath}/{_dbName}?retryWrites=true&w=majority");
                IMongoDatabase db = client.GetDatabase(_dbName);
                _collection = db.GetCollection<Tweet>(_collectionName);

                _tweets = _collection.Find(Builders<Tweet>.Filter.Empty).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry but something went wrong: {ex.Message}");
            }
            
        }

        public void Create(Tweet entity)
        {
            _collection.InsertOne(entity);
            UpdateCollection();
        }



        public void Delete(long id)
        {
            Tweet tweetDelete = _tweets.FirstOrDefault(t => t.Id == id);
            if (tweetDelete != null)
            {
                var deleteFilter = Builders<Tweet>.Filter.Eq("Id", id);
                _collection.DeleteOne(deleteFilter);
            }
            UpdateCollection();

        }

        public Tweet Get(long id)
        {
            var getFilter = Builders<Tweet>.Filter.Eq("Id", id);
            return _collection.Find(getFilter).FirstOrDefault();
        }

        public IEnumerable<Tweet> GetAllByUser(int id)
        {
            UpdateCollection();
            return _tweets.Where(t => t.UserId == id).OrderByDescending(t => t.Created);
        }

        public void Update(long id, Tweet entity)
        {
            var updateFilter = Builders<Tweet>.Filter.Eq("Id", entity.Id);
            var result = _collection.DeleteOne(updateFilter);
            _collection.InsertOne(entity);
        }

        private void UpdateCollection()
        {
            Connect();
        }
    }
}
