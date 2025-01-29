using DataAccessLayer.BaseRepository.Abstract;
using DataAccessLayer.BaseRepository.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Abstract
{
    public interface IAdminRepository : IGenericRepository<Admin>
    {
        Task<string> GetAdminName(int id);
    }
}
