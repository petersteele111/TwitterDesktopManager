using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TDM.Domain.Models;
using TDM.Domain.Services;

namespace TDM.DAL.Services
{
    public class UserRepository : IUserDataService
    {
        readonly DBContextFactory _dBContextFactory;

        public UserRepository(DBContextFactory dBContextFactory)
        {
            _dBContextFactory = dBContextFactory;
        }

        public async Task<User> Create(User entity)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                EntityEntry<User> createdResult = await context.Set<User>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
        }

        public async Task<bool> Delete(long id)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                User entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<User>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<User> Get(int id)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                User entity = await context.Set<User>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                IEnumerable<User> entities = await context.Set<User>().ToListAsync();
                return entities;
            }
        }

        public async Task<User> GetByUserName(string screenname)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                User entity = await context.Set<User>().FirstOrDefaultAsync((u) => u.ScreenName == screenname);
                return entity;
            }
        }

        public async Task<User> Update(int id, User entity)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<User>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
