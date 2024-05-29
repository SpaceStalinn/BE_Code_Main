using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlatformRepository.Repositories
{
    internal interface IGenericRepository<T, X> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, int? expected); 
        T Get(X id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
