﻿using BusinessLayer.Abstract;
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

		public async Task<List<Blog>> GetLast3BlogsByWriterAsync(int id)
		{
         return await _blogRepository.GetLast3BlogsByWriter(id);
		}
        public async Task<List<Blog>> GetListWithCategoryByWriterBm(int id)
        {
            return await _blogRepository.GetListWithCategoryByWriter(id); 
        }

		public async Task<List<Blog>> GetBlogListWithCategory()
		{
		 return await _blogRepository.GetListWithCategory();
		}
        public  async Task<List<Blog>> GetLastThreeBlog()
        {
           return await _blogRepository.GetLastThreeBlog();
        }

        public async Task<List<Blog>> GetListWithCategoryAndCommentsAsync()
        {
            return await _blogRepository.GetListWithCategoryAndComments();
        }

        public async Task<Blog> GetBlogWithCategoryAndCommentsByIdAsync(int id)
        {
            return await _blogRepository.GetBlogWithCategoryAndCommentsById(id);
        }

		public async Task<List<Blog>> MostCommented3PostAsync()
		{
			return await _blogRepository.MostCommented3Post();
		}

        public async Task<int> GetWriterBlogCountAsync(int id)
        {
            return await _blogRepository.GetWriterBlogCount(id);
        }

        public async Task<int> GetTotalBlogsCountAsync()
        {
            return await _blogRepository.GetTotalBlogsCount();
        }

        public async Task<string> GetLastBlogAsync()
        {
            return await _blogRepository.GetLastBlog();
        }
    }
}
