﻿using DataAccessLayer.BaseRepository.Abstract;
using EntityLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.BaseRepository.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
    {
        private readonly BlogProjectContext _context;
        public GenericRepository(BlogProjectContext context)
        {
            _context = context;
        }
        public DbSet<T> Table { get => _context.Set<T>(); }

        public async Task Add(T entity)
        {
            Table.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await Table.FindAsync(id);
            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByFilter(Expression<Func<T, bool>> filter)
        {
            return await Table.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<List<T>> GetFilteredList(Expression<Func<T, bool>> filter)
        {
            return await Table.Where(filter).ToListAsync();
        }

        public async Task<List<T>> ListAll()
        {
            return await Table.ToListAsync();
        }

        public async Task Update(T entity)
        {
            Table.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
