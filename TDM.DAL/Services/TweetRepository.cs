using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDM.Domain.Models;
using TDM.Domain.Services;

namespace TDM.DAL.Services
{
    public class TweetRepository : ITweetService
    {
        readonly DBContextFactory _dBContextFactory;

        public TweetRepository(DBContextFactory dBContextFactory)
        {
            _dBContextFactory = dBContextFactory;
        }

        public async Task<Tweet> Create(Tweet entity)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                EntityEntry<Tweet> createdResult = await context.Set<Tweet>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(long id)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                Tweet entity = await context.Set<Tweet>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<Tweet>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Tweet> Get(int id)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                Tweet entity = await context.Set<Tweet>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Tweet>> GetAll()
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                IEnumerable<Tweet> entities = await context.Tweets.Include(u => u.User).ToListAsync();
                return entities;
            }
        }

        public IEnumerable<Tweet> GetTweets(int id)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                IEnumerable<Tweet> entities = context.Tweets.Include(u => u.User).Where(u => u.User.Id == id).OrderByDescending(d => d.Created).ToList();
                return entities;
            }
        }

        public async Task<Tweet> Update(int id, Tweet entity)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<Tweet>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
