using BusinessLayer.Abstract;
using DataAccessLayer.BaseRepository.Abstract;
using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GenericManager<T>(IGenericRepository<T> _repository) : IGenericService<T> where T : class, IEntity
    {
        public async Task TAddAsync(T entity)
        {
            await _repository.Add(entity);
        }

        public async Task TDeleteAsync(int id)
        {
            await _repository.Delete(id);

        }

        public async Task<T> TGetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await _repository.GetByFilter(filter);
        }

        public Task<T> TGetByIdAsync(int id)
        {
            return _repository.GetById(id);
        }

		public Task<List<T>> TGetFilteredListAsync(Expression<Func<T, bool>> filter)
		{
			return _repository.GetFilteredList(filter);
		}

		public async Task<List<T>> TListAllAsync()
        {
            return await _repository.ListAll();
        }

        public async Task TUpdateAsync(T entity)
        {
             await _repository.Update(entity);
        }
    }
}
