using Microsoft.EntityFrameworkCore;
using MVVM_FirsTry.Models;
using MVVM_FirsTry.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_FirsTry.Database.Services
{
    public class GenericRepository<TEntity> : IDataService<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _applicationContext;
        protected readonly DbSet<TEntity> _dbSet;
        public GenericRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = _applicationContext.Set<TEntity>();
        }

        public async Task Create(TEntity entity)
        {
            _dbSet.Add(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            await _applicationContext.SaveChangesAsync();
            return true;
        }

        public async Task<TEntity> Get(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            using (var context = new ApplicationContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (typeof(TEntity).GetProperty("Car") != null)
                {
                    query = query.Include(entity => ((Order)(object)entity).Car);
                }

                if (typeof(TEntity).GetProperty("User") != null)
                {
                    query = query.Include(entity => ((Order)(object)entity).User);
                }

                return await query.ToListAsync();
            }
        }



        public async Task<TEntity> Update(int id, TEntity entity)
        {
            var existingEntity = await _dbSet.FindAsync(id);
            if (existingEntity == null)
                return null;

            _applicationContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _applicationContext.SaveChangesAsync();
            return existingEntity;
        }
    }
}
