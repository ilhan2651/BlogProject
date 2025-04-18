﻿using DataAccessLayer.BaseRepository.Concrete;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete
{
    public class WriterRepository : GenericRepository<Writer>, IWriterRepository
    {
        private readonly BlogProjectContext _context;
        public WriterRepository(BlogProjectContext context): base(context) 
        {
            _context = context;
        }

        public async Task<Writer> GetWriterByUserId(int userId)
        {
            return await _context.Writers.FirstOrDefaultAsync(x => x.AppUserId == userId);
        }
    }
}
