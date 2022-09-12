using CrudAppEF.DAL;
using CrudAppEF.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppEF.RepositoryPattern
{
    public class Repository<T, TContext> : IRepository<T> where T : BaseEntity, new() where TContext : DbContext
    {

        private readonly TContext _context;

        public Repository(TContext context)
        {
            _context = context;
        }

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public IEnumerable<T> Search(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            return _context.Set<T>().Where(func);
        }
    }
}
