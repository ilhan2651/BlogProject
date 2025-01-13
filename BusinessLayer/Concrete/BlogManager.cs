using BusinessLayer.Abstract;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        public BlogManager(IBlogRepository  blogRepository):base(blogRepository) 
        {
            _blogRepository = blogRepository;
        }

        public async Task<List<Blog>> GetBlogListByCategoryId(int id)
        {
            return await _blogRepository.GetListByBlogID(id);
        }

		public async Task<List<Blog>> GetBlogListByWriter(int id)
		{
            return await _blogRepository.GetFilteredList(w => w.WriterID == id);
		}

		public async Task<List<Blog>> GetBlogListWithCategory()
		{
		 return await _blogRepository.GetListWithCategory();
		}
	}
}
