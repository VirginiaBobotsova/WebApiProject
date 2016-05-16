namespace WebApi.Data.Interfaces
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using WebApi.Models.Interfaces;

    public interface IRepository<TEntity> where TEntity : IDentificatable
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy);
        IQueryable<TEntity> GetById(int id);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        int Count();
    }
}
