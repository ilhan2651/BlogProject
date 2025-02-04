using DataAccessLayer.BaseRepository.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Abstract
{
    public interface IMessageRepository : IGenericRepository<Message2>
    {
        Task<List<Message2>> GetInboxListByWriterAsync(int receiverID);
        Task<List<Message2>> GetListWithMessageByWriter(int id);
        Task<Message2> GetMessageWithWriterById(int id);
        Task<List<Message2>> GetSendboxWithMessageByWriter(int id);

    }
}
