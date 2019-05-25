using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using ApartmentReservation.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApartmentReservation.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> Entities;

        public Repository(DbContext context)
        {
            this.Context = context;
            this.Entities = this.Context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await this.Entities.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await this.Entities.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await this.Entities.Where(predicate).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity> GetAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            return await this.Entities.FindAsync(keyValues, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity> GetAsync(params object[] keyValues)
        {
            return await this.Entities.FindAsync(keyValues).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await this.Entities.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public void Remove(TEntity entity)
        {
            this.Entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            this.Entities.RemoveRange(entities);
        }
    }
}