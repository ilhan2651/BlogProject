﻿using DataAccessLayer.BaseRepository.Concrete;
using DataAccessLayer.Repositories.Abstract;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Concrete
{
    public class MessageRepository : GenericRepository<Message2>,IMessageRepository
    {

        private readonly BlogProjectContext _context;
        public MessageRepository(BlogProjectContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Message2>> GetInboxListByWriterAsync(int receiverID)
        {
            return await _context.Messages2.Where(x => x.ReceiverID == receiverID).ToListAsync();
        }

        public async  Task<List<Message2>> GetInboxListByWriterOrderingDateAsync(int receiverID)
        {
            return await _context.Messages2
                .Include(m=>m.SenderUser)
                .Where(x => x.ReceiverID == receiverID)
                .OrderByDescending(x => x.MessageDate)
                .Take(5)
                .ToListAsync();

        }

        public async Task<List<Message2>> GetListWithMessageByWriter(int id)
        {
            return await _context.Messages2
                .Include(m => m.SenderUser)
                .Where(b => b.ReceiverID == id).ToListAsync();
        }
        public async Task<Message2> GetMessageWithWriterById(int id)
        {
           return await _context.Messages2
                .Include (m => m.SenderUser)
                .FirstOrDefaultAsync(m=>m.MessageID == id);
            
        }
        public async Task<Message2> GetMessageWithWriterReceiverById(int id)
        {
            return await _context.Messages2
                 .Include(m => m.ReceiverUser)
                 .FirstOrDefaultAsync(m => m.MessageID == id);

        }

        public Task<List<Message2>> GetSendboxWithMessageByWriter(int id)
        {
            return _context.Messages2 .Include(m => m.ReceiverUser).Where(m=>m.SenderID==id).ToListAsync();
        }

        public async Task<int> GetTotalMessageCount(int userId)
        {
            return await _context.Messages2.CountAsync(m=>m.ReceiverID==userId);
        }
    }
}
