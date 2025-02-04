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
    public class MessageManager: GenericManager<Message2>,IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageManager(IMessageRepository messageRepository):base(messageRepository) 
        {
                _messageRepository  = messageRepository;
        }

        public async Task<List<Message2>> GetInboxListByWriter(int id)
        {
            return await _messageRepository.GetListWithMessageByWriter(id);
        }
        public async Task<Message2> GetMessageWithWriterById(int id)
        {
            return await _messageRepository.GetMessageWithWriterById(id);
        }

        public async Task<List<Message2>> GetSendboxListByWriter(int id)
        {
              return await _messageRepository.GetSendboxWithMessageByWriter(id);
        }
    }
}
