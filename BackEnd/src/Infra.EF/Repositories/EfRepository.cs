using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Model.Base;
using Infra.EF.Data.Context;
using Infra.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.EF.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : Entity
    {
        private readonly AppDataContext _context;
        private DbSet<T> _dbset;

        public EfRepository(AppDataContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public async Task<IList<T>> GetAll()
        {
            return await _dbset.AsNoTracking().ToListAsync();
        }


        public async Task<T> GetById(Guid id)
        {
            return await _dbset.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<T> Create(T entity)
        {
            if (entity != null)
                await _dbset.AddAsync(entity);

            return entity;
        }

        public async Task<T> Update(T newEntity)
        {
            _context.Update(newEntity);

            return newEntity;
        }

        public async Task<T> Delete(Guid id)
        {
            var entity = await _dbset.FindAsync(id);

            if (entity != null)
                _dbset.Remove(entity);

            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();


            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }


        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> GetAsync(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }


        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IQueryable<T>> include = null)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (where != null)
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync();
        }
    }
}