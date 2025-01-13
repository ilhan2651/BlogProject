using BusinessLayer.Abstract;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class NewsletterManager : GenericManager<Newsletter> , INewsletterService
    {
        private readonly INewsletterRepository _newsletterRepository;
        public NewsletterManager(INewsletterRepository newsletterRepository):base(newsletterRepository)
        {
            _newsletterRepository   = newsletterRepository;
        }
    }
}
