using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Model.Base;

namespace Infra.EF.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> Create(T entity);
        Task<T> Update(T newEntity);
        Task<T> Delete(Guid id);
        Task<IList<T>> GetAll();
        Task<T> GetById(Guid id);
        Task<T> GetByIdAsync(Guid id, Func<IQueryable<T>, IQueryable<T>> include = null);
         Task<IList<T>> GetAsync(Expression<Func<T,bool>> where = null, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task<T>FirstOrDefaultAsync(Expression<Func<T,bool>> where = null, Func<IQueryable<T>, IQueryable<T>> include = null);
        Task Commit();
    }
}