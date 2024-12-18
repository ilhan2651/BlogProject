using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T > where T : class,IEntity
    {
        Task TAddAsync(T entity);
        Task TUpdateAsync(T entity);
        Task TDeleteAsync(int id);
        Task<T> TGetByFilterAsync(Expression<Func<T, bool>> filter);
        Task<T> TGetByIdAsync(int id);
        Task<List<T>> TListAllAsync();
        Task<List<T>> TGetFilteredListAsync(Expression<Func<T, bool>> filter);
      
    }
}
