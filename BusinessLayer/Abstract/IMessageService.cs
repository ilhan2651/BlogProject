using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService : IGenericService<Message2>
    {
        Task<List<Message2>> GetInboxListByWriter(int receiverID);
        Task<Message2> GetMessageWithWriterById(int id);
        Task<List<Message2>> GetSendboxListByWriter(int id);
        Task<Message2> GetMessageWithWriterReceiverUserById(int id);
        Task<List<Message2>> GetInboxListByWriterOrderingDate(int id);
        Task<int> GetTotalMessageCount(int receiverId);

    }
}
