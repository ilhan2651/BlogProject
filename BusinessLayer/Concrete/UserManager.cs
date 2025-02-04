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
    public class UserManager :GenericManager<AppUser> ,IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository  userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
