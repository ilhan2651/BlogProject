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
    public class ContactManager : GenericManager<Contact> , IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactManager(IContactRepository contactRepository):base(contactRepository) 
        {
            _contactRepository  = contactRepository;
        }

        public async Task<int> GetContactCountAsync()
        {
            return await _contactRepository.GetContactCount();
        }
    }
}
