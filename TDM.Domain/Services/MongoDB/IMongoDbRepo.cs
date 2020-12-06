using System;
using System.Collections.Generic;
using System.Text;
using TDM.Domain.Models;

namespace TDM.Domain.Services.MongoDB
{
    public interface IMongoDbRepo
    {
        IEnumerable<Tweet> GetAllByUser(int id);
        Tweet Get(long id);
        void Create(Tweet entity);
        void Update(long id, Tweet entity);
        void Delete(long id);
    }
}
