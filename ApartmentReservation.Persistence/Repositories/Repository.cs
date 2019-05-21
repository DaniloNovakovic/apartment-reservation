using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Add(TEntity entity)
        {
            this.Entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.Entities.AddRange(entities);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Entities.Where(predicate);
        }

        public TEntity Get(int id)
        {
            return this.Entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.Entities.ToList();
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