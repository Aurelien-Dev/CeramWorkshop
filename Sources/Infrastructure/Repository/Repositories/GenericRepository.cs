using System.Linq.Expressions;
using Domain.Interfaces;
using Repository.Extensions;

namespace Repository.Repositories
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;

        protected GenericRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public virtual async Task<TEntity> Get(TId id, CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>()
                .FindAsync(id, cancellationToken)
                .ConfigureAwait(false);
        }

        public virtual async Task<ICollection<TEntity>> GetAll(CancellationToken cancellationToken = default)
        {
            return await Context.Set<TEntity>()
                .ToListAsyncWait(cancellationToken);
        }

        public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity );
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }


        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }

        protected IQueryable<TEntity> AddSorting(IQueryable<TEntity> query, string sortDirection, string propertyName)
        {
            var param = Expression.Parameter(typeof(TEntity));
            var prop = Expression.PropertyOrField(param, propertyName);
            var sortLambda = Expression.Lambda(prop, param);

            Expression<Func<IOrderedQueryable<TEntity>>> sortMethod = null;

            switch (sortDirection)
            {
                case "Ascending" when query.Expression.Type == typeof(IOrderedQueryable<TEntity>):
                    sortMethod = () => ((IOrderedQueryable<TEntity>)query).ThenBy<TEntity, object>(k => null);
                    break;
                case "Ascending":
                    sortMethod = () => query.OrderBy<TEntity, object>(k => null);
                    break;
                case "Descending" when query.Expression.Type == typeof(IOrderedQueryable<TEntity>):
                    sortMethod = () => ((IOrderedQueryable<TEntity>)query).ThenByDescending<TEntity, object>(k => null);
                    break;
                case "Descending":
                    sortMethod = () => query.OrderByDescending<TEntity, object>(k => null);
                    break;
                default:
                    sortMethod = () => query.OrderByDescending<TEntity, object>(k => null);
                    break;
            }

            var methodCallExpression = (sortMethod.Body as MethodCallExpression);
            if (methodCallExpression == null)
                throw new Exception("MethodCallExpression null");

            var method = methodCallExpression.Method.GetGenericMethodDefinition();
            var genericSortMethod = method.MakeGenericMethod(typeof(TEntity), prop.Type);
            return (IOrderedQueryable<TEntity>)genericSortMethod.Invoke(query, new object[] { query, sortLambda });
        }
    }
}