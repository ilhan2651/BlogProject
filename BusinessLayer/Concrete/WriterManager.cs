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
    public class WriterManager : GenericManager<Writer> ,IWriterService
    {
        private readonly IWriterRepository _writerRepository;
        private readonly UserManager _userManager;
        public WriterManager(IWriterRepository writerRepository):base(writerRepository) 
        {   
            _writerRepository = writerRepository;
        }

        public async Task<Writer> GetWriterByUserIdAsync(int userId)
        {
            return await _writerRepository.GetWriterByUserId(userId);
        }
    }
}
