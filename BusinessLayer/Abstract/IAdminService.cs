using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdminService : IGenericService<Admin>
    {
        Task<string> GetAdminNameAsync(int id);
    }
}
