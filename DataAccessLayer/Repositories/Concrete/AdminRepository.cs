using DataAccessLayer.BaseRepository.Concrete;
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
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        private readonly BlogProjectContext _context;
        public AdminRepository(BlogProjectContext context) : base(context)
        {
            {
                _context = context;
            }
        }

        public async Task<string> GetAdminName(int id)
        {
          var admin=  await _context.Admins.Where(x => x.AdminID == 1).Select(y => y.Name).FirstOrDefaultAsync();
            return admin;
        }
    }
}