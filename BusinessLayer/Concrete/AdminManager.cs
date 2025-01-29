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
    public class AdminManager : GenericManager<Admin>, IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminManager(IAdminRepository adminRepository):base(adminRepository) 
        {
            _adminRepository = adminRepository;
        }

        public async Task<string> GetAdminNameAsync(int id)
        {
            return await    _adminRepository.GetAdminName(id);
        }
    }
}
