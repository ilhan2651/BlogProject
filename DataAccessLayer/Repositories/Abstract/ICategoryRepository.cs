﻿using DataAccessLayer.BaseRepository.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<int> GetCategoriesCount();

        Task<List<Category>> GetAllCategories();
    }
}
