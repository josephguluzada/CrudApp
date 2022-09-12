using CrudAppEF.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppEF.RepositoryPattern
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        IEnumerable<T> GetAll();
        T Add(T entity);

        IEnumerable<T> Search(Expression<Func<T, bool>> func);
        
    }
}
