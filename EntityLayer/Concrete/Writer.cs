﻿using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Writer : IEntity
    {
        [Key]

        public int  WriterID { get; set; }
        [ForeignKey("AppUser")]
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public string WriterName { get; set; } 
        public string WriterUserName { get; set; }
        public string WriterAbout { get; set; } 
        public string? WriterImage { get; set; }
        public string WriterMail { get; set; }
        public string WriterPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public bool WriterStatus { get; set; }
        [JsonIgnore]
        public List<Blog> Blogs { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message2> WriterSender { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message2> WriterReceiver { get; set; }



    }
}
    