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
        public WriterManager(IWriterRepository writerRepository):base(writerRepository) 
        {
            _writerRepository = writerRepository;
        }

    }
}
