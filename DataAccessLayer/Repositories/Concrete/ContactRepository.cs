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
    public class ContactRepository : GenericRepository<Contact> , IContactRepository
    {
        private readonly BlogProjectContext _context;
        public ContactRepository(BlogProjectContext context):base(context)
        {
          _context = context;
        }

        public async Task<int> GetContactCount()
        {
            return await _context.Contacts.CountAsync();
        }
    }
}
