﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace TweetApp.Repository.Entities
{
    public class Tweet
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Tag { get; set; }

        [MaxLength(144)]
        public string Subject { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
