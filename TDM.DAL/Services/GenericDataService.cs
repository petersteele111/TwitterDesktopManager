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
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {

        readonly DBContextFactory _dBContextFactory;

        public GenericDataService(DBContextFactory dBContextFactory)
        {
            _dBContextFactory = dBContextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }   
        }

        public async Task<bool> Delete(long id)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (TDMDBContext context = _dBContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
